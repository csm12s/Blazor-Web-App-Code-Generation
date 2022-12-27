// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace Gardener.Client.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            
            InitializeComponent();
            
            services.AddWpfBlazorWebView();
            Resources.Add("services", services.BuildServiceProvider());


            // 设置全屏
            this.WindowState = WindowState.Normal;
            //this.ResizeMode = ResizeMode.NoResize;
            //this.WindowStyle = WindowStyle.None;
            //this.Topmost = true;

            this.Left = 0.0;
            this.Top = 0.0;
            this.Width = SystemParameters.PrimaryScreenWidth;
            this.Height = SystemParameters.PrimaryScreenHeight;
        }
    }
}
