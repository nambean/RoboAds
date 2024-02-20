namespace FacebookAdsTool
{
    partial class tabCookieInfo
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Obsolete]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(tabCookieInfo));
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.bunifuSeparator1 = new Bunifu.Framework.UI.BunifuSeparator();
            this.dgvCookies = new Bunifu.UI.WinForms.BunifuDataGridView();
            this.bunifuPanel1 = new Bunifu.UI.WinForms.BunifuPanel();
            this.btnNext = new System.Windows.Forms.PictureBox();
            this.lblPagingVisitor = new System.Windows.Forms.Label();
            this.btnPrevious = new System.Windows.Forms.PictureBox();
            this.btnComfirm = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.dpToDate = new Bunifu.UI.WinForms.BunifuDatePicker();
            this.dpFromDate = new Bunifu.UI.WinForms.BunifuDatePicker();
            this.toastInfo = new Bunifu.UI.WinForms.BunifuSnackbar(this.components);
            this.idCookie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cookie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_user = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.useCookie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actionButton = new FacebookAdsTool.tabCookieInfo.DataGridViewDisableButtonColumn();
            this.autoButton = new FacebookAdsTool.tabCookieInfo.DataGridViewDisableButtonColumn();
            this.facode = new FacebookAdsTool.tabCookieInfo.DataGridViewDisableButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCookies)).BeginInit();
            this.bunifuPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnNext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrevious)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // bunifuSeparator1
            // 
            this.bunifuSeparator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bunifuSeparator1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.bunifuSeparator1.LineThickness = 1;
            this.bunifuSeparator1.Location = new System.Drawing.Point(3, 385);
            this.bunifuSeparator1.Name = "bunifuSeparator1";
            this.bunifuSeparator1.Size = new System.Drawing.Size(860, 35);
            this.bunifuSeparator1.TabIndex = 24;
            this.bunifuSeparator1.Transparency = 255;
            this.bunifuSeparator1.Vertical = false;
            // 
            // dgvCookies
            // 
            this.dgvCookies.AllowCustomTheming = false;
            this.dgvCookies.AllowUserToResizeColumns = false;
            this.dgvCookies.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(48)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            this.dgvCookies.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCookies.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCookies.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCookies.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvCookies.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(16)))), ((int)(((byte)(18)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCookies.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCookies.ColumnHeadersHeight = 40;
            this.dgvCookies.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idCookie,
            this.IP,
            this.cookie,
            this.c_user,
            this.createdDate,
            this.useCookie,
            this.actionButton,
            this.autoButton,
            this.facode});
            this.dgvCookies.CurrentTheme.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(48)))), ((int)(((byte)(52)))));
            this.dgvCookies.CurrentTheme.AlternatingRowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvCookies.CurrentTheme.AlternatingRowsStyle.ForeColor = System.Drawing.Color.White;
            this.dgvCookies.CurrentTheme.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(117)))), ((int)(((byte)(119)))));
            this.dgvCookies.CurrentTheme.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            this.dgvCookies.CurrentTheme.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(16)))), ((int)(((byte)(18)))));
            this.dgvCookies.CurrentTheme.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(56)))), ((int)(((byte)(62)))));
            this.dgvCookies.CurrentTheme.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(16)))), ((int)(((byte)(18)))));
            this.dgvCookies.CurrentTheme.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            this.dgvCookies.CurrentTheme.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvCookies.CurrentTheme.HeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.dgvCookies.CurrentTheme.HeaderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dgvCookies.CurrentTheme.Name = null;
            this.dgvCookies.CurrentTheme.RowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.dgvCookies.CurrentTheme.RowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvCookies.CurrentTheme.RowsStyle.ForeColor = System.Drawing.Color.White;
            this.dgvCookies.CurrentTheme.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(117)))), ((int)(((byte)(119)))));
            this.dgvCookies.CurrentTheme.RowsStyle.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            this.dgvCookies.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(117)))), ((int)(((byte)(119)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCookies.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCookies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCookies.EnableHeadersVisualStyles = false;
            this.dgvCookies.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(56)))), ((int)(((byte)(62)))));
            this.dgvCookies.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(16)))), ((int)(((byte)(18)))));
            this.dgvCookies.HeaderBgColor = System.Drawing.Color.Empty;
            this.dgvCookies.HeaderForeColor = System.Drawing.Color.White;
            this.dgvCookies.Location = new System.Drawing.Point(0, 0);
            this.dgvCookies.Name = "dgvCookies";
            this.dgvCookies.RowHeadersVisible = false;
            this.dgvCookies.RowTemplate.Height = 40;
            this.dgvCookies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCookies.Size = new System.Drawing.Size(866, 456);
            this.dgvCookies.TabIndex = 25;
            this.dgvCookies.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Dark;
            this.dgvCookies.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCookies_CellContentClick);
            // 
            // bunifuPanel1
            // 
            this.bunifuPanel1.BackgroundColor = System.Drawing.Color.Transparent;
            this.bunifuPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuPanel1.BackgroundImage")));
            this.bunifuPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuPanel1.BorderColor = System.Drawing.Color.Transparent;
            this.bunifuPanel1.BorderRadius = 3;
            this.bunifuPanel1.BorderThickness = 1;
            this.bunifuPanel1.Controls.Add(this.btnNext);
            this.bunifuPanel1.Controls.Add(this.lblPagingVisitor);
            this.bunifuPanel1.Controls.Add(this.btnPrevious);
            this.bunifuPanel1.Controls.Add(this.btnComfirm);
            this.bunifuPanel1.Controls.Add(this.dpToDate);
            this.bunifuPanel1.Controls.Add(this.dpFromDate);
            this.bunifuPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bunifuPanel1.Location = new System.Drawing.Point(0, 415);
            this.bunifuPanel1.Name = "bunifuPanel1";
            this.bunifuPanel1.ShowBorders = true;
            this.bunifuPanel1.Size = new System.Drawing.Size(866, 41);
            this.bunifuPanel1.TabIndex = 26;
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.Transparent;
            this.btnNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.Location = new System.Drawing.Point(796, 10);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(24, 24);
            this.btnNext.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnNext.TabIndex = 22;
            this.btnNext.TabStop = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblPagingVisitor
            // 
            this.lblPagingVisitor.AutoSize = true;
            this.lblPagingVisitor.BackColor = System.Drawing.Color.Transparent;
            this.lblPagingVisitor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblPagingVisitor.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPagingVisitor.ForeColor = System.Drawing.Color.White;
            this.lblPagingVisitor.Location = new System.Drawing.Point(736, 10);
            this.lblPagingVisitor.Name = "lblPagingVisitor";
            this.lblPagingVisitor.Size = new System.Drawing.Size(43, 19);
            this.lblPagingVisitor.TabIndex = 21;
            this.lblPagingVisitor.Text = "1  /  1";
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackColor = System.Drawing.Color.Transparent;
            this.btnPrevious.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrevious.Image = ((System.Drawing.Image)(resources.GetObject("btnPrevious.Image")));
            this.btnPrevious.Location = new System.Drawing.Point(690, 10);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(24, 24);
            this.btnPrevious.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnPrevious.TabIndex = 20;
            this.btnPrevious.TabStop = false;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnComfirm
            // 
            this.btnComfirm.AllowAnimations = true;
            this.btnComfirm.AllowMouseEffects = true;
            this.btnComfirm.AllowToggling = false;
            this.btnComfirm.AnimationSpeed = 200;
            this.btnComfirm.AutoGenerateColors = false;
            this.btnComfirm.AutoRoundBorders = false;
            this.btnComfirm.AutoSizeLeftIcon = true;
            this.btnComfirm.AutoSizeRightIcon = true;
            this.btnComfirm.BackColor = System.Drawing.Color.Transparent;
            this.btnComfirm.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(58)))), ((int)(((byte)(108)))));
            this.btnComfirm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnComfirm.BackgroundImage")));
            this.btnComfirm.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnComfirm.ButtonText = "SEARCH";
            this.btnComfirm.ButtonTextMarginLeft = 0;
            this.btnComfirm.ColorContrastOnClick = 45;
            this.btnComfirm.ColorContrastOnHover = 45;
            this.btnComfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            borderEdges1.BottomLeft = true;
            borderEdges1.BottomRight = true;
            borderEdges1.TopLeft = true;
            borderEdges1.TopRight = true;
            this.btnComfirm.CustomizableEdges = borderEdges1;
            this.btnComfirm.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnComfirm.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.btnComfirm.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnComfirm.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.btnComfirm.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.btnComfirm.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnComfirm.ForeColor = System.Drawing.Color.White;
            this.btnComfirm.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnComfirm.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.btnComfirm.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.btnComfirm.IconMarginLeft = 11;
            this.btnComfirm.IconPadding = 10;
            this.btnComfirm.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnComfirm.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.btnComfirm.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.btnComfirm.IconSize = 25;
            this.btnComfirm.IdleBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(58)))), ((int)(((byte)(108)))));
            this.btnComfirm.IdleBorderRadius = 1;
            this.btnComfirm.IdleBorderThickness = 1;
            this.btnComfirm.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(58)))), ((int)(((byte)(108)))));
            this.btnComfirm.IdleIconLeftImage = null;
            this.btnComfirm.IdleIconRightImage = null;
            this.btnComfirm.IndicateFocus = false;
            this.btnComfirm.Location = new System.Drawing.Point(401, 4);
            this.btnComfirm.Name = "btnComfirm";
            this.btnComfirm.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.btnComfirm.OnDisabledState.BorderRadius = 1;
            this.btnComfirm.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnComfirm.OnDisabledState.BorderThickness = 1;
            this.btnComfirm.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnComfirm.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.btnComfirm.OnDisabledState.IconLeftImage = null;
            this.btnComfirm.OnDisabledState.IconRightImage = null;
            this.btnComfirm.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(58)))), ((int)(((byte)(150)))));
            this.btnComfirm.onHoverState.BorderRadius = 1;
            this.btnComfirm.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnComfirm.onHoverState.BorderThickness = 1;
            this.btnComfirm.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(58)))), ((int)(((byte)(108)))));
            this.btnComfirm.onHoverState.ForeColor = System.Drawing.Color.White;
            this.btnComfirm.onHoverState.IconLeftImage = null;
            this.btnComfirm.onHoverState.IconRightImage = null;
            this.btnComfirm.OnIdleState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(58)))), ((int)(((byte)(108)))));
            this.btnComfirm.OnIdleState.BorderRadius = 1;
            this.btnComfirm.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnComfirm.OnIdleState.BorderThickness = 1;
            this.btnComfirm.OnIdleState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(58)))), ((int)(((byte)(108)))));
            this.btnComfirm.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.btnComfirm.OnIdleState.IconLeftImage = null;
            this.btnComfirm.OnIdleState.IconRightImage = null;
            this.btnComfirm.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(58)))), ((int)(((byte)(108)))));
            this.btnComfirm.OnPressedState.BorderRadius = 1;
            this.btnComfirm.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnComfirm.OnPressedState.BorderThickness = 1;
            this.btnComfirm.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(58)))), ((int)(((byte)(108)))));
            this.btnComfirm.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.btnComfirm.OnPressedState.IconLeftImage = null;
            this.btnComfirm.OnPressedState.IconRightImage = null;
            this.btnComfirm.Size = new System.Drawing.Size(125, 32);
            this.btnComfirm.TabIndex = 5;
            this.btnComfirm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnComfirm.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnComfirm.TextMarginLeft = 0;
            this.btnComfirm.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnComfirm.UseDefaultRadiusAndThickness = true;
            this.btnComfirm.Click += new System.EventHandler(this.btnComfirm_Click_1);
            // 
            // dpToDate
            // 
            this.dpToDate.BackColor = System.Drawing.Color.Transparent;
            this.dpToDate.BorderRadius = 1;
            this.dpToDate.Color = System.Drawing.Color.Silver;
            this.dpToDate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dpToDate.CustomFormat = "dd / MM / yyyy";
            this.dpToDate.DateBorderThickness = Bunifu.UI.WinForms.BunifuDatePicker.BorderThickness.Thin;
            this.dpToDate.DateTextAlign = Bunifu.UI.WinForms.BunifuDatePicker.TextAlign.Left;
            this.dpToDate.DisabledColor = System.Drawing.Color.Gray;
            this.dpToDate.DisplayWeekNumbers = false;
            this.dpToDate.DPHeight = 0;
            this.dpToDate.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dpToDate.FillDatePicker = false;
            this.dpToDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dpToDate.ForeColor = System.Drawing.Color.White;
            this.dpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpToDate.Icon = ((System.Drawing.Image)(resources.GetObject("dpToDate.Icon")));
            this.dpToDate.IconColor = System.Drawing.Color.Gray;
            this.dpToDate.IconLocation = Bunifu.UI.WinForms.BunifuDatePicker.Indicator.Right;
            this.dpToDate.LeftTextMargin = 5;
            this.dpToDate.Location = new System.Drawing.Point(209, 3);
            this.dpToDate.MinimumSize = new System.Drawing.Size(4, 32);
            this.dpToDate.Name = "dpToDate";
            this.dpToDate.Size = new System.Drawing.Size(157, 32);
            this.dpToDate.TabIndex = 4;
            this.dpToDate.Value = new System.DateTime(2023, 4, 12, 0, 0, 0, 0);
            this.dpToDate.ValueChanged += new System.EventHandler(this.dpToDate_ValueChanged_1);
            // 
            // dpFromDate
            // 
            this.dpFromDate.BackColor = System.Drawing.Color.Transparent;
            this.dpFromDate.BorderRadius = 1;
            this.dpFromDate.Color = System.Drawing.Color.Silver;
            this.dpFromDate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dpFromDate.CustomFormat = "dd / MM / yyyy";
            this.dpFromDate.DateBorderThickness = Bunifu.UI.WinForms.BunifuDatePicker.BorderThickness.Thin;
            this.dpFromDate.DateTextAlign = Bunifu.UI.WinForms.BunifuDatePicker.TextAlign.Left;
            this.dpFromDate.DisabledColor = System.Drawing.Color.Gray;
            this.dpFromDate.DisplayWeekNumbers = false;
            this.dpFromDate.DPHeight = 0;
            this.dpFromDate.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dpFromDate.FillDatePicker = false;
            this.dpFromDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dpFromDate.ForeColor = System.Drawing.Color.White;
            this.dpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpFromDate.Icon = ((System.Drawing.Image)(resources.GetObject("dpFromDate.Icon")));
            this.dpFromDate.IconColor = System.Drawing.Color.Gray;
            this.dpFromDate.IconLocation = Bunifu.UI.WinForms.BunifuDatePicker.Indicator.Right;
            this.dpFromDate.LeftTextMargin = 5;
            this.dpFromDate.Location = new System.Drawing.Point(29, 3);
            this.dpFromDate.MinimumSize = new System.Drawing.Size(4, 32);
            this.dpFromDate.Name = "dpFromDate";
            this.dpFromDate.Size = new System.Drawing.Size(157, 32);
            this.dpFromDate.TabIndex = 3;
            this.dpFromDate.Value = new System.DateTime(2023, 4, 13, 0, 0, 0, 0);
            // 
            // toastInfo
            // 
            this.toastInfo.AllowDragging = false;
            this.toastInfo.AllowMultipleViews = false;
            this.toastInfo.ClickToClose = true;
            this.toastInfo.DoubleClickToClose = true;
            this.toastInfo.DurationAfterIdle = 2000;
            this.toastInfo.ErrorOptions.ActionBackColor = System.Drawing.Color.White;
            this.toastInfo.ErrorOptions.ActionBorderColor = System.Drawing.Color.White;
            this.toastInfo.ErrorOptions.ActionBorderRadius = 1;
            this.toastInfo.ErrorOptions.ActionFont = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.toastInfo.ErrorOptions.ActionForeColor = System.Drawing.Color.White;
            this.toastInfo.ErrorOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(31)))), ((int)(((byte)(74)))));
            this.toastInfo.ErrorOptions.BorderColor = System.Drawing.Color.White;
            this.toastInfo.ErrorOptions.CloseIconColor = System.Drawing.Color.White;
            this.toastInfo.ErrorOptions.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.toastInfo.ErrorOptions.ForeColor = System.Drawing.Color.White;
            this.toastInfo.ErrorOptions.Icon = ((System.Drawing.Image)(resources.GetObject("resource.Icon")));
            this.toastInfo.ErrorOptions.IconLeftMargin = 12;
            this.toastInfo.FadeCloseIcon = true;
            this.toastInfo.Host = Bunifu.UI.WinForms.BunifuSnackbar.Hosts.FormOwner;
            this.toastInfo.InformationOptions.ActionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.toastInfo.InformationOptions.ActionBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.toastInfo.InformationOptions.ActionBorderRadius = 1;
            this.toastInfo.InformationOptions.ActionFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.toastInfo.InformationOptions.ActionForeColor = System.Drawing.Color.Black;
            this.toastInfo.InformationOptions.BackColor = System.Drawing.Color.White;
            this.toastInfo.InformationOptions.BorderColor = System.Drawing.Color.White;
            this.toastInfo.InformationOptions.CloseIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(213)))), ((int)(((byte)(255)))));
            this.toastInfo.InformationOptions.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.toastInfo.InformationOptions.ForeColor = System.Drawing.Color.Black;
            this.toastInfo.InformationOptions.Icon = ((System.Drawing.Image)(resources.GetObject("resource.Icon1")));
            this.toastInfo.InformationOptions.IconLeftMargin = 12;
            this.toastInfo.Margin = 10;
            this.toastInfo.MaximumSize = new System.Drawing.Size(0, 0);
            this.toastInfo.MaximumViews = 7;
            this.toastInfo.MessageRightMargin = 15;
            this.toastInfo.MinimumSize = new System.Drawing.Size(0, 0);
            this.toastInfo.ShowBorders = false;
            this.toastInfo.ShowCloseIcon = false;
            this.toastInfo.ShowIcon = true;
            this.toastInfo.ShowShadows = true;
            this.toastInfo.SuccessOptions.ActionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.toastInfo.SuccessOptions.ActionBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.toastInfo.SuccessOptions.ActionBorderRadius = 1;
            this.toastInfo.SuccessOptions.ActionFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.toastInfo.SuccessOptions.ActionForeColor = System.Drawing.Color.Black;
            this.toastInfo.SuccessOptions.BackColor = System.Drawing.Color.White;
            this.toastInfo.SuccessOptions.BorderColor = System.Drawing.Color.White;
            this.toastInfo.SuccessOptions.CloseIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(255)))), ((int)(((byte)(237)))));
            this.toastInfo.SuccessOptions.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.toastInfo.SuccessOptions.ForeColor = System.Drawing.Color.Black;
            this.toastInfo.SuccessOptions.Icon = ((System.Drawing.Image)(resources.GetObject("resource.Icon2")));
            this.toastInfo.SuccessOptions.IconLeftMargin = 12;
            this.toastInfo.ViewsMargin = 7;
            this.toastInfo.WarningOptions.ActionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.toastInfo.WarningOptions.ActionBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.toastInfo.WarningOptions.ActionBorderRadius = 1;
            this.toastInfo.WarningOptions.ActionFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.toastInfo.WarningOptions.ActionForeColor = System.Drawing.Color.Black;
            this.toastInfo.WarningOptions.BackColor = System.Drawing.Color.White;
            this.toastInfo.WarningOptions.BorderColor = System.Drawing.Color.White;
            this.toastInfo.WarningOptions.CloseIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(229)))), ((int)(((byte)(143)))));
            this.toastInfo.WarningOptions.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.toastInfo.WarningOptions.ForeColor = System.Drawing.Color.Black;
            this.toastInfo.WarningOptions.Icon = ((System.Drawing.Image)(resources.GetObject("resource.Icon3")));
            this.toastInfo.WarningOptions.IconLeftMargin = 12;
            this.toastInfo.ZoomCloseIcon = true;
            // 
            // idCookie
            // 
            this.idCookie.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.idCookie.Frozen = true;
            this.idCookie.HeaderText = "ID";
            this.idCookie.Name = "idCookie";
            this.idCookie.ReadOnly = true;
            this.idCookie.Width = 124;
            // 
            // IP
            // 
            this.IP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IP.Frozen = true;
            this.IP.HeaderText = "IP";
            this.IP.Name = "IP";
            this.IP.ReadOnly = true;
            this.IP.Width = 123;
            // 
            // cookie
            // 
            this.cookie.HeaderText = "COOKIE";
            this.cookie.Name = "cookie";
            this.cookie.ReadOnly = true;
            // 
            // c_user
            // 
            this.c_user.HeaderText = "C_USER";
            this.c_user.Name = "c_user";
            this.c_user.ReadOnly = true;
            // 
            // createdDate
            // 
            this.createdDate.HeaderText = "CREATED";
            this.createdDate.Name = "createdDate";
            this.createdDate.ReadOnly = true;
            // 
            // useCookie
            // 
            this.useCookie.HeaderText = "STATUS";
            this.useCookie.Name = "useCookie";
            this.useCookie.ReadOnly = true;
            // 
            // actionButton
            // 
            this.actionButton.HeaderText = "ACTION";
            this.actionButton.Name = "actionButton";
            // 
            // autoButton
            // 
            this.autoButton.HeaderText = "AUTO";
            this.autoButton.Name = "autoButton";
            this.autoButton.ReadOnly = true;
            this.autoButton.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.autoButton.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // facode
            // 
            this.facode.HeaderText = "2FA";
            this.facode.Name = "facode";
            this.facode.ReadOnly = true;
            this.facode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.facode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.facode.ToolTipText = "GET 2FA CODE";
            // 
            // tabCookieInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(49)))));
            this.Controls.Add(this.bunifuPanel1);
            this.Controls.Add(this.dgvCookies);
            this.Controls.Add(this.bunifuSeparator1);
            this.Name = "tabCookieInfo";
            this.Size = new System.Drawing.Size(866, 456);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCookies)).EndInit();
            this.bunifuPanel1.ResumeLayout(false);
            this.bunifuPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnNext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrevious)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator1;
        private Bunifu.UI.WinForms.BunifuDataGridView dgvCookies;
        private Bunifu.UI.WinForms.BunifuPanel bunifuPanel1;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnComfirm;
        private Bunifu.UI.WinForms.BunifuDatePicker dpToDate;
        private Bunifu.UI.WinForms.BunifuDatePicker dpFromDate;
        private System.Windows.Forms.PictureBox btnNext;
        private System.Windows.Forms.Label lblPagingVisitor;
        private System.Windows.Forms.PictureBox btnPrevious;
        private Bunifu.UI.WinForms.BunifuSnackbar toastInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn idCookie;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP;
        private System.Windows.Forms.DataGridViewTextBoxColumn cookie;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_user;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn useCookie;
        private DataGridViewDisableButtonColumn actionButton;
        private DataGridViewDisableButtonColumn autoButton;
        private DataGridViewDisableButtonColumn facode;
    }
}
