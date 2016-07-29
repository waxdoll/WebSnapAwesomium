using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Drawing.Drawing2D;

using Awesomium;
using Awesomium.Core;


namespace WebSnapAwesomium
{
    public partial class frmMain : Form
    {
        public frmMain()
        {

            #region Awesomium 线程


            // 开始一个 Awesomium 专用线程
            Thread threadAwesomium = new Thread(awesomiumThread) { Name = "Awesomium Thread" };
            threadAwesomium.Start();

            // 等待 WebCore 的启动
            while (!webCoreStarted)
                Thread.Sleep(10);


            #endregion


            InitializeComponent();
        }



        # region 常量与变量


        /// <summary>
        /// WebCore 是否已启动
        /// </summary>
        volatile bool webCoreStarted;

        /// <summary>
        /// 基准宽度，模拟浏览器宽度
        /// </summary>
        volatile int baseWidth = 1440;

        /// <summary>
        /// 基准高度，模拟浏览器的高度
        /// </summary>
        volatile int baseHeight = 900;

        /// <summary>
        /// 缩略图宽度
        /// </summary>
        volatile int thumbWidth = 480;

        /// <summary>
        /// 缩略图高度
        /// </summary>
        volatile int thumbHeight = 320;

        /// <summary>
        /// 是否包含全页面截图
        /// </summary>
        volatile bool fullPage = false;

        /// <summary>
        /// 截图保存文件夹
        /// </summary>
        volatile string folder = Environment.CurrentDirectory;

        /// <summary>
        /// 已经销毁的 WebView 的数量，每次处理完一个 URL 的截图后，计数加 1
        /// </summary>
        volatile int destroyedCount = 0;

        /// <summary>
        /// 要进行截图的 URL 的数量
        /// </summary>
        volatile int urlCount = 0;


        /// <summary>
        /// 开始截图的时间
        /// </summary>
        DateTime beginTime = DateTime.Now;


        // 隐藏滚动条的自定义 CSS 代码
        const string SCROLLBAR_CSS = "::-webkit-scrollbar { visibility: hidden; }";

        // 获取页面高度的 JS 代码
        const string PAGE_HEIGHT_FUNC = @"
            (function() {
                var bodyElmnt = document.body;
                var html = document.documentElement;
                var height = Math.max(
                            bodyElmnt.scrollHeight, 
                            bodyElmnt.offsetHeight, 
                            html.clientHeight, 
                            html.scrollHeight, 
                            html.offsetHeight
                ); 
                return height; 
            })();
        ";


        #endregion


        #region 自定义函数


        /// <summary>
        /// 配置 Awesomium 线程
        /// </summary>
        private void awesomiumThread()
        {
            // 使用配置选项初始化 WebCore
            WebCore.Initialize(new WebConfig()
            {
                LogPath = Environment.CurrentDirectory + "/awesomium.log",
                LogLevel = LogLevel.Verbose,
                // 指定插件所在路径，如希望渲染 Flash 动画，必须在指定文件夹中包含 NPSWF32*.dll 文件
                PluginsPath = Environment.CurrentDirectory
            });

            // 使用自定义 CSS 去掉渲染时页面可能出现的滚动条
            WebCore.CreateWebSession(new WebPreferences() { CustomCSS = SCROLLBAR_CSS });

            // 检查 WebCore 是否已经启动或者已经被关闭
            if (WebCore.UpdateState != WebCoreUpdateState.NotUpdating)
                return;

            // 运行 WebCore
            WebCore.Run((s, e) => webCoreStarted = true);
        }


        /// <summary>
        /// 监听 URL，开始对指定 URL 进行处理
        /// </summary>
        /// <param name="address">URL</param>
        private void listenForURL(string address)
        {
            Uri url = new Uri(address);

            // 使用 WebCore 的 QueueWork 方法将
            // 需要在 Awesomium 线程上执行的工作放入队列中
            WebCore.QueueWork(() => navigateAndTakeSnapshots(url));

        }

        
        /// <summary>
        /// 导航到指定 URL 以截图，在 Awesomium 线程上运行
        /// </summary>
        /// <param name="url">URL</param>
        private void navigateAndTakeSnapshots(Uri url)
        {
            
            // 使用 WebCore 创建一个 WebView 视图
            WebView view = WebCore.CreateWebView(baseWidth, baseHeight, WebCore.Sessions.Last());
            
            // 阻止弹出窗口
            view.ShowCreatedWebView += (s, e) => e.Cancel = true;

            // 对加载失败进行处理
            view.LoadingFrameFailed += (s, e) =>
            {
                // 忽略加载子框架页面可能产生的错误
                if (!e.IsMainFrame)
                    return;
            };


            // 主框架页面加载完毕时触发的事件
            view.LoadingFrameComplete += (s, e) =>
            {
                // 同样忽略可能存在的子框架页面
                if (!e.IsMainFrame)
                    return;

                // 对页面进行截图
                takeSnapshots((WebView)s);
            };

            // 加载指定 URL 对应的页面
            view.Source = url;

            view.Disposed += (s, e) =>
            {
                destroyedCount++;

                // 已销毁 WebView 数量和 URL 数量相同，处理完毕
                if (destroyedCount == urlCount)
                {
                    // 恢复窗体上控件的可用状态
                    setControlAvailability(true);

                    // 带消耗时间（单位：秒）的完成提示
                    MessageBox.Show("Complete in " + Math.Round((DateTime.Now - beginTime).TotalSeconds, 2).ToString() + " seconds!");
                }
            }; 
        }


        /// <summary>
        /// 对页面进行截图，运行在 Awesomium 线程上
        /// </summary>
        /// <param name="view">WebView</param>
        private void takeSnapshots(WebView view)
        {
            if (!view.IsLive)
            {
                // 如果 WebView 处于非活动状态，销毁之
                view.Dispose();
                return;
            }

            // 获取 URL 的域名部分
            string host = String.IsNullOrEmpty(view.Source.Host) ? "JS" : view.Source.Host;

            // 为所有 WebView 指定一个默认的 BitmapSurface
            BitmapSurface surface = (BitmapSurface)view.Surface;

            // 构造要存储截图的文件名
            string imageFile = String.Format("{0}-{1:yyyyMMddHHmmss}.png", host, DateTime.Now);

            imageFile = Path.Combine(folder, imageFile);

            // 将页面内容保存为一个 PNG 图片
            surface.SaveToPNG(imageFile, true);


            // 根据截图生成缩略图
            using (Image thumb = new Bitmap(thumbWidth, thumbHeight))
            {
                using (Graphics g = Graphics.FromImage(thumb))
                {
                    // 缩略图的质量
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = SmoothingMode.HighQuality;

                    // 背景色
                    g.Clear(Color.White);

                    using (Image initImage = Image.FromFile(imageFile))
                    {
                        // 生成缩略图
                        g.DrawImage(
                            initImage,
                            new Rectangle(0, 0, thumb.Width, thumb.Height),
                            new Rectangle(0, 0, initImage.Width, initImage.Height),
                            GraphicsUnit.Pixel
                        );
                    }
                }

                imageFile = String.Format("{0}-{1:yyyyMMddHHmmss}-thumb.png", host, DateTime.Now);

                imageFile = Path.Combine(folder, imageFile);

                // 保存缩略图
                thumb.Save(imageFile);
            }


            // 检查是否可以执行 Javascript 脚本
            if (!view.IsDocumentReady)
            {
                // 不允许执行销毁 WebView
                view.Dispose();
                
                return;
            }


            // 调用 JS 脚本以获取已加载页面的实际高度
            int docHeight = (int)view.ExecuteJavascriptWithResult(PAGE_HEIGHT_FUNC);

            // 高度获取失败则返回
            if (docHeight == 0)
                return;

            // 如果页面高度和 WebView 的初始指定高度一致，则返回
            if (docHeight == view.Height || !fullPage)
            {
                view.Dispose();

                return;
            }

            // surface 对象的 调整大小（Resized） 事件
            surface.Resized += (s, e) =>
            {
                // 构造整页截屏保存文件名
                imageFile = String.Format("{0}-{1:yyyyMMddHHmmss}-full.png", host, DateTime.Now);

                imageFile = Path.Combine(folder, imageFile);

                // 保存整页截屏图片
                surface.SaveToPNG(imageFile, true);

                // 获取 Awesomium 同步上下文
                SynchronizationContext syncCtx = SynchronizationContext.Current;

                // 检查同步上下文是否可用
                if ((syncCtx != null) && (syncCtx.GetType() != typeof(SynchronizationContext)))
                {
                    // 延缓 WebView 的销毁，等待 WebCore 的下一次更新
                    syncCtx.Post((v) =>
                    {
                        WebView completedView = (WebView)v;

                        completedView.Dispose();

                    }, view);
                }
            };

            // 调整 WebView 大小，触发 surface 的 Resized 事件
            view.Resize(view.Width, docHeight);


            /*

            // 通过滚动条位移进行 Lazyload 图片加载的处理

            int scrollVertPos = 0;

            while(scrollVertPos < docHeight)
            {
                scrollVertPos += baseHeight;

                System.Threading.Thread.Sleep(1000);

                view.InjectMouseWheel(scrollVertPos > docHeight ? docHeight : scrollVertPos, 0);
            }

            */
        }


        /// <summary>
        /// 启用或禁用窗体上的控件，在 Awesomium 线程中需要使用代理
        /// </summary>
        /// <param name="enabled">是否启用</param>
        private void setControlAvailability(bool enabled)
        {
            try
            {
                foreach (Control ctl in this.Controls)
                {
                    if (ctl.InvokeRequired)
                    {
                        Action<bool> actionDelegate = (x) => { ctl.Enabled = x; };
                        ctl.Invoke(actionDelegate, enabled);
                    }
                    else
                    {
                        ctl.Enabled = enabled;
                    }

                    ctl.Enabled = enabled;
                }
            }
            catch
            { 
            
            }
        }


        #endregion


        #region 控件事件


        private void btnCapture_Click(object sender, EventArgs e)
        {

            try
            {
                baseWidth = Convert.ToInt32(this.cmbBaseWidth.SelectedItem);
                thumbWidth = Convert.ToInt32(this.txtWidth.Text);
                thumbHeight = Convert.ToInt32(this.txtHeight.Text);
            }
            catch
            {
                MessageBox.Show("Please correct your input！");
                return;
            }
            
            fullPage = this.chkFullPage.Checked;

            string[] arrUrls = this.txtUrls.Text.Trim().Split(new char[]{ ',' }, StringSplitOptions.RemoveEmptyEntries);

            destroyedCount = 0;

            urlCount = arrUrls.Length;

            for (int i = 0; i < urlCount; i++)
            {
                listenForURL(arrUrls[i].Trim());
            }

            baseHeight = baseWidth * thumbHeight / thumbWidth;

            setControlAvailability(false);

            beginTime = DateTime.Now;
        }


        private void frmMain_Load(object sender, EventArgs e)
        {
            // 假设浏览器宽度是 1152
            this.cmbBaseWidth.SelectedIndex = 8;
            this.txtSaveFolder.Text = folder;
        }


        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    folder = this.txtSaveFolder.Text = fbd.SelectedPath;
                }
            }
        }


        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (urlCount != destroyedCount)
            {
                e.Cancel = true;
                return;
            }

            // 窗体关闭时，销毁 WebCore 实例
            WebCore.Shutdown();
        }


        #endregion


    }
}
