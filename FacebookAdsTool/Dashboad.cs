
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FacebookAdsTool.Utils;
using FacebookAdsTool.Entity;
using Newtonsoft.Json.Linq;
using System.Collections.Immutable;
using Quobject.EngineIoClientDotNet.Client;
using SocketIOClient;
using SocketIOClient.Transport;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading;

namespace FacebookAdsTool
{
    public partial class Dashboad : Form
    {
        public Dashboad()
        {
            InitializeComponent();          
        }

        bool DrawerOpen = true;
        

        private void btnToggleDrawer_Click(object sender, EventArgs e)
        {
            DrawerOpen = !DrawerOpen;
            pnlDrawer.Visible = false;

            if (DrawerOpen)
            {
                //animated Drawer Open
                pnlDrawer.Width = 233;
                btnToggleDrawer.Image = imageList1.Images[0];
                bunifuTransition1.ShowSync(pnlDrawer);
            }
            else
            {
                //Aminated Drawer close
                pnlDrawer.Width = 56;
                btnToggleDrawer.Image = imageList1.Images[1];
                bunifuTransition1.ShowSync(pnlDrawer);
            }
        }

        private void bunifuFlatButton1_MouseUp(object sender, EventArgs e)
        {
            
            tabCookie.BringToFront();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            
            tabMessages1.BringToFront();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
           
            tabDashboard1.BringToFront();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void Main_LoadAsync(object sender, EventArgs e)
        {
            var accessToken = RegistryUtil.GetConfigValueFromRegistry(RegistryType.AccessToken);

            try
            {
                var client = new SocketIO("https://ads0806.com/", new SocketIOOptions
                {
                    Query = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("accessToken", accessToken)

                },
                    Transport = TransportProtocol.WebSocket
                });


                client.On("hi", response =>
                {
                    string text = response.GetValue<string>();

                });

                lblStatusConnect.Text = "Connected";
                lblStatusConnect.ForeColor = Color.Green;


                client.OnConnected += async (sender1, res) =>
                {
                    await Task.Delay(10000);
                    Console.WriteLine("OnConnected" + res);
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        lblStatusConnect.Text = "Connected";
                        lblStatusConnect.ForeColor = Color.Green;
                    });

                    /*lblStatusConnect.Text = "OnConnected";
                    lblStatusConnect.ForeColor = Color.Green;*/

                };

                client.OnDisconnected += async (sender1, res) =>
                {
                    await Task.Delay(20000);
                    Console.WriteLine("OnDisconnected" + res);
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        lblStatusConnect.Text = "Disconnected";
                        lblStatusConnect.ForeColor = Color.Red;
                    });

                };

                client.OnError += async (sender1, res) =>
                {
                    await Task.Delay(30000);
                    Console.WriteLine("OnError" + res);
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        lblStatusConnect.Text = "OnError";
                        lblStatusConnect.ForeColor = Color.Red;
                    });

                };

                client.OnReconnected += async (sender1, res) =>
                {
                    await Task.Delay(40000);
                    Console.WriteLine("OnReconnected" + res);
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        lblStatusConnect.Text = "Connected";
                        lblStatusConnect.ForeColor = Color.Green;
                    });

                };

                client.OnReconnectError += async (sender1, res) =>
                {
                    await Task.Delay(50000);
                    Console.WriteLine("OnReconnectError : " + res);
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        lblStatusConnect.Text = "Reconnect Error";
                        lblStatusConnect.ForeColor = Color.Red;
                    });

                };

                client.OnReconnectFailed += async (sender1, res) =>
                {
                    await Task.Delay(60000);
                    Console.WriteLine("OnReconnectFailed : " + res);
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        lblStatusConnect.Text = "ReconnectFailed";
                        lblStatusConnect.ForeColor = Color.Red;
                    });

                };

                await client.ConnectAsync();

                Console.WriteLine(client.Connected);

                
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex);
            }
        }
    }
}
