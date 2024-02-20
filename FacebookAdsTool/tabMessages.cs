using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Http;
using FacebookAds.Entity.UserAds;
using Newtonsoft.Json;
using FacebookAdsTool.Entity;
using System.Threading;

namespace FacebookAdsTool
{
    public partial class tabMessages : UserControl
    {
        RegistryKey reg_key;
        String ipAdress = "";

        String ipProxy;
        String countryProxy;
        String ispProxy;

        int liveProxy = 0;
        int deadProxy = 0;
        int totalProxy = 0;
        string targetDirectory = "proxy";
        string[] urlProxyFree;
        string urlProxyFreeType = "";
        public tabMessages()
        {
            /*if (Program.IsInDesignMode())
                  {
                      return;
                  }*/
            InitializeComponent();
            CheckMyIpAsync(ipAdress);

            bunifuDropdown1.Items.Add("SELECT PROXY FREE");

            bunifuDropdown1.Items.Add("SOCKS5");
            bunifuDropdown1.Items.Add("SOCKS4");
            bunifuDropdown1.Items.Add("HTTP(S)");
            //bunifuDropdown1.SelectedIndex = 0;

            //listViewFiles.View = View.Details;
        }

        private void tabMessages_Load(object sender, EventArgs e)
        {
            CheckMyIpAsync(ipAdress);

            bunifuDropdown1.SelectedIndex = 0;

            // Kiểm tra nếu thư mục đích không tồn tại, hãy tạo nó
            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }
            // Thiết lập chế độ View cho ListView
            listViewFiles.View = View.Details;

            // Tạo các cột cho ListView
            listViewFiles.Columns.Add("File Name", 150);
            listViewFiles.Columns.Add("File Size (bytes)", 100);
            listViewFiles.Columns.Add("Created", 150);
            RefreshFileList();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(txtIp.Text) && String.IsNullOrEmpty(txtPort.Text))
            {
                toastInfo.Show(this.ParentForm, "PROXY IP OR PORT IS EMPTY !!!", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                return;
            }

            reg_key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            string proxy = txtIp.Text + ":" + txtPort.Text; // ip:port
            reg_key.SetValue("ProxyEnable", 1);
            reg_key.SetValue("ProxyServer", proxy);

            toastInfo.Show(this.ParentForm, "Changer Proxy Sucsessfull !", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Success);

            txtIp.ReadOnly = true;
            txtPort.ReadOnly = true;
            txtPort.Enabled = false;
            txtIp.Enabled = false;

            CheckMyIpAsync(txtIp.Text);

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ipAdress = GetLocalIPAddress();
            CheckMyIpAsync(ipAdress);

            if (String.IsNullOrEmpty(txtIp.Text) && String.IsNullOrEmpty(txtPort.Text))
            {
                toastInfo.Show(this.ParentForm, "PROXY IP OR PORT IS EMPTY !!!", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                return;
            }
            reg_key.SetValue("ProxyEnable", 0);
            toastInfo.Show(this.ParentForm, "Reset Proxy Sucsessfull !", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Success);

            txtPort.Enabled = true;
            txtIp.Enabled = true;
            txtIp.Clear();
            txtPort.Clear();
        }

        private async Task CheckMyIpAsync(string ipProxy)
        {
            try
            {
                if (String.IsNullOrEmpty(ipProxy))
                {
                    ipAdress = GetLocalIPAddress();
                }
                else
                {
                    ipAdress = ipProxy;
                }

                //GetInfoProxy(ipAdress);
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("https://api.iplocation.net/?ip=" + ipAdress + "");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Proxy proxy = JsonConvert.DeserializeObject<Proxy>(responseBody);

                ipProxy = proxy.ip.ToString();
                countryProxy = proxy.country_name.ToString();
                ispProxy = proxy.isp.ToString();

                label1.Text = ipProxy;
                label2.Text = countryProxy;
                label3.Text = ispProxy;
            }
            catch (Exception e)
            {

                toastInfo.Show(this.ParentForm, e.ToString(), Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
            }
        }

        public static string GetLocalIPAddress()
        {
            string pubIp = new System.Net.WebClient().DownloadString("https://api.ipify.org");

            return pubIp;
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public class WebClientWithTimeout : WebClient
        {
            protected override WebRequest GetWebRequest(Uri address)
            {
                WebRequest wr = base.GetWebRequest(address);
                wr.Timeout = 3000; // timeout in milliseconds (ms)
                return wr;
            }
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            // Khởi tạo OpenFileDialog
            using (System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt"; // Chỉ cho phép chọn file .txt
                openFileDialog.Title = "Select the .txt file to upload";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName; // Đường dẫn đến file được chọn

                    // Kiểm tra nếu thư mục đích không tồn tại, hãy tạo nó
                    //string targetDirectory = "proxy";
                    if (!Directory.Exists(targetDirectory))
                    {
                        Directory.CreateDirectory(targetDirectory);
                    }

                    // Đường dẫn đầy đủ đến thư mục đích
                    string targetFolderPath = Path.Combine(Environment.CurrentDirectory, targetDirectory);

                    // Đường dẫn đầy đủ đến file đích
                    string targetFilePath = Path.Combine(targetFolderPath, Path.GetFileName(selectedFilePath));

                    try
                    {
                        // Copy file từ nguồn đến đích
                        File.Copy(selectedFilePath, targetFilePath);

                        RefreshFileList();

                        toastInfo.Show(this.ParentForm, "Upload File Sucsessfull !", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Success);
                    }
                    catch (Exception ex)
                    {
                        // Xử lý lỗi nếu có
                        toastInfo.Show(this.ParentForm, "Error" + ex.Message, Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                        //Console.WriteLine("Đã xảy ra lỗi: " + ex.Message);
                    }
                }
                else
                {
                    toastInfo.Show(this.ParentForm, "The user has deselected the file.", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Information);
                    Console.WriteLine("Người dùng đã hủy chọn file.");
                }
            }
        }

        private void RefreshFileList()
        {
            // Xóa danh sách hiện tại trong ListView
            listViewFiles.Items.Clear();

            // Lấy danh sách các file trong thư mục đích
            string[] files = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, targetDirectory), "*.txt");

            // Hiển thị các file trong ListView
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                Console.WriteLine(fileName);
                FileInfo fileInfo = new FileInfo(file);
                // Tạo một ListViewItem để hiển thị thông tin của file
                ListViewItem item = new ListViewItem(fileName);

                // Thêm các sub-items cho ListViewItem, ví dụ: kích thước và ngày tạo
                item.SubItems.Add(fileInfo.Length.ToString());
                item.SubItems.Add(fileInfo.CreationTimeUtc.ToString());
                item.SubItems.Add(fileInfo.Directory.ToString());
                // Thêm ListViewItem vào ListView
                listViewFiles.Items.Add(item);

            }
        }

        private void listViewFiles_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Xác định item được chọn trong ListView
                ListViewItem selectedItem = listViewFiles.GetItemAt(e.X, e.Y);

                // Hiển thị ContextMenuStrip tại vị trí chuột
                if (selectedItem != null)
                {
                    contextMenuFiles.Show(listViewFiles, e.Location);
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listViewFiles.SelectedItems.Count > 0)
            {
                // Xóa file đã chọn trong ListView
                ListViewItem selectedItem = listViewFiles.SelectedItems[0];
                string fileName = selectedItem.Text;
                string filePath = Path.Combine(targetDirectory, fileName);

                // Xóa file trong thư mục proxy
                File.Delete(filePath);

                // Làm mới danh sách file trong ListView
                RefreshFileList();
            }
        }

        private void listViewFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewFiles.SelectedItems.Count > 0)
            {
                // Khi người dùng chọn một file trong ListView, đọc và hiển thị nội dung trong RichTextBox
                ListViewItem selectedItem = listViewFiles.SelectedItems[0];
                string fileName = selectedItem.Text;
                string filePath = Path.Combine(targetDirectory, fileName);

                // Đọc nội dung file
                string fileContent = File.ReadAllText(filePath);

                // Hiển thị nội dung file trong RichTextBox
                Console.WriteLine(fileContent);

                //lay thong tin file vao menustrip

                if (checkLiveToolStripMenuItem.ToString() == "Check Live")
                {
                    string targetFolderPath = Path.Combine(Environment.CurrentDirectory, targetDirectory);
                    // Lưu đường dẫn của file được chọn vào Tag của ToolStripMenuItem
                    string filePathSelect = Path.Combine(targetFolderPath, selectedItem.Text);
                    Console.WriteLine(targetFolderPath);
                    checkLiveToolStripMenuItem.Tag = filePathSelect; // Gán đường dẫn vào Tag
                    Console.WriteLine(filePathSelect);
                }

            }
        }

        //Check live Proxy
        private void checkLiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem toolStripMenuItem)
            {
                // Lấy thông tin đường dẫn của file từ Tag của ToolStripMenuItem
                string filePath = toolStripMenuItem.Tag as string;
                Console.WriteLine(filePath);
                urlProxyFree = new string[1] {
          filePath
        };
                urlProxyFreeType = "ProxyFile";
                string targetFolderPath = Path.Combine(Environment.CurrentDirectory, targetDirectory);
                WriteProxies(urlProxyFree, urlProxyFreeType, targetFolderPath, liveProxy, deadProxy);
                RefreshFileList();
            }

        }

        private void bunifuDropdown1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //urlProxyFree
            switch (bunifuDropdown1.SelectedIndex)
            {
                case 1:
                    urlProxyFree = new string[1] {
          "https://ads0806.com/upload/bat/proxy.txt"
        };
                    urlProxyFreeType = "SOCKS5";

                    toastInfo.Show(this.ParentForm, "SOCKS5 SELECTED, PLEASE CLICK BUTTON LOAD PROXY!", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Information);
                    Console.WriteLine("Sock 5 List");
                    break;
                case 2:
                    urlProxyFree = new string[1] {
          "https://ads0806.com/upload/bat/proxy.txt"
        };
                    urlProxyFreeType = "SOCKS4";
                    toastInfo.Show(this.ParentForm, "SOCKS4 SELECTED, PLEASE CLICK BUTTON LOAD PROXY!", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Information);
                    Console.WriteLine("Sock 4 List");
                    break;

                case 3:

                    urlProxyFree = new string[1] {
          "https://ads0806.com/upload/bat/proxy.txt"
        };
                    urlProxyFreeType = "HTTP(S)";
                    toastInfo.Show(this.ParentForm, "HTTP(S) SELECTED, PLEASE CLICK BUTTON LOAD PROXY!", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Information);
                    Console.WriteLine("Sock HTTP List");
                    break;
            }
        }

        private void btnLoadProxy_Click(object sender, EventArgs e)
        {
            string targetFolderPath = Path.Combine(Environment.CurrentDirectory, targetDirectory);
            WriteProxies(urlProxyFree, urlProxyFreeType, targetFolderPath, liveProxy, deadProxy);
            RefreshFileList();
        }

        private void WriteProxies(string[] urlProxyFree, string type, string targetFolderPath, int liveProxy, int deadProxy)
        {
            if (urlProxyFree.Length == 0)
            {
                toastInfo.Show(this.ParentForm, "PROXY NOT LOAD, PLEASE TRY AGAIN!", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Warning);
                return;
            }

            string targetFilePath = "";
            if (urlProxyFreeType == "ProxyFile")
            {
                targetFilePath = urlProxyFree[0].ToString();
            }
            else
            {
                string filename = type + "_proxies.txt";
                targetFilePath = Path.Combine(targetFolderPath, filename);

                if (File.Exists(targetFilePath))
                {
                    File.Delete(targetFilePath);
                }
                WebClient webClient = new WebClientWithTimeout();
                //webClient.Timeout = 100000;
                string proxylist = "";
                string[] proxyarray = urlProxyFree;
                foreach (string proxyurl in proxyarray)
                {
                    proxylist = proxylist + webClient.DownloadString(proxyurl) + Environment.NewLine;
                }

                // Kiểm tra nếu thư mục đích không tồn tại, hãy tạo nó
                if (!Directory.Exists(targetFolderPath))
                {
                    Directory.CreateDirectory(targetFolderPath);
                }

                using (StreamWriter writer = new StreamWriter(targetFilePath))
                {
                    writer.Write(proxylist);
                }
            }

            CheckProxyFileFormat(targetFilePath);

            label6.Text = File.ReadLines(targetFilePath).Count().ToString();

            string[] proxies = File.ReadAllLines(targetFilePath);
            foreach (string proxy in proxies)
            {
                Thread.Sleep(800);
                if (ProxyCheck(proxy) == true)
                {
                    /*string append = File.ReadAllText("good_proxies.txt");
                              using (StreamWriter writer = new StreamWriter("good_proxies.txt"))
                              {
                                  writer.WriteLine(append + proxy);
                              }*/
                    string[] data = proxy.Split(':');

                    liveProxy++;
                    label4.Text = liveProxy.ToString();
                    //Add to data grid view
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(bunifuDataGridView1);

                    row.Cells[0].Value = data[0].ToString();
                    row.Cells[1].Value = data[1].ToString();

                    bunifuDataGridView1.Rows.Add(row);

                }
                else
                {
                    deadProxy++;
                    label5.Text = deadProxy.ToString();
                }

            }

            //Console.Clear();
            //Console.ForegroundColor = ConsoleColor.Blue;
            //int lineCount = File.ReadLines(filename).Count();
            //Console.WriteLine("Exported " + lineCount + " proxies to: " + AppDomain.CurrentDomain.BaseDirectory + filename);
            //Console.ReadKey();
        }

        public static bool ProxyCheck(string ipAddressport)
        {
            string[] data = ipAddressport.Split(':');

            int port = 0;
            try
            {
                port = int.Parse(data[1]);
            }
            catch
            {
                return false;
            }
            try
            {
                IWebProxy proxy = new WebProxy(data[0], port);
                WebClient wc = new WebClientWithTimeout();
                //wc.time = 3500;
                wc.Proxy = proxy;
                wc.Encoding = Encoding.UTF8;
                string result = wc.DownloadString("http://ip-api.com/line/?fields=8192");

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsProxyFormatCorrect(string proxy)
        {
            // Kiểm tra định dạng của proxy, ví dụ: "176.214.78.230:5678"
            string[] parts = proxy.Split(':');
            if (parts.Length == 2 && !string.IsNullOrWhiteSpace(parts[0]) && !string.IsNullOrWhiteSpace(parts[1]))
            {
                if (int.TryParse(parts[1], out int port))
                {
                    return true; // Định dạng hợp lệ
                }
            }
            return false; // Định dạng không hợp lệ
        }

        public void CheckProxyFileFormat(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    int lineNumber = 1;
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!IsProxyFormatCorrect(line))
                        {
                            // Hiển thị thông báo lỗi với số dòng không hợp lệ

                            //toastInfo.Show(this.ParentForm, $"Error line format {lineNumber}: {line}", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Warning);
                            toastInfo.Show(this.ParentForm, "Error line format " + lineNumber + " : " + line + "", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Warning);
                            return;
                        }
                        lineNumber++;

                    }
                    return;
                }
            }
            catch (Exception ex)
            {
                toastInfo.Show(this.ParentForm, "PROXY NOT LOAD, PLEASE TRY AGAIN!", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Warning);
                return;
            }
        }

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuCards1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtPort_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtIp.Text) && String.IsNullOrEmpty(txtPort.Text))
            {
                toastInfo.Show(this.ParentForm, "PROXY IP OR PORT IS EMPTY !!!", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                return;
            }
        }

        private void txtIp_TextChanged(object sender, EventArgs e)
        {

        }
    }
}