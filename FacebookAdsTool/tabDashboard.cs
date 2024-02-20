using Bunifu.Framework.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacebookAdsTool
{
    public partial class tabDashboard : UserControl
    {
        public tabDashboard()
        {
            if (Program.IsInDesignMode())
            {
                return;
            }
            InitializeComponent();


        }

        private void chartDelay_Tick(object sender, EventArgs e)
        {
            //delay for our charts to render.
            chartDelay.Stop();
            //renderCharts();
        }



        private void progressBarUpdate_Tick(object sender, EventArgs e)
        {
            Random r = new Random();
           /* bunifuProgressBar1.Value = r.Next(0, 100);
            bunifuProgressBar2.Value = r.Next(bunifuProgressBar1.Value, 100);
            bunifuCircleProgressbar1.Value = bunifuProgressBar1.Value;*/
        }

        private void tabDashboard_Load(object sender, EventArgs e)
        {

        }
    }
}
