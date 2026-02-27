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

        public InfoForm(string title, string htmlContent)
        {
            InitializeComponent();

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
