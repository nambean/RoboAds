
namespace FacebookAdsTool
{
    partial class Main
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            this.panelHeader = new Bunifu.UI.WinForms.BunifuPanel();
            this.userPictureBox = new Bunifu.UI.WinForms.BunifuPictureBox();
            this.contextUser = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.userMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnMaximized = new Bunifu.UI.WinForms.BunifuImageButton();
            this.btnMinimize = new Bunifu.UI.WinForms.BunifuImageButton();
            this.btnClose = new Bunifu.UI.WinForms.BunifuImageButton();
            this.panelMainContainer = new Bunifu.UI.WinForms.BunifuPanel();
            this.btnInject = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.dragControl = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.toastInfo = new Bunifu.UI.WinForms.BunifuSnackbar(this.components);
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userPictureBox)).BeginInit();
            this.contextUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelMainContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(41)))), ((int)(((byte)(61)))));
            this.panelHeader.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelHeader.BackgroundImage")));
            this.panelHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelHeader.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(55)))), ((int)(((byte)(81)))));
            this.panelHeader.BorderRadius = 0;
            this.panelHeader.BorderThickness = 0;
            this.panelHeader.Controls.Add(this.userPictureBox);
            this.panelHeader.Controls.Add(this.pictureBox1);
            this.panelHeader.Controls.Add(this.btnMaximized);
            this.panelHeader.Controls.Add(this.btnMinimize);
            this.panelHeader.Controls.Add(this.btnClose);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.ShowBorders = true;
            this.panelHeader.Size = new System.Drawing.Size(1260, 44);
            this.panelHeader.TabIndex = 0;
            // 
            // userPictureBox
            // 
            this.userPictureBox.AllowFocused = false;
            this.userPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.userPictureBox.AutoSizeHeight = true;
            this.userPictureBox.BorderRadius = 15;
            this.userPictureBox.ContextMenuStrip = this.contextUser;
            this.userPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("userPictureBox.Image")));
            this.userPictureBox.IsCircle = true;
            this.userPictureBox.Location = new System.Drawing.Point(954, 9);
            this.userPictureBox.Name = "userPictureBox";
            this.userPictureBox.Size = new System.Drawing.Size(30, 30);
            this.userPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.userPictureBox.TabIndex = 14;
            this.userPictureBox.TabStop = false;
            this.userPictureBox.Type = Bunifu.UI.WinForms.BunifuPictureBox.Types.Circle;
            // 
            // contextUser
            // 
            this.contextUser.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userMenuItem,
            this.logoutMenuItem});
            this.contextUser.Name = "contextUser";
            this.contextUser.Size = new System.Drawing.Size(128, 48);
            // 
            // userMenuItem
            // 
            this.userMenuItem.Name = "userMenuItem";
            this.userMenuItem.Size = new System.Drawing.Size(127, 22);
            this.userMenuItem.Text = "Cá nhân";
            // 
            // logoutMenuItem
            // 
            this.logoutMenuItem.Name = "logoutMenuItem";
            this.logoutMenuItem.Size = new System.Drawing.Size(127, 22);
            this.logoutMenuItem.Text = "Đăng xuất";
            this.logoutMenuItem.Click += new System.EventHandler(this.logoutMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(2, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(88, 41);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // btnMaximized
            // 
            this.btnMaximized.ActiveImage = null;
            this.btnMaximized.AllowAnimations = true;
            this.btnMaximized.AllowBuffering = false;
            this.btnMaximized.AllowToggling = false;
            this.btnMaximized.AllowZooming = true;
            this.btnMaximized.AllowZoomingOnFocus = false;
            this.btnMaximized.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnMaximized.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(41)))), ((int)(((byte)(61)))));
            this.btnMaximized.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnMaximized.ErrorImage = ((System.Drawing.Image)(resources.GetObject("btnMaximized.ErrorImage")));
            this.btnMaximized.FadeWhenInactive = false;
            this.btnMaximized.Flip = Bunifu.UI.WinForms.BunifuImageButton.FlipOrientation.Normal;
            this.btnMaximized.Image = ((System.Drawing.Image)(resources.GetObject("btnMaximized.Image")));
            this.btnMaximized.ImageActive = null;
            this.btnMaximized.ImageLocation = null;
            this.btnMaximized.ImageMargin = 0;
            this.btnMaximized.ImageSize = new System.Drawing.Size(13, 13);
            this.btnMaximized.ImageZoomSize = new System.Drawing.Size(14, 14);
            this.btnMaximized.InitialImage = ((System.Drawing.Image)(resources.GetObject("btnMaximized.InitialImage")));
            this.btnMaximized.Location = new System.Drawing.Point(1191, 16);
            this.btnMaximized.Margin = new System.Windows.Forms.Padding(0);
            this.btnMaximized.Name = "btnMaximized";
            this.btnMaximized.Rotation = 0;
            this.btnMaximized.ShowActiveImage = true;
            this.btnMaximized.ShowCursorChanges = true;
            this.btnMaximized.ShowImageBorders = true;
            this.btnMaximized.ShowSizeMarkers = false;
            this.btnMaximized.Size = new System.Drawing.Size(14, 14);
            this.btnMaximized.TabIndex = 8;
            this.btnMaximized.ToolTipText = "";
            this.btnMaximized.WaitOnLoad = false;
            this.btnMaximized.Zoom = 0;
            this.btnMaximized.ZoomSpeed = 10;
            this.btnMaximized.Click += new System.EventHandler(this.btnMaximized_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.ActiveImage = null;
            this.btnMinimize.AllowAnimations = true;
            this.btnMinimize.AllowBuffering = false;
            this.btnMinimize.AllowToggling = false;
            this.btnMinimize.AllowZooming = true;
            this.btnMinimize.AllowZoomingOnFocus = false;
            this.btnMinimize.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnMinimize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(41)))), ((int)(((byte)(61)))));
            this.btnMinimize.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnMinimize.ErrorImage = ((System.Drawing.Image)(resources.GetObject("btnMinimize.ErrorImage")));
            this.btnMinimize.FadeWhenInactive = false;
            this.btnMinimize.Flip = Bunifu.UI.WinForms.BunifuImageButton.FlipOrientation.Normal;
            this.btnMinimize.Image = ((System.Drawing.Image)(resources.GetObject("btnMinimize.Image")));
            this.btnMinimize.ImageActive = null;
            this.btnMinimize.ImageLocation = null;
            this.btnMinimize.ImageMargin = 0;
            this.btnMinimize.ImageSize = new System.Drawing.Size(17, 23);
            this.btnMinimize.ImageZoomSize = new System.Drawing.Size(18, 24);
            this.btnMinimize.InitialImage = ((System.Drawing.Image)(resources.GetObject("btnMinimize.InitialImage")));
            this.btnMinimize.Location = new System.Drawing.Point(1152, 11);
            this.btnMinimize.Margin = new System.Windows.Forms.Padding(0);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Rotation = 0;
            this.btnMinimize.ShowActiveImage = true;
            this.btnMinimize.ShowCursorChanges = true;
            this.btnMinimize.ShowImageBorders = true;
            this.btnMinimize.ShowSizeMarkers = false;
            this.btnMinimize.Size = new System.Drawing.Size(18, 24);
            this.btnMinimize.TabIndex = 6;
            this.btnMinimize.ToolTipText = "";
            this.btnMinimize.WaitOnLoad = false;
            this.btnMinimize.Zoom = 0;
            this.btnMinimize.ZoomSpeed = 10;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnClose
            // 
            this.btnClose.ActiveImage = null;
            this.btnClose.AllowAnimations = true;
            this.btnClose.AllowBuffering = false;
            this.btnClose.AllowToggling = false;
            this.btnClose.AllowZooming = true;
            this.btnClose.AllowZoomingOnFocus = false;
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(41)))), ((int)(((byte)(61)))));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClose.ErrorImage = ((System.Drawing.Image)(resources.GetObject("btnClose.ErrorImage")));
            this.btnClose.FadeWhenInactive = false;
            this.btnClose.Flip = Bunifu.UI.WinForms.BunifuImageButton.FlipOrientation.Normal;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageActive = null;
            this.btnClose.ImageLocation = null;
            this.btnClose.ImageMargin = 0;
            this.btnClose.ImageSize = new System.Drawing.Size(21, 21);
            this.btnClose.ImageZoomSize = new System.Drawing.Size(22, 22);
            this.btnClose.InitialImage = ((System.Drawing.Image)(resources.GetObject("btnClose.InitialImage")));
            this.btnClose.Location = new System.Drawing.Point(1226, 12);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Rotation = 0;
            this.btnClose.ShowActiveImage = true;
            this.btnClose.ShowCursorChanges = true;
            this.btnClose.ShowImageBorders = true;
            this.btnClose.ShowSizeMarkers = false;
            this.btnClose.Size = new System.Drawing.Size(22, 22);
            this.btnClose.TabIndex = 7;
            this.btnClose.ToolTipText = "";
            this.btnClose.WaitOnLoad = false;
            this.btnClose.Zoom = 0;
            this.btnClose.ZoomSpeed = 10;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.BackgroundColor = System.Drawing.Color.Transparent;
            this.panelMainContainer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelMainContainer.BackgroundImage")));
            this.panelMainContainer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelMainContainer.BorderColor = System.Drawing.Color.Transparent;
            this.panelMainContainer.BorderRadius = 0;
            this.panelMainContainer.BorderThickness = 0;
            this.panelMainContainer.Controls.Add(this.btnInject);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 44);
            this.panelMainContainer.Margin = new System.Windows.Forms.Padding(0);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.ShowBorders = true;
            this.panelMainContainer.Size = new System.Drawing.Size(1260, 616);
            this.panelMainContainer.TabIndex = 0;
            // 
            // btnInject
            // 
            this.btnInject.AllowAnimations = true;
            this.btnInject.AllowMouseEffects = true;
            this.btnInject.AllowToggling = false;
            this.btnInject.AnimationSpeed = 200;
            this.btnInject.AutoGenerateColors = false;
            this.btnInject.AutoRoundBorders = false;
            this.btnInject.AutoSizeLeftIcon = true;
            this.btnInject.AutoSizeRightIcon = true;
            this.btnInject.BackColor = System.Drawing.Color.Transparent;
            this.btnInject.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(152)))), ((int)(((byte)(98)))));
            this.btnInject.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnInject.BackgroundImage")));
            this.btnInject.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnInject.ButtonText = "Inject Cookie";
            this.btnInject.ButtonTextMarginLeft = 0;
            this.btnInject.ColorContrastOnClick = 45;
            this.btnInject.ColorContrastOnHover = 45;
            this.btnInject.Cursor = System.Windows.Forms.Cursors.Hand;
            borderEdges1.BottomLeft = true;
            borderEdges1.BottomRight = true;
            borderEdges1.TopLeft = true;
            borderEdges1.TopRight = true;
            this.btnInject.CustomizableEdges = borderEdges1;
            this.btnInject.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnInject.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.btnInject.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnInject.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.btnInject.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.btnInject.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnInject.ForeColor = System.Drawing.Color.White;
            this.btnInject.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInject.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.btnInject.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.btnInject.IconMarginLeft = 11;
            this.btnInject.IconPadding = 10;
            this.btnInject.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnInject.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.btnInject.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.btnInject.IconSize = 25;
            this.btnInject.IdleBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(152)))), ((int)(((byte)(98)))));
            this.btnInject.IdleBorderRadius = 15;
            this.btnInject.IdleBorderThickness = 1;
            this.btnInject.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(152)))), ((int)(((byte)(98)))));
            this.btnInject.IdleIconLeftImage = null;
            this.btnInject.IdleIconRightImage = null;
            this.btnInject.IndicateFocus = false;
            this.btnInject.Location = new System.Drawing.Point(412, 253);
            this.btnInject.Name = "btnInject";
            this.btnInject.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.btnInject.OnDisabledState.BorderRadius = 15;
            this.btnInject.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnInject.OnDisabledState.BorderThickness = 1;
            this.btnInject.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnInject.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.btnInject.OnDisabledState.IconLeftImage = null;
            this.btnInject.OnDisabledState.IconRightImage = null;
            this.btnInject.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(152)))), ((int)(((byte)(98)))));
            this.btnInject.onHoverState.BorderRadius = 15;
            this.btnInject.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnInject.onHoverState.BorderThickness = 1;
            this.btnInject.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(152)))), ((int)(((byte)(98)))));
            this.btnInject.onHoverState.ForeColor = System.Drawing.Color.White;
            this.btnInject.onHoverState.IconLeftImage = null;
            this.btnInject.onHoverState.IconRightImage = null;
            this.btnInject.OnIdleState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(152)))), ((int)(((byte)(98)))));
            this.btnInject.OnIdleState.BorderRadius = 15;
            this.btnInject.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnInject.OnIdleState.BorderThickness = 1;
            this.btnInject.OnIdleState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(152)))), ((int)(((byte)(98)))));
            this.btnInject.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.btnInject.OnIdleState.IconLeftImage = null;
            this.btnInject.OnIdleState.IconRightImage = null;
            this.btnInject.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(152)))), ((int)(((byte)(98)))));
            this.btnInject.OnPressedState.BorderRadius = 15;
            this.btnInject.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnInject.OnPressedState.BorderThickness = 1;
            this.btnInject.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(152)))), ((int)(((byte)(98)))));
            this.btnInject.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.btnInject.OnPressedState.IconLeftImage = null;
            this.btnInject.OnPressedState.IconRightImage = null;
            this.btnInject.Size = new System.Drawing.Size(328, 36);
            this.btnInject.TabIndex = 4;
            this.btnInject.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnInject.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnInject.TextMarginLeft = 0;
            this.btnInject.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnInject.UseDefaultRadiusAndThickness = true;
            // 
            // dragControl
            // 
            this.dragControl.Fixed = true;
            this.dragControl.Horizontal = true;
            this.dragControl.TargetControl = this.panelHeader;
            this.dragControl.Vertical = true;
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
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(47)))));
            this.ClientSize = new System.Drawing.Size(1260, 660);
            this.Controls.Add(this.panelMainContainer);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.userPictureBox)).EndInit();
            this.contextUser.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelMainContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.UI.WinForms.BunifuPanel panelHeader;
        private Bunifu.UI.WinForms.BunifuPanel panelMainContainer;
        private Bunifu.Framework.UI.BunifuDragControl dragControl;
        private Bunifu.UI.WinForms.BunifuImageButton btnMinimize;
        private Bunifu.UI.WinForms.BunifuImageButton btnClose;
        private Bunifu.UI.WinForms.BunifuImageButton btnMaximized;
        private Bunifu.UI.WinForms.BunifuSnackbar toastInfo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Bunifu.UI.WinForms.BunifuPictureBox userPictureBox;
        private System.Windows.Forms.ContextMenuStrip contextUser;
        private System.Windows.Forms.ToolStripMenuItem userMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutMenuItem;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnInject;
    }
}