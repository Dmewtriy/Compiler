using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;

namespace Compiler.Views
{
    public partial class InfoForm : Form
    {
        private WebView2 webBrowser;
        private string _htmlToLoad;

        public InfoForm(string title, string htmlBody)
        {
            InitializeComponent();

            this.Text = title;
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            webBrowser = new WebView2 { Dock = DockStyle.Fill };
            this.Controls.Add(webBrowser);

            _htmlToLoad = $@"
            <html>
            <head>
                <style>
                    body {{ font-family: 'Segoe UI', sans-serif; padding: 20px; line-height: 1.6; }}
                    code {{ background-color: #f4f4f4; padding: 2px 4px; border-radius: 4px; }}
                    pre {{ background-color: #f4f4f4; padding: 10px; border: 1px solid #ddd; overflow: auto; }}
                    h1, h2 {{ color: #2c3e50; border-bottom: 1px solid #eee; }}
                </style>
            </head>
            <body>
                {htmlBody}
            </body>
            </html>";

            InitializeAsync();
        }

        async void InitializeAsync()
        {
            try
            {
                await webBrowser.EnsureCoreWebView2Async(null);

                webBrowser.CoreWebView2.Profile.PreferredColorScheme =
                    Microsoft.Web.WebView2.Core.CoreWebView2PreferredColorScheme.Light;

                webBrowser.CoreWebView2.NavigateToString(_htmlToLoad);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации браузера: {ex.Message}");
            }
        }
    }
}