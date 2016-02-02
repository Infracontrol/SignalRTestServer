using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SignalRTestServer
{
    public partial class Form1 : Form
    {
        IDisposable server;

        public Form1()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (server == null)
            {
                server = WebApp.Start(addressTextBox.Text, app =>
                {
                    app.UseCors(CorsOptions.AllowAll);
                    app.MapSignalR("", new HubConfiguration
                    {
                        // You can enable JSONP by uncommenting line below.
                        // JSONP requests are insecure but some older browsers (and some
                        // versions of IE) require JSONP to work cross domain
                        EnableJSONP = true,
                        EnableDetailedErrors = true,
                        Resolver = new DefaultDependencyResolver()
                    });
                });
                startButton.Enabled = false;
                stopButton.Enabled = true;
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            if (server != null)
            {
                server.Dispose();
                server = null;
                startButton.Enabled = true;
                stopButton.Enabled = false;
            }
        }
    }
}
