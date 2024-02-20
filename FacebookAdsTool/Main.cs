using FacebookAdsTool.Libs;
using FacebookAdsTool.Utils;
using System;
using System.Threading;
using System.Windows.Forms;

namespace FacebookAdsTool
{
    public partial class Main : Form
    {
        private bool IsWindowMaximized;

        public Main()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Xoá accessToken
            RegistryUtil.SetCongifValueToRegistry(RegistryType.AccessToken, string.Empty);

            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            try
            {
                WindowState = FormWindowState.Minimized;
            }
            catch (Exception)
            {
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // RenderCameraByNumber(_cameraListItems);
        }

        private void btnMaximized_Click(object sender, EventArgs e)
        {
            IsWindowMaximized = !IsWindowMaximized;

            if (IsWindowMaximized)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
        }
        private void logoutMenuItem_Click(object sender, EventArgs e)
        {
            // Xoá accessToken
            RegistryUtil.SetCongifValueToRegistry(RegistryType.AccessToken, string.Empty);

            // Mở màn hình chính và đóng màn hình cũ
            Login login = new Login();
            login.Show();

            // Tắt màn hình chính
            this.Hide();
        }
    }
}
