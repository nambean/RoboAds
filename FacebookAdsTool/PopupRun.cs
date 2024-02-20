using FacebookAdsTool.Entity;
using FacebookAdsTool.Utils;
using Newtonsoft.Json;
using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OtpNet;
using System.Security.Cryptography;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace FacebookAdsTool
{
    public partial class PopupRun : Form
    {
        readonly QRCodeGenerator qrGenerator = new QRCodeGenerator();
        const int DEFAULT_STEP = 30;
        string uid = "";
       

        public PopupRun(string c_user)
        {
            InitializeComponent();
            uid = c_user;
            lblUid.Text = uid;
        }

        private void PopupRun_Load(object sender, EventArgs e)
        {
            txtName2Fa.Text = "ABC XYZ";
            txtSecretCode.Text = "PNU2XK326OPXRFHEFBB53ZG2WHQGHAR7";
            GetCode2Fa();
        }



        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnGet2fa_Click(object sender, EventArgs e)
        {
            
            GetCode2Fa();
        }

        private void GetCode2Fa()
        {
            if (txtName2Fa.Text != "" && txtSecretCode.Text != "")
            {
                var totp = new Totp(Base32Encoding.ToBytes(txtSecretCode.Text));

                //var isValidTotpCode = totp.VerifyTotp(Code.Text, out long timeStepMatched, new VerificationWindow(previous: 1, future: 1));

                var now = DateTime.UtcNow;
                var step = Math.Floor((now - new DateTime(1970, 1, 1)).TotalSeconds / DEFAULT_STEP);

                lblPre.Text = $"{totp.ComputeTotp(now.AddSeconds(-DEFAULT_STEP))}{Environment.NewLine}({step - 1})";
                lblCurrent.Text = $"{totp.ComputeTotp(now)}{Environment.NewLine}({step})";
                lblNext.Text = $"{totp.ComputeTotp(now.AddSeconds(DEFAULT_STEP))}{Environment.NewLine}({step + 1})";

                txt6digit.Text = $"{totp.ComputeTotp(now)}{Environment.NewLine}";
            }
            else
            {
                toastInfo.Show(this.ParentForm, "Please Name 2FA and Secret code!", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
            }
        }

        private void txtSecretCode_TextChange(object sender, EventArgs e)
        {
            if (txtName2Fa.Text != "" && txtSecretCode.Text != "")
            {
                var qrCodeUri = $"otpauth://totp/{Uri.EscapeDataString(txtName2Fa.Text)}?secret={txtSecretCode.Text}&issuer={Uri.EscapeDataString(txtName2Fa.Text)}";

                using (var qrCodeData = qrGenerator.CreateQrCode(qrCodeUri, QRCodeGenerator.ECCLevel.Q))
                {
                    using (var qrCode = new QRCode(qrCodeData))
                    {
                        var qrCodeImage = qrCode.GetGraphic(20);

                        using (var memory = new MemoryStream())
                        {
                            qrCodeImage.Save(memory, ImageFormat.Bmp);
                            memory.Position = 0;
                            var bitmapImage = new BitmapImage();
                            bitmapImage.BeginInit();
                            bitmapImage.StreamSource = memory;
                            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                            bitmapImage.EndInit();
                            QrCode.Image = qrCodeImage;
                        }
                    }
                }

            }
            else
            {
                toastInfo.Show(this.ParentForm, "Please Name 2FA and Secret code!", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
            }
        }

        private void btn2FaSecret_Click(object sender, EventArgs e)
        {
            TwoFa twoFa = new TwoFa();
            twoFa.fbUserId = uid;
            twoFa.fbUserName = txtUserFacebook.Text;
            twoFa.fbPassword = textPassFacebook.Text;
            twoFa.secretCode = txtSecretCode.Text;
            twoFa.twoFaName = txtName2Fa.Text;

            var aaa = HttpClientUtils.Instance.Save2Fa("/twofa/save2Fa", twoFa);
            TwoFaRespon twoFaRespon = JsonConvert.DeserializeObject<TwoFaRespon>(aaa.Content);
        }
    }
}

