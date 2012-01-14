namespace AnimationMaker
{
	partial class Main
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.GraphicList = new System.Windows.Forms.ListBox();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.DeleteMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.NewTool = new System.Windows.Forms.ToolStripMenuItem();
			this.OpenTool = new System.Windows.Forms.ToolStripMenuItem();
			this.UpdateTool = new System.Windows.Forms.ToolStripMenuItem();
			this.SaveTool = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.ExitTool = new System.Windows.Forms.ToolStripMenuItem();
			this.EdittoolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.AddTool = new System.Windows.Forms.ToolStripMenuItem();
			this.RemoveTool = new System.Windows.Forms.ToolStripMenuItem();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.ImgWidth = new System.Windows.Forms.TextBox();
			this.ImgHeight = new System.Windows.Forms.TextBox();
			this.SaveButton = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.ID_Name = new System.Windows.Forms.TextBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.DivCount = new System.Windows.Forms.TextBox();
			this.Y_Div = new System.Windows.Forms.TextBox();
			this.X_Div = new System.Windows.Forms.TextBox();
			this.CanselButton = new System.Windows.Forms.Button();
			this.DivPanel = new System.Windows.Forms.Panel();
			this.DataPanel = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.DrawablePanel = new System.Windows.Forms.Panel();
			this.label6 = new System.Windows.Forms.Label();
			this.X_Move = new System.Windows.Forms.NumericUpDown();
			this.label7 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.Y_Move = new System.Windows.Forms.NumericUpDown();
			this.enableBackGround = new System.Windows.Forms.CheckBox();
			this.enableAnimation = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.animationFrame = new System.Windows.Forms.NumericUpDown();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.selectBackGround = new System.Windows.Forms.Button();
			this.backGroundFile = new System.Windows.Forms.TextBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.label12 = new System.Windows.Forms.Label();
			this.rangeEnd = new System.Windows.Forms.NumericUpDown();
			this.label13 = new System.Windows.Forms.Label();
			this.rangeBegin = new System.Windows.Forms.NumericUpDown();
			this.contextMenuStrip1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.DivPanel.SuspendLayout();
			this.DataPanel.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.X_Move)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Y_Move)).BeginInit();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.animationFrame)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.rangeEnd)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rangeBegin)).BeginInit();
			this.SuspendLayout();
			// 
			// GraphicList
			// 
			this.GraphicList.ContextMenuStrip = this.contextMenuStrip1;
			this.GraphicList.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.GraphicList.FormattingEnabled = true;
			this.GraphicList.ItemHeight = 20;
			this.GraphicList.Location = new System.Drawing.Point(7, 27);
			this.GraphicList.Margin = new System.Windows.Forms.Padding(4);
			this.GraphicList.Name = "GraphicList";
			this.GraphicList.Size = new System.Drawing.Size(133, 404);
			this.GraphicList.Sorted = true;
			this.GraphicList.TabIndex = 0;
			this.GraphicList.TabStop = false;
			this.GraphicList.SelectedIndexChanged += new System.EventHandler(this.GraphicList_SelectedIndexChanged);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.DeleteMenu});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(161, 26);
			// 
			// DeleteMenu
			// 
			this.DeleteMenu.Enabled = false;
			this.DeleteMenu.Name = "DeleteMenu";
			this.DeleteMenu.Size = new System.Drawing.Size(160, 22);
			this.DeleteMenu.Text = "エントリの削除";
			this.DeleteMenu.Click += new System.EventHandler(this.DeleteMenu_Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.FileToolStripMenuItem,
			this.EdittoolStripMenuItem1});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
			this.menuStrip1.Size = new System.Drawing.Size(864, 28);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// FileToolStripMenuItem
			// 
			this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.NewTool,
			this.OpenTool,
			this.UpdateTool,
			this.SaveTool,
			this.toolStripSeparator1,
			this.ExitTool});
			this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
			this.FileToolStripMenuItem.Size = new System.Drawing.Size(68, 22);
			this.FileToolStripMenuItem.Text = "ファイル";
			// 
			// NewTool
			// 
			this.NewTool.Name = "NewTool";
			this.NewTool.Size = new System.Drawing.Size(172, 22);
			this.NewTool.Text = "新規";
			this.NewTool.Click += new System.EventHandler(this.NewTool_Click);
			// 
			// OpenTool
			// 
			this.OpenTool.Name = "OpenTool";
			this.OpenTool.Size = new System.Drawing.Size(172, 22);
			this.OpenTool.Text = "開く";
			this.OpenTool.Click += new System.EventHandler(this.OpenTool_Click);
			// 
			// UpdateTool
			// 
			this.UpdateTool.Name = "UpdateTool";
			this.UpdateTool.Size = new System.Drawing.Size(172, 22);
			this.UpdateTool.Text = "上書き保存";
			this.UpdateTool.Click += new System.EventHandler(this.UpdateTool_Click);
			// 
			// SaveTool
			// 
			this.SaveTool.Name = "SaveTool";
			this.SaveTool.Size = new System.Drawing.Size(172, 22);
			this.SaveTool.Text = "名前をつけて保存";
			this.SaveTool.Click += new System.EventHandler(this.SaveTool_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(169, 6);
			// 
			// ExitTool
			// 
			this.ExitTool.Name = "ExitTool";
			this.ExitTool.Size = new System.Drawing.Size(172, 22);
			this.ExitTool.Text = "終了";
			this.ExitTool.Click += new System.EventHandler(this.ExitTool_Click);
			// 
			// EdittoolStripMenuItem1
			// 
			this.EdittoolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.AddTool,
			this.RemoveTool});
			this.EdittoolStripMenuItem1.Name = "EdittoolStripMenuItem1";
			this.EdittoolStripMenuItem1.Size = new System.Drawing.Size(44, 22);
			this.EdittoolStripMenuItem1.Text = "編集";
			// 
			// AddTool
			// 
			this.AddTool.Name = "AddTool";
			this.AddTool.Size = new System.Drawing.Size(160, 22);
			this.AddTool.Text = "エントリの追加";
			this.AddTool.Click += new System.EventHandler(this.AddTool_Click);
			// 
			// RemoveTool
			// 
			this.RemoveTool.Enabled = false;
			this.RemoveTool.Name = "RemoveTool";
			this.RemoveTool.Size = new System.Drawing.Size(160, 22);
			this.RemoveTool.Text = "エントリの削除";
			this.RemoveTool.Click += new System.EventHandler(this.RemoveTool_Click);
			// 
			// textBox1
			// 
			this.textBox1.ForeColor = System.Drawing.Color.Black;
			this.textBox1.Location = new System.Drawing.Point(273, 31);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(142, 23);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "graphics/";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(12, 31);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(218, 24);
			this.label2.TabIndex = 8;
			this.label2.Text = "実行ファイルからの相対パス";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(246, 34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(21, 16);
			this.label1.TabIndex = 9;
			this.label1.Text = "./";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label3.Location = new System.Drawing.Point(90, 235);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(90, 24);
			this.label3.TabIndex = 33;
			this.label3.Text = "画像の高さ";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label4.Location = new System.Drawing.Point(106, 203);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(74, 24);
			this.label4.TabIndex = 32;
			this.label4.Text = "画像の幅";
			// 
			// ImgWidth
			// 
			this.ImgWidth.Location = new System.Drawing.Point(184, 203);
			this.ImgWidth.Name = "ImgWidth";
			this.ImgWidth.ReadOnly = true;
			this.ImgWidth.Size = new System.Drawing.Size(56, 23);
			this.ImgWidth.TabIndex = 31;
			this.ImgWidth.TabStop = false;
			// 
			// ImgHeight
			// 
			this.ImgHeight.Location = new System.Drawing.Point(184, 236);
			this.ImgHeight.Name = "ImgHeight";
			this.ImgHeight.ReadOnly = true;
			this.ImgHeight.Size = new System.Drawing.Size(56, 23);
			this.ImgHeight.TabIndex = 30;
			this.ImgHeight.TabStop = false;
			// 
			// SaveButton
			// 
			this.SaveButton.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.SaveButton.Location = new System.Drawing.Point(30, 271);
			this.SaveButton.Name = "SaveButton";
			this.SaveButton.Size = new System.Drawing.Size(74, 55);
			this.SaveButton.TabIndex = 9;
			this.SaveButton.Text = "保存";
			this.SaveButton.UseVisualStyleBackColor = true;
			this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.button3.Location = new System.Drawing.Point(110, 271);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(130, 25);
			this.button3.TabIndex = 7;
			this.button3.Text = "ID重複チェック";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label5.Location = new System.Drawing.Point(50, 48);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(31, 24);
			this.label5.TabIndex = 21;
			this.label5.Text = "ID";
			// 
			// ID_Name
			// 
			this.ID_Name.Location = new System.Drawing.Point(81, 48);
			this.ID_Name.Name = "ID_Name";
			this.ID_Name.Size = new System.Drawing.Size(159, 23);
			this.ID_Name.TabIndex = 2;
			// 
			// comboBox1
			// 
			this.comboBox1.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] {
			"単一グラフィック",
			"分割グラフィック",
			"アニメーション"});
			this.comboBox1.Location = new System.Drawing.Point(30, 0);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(210, 31);
			this.comboBox1.TabIndex = 1;
			this.comboBox1.Text = "単一グラフィック";
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label8.Location = new System.Drawing.Point(21, 66);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(106, 24);
			this.label8.TabIndex = 24;
			this.label8.Text = "分割数の合計";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label9.Location = new System.Drawing.Point(3, 28);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(122, 24);
			this.label9.TabIndex = 23;
			this.label9.Text = "縦方向の分割数";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label10.Location = new System.Drawing.Point(3, 3);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(122, 24);
			this.label10.TabIndex = 22;
			this.label10.Text = "横方向の分割数";
			// 
			// DivCount
			// 
			this.DivCount.Location = new System.Drawing.Point(133, 67);
			this.DivCount.Name = "DivCount";
			this.DivCount.Size = new System.Drawing.Size(60, 23);
			this.DivCount.TabIndex = 6;
			this.DivCount.TextChanged += new System.EventHandler(this.DivCount_TextChanged);
			// 
			// Y_Div
			// 
			this.Y_Div.Location = new System.Drawing.Point(133, 29);
			this.Y_Div.Name = "Y_Div";
			this.Y_Div.Size = new System.Drawing.Size(60, 23);
			this.Y_Div.TabIndex = 5;
			this.Y_Div.TextChanged += new System.EventHandler(this.Y_Div_TextChanged);
			// 
			// X_Div
			// 
			this.X_Div.Location = new System.Drawing.Point(133, 4);
			this.X_Div.Name = "X_Div";
			this.X_Div.Size = new System.Drawing.Size(60, 23);
			this.X_Div.TabIndex = 4;
			this.X_Div.TextChanged += new System.EventHandler(this.X_Div_TextChanged);
			// 
			// CanselButton
			// 
			this.CanselButton.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.CanselButton.Location = new System.Drawing.Point(110, 301);
			this.CanselButton.Name = "CanselButton";
			this.CanselButton.Size = new System.Drawing.Size(130, 25);
			this.CanselButton.TabIndex = 8;
			this.CanselButton.Text = "保存キャンセル";
			this.CanselButton.UseVisualStyleBackColor = true;
			this.CanselButton.Click += new System.EventHandler(this.CanselButton_Click);
			// 
			// DivPanel
			// 
			this.DivPanel.Controls.Add(this.label10);
			this.DivPanel.Controls.Add(this.X_Div);
			this.DivPanel.Controls.Add(this.Y_Div);
			this.DivPanel.Controls.Add(this.label8);
			this.DivPanel.Controls.Add(this.DivCount);
			this.DivPanel.Controls.Add(this.label9);
			this.DivPanel.Location = new System.Drawing.Point(47, 77);
			this.DivPanel.Name = "DivPanel";
			this.DivPanel.Size = new System.Drawing.Size(193, 100);
			this.DivPanel.TabIndex = 3;
			this.DivPanel.Visible = false;
			// 
			// DataPanel
			// 
			this.DataPanel.Controls.Add(this.comboBox1);
			this.DataPanel.Controls.Add(this.button3);
			this.DataPanel.Controls.Add(this.CanselButton);
			this.DataPanel.Controls.Add(this.SaveButton);
			this.DataPanel.Controls.Add(this.label5);
			this.DataPanel.Controls.Add(this.label3);
			this.DataPanel.Controls.Add(this.ID_Name);
			this.DataPanel.Controls.Add(this.label4);
			this.DataPanel.Controls.Add(this.ImgHeight);
			this.DataPanel.Controls.Add(this.DivPanel);
			this.DataPanel.Controls.Add(this.ImgWidth);
			this.DataPanel.Location = new System.Drawing.Point(421, 34);
			this.DataPanel.Name = "DataPanel";
			this.DataPanel.Size = new System.Drawing.Size(269, 326);
			this.DataPanel.TabIndex = 35;
			this.DataPanel.Visible = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.GraphicList);
			this.groupBox1.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.groupBox1.Location = new System.Drawing.Point(706, 34);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(146, 437);
			this.groupBox1.TabIndex = 36;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "登録済みリスト";
			// 
			// DrawablePanel
			// 
			this.DrawablePanel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.DrawablePanel.Location = new System.Drawing.Point(16, 61);
			this.DrawablePanel.Name = "DrawablePanel";
			this.DrawablePanel.Size = new System.Drawing.Size(400, 300);
			this.DrawablePanel.TabIndex = 37;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label6.Location = new System.Drawing.Point(23, 19);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(106, 24);
			this.label6.TabIndex = 40;
			this.label6.Text = "フレーム間隔";
			// 
			// X_Move
			// 
			this.X_Move.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.X_Move.Increment = new decimal(new int[] {
			5,
			0,
			0,
			0});
			this.X_Move.Location = new System.Drawing.Point(53, 26);
			this.X_Move.Maximum = new decimal(new int[] {
			200,
			0,
			0,
			0});
			this.X_Move.Minimum = new decimal(new int[] {
			200,
			0,
			0,
			-2147483648});
			this.X_Move.Name = "X_Move";
			this.X_Move.Size = new System.Drawing.Size(60, 23);
			this.X_Move.TabIndex = 41;
			this.X_Move.TabStop = false;
			this.X_Move.ValueChanged += new System.EventHandler(this.X_Move_ValueChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label7.Location = new System.Drawing.Point(9, 25);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(38, 24);
			this.label7.TabIndex = 42;
			this.label7.Text = "X軸";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label11.Location = new System.Drawing.Point(9, 63);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(37, 24);
			this.label11.TabIndex = 44;
			this.label11.Text = "Y軸";
			// 
			// Y_Move
			// 
			this.Y_Move.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Y_Move.Increment = new decimal(new int[] {
			5,
			0,
			0,
			0});
			this.Y_Move.Location = new System.Drawing.Point(53, 64);
			this.Y_Move.Maximum = new decimal(new int[] {
			150,
			0,
			0,
			0});
			this.Y_Move.Minimum = new decimal(new int[] {
			150,
			0,
			0,
			-2147483648});
			this.Y_Move.Name = "Y_Move";
			this.Y_Move.Size = new System.Drawing.Size(60, 23);
			this.Y_Move.TabIndex = 43;
			this.Y_Move.TabStop = false;
			this.Y_Move.ValueChanged += new System.EventHandler(this.Y_Move_ValueChanged);
			// 
			// enableBackGround
			// 
			this.enableBackGround.AutoSize = true;
			this.enableBackGround.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.enableBackGround.Location = new System.Drawing.Point(272, 19);
			this.enableBackGround.Name = "enableBackGround";
			this.enableBackGround.Size = new System.Drawing.Size(119, 24);
			this.enableBackGround.TabIndex = 47;
			this.enableBackGround.TabStop = false;
			this.enableBackGround.Text = "背景を描画する";
			this.enableBackGround.UseVisualStyleBackColor = true;
			this.enableBackGround.CheckedChanged += new System.EventHandler(this.enableBackGround_CheckedChanged);
			// 
			// enableAnimation
			// 
			this.enableAnimation.AutoSize = true;
			this.enableAnimation.Enabled = false;
			this.enableAnimation.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.enableAnimation.Location = new System.Drawing.Point(233, 21);
			this.enableAnimation.Name = "enableAnimation";
			this.enableAnimation.Size = new System.Drawing.Size(158, 24);
			this.enableAnimation.TabIndex = 48;
			this.enableAnimation.TabStop = false;
			this.enableAnimation.Text = "アニメーションを行う";
			this.enableAnimation.UseVisualStyleBackColor = true;
			this.enableAnimation.CheckedChanged += new System.EventHandler(this.enableAnimation_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.animationFrame);
			this.groupBox2.Controls.Add(this.enableAnimation);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox2.Location = new System.Drawing.Point(16, 367);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(399, 49);
			this.groupBox2.TabIndex = 49;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "アニメーション設定";
			// 
			// animationFrame
			// 
			this.animationFrame.Enabled = false;
			this.animationFrame.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.animationFrame.Location = new System.Drawing.Point(154, 20);
			this.animationFrame.Maximum = new decimal(new int[] {
			120,
			0,
			0,
			0});
			this.animationFrame.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.animationFrame.Name = "animationFrame";
			this.animationFrame.Size = new System.Drawing.Size(60, 23);
			this.animationFrame.TabIndex = 49;
			this.animationFrame.TabStop = false;
			this.animationFrame.Value = new decimal(new int[] {
			10,
			0,
			0,
			0});
			this.animationFrame.ValueChanged += new System.EventHandler(this.animationFrame_ValueChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label11);
			this.groupBox3.Controls.Add(this.Y_Move);
			this.groupBox3.Controls.Add(this.label7);
			this.groupBox3.Controls.Add(this.X_Move);
			this.groupBox3.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.groupBox3.Location = new System.Drawing.Point(424, 367);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(130, 104);
			this.groupBox3.TabIndex = 50;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "位置";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.selectBackGround);
			this.groupBox4.Controls.Add(this.enableBackGround);
			this.groupBox4.Controls.Add(this.backGroundFile);
			this.groupBox4.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox4.Location = new System.Drawing.Point(16, 422);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(399, 49);
			this.groupBox4.TabIndex = 50;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "背景設定";
			// 
			// selectBackGround
			// 
			this.selectBackGround.Enabled = false;
			this.selectBackGround.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.selectBackGround.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.selectBackGround.Location = new System.Drawing.Point(227, 19);
			this.selectBackGround.Name = "selectBackGround";
			this.selectBackGround.Size = new System.Drawing.Size(28, 24);
			this.selectBackGround.TabIndex = 48;
			this.selectBackGround.TabStop = false;
			this.selectBackGround.Text = "...";
			this.selectBackGround.UseVisualStyleBackColor = true;
			this.selectBackGround.Click += new System.EventHandler(this.selectBackGround_Click);
			// 
			// backGroundFile
			// 
			this.backGroundFile.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.backGroundFile.Location = new System.Drawing.Point(27, 19);
			this.backGroundFile.Name = "backGroundFile";
			this.backGroundFile.ReadOnly = true;
			this.backGroundFile.Size = new System.Drawing.Size(187, 23);
			this.backGroundFile.TabIndex = 25;
			this.backGroundFile.TabStop = false;
			this.backGroundFile.TextChanged += new System.EventHandler(this.backGroundFile_TextChanged);
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.label12);
			this.groupBox5.Controls.Add(this.rangeEnd);
			this.groupBox5.Controls.Add(this.label13);
			this.groupBox5.Controls.Add(this.rangeBegin);
			this.groupBox5.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.groupBox5.Location = new System.Drawing.Point(560, 367);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(130, 104);
			this.groupBox5.TabIndex = 51;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Range";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label12.Location = new System.Drawing.Point(9, 63);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(42, 24);
			this.label12.TabIndex = 44;
			this.label12.Text = "End";
			// 
			// rangeEnd
			// 
			this.rangeEnd.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.rangeEnd.Location = new System.Drawing.Point(58, 63);
			this.rangeEnd.Maximum = new decimal(new int[] {
			10,
			0,
			0,
			0});
			this.rangeEnd.Name = "rangeEnd";
			this.rangeEnd.Size = new System.Drawing.Size(60, 23);
			this.rangeEnd.TabIndex = 43;
			this.rangeEnd.TabStop = false;
			this.rangeEnd.ValueChanged += new System.EventHandler(this.rangeEnd_ValueChanged);
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label13.Location = new System.Drawing.Point(9, 25);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(43, 24);
			this.label13.TabIndex = 42;
			this.label13.Text = "Beg";
			// 
			// rangeBegin
			// 
			this.rangeBegin.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.rangeBegin.Location = new System.Drawing.Point(58, 25);
			this.rangeBegin.Maximum = new decimal(new int[] {
			10,
			0,
			0,
			0});
			this.rangeBegin.Name = "rangeBegin";
			this.rangeBegin.Size = new System.Drawing.Size(60, 23);
			this.rangeBegin.TabIndex = 41;
			this.rangeBegin.TabStop = false;
			this.rangeBegin.ValueChanged += new System.EventHandler(this.rangeBegin_ValueChanged);
			// 
			// Main
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(864, 482);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.DrawablePanel);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.DataPanel);
			this.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(1024, 1024);
			this.MinimumSize = new System.Drawing.Size(397, 520);
			this.Name = "Main";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AnimationMaker ver 0.11 (bug exists.)";
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Main_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Main_DragEnter);
			this.contextMenuStrip1.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.DivPanel.ResumeLayout(false);
			this.DivPanel.PerformLayout();
			this.DataPanel.ResumeLayout(false);
			this.DataPanel.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.X_Move)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Y_Move)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.animationFrame)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.rangeEnd)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rangeBegin)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox GraphicList;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem NewTool;
		private System.Windows.Forms.ToolStripMenuItem OpenTool;
		private System.Windows.Forms.ToolStripMenuItem UpdateTool;
		private System.Windows.Forms.ToolStripMenuItem SaveTool;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem ExitTool;
		private System.Windows.Forms.ToolStripMenuItem EdittoolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem AddTool;
		private System.Windows.Forms.ToolStripMenuItem RemoveTool;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.OpenFileDialog openFileDialog2;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem DeleteMenu;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox ImgWidth;
		private System.Windows.Forms.TextBox ImgHeight;
		private System.Windows.Forms.Button SaveButton;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox ID_Name;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox DivCount;
		private System.Windows.Forms.TextBox Y_Div;
		private System.Windows.Forms.TextBox X_Div;
		private System.Windows.Forms.Button CanselButton;
		private System.Windows.Forms.Panel DivPanel;
		private System.Windows.Forms.Panel DataPanel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Panel DrawablePanel;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown X_Move;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.NumericUpDown Y_Move;
		private System.Windows.Forms.CheckBox enableBackGround;
		private System.Windows.Forms.CheckBox enableAnimation;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.TextBox backGroundFile;
		private System.Windows.Forms.Button selectBackGround;
		private System.Windows.Forms.NumericUpDown animationFrame;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.NumericUpDown rangeEnd;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.NumericUpDown rangeBegin;
	}
}

