namespace WorkWinder
{
    partial class ReportWindow
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportWindow));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Report = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.reportDiffFileButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelTotal = new System.Windows.Forms.Label();
            this.labelStoppedTotal = new System.Windows.Forms.Label();
            this.labelDutyCycle = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButtonWorkWinderData = new System.Windows.Forms.RadioButton();
            this.radioButtonManicTimeAll = new System.Windows.Forms.RadioButton();
            this.radioButtonManicTimeGrouped = new System.Windows.Forms.RadioButton();
            this.radioButtonManicTimeCategory = new System.Windows.Forms.RadioButton();
            this.comboBoxManicTimeCategory = new System.Windows.Forms.ComboBox();
            this.displayGrid = new System.Windows.Forms.DataGridView();
            this.Configuration = new System.Windows.Forms.TabPage();
            this.panelConfiguration = new System.Windows.Forms.Panel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.linkLabelWorkWinderHelp = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabelFeedback = new System.Windows.Forms.LinkLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox_RunOnStartup = new System.Windows.Forms.CheckBox();
            this.buttonSaveColumnConfig = new System.Windows.Forms.Button();
            this.checkFlashStop = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonEventSaving_Custom = new System.Windows.Forms.RadioButton();
            this.button_EventSaving_DirectoryDialog = new System.Windows.Forms.Button();
            this.textBox_EventSavingPath = new System.Windows.Forms.TextBox();
            this.radioButtonEventSaving_Default = new System.Windows.Forms.RadioButton();
            this.radioButtonEventSaving_Disabled = new System.Windows.Forms.RadioButton();
            this.groupBoxManicTimeSettings = new System.Windows.Forms.GroupBox();
            this.linkLabel_DatabaseLocation = new System.Windows.Forms.LinkLabel();
            this.button_ManicTime_DirectoryDialog = new System.Windows.Forms.Button();
            this.textBox_ManicTimeDatabasePath = new System.Windows.Forms.TextBox();
            this.radioButtonManicTime_Enabled = new System.Windows.Forms.RadioButton();
            this.radioButtonManicTime_Disabled = new System.Windows.Forms.RadioButton();
            this.tabControl1.SuspendLayout();
            this.Report.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.flowLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.displayGrid)).BeginInit();
            this.Configuration.SuspendLayout();
            this.panelConfiguration.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxManicTimeSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Report);
            this.tabControl1.Controls.Add(this.Configuration);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1114, 546);
            this.tabControl1.TabIndex = 0;
            // 
            // Report
            // 
            this.Report.Controls.Add(this.tableLayoutPanel2);
            this.Report.Location = new System.Drawing.Point(4, 27);
            this.Report.Name = "Report";
            this.Report.Padding = new System.Windows.Forms.Padding(3);
            this.Report.Size = new System.Drawing.Size(1106, 515);
            this.Report.TabIndex = 0;
            this.Report.Text = "Report";
            this.Report.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.reportDiffFileButton, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.chart1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.displayGrid, 0, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1100, 509);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // reportDiffFileButton
            // 
            this.reportDiffFileButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportDiffFileButton.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reportDiffFileButton.Location = new System.Drawing.Point(3, 479);
            this.reportDiffFileButton.Name = "reportDiffFileButton";
            this.reportDiffFileButton.Size = new System.Drawing.Size(1094, 27);
            this.reportDiffFileButton.TabIndex = 19;
            this.reportDiffFileButton.Text = "Run Report For Different File";
            this.reportDiffFileButton.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.labelTotal);
            this.flowLayoutPanel1.Controls.Add(this.labelStoppedTotal);
            this.flowLayoutPanel1.Controls.Add(this.labelDutyCycle);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1094, 34);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // labelTotal
            // 
            this.labelTotal.AutoSize = true;
            this.labelTotal.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotal.ForeColor = System.Drawing.Color.MidnightBlue;
            this.labelTotal.Location = new System.Drawing.Point(3, 0);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.labelTotal.Size = new System.Drawing.Size(120, 25);
            this.labelTotal.TabIndex = 2;
            this.labelTotal.Text = "Total 00:00:00";
            // 
            // labelStoppedTotal
            // 
            this.labelStoppedTotal.AutoSize = true;
            this.labelStoppedTotal.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStoppedTotal.ForeColor = System.Drawing.Color.MidnightBlue;
            this.labelStoppedTotal.Location = new System.Drawing.Point(129, 0);
            this.labelStoppedTotal.Name = "labelStoppedTotal";
            this.labelStoppedTotal.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.labelStoppedTotal.Size = new System.Drawing.Size(184, 25);
            this.labelStoppedTotal.TabIndex = 3;
            this.labelStoppedTotal.Text = "Stopped Total 00:00:00";
            // 
            // labelDutyCycle
            // 
            this.labelDutyCycle.AutoSize = true;
            this.labelDutyCycle.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDutyCycle.ForeColor = System.Drawing.Color.MidnightBlue;
            this.labelDutyCycle.Location = new System.Drawing.Point(319, 0);
            this.labelDutyCycle.Name = "labelDutyCycle";
            this.labelDutyCycle.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.labelDutyCycle.Size = new System.Drawing.Size(24, 25);
            this.labelDutyCycle.TabIndex = 4;
            this.labelDutyCycle.Text = "5%";
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(3, 43);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(1094, 244);
            this.chart1.TabIndex = 10;
            this.chart1.Text = "chart1";
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.radioButtonWorkWinderData);
            this.flowLayoutPanel3.Controls.Add(this.radioButtonManicTimeAll);
            this.flowLayoutPanel3.Controls.Add(this.radioButtonManicTimeGrouped);
            this.flowLayoutPanel3.Controls.Add(this.radioButtonManicTimeCategory);
            this.flowLayoutPanel3.Controls.Add(this.comboBoxManicTimeCategory);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 293);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(1094, 30);
            this.flowLayoutPanel3.TabIndex = 16;
            // 
            // radioButtonWorkWinderData
            // 
            this.radioButtonWorkWinderData.AutoSize = true;
            this.radioButtonWorkWinderData.Location = new System.Drawing.Point(3, 3);
            this.radioButtonWorkWinderData.Name = "radioButtonWorkWinderData";
            this.radioButtonWorkWinderData.Size = new System.Drawing.Size(138, 22);
            this.radioButtonWorkWinderData.TabIndex = 0;
            this.radioButtonWorkWinderData.TabStop = true;
            this.radioButtonWorkWinderData.Text = "Work Winder Data";
            this.radioButtonWorkWinderData.UseVisualStyleBackColor = true;
            // 
            // radioButtonManicTimeAll
            // 
            this.radioButtonManicTimeAll.AutoSize = true;
            this.radioButtonManicTimeAll.Location = new System.Drawing.Point(147, 3);
            this.radioButtonManicTimeAll.Name = "radioButtonManicTimeAll";
            this.radioButtonManicTimeAll.Size = new System.Drawing.Size(156, 22);
            this.radioButtonManicTimeAll.TabIndex = 1;
            this.radioButtonManicTimeAll.TabStop = true;
            this.radioButtonManicTimeAll.Text = "ManicTime Event List";
            this.radioButtonManicTimeAll.UseVisualStyleBackColor = true;
            // 
            // radioButtonManicTimeGrouped
            // 
            this.radioButtonManicTimeGrouped.AutoSize = true;
            this.radioButtonManicTimeGrouped.Location = new System.Drawing.Point(309, 3);
            this.radioButtonManicTimeGrouped.Name = "radioButtonManicTimeGrouped";
            this.radioButtonManicTimeGrouped.Size = new System.Drawing.Size(221, 22);
            this.radioButtonManicTimeGrouped.TabIndex = 2;
            this.radioButtonManicTimeGrouped.TabStop = true;
            this.radioButtonManicTimeGrouped.Text = "ManicTime Event List - Grouped";
            this.radioButtonManicTimeGrouped.UseVisualStyleBackColor = true;
            // 
            // radioButtonManicTimeCategory
            // 
            this.radioButtonManicTimeCategory.AutoSize = true;
            this.radioButtonManicTimeCategory.Location = new System.Drawing.Point(536, 3);
            this.radioButtonManicTimeCategory.Name = "radioButtonManicTimeCategory";
            this.radioButtonManicTimeCategory.Size = new System.Drawing.Size(222, 22);
            this.radioButtonManicTimeCategory.TabIndex = 4;
            this.radioButtonManicTimeCategory.TabStop = true;
            this.radioButtonManicTimeCategory.Text = "ManicTime Event List - Category";
            this.radioButtonManicTimeCategory.UseVisualStyleBackColor = true;
            // 
            // comboBoxManicTimeCategory
            // 
            this.comboBoxManicTimeCategory.FormattingEnabled = true;
            this.comboBoxManicTimeCategory.Location = new System.Drawing.Point(764, 3);
            this.comboBoxManicTimeCategory.Name = "comboBoxManicTimeCategory";
            this.comboBoxManicTimeCategory.Size = new System.Drawing.Size(248, 26);
            this.comboBoxManicTimeCategory.TabIndex = 3;
            // 
            // displayGrid
            // 
            this.displayGrid.AllowUserToAddRows = false;
            this.displayGrid.AllowUserToDeleteRows = false;
            this.displayGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.displayGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayGrid.Location = new System.Drawing.Point(3, 329);
            this.displayGrid.Name = "displayGrid";
            this.displayGrid.ReadOnly = true;
            this.displayGrid.Size = new System.Drawing.Size(1094, 144);
            this.displayGrid.TabIndex = 17;
            // 
            // Configuration
            // 
            this.Configuration.Controls.Add(this.panelConfiguration);
            this.Configuration.Location = new System.Drawing.Point(4, 27);
            this.Configuration.Name = "Configuration";
            this.Configuration.Padding = new System.Windows.Forms.Padding(3);
            this.Configuration.Size = new System.Drawing.Size(1106, 515);
            this.Configuration.TabIndex = 1;
            this.Configuration.Text = "Configuration";
            this.Configuration.UseVisualStyleBackColor = true;
            // 
            // panelConfiguration
            // 
            this.panelConfiguration.Controls.Add(this.flowLayoutPanel2);
            this.panelConfiguration.Controls.Add(this.groupBox2);
            this.panelConfiguration.Controls.Add(this.groupBox1);
            this.panelConfiguration.Controls.Add(this.groupBoxManicTimeSettings);
            this.panelConfiguration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelConfiguration.Location = new System.Drawing.Point(3, 3);
            this.panelConfiguration.Name = "panelConfiguration";
            this.panelConfiguration.Size = new System.Drawing.Size(1100, 509);
            this.panelConfiguration.TabIndex = 0;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.linkLabelWorkWinderHelp);
            this.flowLayoutPanel2.Controls.Add(this.label2);
            this.flowLayoutPanel2.Controls.Add(this.linkLabelFeedback);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(8, 482);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(399, 22);
            this.flowLayoutPanel2.TabIndex = 10;
            // 
            // linkLabelWorkWinderHelp
            // 
            this.linkLabelWorkWinderHelp.AutoSize = true;
            this.linkLabelWorkWinderHelp.Location = new System.Drawing.Point(3, 0);
            this.linkLabelWorkWinderHelp.Name = "linkLabelWorkWinderHelp";
            this.linkLabelWorkWinderHelp.Size = new System.Drawing.Size(121, 18);
            this.linkLabelWorkWinderHelp.TabIndex = 0;
            this.linkLabelWorkWinderHelp.TabStop = true;
            this.linkLabelWorkWinderHelp.Text = "Work Winder Help";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(130, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "-";
            // 
            // linkLabelFeedback
            // 
            this.linkLabelFeedback.AutoSize = true;
            this.linkLabelFeedback.Location = new System.Drawing.Point(149, 0);
            this.linkLabelFeedback.Name = "linkLabelFeedback";
            this.linkLabelFeedback.Size = new System.Drawing.Size(118, 18);
            this.linkLabelFeedback.TabIndex = 1;
            this.linkLabelFeedback.TabStop = true;
            this.linkLabelFeedback.Text = "Provide Feedback";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox_RunOnStartup);
            this.groupBox2.Controls.Add(this.buttonSaveColumnConfig);
            this.groupBox2.Controls.Add(this.checkFlashStop);
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(8, 5);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(8, 5, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(399, 121);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Application Settings";
            // 
            // checkBox_RunOnStartup
            // 
            this.checkBox_RunOnStartup.AutoSize = true;
            this.checkBox_RunOnStartup.Location = new System.Drawing.Point(20, 54);
            this.checkBox_RunOnStartup.Name = "checkBox_RunOnStartup";
            this.checkBox_RunOnStartup.Size = new System.Drawing.Size(120, 22);
            this.checkBox_RunOnStartup.TabIndex = 2;
            this.checkBox_RunOnStartup.Text = "Run On Startup";
            this.checkBox_RunOnStartup.UseVisualStyleBackColor = true;
            // 
            // buttonSaveColumnConfig
            // 
            this.buttonSaveColumnConfig.Location = new System.Drawing.Point(20, 82);
            this.buttonSaveColumnConfig.Name = "buttonSaveColumnConfig";
            this.buttonSaveColumnConfig.Size = new System.Drawing.Size(357, 28);
            this.buttonSaveColumnConfig.TabIndex = 1;
            this.buttonSaveColumnConfig.Text = "Save Current Column Configuration as Default";
            this.buttonSaveColumnConfig.UseVisualStyleBackColor = true;
            // 
            // checkFlashStop
            // 
            this.checkFlashStop.AutoSize = true;
            this.checkFlashStop.Location = new System.Drawing.Point(20, 26);
            this.checkFlashStop.Name = "checkFlashStop";
            this.checkFlashStop.Size = new System.Drawing.Size(257, 22);
            this.checkFlashStop.TabIndex = 0;
            this.checkFlashStop.Text = "Flash Display Window When Stopped";
            this.checkFlashStop.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonEventSaving_Custom);
            this.groupBox1.Controls.Add(this.button_EventSaving_DirectoryDialog);
            this.groupBox1.Controls.Add(this.textBox_EventSavingPath);
            this.groupBox1.Controls.Add(this.radioButtonEventSaving_Default);
            this.groupBox1.Controls.Add(this.radioButtonEventSaving_Disabled);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(8, 260);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(8, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(399, 151);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Event Saving";
            // 
            // radioButtonEventSaving_Custom
            // 
            this.radioButtonEventSaving_Custom.AutoSize = true;
            this.radioButtonEventSaving_Custom.Location = new System.Drawing.Point(20, 81);
            this.radioButtonEventSaving_Custom.Name = "radioButtonEventSaving_Custom";
            this.radioButtonEventSaving_Custom.Size = new System.Drawing.Size(313, 22);
            this.radioButtonEventSaving_Custom.TabIndex = 5;
            this.radioButtonEventSaving_Custom.TabStop = true;
            this.radioButtonEventSaving_Custom.Text = "Custom Directory (requires application restart)";
            this.radioButtonEventSaving_Custom.UseVisualStyleBackColor = true;
            // 
            // button_EventSaving_DirectoryDialog
            // 
            this.button_EventSaving_DirectoryDialog.Enabled = false;
            this.button_EventSaving_DirectoryDialog.Location = new System.Drawing.Point(345, 109);
            this.button_EventSaving_DirectoryDialog.Name = "button_EventSaving_DirectoryDialog";
            this.button_EventSaving_DirectoryDialog.Size = new System.Drawing.Size(32, 26);
            this.button_EventSaving_DirectoryDialog.TabIndex = 4;
            this.button_EventSaving_DirectoryDialog.Text = "...";
            this.button_EventSaving_DirectoryDialog.UseVisualStyleBackColor = true;
            // 
            // textBox_EventSavingPath
            // 
            this.textBox_EventSavingPath.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_EventSavingPath.Location = new System.Drawing.Point(42, 109);
            this.textBox_EventSavingPath.Name = "textBox_EventSavingPath";
            this.textBox_EventSavingPath.ReadOnly = true;
            this.textBox_EventSavingPath.Size = new System.Drawing.Size(297, 26);
            this.textBox_EventSavingPath.TabIndex = 3;
            // 
            // radioButtonEventSaving_Default
            // 
            this.radioButtonEventSaving_Default.AutoSize = true;
            this.radioButtonEventSaving_Default.Location = new System.Drawing.Point(20, 53);
            this.radioButtonEventSaving_Default.Name = "radioButtonEventSaving_Default";
            this.radioButtonEventSaving_Default.Size = new System.Drawing.Size(212, 22);
            this.radioButtonEventSaving_Default.TabIndex = 2;
            this.radioButtonEventSaving_Default.TabStop = true;
            this.radioButtonEventSaving_Default.Text = "Save Files to Default Directory";
            this.radioButtonEventSaving_Default.UseVisualStyleBackColor = true;
            // 
            // radioButtonEventSaving_Disabled
            // 
            this.radioButtonEventSaving_Disabled.AutoSize = true;
            this.radioButtonEventSaving_Disabled.Location = new System.Drawing.Point(20, 25);
            this.radioButtonEventSaving_Disabled.Name = "radioButtonEventSaving_Disabled";
            this.radioButtonEventSaving_Disabled.Size = new System.Drawing.Size(80, 22);
            this.radioButtonEventSaving_Disabled.TabIndex = 1;
            this.radioButtonEventSaving_Disabled.TabStop = true;
            this.radioButtonEventSaving_Disabled.Text = "Disabled";
            this.radioButtonEventSaving_Disabled.UseVisualStyleBackColor = true;
            // 
            // groupBoxManicTimeSettings
            // 
            this.groupBoxManicTimeSettings.Controls.Add(this.linkLabel_DatabaseLocation);
            this.groupBoxManicTimeSettings.Controls.Add(this.button_ManicTime_DirectoryDialog);
            this.groupBoxManicTimeSettings.Controls.Add(this.textBox_ManicTimeDatabasePath);
            this.groupBoxManicTimeSettings.Controls.Add(this.radioButtonManicTime_Enabled);
            this.groupBoxManicTimeSettings.Controls.Add(this.radioButtonManicTime_Disabled);
            this.groupBoxManicTimeSettings.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxManicTimeSettings.Location = new System.Drawing.Point(8, 130);
            this.groupBoxManicTimeSettings.Margin = new System.Windows.Forms.Padding(8, 2, 2, 2);
            this.groupBoxManicTimeSettings.Name = "groupBoxManicTimeSettings";
            this.groupBoxManicTimeSettings.Size = new System.Drawing.Size(399, 126);
            this.groupBoxManicTimeSettings.TabIndex = 7;
            this.groupBoxManicTimeSettings.TabStop = false;
            this.groupBoxManicTimeSettings.Text = "ManicTime Integration";
            // 
            // linkLabel_DatabaseLocation
            // 
            this.linkLabel_DatabaseLocation.AutoSize = true;
            this.linkLabel_DatabaseLocation.Location = new System.Drawing.Point(271, 55);
            this.linkLabel_DatabaseLocation.Name = "linkLabel_DatabaseLocation";
            this.linkLabel_DatabaseLocation.Size = new System.Drawing.Size(46, 18);
            this.linkLabel_DatabaseLocation.TabIndex = 5;
            this.linkLabel_DatabaseLocation.TabStop = true;
            this.linkLabel_DatabaseLocation.Text = "(help)";
            // 
            // button_ManicTime_DirectoryDialog
            // 
            this.button_ManicTime_DirectoryDialog.Enabled = false;
            this.button_ManicTime_DirectoryDialog.Location = new System.Drawing.Point(345, 81);
            this.button_ManicTime_DirectoryDialog.Name = "button_ManicTime_DirectoryDialog";
            this.button_ManicTime_DirectoryDialog.Size = new System.Drawing.Size(32, 26);
            this.button_ManicTime_DirectoryDialog.TabIndex = 4;
            this.button_ManicTime_DirectoryDialog.Text = "...";
            this.button_ManicTime_DirectoryDialog.UseVisualStyleBackColor = true;
            // 
            // textBox_ManicTimeDatabasePath
            // 
            this.textBox_ManicTimeDatabasePath.Location = new System.Drawing.Point(42, 81);
            this.textBox_ManicTimeDatabasePath.Name = "textBox_ManicTimeDatabasePath";
            this.textBox_ManicTimeDatabasePath.ReadOnly = true;
            this.textBox_ManicTimeDatabasePath.Size = new System.Drawing.Size(297, 26);
            this.textBox_ManicTimeDatabasePath.TabIndex = 3;
            // 
            // radioButtonManicTime_Enabled
            // 
            this.radioButtonManicTime_Enabled.AutoSize = true;
            this.radioButtonManicTime_Enabled.Location = new System.Drawing.Point(20, 53);
            this.radioButtonManicTime_Enabled.Name = "radioButtonManicTime_Enabled";
            this.radioButtonManicTime_Enabled.Size = new System.Drawing.Size(256, 22);
            this.radioButtonManicTime_Enabled.TabIndex = 2;
            this.radioButtonManicTime_Enabled.TabStop = true;
            this.radioButtonManicTime_Enabled.Text = "Specify ManicTime Database Location";
            this.radioButtonManicTime_Enabled.UseVisualStyleBackColor = true;
            // 
            // radioButtonManicTime_Disabled
            // 
            this.radioButtonManicTime_Disabled.AutoSize = true;
            this.radioButtonManicTime_Disabled.Location = new System.Drawing.Point(20, 25);
            this.radioButtonManicTime_Disabled.Name = "radioButtonManicTime_Disabled";
            this.radioButtonManicTime_Disabled.Size = new System.Drawing.Size(80, 22);
            this.radioButtonManicTime_Disabled.TabIndex = 1;
            this.radioButtonManicTime_Disabled.TabStop = true;
            this.radioButtonManicTime_Disabled.Text = "Disabled";
            this.radioButtonManicTime_Disabled.UseVisualStyleBackColor = true;
            // 
            // ReportWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1114, 546);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1130, 585);
            this.Name = "ReportWindow";
            this.Text = "WorkWInder v0.6.1 - Report";
            this.tabControl1.ResumeLayout(false);
            this.Report.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.displayGrid)).EndInit();
            this.Configuration.ResumeLayout(false);
            this.panelConfiguration.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxManicTimeSettings.ResumeLayout(false);
            this.groupBoxManicTimeSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Report;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.Label labelStoppedTotal;
        private System.Windows.Forms.Label labelDutyCycle;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TabPage Configuration;
        private System.Windows.Forms.Panel panelConfiguration;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.LinkLabel linkLabelWorkWinderHelp;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox_RunOnStartup;
        private System.Windows.Forms.Button buttonSaveColumnConfig;
        private System.Windows.Forms.CheckBox checkFlashStop;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonEventSaving_Custom;
        private System.Windows.Forms.Button button_EventSaving_DirectoryDialog;
        private System.Windows.Forms.TextBox textBox_EventSavingPath;
        private System.Windows.Forms.RadioButton radioButtonEventSaving_Default;
        private System.Windows.Forms.RadioButton radioButtonEventSaving_Disabled;
        private System.Windows.Forms.GroupBox groupBoxManicTimeSettings;
        private System.Windows.Forms.LinkLabel linkLabel_DatabaseLocation;
        private System.Windows.Forms.Button button_ManicTime_DirectoryDialog;
        private System.Windows.Forms.TextBox textBox_ManicTimeDatabasePath;
        private System.Windows.Forms.RadioButton radioButtonManicTime_Enabled;
        private System.Windows.Forms.RadioButton radioButtonManicTime_Disabled;
        private System.Windows.Forms.LinkLabel linkLabelFeedback;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button reportDiffFileButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.DataGridView displayGrid;
        private System.Windows.Forms.RadioButton radioButtonWorkWinderData;
        private System.Windows.Forms.RadioButton radioButtonManicTimeAll;
        private System.Windows.Forms.RadioButton radioButtonManicTimeGrouped;
        private System.Windows.Forms.RadioButton radioButtonManicTimeCategory;
        private System.Windows.Forms.ComboBox comboBoxManicTimeCategory;
    }
}