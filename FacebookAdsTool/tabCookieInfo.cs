using FacebookAdsTool.Entity;
using FacebookAdsTool.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace FacebookAdsTool
{
    public partial class tabCookieInfo : UserControl
    {
        Stopwatch timer;
        bool start = false;

        private RegistryUtil registry = new RegistryUtil();

        public string id { get; set; }
        private int _currentPage = 1;
        private int _totalPage = 0;
        private int pageSize = 20;
        private int useStatusid;

        string startDate = DateTime.Today.AddTicks(1).ToString("yyyy-MM-ddT00:01:ssZ");
        string endDate = DateTime.Today.AddDays(1).ToString("yyyy-MM-ddT23:59:ssZ");

        public List<Cookies> cookies;
        private string cookieUse;
        private int cookieIdUse;
        private string license;
        private int timeSetting;

        private int injectChrome = 0;

        public tabCookieInfo()
        {
            if (Program.IsInDesignMode())
            {
                return;
            }
            InitializeComponent();

            //Load setting
            registry.LoadSettings();
            timeSetting = registry.timeAuto;
            license = registry.lisence;

            this.Load += CookieItem_Load;
            dpFromDate.Text = DateTime.Today.AddTicks(1).ToString("yyyy-MM-ddT00:01:ssZ");
            dpFromDate.Text = DateTime.Today.AddTicks(1).ToString("yyyy-MM-ddT23:59:ssZ");

            GetAllCookies();

        }

        public void GetAllCookies()
        {
            //Load setting
            registry.LoadSettings();
            timeSetting = registry.timeAuto;
            license = registry.lisence;

            dgvCookies.Rows.Clear();
            dgvCookies.Refresh();

            dgvCookies.AllowUserToAddRows   = false;
            dgvCookies.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCookies.RowTemplate.Height   = 25;
            dgvCookies.ColumnHeadersHeight  = 30;


            var cookiesSearchCondition = new CookiesSearchCondition();
            cookiesSearchCondition.start = startDate;
            cookiesSearchCondition.end = endDate;
            cookiesSearchCondition.page = _totalPage;
            cookiesSearchCondition.size = pageSize;



            var cookiesResponse = HttpClientUtils.Instance.PostProcessor("/cookies/getList", _currentPage, pageSize);

            Cookies cookies = JsonConvert.DeserializeObject<Cookies>(cookiesResponse.Content);

            _currentPage = cookies.meta.currentPage;
            _totalPage = cookies.meta.totalPages;

            var returnList = new List<Cookies>();

            foreach (var x in cookies.items)
            {
                int n = dgvCookies.Rows.Add();
                dgvCookies.Rows[n].Cells[0].Value = x.id;
                dgvCookies.Rows[n].Cells[1].Value = x.ipAddress;
                dgvCookies.Rows[n].Cells[2].Value = x.cookie;
                dgvCookies.Rows[n].Cells[3].Value = x.fbUserId;
                dgvCookies.Rows[n].Cells[4].Value = x.createdAt;
                dgvCookies.Rows[n].Cells[5].Value = false;
                if (x.isRunning == true)
                {
                    useStatusid = x.id;
                } ;
            }

            foreach (DataGridViewRow row in dgvCookies.Rows)
            {
                DataGridViewCell cell2Fa = row.Cells[8];//Column Index
                DataGridViewCell cellAction = row.Cells[6];//Column Index
                DataGridViewCell cellActionCancel = row.Cells[7];//Column Index
                DataGridViewCell cellIsRunning = row.Cells[0];
                if (cellIsRunning.Value.ToString() == useStatusid.ToString())
                {
                    cellAction.Value = "RUNNING";
                    cookieUse = row.Cells[2].Value.ToString();
                    cookieIdUse = Int32.Parse(row.Cells[0].Value.ToString());

                    cellActionCancel.Value = "CANCEL";
                }
                else
                {
                    cellAction.Value = "RUN";
                }

                cell2Fa.Value = "GET 2FA";

                cellAction.Style.BackColor = Color.FromArgb(219, 58, 108);
                cellAction.Style.ForeColor = Color.FromArgb(219, 58, 108);
            }

            // Tính toán paging
            this.CalculatorPaging(returnList, _totalPage);

            // Lấy mảng camera theo phân trang
            var pagingCookie = GetPage(returnList, _currentPage, _totalPage);

            dgvCookies.Refresh();

            

            if (cookieIdUse != 0)
            {
                RunningCookieFist(cookieUse);
            }


        }

        private void RunningCookieFist(string cookieUse)
        {
            try
            {
                //Start inject cookie
                if (injectChrome == 0) {
                    FacebookAds.ChromeManager.InjectCookieToChrome(cookieUse).Wait();
                    injectChrome = 1;
                }
                if (cookieIdUse != 0)
                {
                    //Start Get Inffomation on Timer
                    StartGetInfoCookies(cookieIdUse, cookieUse);
                }
                
            }
            catch (Exception e)
            {
                toastInfo.Show(this.ParentForm, e.ToString(), Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                Libs.LogUtil.WriteLogExeption(e.ToString());
            }                         
        }

        private void StartGetInfoCookies(int cookieIdUse, string cookieUse)
        {
            try
            {
                var t = new System.Timers.Timer(timeSetting * 60000) { Enabled = true };
                t.Elapsed += (sender, args) => {
                    Console.WriteLine("Cookie id " + cookieIdUse + " running auto : " + DateTime.Now);
                    FacebookAds.UserAdsManager.GetFacebookAdsInformation(cookieIdUse, cookieUse);
                };             
            }
            catch (Exception e)
            {
                toastInfo.Show(this.ParentForm, e.ToString(), Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
            }
        }

        private List<Cookies> GetPage(List<Cookies> list, int page, int pageSize)
        {
            return list.Skip(page * pageSize).Take(pageSize).ToList();
        }

        private void CalculatorPaging(IList<Cookies> visitorListItems, int totalVisitorDisplay)
        {
            // Gán tổng số trang và số trang hiện tại
            lblPagingVisitor.Text = string.Format("{0} / {1}", _currentPage, totalVisitorDisplay);
        }

        private void dpFromDate_ValueChanged(object sender, EventArgs e)
        {
            startDate = dpFromDate.Value.ToString("yyyy-MM-ddT00:01:ssZ");
            Console.WriteLine(this.Text);
        }
        private void dpToDate_ValueChanged(object sender, EventArgs e)
        {
            endDate = dpToDate.Value.ToString("yyyy-MM-ddT23:59:ssZ");
            Console.WriteLine(this.Text);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            _currentPage--;
            if (_currentPage <= 2)
            {
                _currentPage = 1;
                return;
            }
            GetAllCookies();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _currentPage++;
            if (_currentPage == 2)
            {
                return;
            }
            GetAllCookies();
        }

        private void btnComfirm_Click(object sender, EventArgs e)
        {

            GetAllCookies();
        }

        private void CookieItem_Load(object sender, EventArgs e)
        {
            Console.WriteLine("render data");
        }

        private void dgvCookies_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                try
                {
                    var aa = dgvCookies.SelectedCells[2].Value.ToString().Trim();
                    var cookieList = aa.Split(';').Where(x => x.Length > 0 && x != null).ToList();
                    
                    string c_user = "";
                    foreach (var item in cookieList)
                    {
                        var cookie = item.Split('=');
                        var name = cookie[0].ToString().Trim();
                        var value = cookie[1].ToString().Trim();
                        if (name == "c_user")
                        {
                            c_user = value;
                        }
                    }

                    PopupRun popupRun = new PopupRun(c_user);
                    // Show the settings form
                    popupRun.Show(this);
                }
                catch (Exception)
                {
                    toastInfo.Show(this.ParentForm, "Get 2FA code error, please try again or check cookie!", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                    return;
                }
                
            }

            

            if (e.ColumnIndex == 7)
            {
                if (Int32.Parse(dgvCookies.SelectedCells[0].Value.ToString()) != cookieIdUse && cookieIdUse != 0)
                {
                    toastInfo.Show(this.ParentForm, "Cookie is not running!", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                    return;
                }

                var cookiesUseAuto = HttpClientUtils.Instance.PostCookieStatus("/cookies/pauseRunningCookie", cookieIdUse, license);
                CookieUsingRespon cookiesUseAutoRespon = JsonConvert.DeserializeObject<CookieUsingRespon>(cookiesUseAuto.Content);
                if (cookiesUseAutoRespon.success)
                    {
                        toastInfo.Show(this.ParentForm, "Cancel Auto successfull!", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Success);
                        dgvCookies.SelectedCells[6].Value = "RUN";
                        dgvCookies.SelectedCells[7].Value = null;                       
                        if (timer!= null) { timer.Stop(); }
                        useStatusid = 0;
                        cookieIdUse = 0;
                        cookieUse = "";

                        GetAllCookies();

                        useStatusid = 0;
                        cookieIdUse = 0;
                        cookieUse = "";
                        injectChrome = 0;
                        dgvCookies.Enabled = true;
                        return;
                    }
                else
                    {
                        toastInfo.Show(this.ParentForm, "Cancel Auto error, please try again!", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                        return;
                    }
            }
           

            
            if (e.ColumnIndex == 6)
            {
                if (Int32.Parse(dgvCookies.SelectedCells[0].Value.ToString()) != cookieIdUse && cookieIdUse != 0)
                {
                    toastInfo.Show(this.ParentForm, "Cookie is not running!", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                    return;
                }
                cookieIdUse = Int32.Parse(dgvCookies.SelectedCells[0].Value.ToString());
                //Push data Cookie using to server
                var cookiesUse = HttpClientUtils.Instance.PostCookieStatus("/cookies/runningCookie", cookieIdUse, license);
                CookieUsingRespon cookieUsingRespon = JsonConvert.DeserializeObject<CookieUsingRespon>(cookiesUse.Content);
                
                if (cookieUsingRespon.success)
                {
                    CronAutoCookie(e);
                }
                else
                {
                    toastInfo.Show(this.ParentForm, "Run auto Cookie error, please try again!", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                    return;
                }
            }

           

        }

      


        //backbroud thread
        public async Task CronAutoCookie(DataGridViewCellEventArgs e)
        {
            registry.LoadSettings();
            timeSetting = registry.timeAuto;
            license = registry.lisence;
            if (start)
            {
                
            }
            else
            {
                if (e.ColumnIndex == 6)
                {
                    if (MessageBox.Show("Are you sure want to run Cookie auto " + timeSetting + " minitues ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (useStatusid == Int32.Parse(dgvCookies.SelectedCells[0].Value.ToString()))
                        {
                            toastInfo.Show(this.ParentForm, "Cookie ID " + useStatusid + " is running, please pause process!", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);

                            RunCookie(e);

                            //PopupRun popupRun = new PopupRun(cookieIdUse);
                            // Show the settings form
                            //popupRun.Show(this);
                            //dgvCookies.Enabled = false;
                        }
                        else   
                        {
                            RunCookie(e);

                            //PopupRun popupRun = new PopupRun(cookieIdUse);
                            // Show the settings form
                            //popupRun.Show(this);
                            //dgvCookies.Enabled = false;
                        }

                        /*toastInfo.Show(this.ParentForm, "Cookie ID" + useStatusid + "is running, please pause process!", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                        return;*/

                    }
                }
            }
        }

        private void RunCookie(DataGridViewCellEventArgs e)
        {
            if (!string.IsNullOrEmpty(dgvCookies.SelectedCells[2].Value.ToString()))
            {
                cookieUse = dgvCookies.SelectedCells[2].Value.ToString();
            }
            Console.WriteLine(dgvCookies.SelectedCells[2].Value.ToString());

            //dgvCookies.Enabled = false;

            dgvCookies[e.ColumnIndex, e.RowIndex].Value = "RUNNING";
            dgvCookies[e.ColumnIndex+1, e.RowIndex].Value = "CANCEL";


       
            foreach (DataGridViewRow row in dgvCookies.Rows)
            {
                DataGridViewDisableButtonCell buttonCell = (DataGridViewDisableButtonCell)row.Cells["actionButton"];

                DataGridViewCell cell = row.Cells["actionButton"];
                if (cell.Value != "RUNNING") {
                    buttonCell.ReadOnly = true;
                    buttonCell.Enabled = false; 
                }
                //autoButton
                DataGridViewCell cellCancel = row.Cells["autoButton"];
                if (cellCancel.Value != "CANCEL")
                {
                    buttonCell.ReadOnly = true;
                    buttonCell.Enabled = false;
                }
            }

            RunningCookieFist(cookieUse);

        }



        private void btnComfirm_Click_1(object sender, EventArgs e)
        {
            GetAllCookies();
        }


        public class DataGridViewDisableButtonColumn : DataGridViewButtonColumn
        {
            public DataGridViewDisableButtonColumn()
            {
                this.CellTemplate = new DataGridViewDisableButtonCell();
            }
        }

        public class DataGridViewDisableButtonCell : DataGridViewButtonCell
        {
            private bool enabledValue;
            public bool Enabled
            {
                get
                {
                    return enabledValue;
                }
                set
                {
                    enabledValue = value;
                }
            }

            public override object Clone()
            {
                DataGridViewDisableButtonCell cell = (DataGridViewDisableButtonCell)base.Clone();
                cell.Enabled = this.Enabled;
                return cell;
            }

            public DataGridViewDisableButtonCell()
            {
                this.enabledValue = true;
            }

            protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState,
                                            object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle,
                                            DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
            {
                if (!this.enabledValue)
                {
                    if ((paintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
                    {
                        SolidBrush cellBackground = new SolidBrush(cellStyle.BackColor);
                        graphics.FillRectangle(cellBackground, cellBounds);
                        cellBackground.Dispose();
                    }

                    if ((paintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border)
                    {
                        PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
                    }

                    Rectangle buttonArea = cellBounds;
                    Rectangle buttonAdjustment = this.BorderWidths(advancedBorderStyle);
                    buttonArea.X += buttonAdjustment.X;
                    buttonArea.Y += buttonAdjustment.Y;
                    buttonArea.Height -= buttonAdjustment.Height;
                    buttonArea.Width -= buttonAdjustment.Width;

                    ButtonRenderer.DrawButton(graphics, buttonArea, PushButtonState.Disabled);

                    if (this.FormattedValue is String)
                    {
                        TextRenderer.DrawText(graphics, (string)this.FormattedValue, this.DataGridView.Font, buttonArea, SystemColors.GrayText);
                    }
                }
                else
                {
                    base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
                }
            }
        }

        private void dpToDate_ValueChanged_1(object sender, EventArgs e)
        {

        }
    }
}
