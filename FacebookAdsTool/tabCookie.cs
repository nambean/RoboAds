using FacebookAdsTool.Entity;
using FacebookAdsTool.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;


namespace FacebookAdsTool
{
    public partial class tabCookie : UserControl
    {

        public tabCookie()
        {
            if (Program.IsInDesignMode())
            {
                return;
            }
                        


            InitializeComponent();
 
        }



        private void label2_Click(object sender, EventArgs e)
        {
            
            this.tabPrivacySetting1.ReloadData();
            //this.tabCookieInfo1.GetAllCookies();
            bunifuSeparator1.Visible = false;
            bunifuSeparator1.Left = ((Control)sender).Left;
            bunifuSeparator1.Width = ((Control)sender).Width;
            bunifuTransition1.ShowSync(bunifuSeparator1);
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            
            tabCookieInfo1.BringToFront();
        }

        private void label2_MouseUp(object sender, MouseEventArgs e)
        {
            
            tabPrivacySetting1.BringToFront();
        }

        private void dgvCookie_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
