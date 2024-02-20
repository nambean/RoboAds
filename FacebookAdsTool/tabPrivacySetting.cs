using FacebookAdsTool.Utils;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FacebookAdsTool
{
    
    public partial class tabPrivacySetting : UserControl
    {
       
        public tabPrivacySetting()
        {
            InitializeComponent();
            ReloadData();

            txtMintues.Text = RegistryUtil.GetConfigValueFromRegistry(RegistryType.TimeScheduler);
            txtLicense.Text = RegistryUtil.GetConfigValueFromRegistry(RegistryType.License);
        }

        public void ReloadData()
        {

            string license = "";

            license = RegistryUtil.GetConfigValueFromRegistry(RegistryType.License);
            if (license == null)
            {
                //storing the values  
                Random rnd = new Random();
                license = "RoboADS-" + rnd.Next(10000000, 999999999).ToString();
                RegistryUtil.SetCongifValueToRegistry(RegistryType.License, license);
                RegistryUtil.SetCongifValueToRegistry(RegistryType.TimeScheduler, "5");
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMintues.Text))
            {
                toastInfo.Show(this.ParentForm, "Enter time is number!", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                return;
            }


            try
            {
                RegistryUtil.SetCongifValueToRegistry(RegistryType.TimeScheduler, txtMintues.Text);

  
                ReloadData();
                toastInfo.Show(this.ParentForm.ParentForm, "Save setting success!", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Success);
            }
            catch (Exception)
            {
                toastInfo.Show(this.ParentForm.ParentForm, "Save setting error!", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                return;
            }  
            
        }
    }
}
