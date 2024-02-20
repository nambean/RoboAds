using Newtonsoft.Json;
using System.Windows.Forms;
using FacebookAdsTool.Utils;
using FacebookAdsTool.Entity;
using Bunifu.Framework.UI;

namespace FacebookAdsTool
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            var userName = RegistryUtil.GetConfigValueFromRegistry(RegistryType.UserName);
            var password = RegistryUtil.GetConfigValueFromRegistry(RegistryType.Password);
            txtUserName.Text = userName;
            txtPassword.Text = password;
        }

        private void btnLogin_ClickAsync(object sender, System.EventArgs e)
        {
            // Kiểm tra tài khoản
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                toastInfo.Show(this, "Vui lòng nhập tài khoản.", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                txtUserName.Focus();
                return;
            }

            // Kiểm tra mật khẩu
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                toastInfo.Show(this, "Vui lòng nhập mật khẩu.", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                txtPassword.Focus();
                return;
            }

            var response = HttpClientUtils.Instance.Login(txtUserName.Text, txtPassword.Text);
            if (response.StatusCode == 0)
            {
                toastInfo.Show(this, "Không kết nối được tới máy chủ. Vui lòng kiểm tra kết nối mạng", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                return;
            }

            if (!response.IsSuccessStatusCode)
            {
                toastInfo.Show(this, "Sai thông tin tài khoản hoặc mật khẩu.", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                return;
            }

            var userResponse = JsonConvert.DeserializeObject<UserLoginResponse>(response.Content);
            if (string.IsNullOrEmpty(userResponse.accessToken))
            {
                toastInfo.Show(this, "Không tồn tại mã truy cập.", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                return;
            }

            // Lưu thông tin tài khoản mật khẩu trong TH có chọn Ghi nhớ
            if (!chkRemember.Checked)
            {
                RegistryUtil.SetCongifValueToRegistry(RegistryType.UserName, string.Empty);
                RegistryUtil.SetCongifValueToRegistry(RegistryType.Password, string.Empty);
            }
            else
            {
                RegistryUtil.SetCongifValueToRegistry(RegistryType.UserName, txtUserName.Text);
                RegistryUtil.SetCongifValueToRegistry(RegistryType.Password, txtPassword.Text);
            }

            // Lưu token
            RegistryUtil.SetCongifValueToRegistry(RegistryType.AccessToken, userResponse.accessToken);

            // Tắt màn hình đăng nhập
            this.Hide();

            // Mở màn hình chính và đóng màn hình cũ
            Dashboad main = new Dashboad();
            main.Show();
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, System.EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void lblRemember_Click(object sender, System.EventArgs e)
        {
            chkRemember.Checked = !chkRemember.Checked;
        }
    }
}
