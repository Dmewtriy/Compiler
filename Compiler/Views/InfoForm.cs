using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compiler.Views
{
    public partial class InfoForm : Form
    {
        private WebBrowser webBrowser;

        public InfoForm(string title, string htmlBody)
        {
            InitializeComponent();
            string htmlContent = $@"
            <html>
            <head>
                <meta http-equiv='X-UA-Compatible' content='IE=edge' />
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
            this.Text = title;
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            webBrowser = new WebBrowser();
            webBrowser.Dock = DockStyle.Fill;

            this.Controls.Add(webBrowser);

            webBrowser.DocumentText = htmlContent;
        }
    }
}
