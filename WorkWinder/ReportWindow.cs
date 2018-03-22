using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;
using IWshRuntimeLibrary;
using System.IO;

namespace WorkWinder
{
    public partial class ReportWindow : Form
    {
        WorkWinderDisplay DISPLAY;
        FileMechanism FILEMECH;
        TimerEngine TIMEENGINE;
        string[] seriesArray = { "< 0.125", "0.25", "0.5", "0.75", "1.0", "1.25", "1.5", "1.75", "< 2.0" }; //group types
        double[] seriesARRAYNUM = new double[StaticUtility.maximumRows];
        Color hldClr = Color.Black;
        HitTestResult hldPt = null;
        int hldSrs = -1;

        public ReportWindow(TimerEngine t, WorkWinderDisplay d, FileMechanism f)
        {
            InitializeComponent();
            DISPLAY = d;
            FILEMECH = f;
            TIMEENGINE = t;
            initializeWindow();
        }

        /// <summary>
        /// 
        /// </summary>
        private void initializeWindow()
        {
            Text = StaticUtility.returnTitleName() + " - Report";

            initializeControlSettings();
            refreshConfigFromFile(); //Want to update before we put all the event handlers in.            

            //Report Tab Event Handlers
            Shown += new EventHandler(config_Shown);
            chart1.MouseDown += new MouseEventHandler(mouseDown_Click);

            reportDiffFileButton.Click += new EventHandler(reportDiffFileButton_Click);
            radioButtonManicTimeGrouped.CheckedChanged += new EventHandler(radioButtons_ReportPage);
            radioButtonManicTimeAll.CheckedChanged += new EventHandler(radioButtons_ReportPage);
            radioButtonManicTimeCategory.CheckedChanged += new EventHandler(radioButtons_ReportPage);
            radioButtonWorkWinderData.CheckedChanged += new EventHandler(radioButtons_ReportPage);            

            //Application Settings EventHandlers
            checkFlashStop.CheckedChanged += new EventHandler(checkFlashStop_CheckedChanged);
            checkBox_RunOnStartup.CheckedChanged += new EventHandler(runOnStartUp_CheckedChanged);
            buttonSaveColumnConfig.Click += new EventHandler(saveColumnConfigButton_Click);
            //themeComboBox.SelectedIndexChanged += new EventHandler(themeComboBox_IndexChanged);

            //ManicTime Integration EventHandlers
            radioButtonManicTime_Disabled.CheckedChanged += new EventHandler(radioButtons_ManicTime_Changed);
            radioButtonManicTime_Enabled.CheckedChanged += new EventHandler(radioButtons_ManicTime_Changed);
            button_ManicTime_DirectoryDialog.Click += new EventHandler(button_ManicTime_DirectoryDialog_Click);
            linkLabel_DatabaseLocation.LinkClicked += new LinkLabelLinkClickedEventHandler(clicked_ManicTimeDBHelp);

            //Event Saving EventHandlers
            radioButtonEventSaving_Disabled.CheckedChanged += new EventHandler(radioButtons_SaveEvents_Changed);
            radioButtonEventSaving_Default.CheckedChanged += new EventHandler(radioButtons_SaveEvents_Changed);
            radioButtonEventSaving_Custom.CheckedChanged += new EventHandler(radioButtons_SaveEvents_Changed);
            button_EventSaving_DirectoryDialog.Click += new EventHandler(button_EventSaving_DirectoryDialog_Click);

            //General Help EventHandler
            linkLabelWorkWinderHelp.LinkClicked += new LinkLabelLinkClickedEventHandler(clicked_WorkWinderHelp);
            linkLabelFeedback.LinkClicked += new LinkLabelLinkClickedEventHandler(clicked_linkFeedback);
        }

        private void initializeControlSettings()
        {
            displayGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            radioButtonWorkWinderData.Checked = true;
            //themeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            //themeComboBox.Items.Add("Default");
            //themeComboBox.Items.Add("Light");
            //themeComboBox.Items.Add("Dark");
        }

        /// <summary>
        /// 
        /// </summary>
        private void refreshConfigFromFile()
        {
            //Report Page Settings
            if (Properties.Settings.Default.ManicTimeEnabled) radioButtonManicTimeGrouped.Enabled = radioButtonManicTimeAll.Enabled = true;
            else radioButtonManicTimeGrouped.Enabled = radioButtonManicTimeAll.Enabled = false;

            //Application Settings
            checkFlashStop.Checked = Properties.Settings.Default.FlashOnStop;
            checkBox_RunOnStartup.Checked = Properties.Settings.Default.RunOnStartup;

            //if (themeComboBox.Items.Contains(Properties.Settings.Default.Theme)) themeComboBox.SelectedIndex = themeComboBox.FindStringExact(Properties.Settings.Default.Theme);
            //else themeComboBox.SelectedIndex = themeComboBox.FindStringExact("Default");

            //Manic Time Settings
            radioButtonManicTime_Enabled.Checked = Properties.Settings.Default.ManicTimeEnabled;
            radioButtonManicTime_Disabled.Checked = !Properties.Settings.Default.ManicTimeEnabled; //Need to do both otherwise potentially nothing gets set.
            button_ManicTime_DirectoryDialog.Enabled = Properties.Settings.Default.ManicTimeEnabled;
            textBox_ManicTimeDatabasePath.Text = Properties.Settings.Default.ManicTimePath;

            //Event Saving Settings
            if (Properties.Settings.Default.SaveActions)
            {
                if (Properties.Settings.Default.SaveFolderOverride != "")//Save, use custom path
                {
                    radioButtonEventSaving_Custom.Checked = true;
                    button_EventSaving_DirectoryDialog.Enabled = true;
                    textBox_EventSavingPath.Text = Properties.Settings.Default.SaveFolderOverride;
                }
                else //Save, use default
                {
                    radioButtonEventSaving_Default.Checked = true;
                    button_EventSaving_DirectoryDialog.Enabled = false;
                    textBox_EventSavingPath.Text = "";
                }
            }
            else //Don't save
            {
                radioButtonEventSaving_Disabled.Checked = true;
                button_EventSaving_DirectoryDialog.Enabled = false;
                textBox_EventSavingPath.Text = "";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtons_ReportPage(object sender, EventArgs e)
        {
            if (radioButtonWorkWinderData.Checked) //Went from something to just the workwinder event list
            {
                //removes the entry on the chart if there is one
                resetPreviousPoint();
                //just reruns the report
                runReport(FILEMECH.getDirString(true));
            }
            else
            {
                runManicTimeReports(hldPt);
            }

        }

        /// <summary>
        /// Tells the parent window that the config check box changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkFlashStop_CheckedChanged(object sender, EventArgs e)
        {
            DISPLAY.flashOnStop(checkFlashStop.Checked);
            Properties.Settings.Default.FlashOnStop = checkFlashStop.Checked;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void runOnStartUp_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.RunOnStartup = checkBox_RunOnStartup.Checked;
            Properties.Settings.Default.Save();

            if(checkBox_RunOnStartup.Checked)
            {
                createStartupShortCut();
            }
            else
            {
                deleteStartupShortCut();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveColumnConfigButton_Click(object sender, EventArgs e)
        {
            FILEMECH.saveConfigColumns();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void themeComboBox_IndexChanged(object sender, EventArgs e)
        //{
        //    Properties.Settings.Default.Theme = themeComboBox.SelectedItem.ToString();
        //    Properties.Settings.Default.Save();
        //}

        /// <summary>
        /// Saves config change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtons_ManicTime_Changed(object sender, EventArgs e)
        {
            if (radioButtonManicTime_Enabled.Checked == true)
            {
                Properties.Settings.Default.ManicTimeEnabled = radioButtonManicTime_Enabled.Checked;
                button_ManicTime_DirectoryDialog.Enabled = true;
                radioButtonManicTimeGrouped.Enabled = radioButtonManicTimeAll.Enabled = true;
            }
            else
            {
                Properties.Settings.Default.ManicTimeEnabled = radioButtonManicTime_Enabled.Checked;
                button_ManicTime_DirectoryDialog.Enabled = false;
                radioButtonWorkWinderData.Checked = true;
                radioButtonManicTimeGrouped.Enabled = radioButtonManicTimeAll.Enabled = false;
            }
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clicked_ManicTimeDBHelp(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo(Properties.Settings.Default.HelpLinkManicTimeDBPath);
            Process.Start(sInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ManicTime_DirectoryDialog_Click(object sender, EventArgs e)
        {
            getManicTimeDbPath();
        }

        /// <summary>
        /// Saves config change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtons_SaveEvents_Changed(object sender, EventArgs e)
        {
            if (radioButtonEventSaving_Default.Checked || radioButtonEventSaving_Custom.Checked) //Yes Save
            {
                Properties.Settings.Default.SaveActions = true;
                if (radioButtonEventSaving_Custom.Checked)                                          //Custom file path
                {
                    button_EventSaving_DirectoryDialog.Enabled = true;
                }
                else                                                                                //Save at default path
                {
                    button_EventSaving_DirectoryDialog.Enabled = false;
                    textBox_EventSavingPath.Text = "";
                    Properties.Settings.Default.SaveFolderOverride = "";
                }
            }
            else                                                                                //File saving disabled
            {
                Properties.Settings.Default.SaveActions = false;
                button_EventSaving_DirectoryDialog.Enabled = false;
                textBox_EventSavingPath.Text = "";
                Properties.Settings.Default.SaveFolderOverride = "";
            }
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_EventSaving_DirectoryDialog_Click(object sender, EventArgs e)
        {
            getEventSavePath();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clicked_WorkWinderHelp(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo(Properties.Settings.Default.HelpLinkWorkWinder);
            Process.Start(sInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clicked_linkFeedback(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo(Properties.Settings.Default.LinkFeedback);
            Process.Start(sInfo);
        }

        private void getEventSavePath()
        {
            // Create an instance of the open file dialog box.
            FolderBrowserDialog openFolderDialog1 = new FolderBrowserDialog();

            //openFolderDialog1.RootFolder = (Properties.Settings.Default.SaveFolderOverride == "" ? (Application.StartupPath + @"\TimeFiles") : Properties.Settings.Default.SaveFolderOverride); ;

            // Call the ShowDialog method to show the dialog box.
            if (openFolderDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.SaveFolderOverride = openFolderDialog1.SelectedPath;
                textBox_EventSavingPath.Text = openFolderDialog1.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }

        private void createStartupShortCut()
        {
            string shortcutName = StaticUtility.returnTitleName();
            string shortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string shortcutLocation = Path.Combine(shortcutPath, shortcutName + ".lnk");
            deleteStartupShortCut();
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.Description = StaticUtility.returnTitleName() + " startup shortcut. Instead of deleting this it's recommended to uncheck the box in the application.";   // The description of the shortcut
            //shortcut.IconLocation = @"c:\myicon.ico";         // The icon of the shortcut
            shortcut.TargetPath = Application.ExecutablePath;   // The path of the file that will launch when the shortcut is run
            try { shortcut.Save(); }                            // Save the shortcut
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void deleteStartupShortCut()
        {
            string shortcutName = StaticUtility.returnTitleName();
            string shortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string shortcutLocation = Path.Combine(shortcutPath, shortcutName + ".lnk");
            try { if (System.IO.File.Exists(shortcutLocation)) System.IO.File.Delete(shortcutLocation); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        /// <summary>
        /// 
        /// </summary>
        private void getManicTimeDbPath()
        {
            // Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Filter = "SDF Files|*.sdf";
            openFileDialog1.FilterIndex = 1;

            openFileDialog1.Multiselect = false;
            openFileDialog1.InitialDirectory = (Properties.Settings.Default.ManicTimePath == "" ? (Path.Combine(Application.LocalUserAppDataPath,@"Finkit\ManicTime")) : Properties.Settings.Default.ManicTimePath);

            // Call the ShowDialog method to show the dialog box.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (System.IO.File.Exists(openFileDialog1.FileName))
                {
                    Properties.Settings.Default.ManicTimePath = openFileDialog1.FileName;
                    textBox_ManicTimeDatabasePath.Text = openFileDialog1.FileName;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    MessageBox.Show("Selected file does not exist.");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jArray"></param>
        /// <returns></returns>
        private int getUsableHeaderColumns(string[][] jArray)
        {
            int usableHeaderColumns = 0;

            //HeaderRow
            string[] hdtmp = jArray[0].ToArray();

            for (int i = 0; i < hdtmp.Length; i++)
            {
                if (hdtmp[i] != "") usableHeaderColumns = i;
            }
            return usableHeaderColumns + 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jArray"></param>
        /// <returns></returns>
        private int getUsableTimerColumns(string[][] jArray)
        {
            int usableTimerColumns = 0;

            for (int i = 0; i < jArray.Length; i++)
            {
                string[] rw = jArray[i].ToArray();
                for (int j = 0; j < rw.Length; j++)
                {
                    if (rw[j] != "") usableTimerColumns = Math.Max(j, usableTimerColumns);
                }
            }
            return usableTimerColumns + 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jArray"></param>
        private void ConvertListToDataTable(string[][] jArray)
        {
            // New table.
            DataTable table = new DataTable();

            //HeaderRow
            string[] hdtmp = jArray[0].ToArray();

            int maxCols = Math.Max(getUsableHeaderColumns(jArray), getUsableTimerColumns(jArray));

            // Add columns to the table
            for (int i = 0; i < maxCols; i++)
            {
                table.Columns.Add();
            }

            // Add rows to the table start at 1 to avoid the header row
            for (int i = 1; i < jArray.Length; i++)
            {
                string[] rw = jArray[i].ToArray();
                string[] rwSht = new string[maxCols];
                for (int j = 0; j < rwSht.Length; j++) rwSht[j] = rw[j];

                table.Rows.Add(rwSht);
            }

            // Convert to DataTable.
            displayGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            displayGrid.MultiSelect = false;
            displayGrid.DataSource = table;
            displayGrid.FirstDisplayedScrollingRowIndex = displayGrid.RowCount - 1;

            //Assigns the legend values
            for (int i = 0; i < maxCols; i++)
            {
                displayGrid.Columns[i].HeaderText = hdtmp[i];
            }
            //displayGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            table.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inpt"></param>
        /// <returns></returns>
        private string cnvrtDateTime2Time(string inpt)
        {
            DateTime dt = Convert.ToDateTime(inpt);
            return dt.ToString("h:mm tt");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inpt"></param>
        /// <param name="tr"></param>
        /// <returns></returns>
        private int cnvrtTimeSpanString2sec(string inpt, ref bool tr)
        {
            TimeSpan interval;
            tr = TimeSpan.TryParse(inpt, out interval);
            if (!tr) return -1;
            return Convert.ToInt32(interval.TotalSeconds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seriesNum"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="hovertext"></param>
        private void addDataPNT2(int seriesNum, double x, double y, string hovertext = "")
        {
            DataPoint point = new DataPoint();
            point.SetValueXY(x, seriesARRAYNUM[Convert.ToInt32(x)], seriesARRAYNUM[Convert.ToInt32(x)] + y / 60.0);
            seriesARRAYNUM[Convert.ToInt32(x)] += y / 60.0;
            point.ToolTip = hovertext;
            chart1.Series[seriesNum].Points.Add(point);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arry"></param>
        private void convert2PT(string[][] arry)
        {
            bool tr = true;
            //Loop through the file rows
            for (int Y = 1; Y < arry.Length; Y++)
            {
                tr = true;
                int series;
                string[] line = arry[Y].ToArray();

                int duration = cnvrtTimeSpanString2sec(line[2], ref tr);
                if (!tr || duration == 0) continue;

                if (duration >= 7650) series = 8;
                else series = (duration + 450) / 900;

                int activeCol = Int32.Parse(line[4]);
                if (activeCol > -1)
                {
                    //addDataPNT2(series, activeCol, duration / 60.0, cnvrtDateTime2Time(line[0]) + " " + (duration / 60.0 / 60.0).ToString("N2") + " hrs");
                    addDataPNT2(series, activeCol, duration / 60.0, line[0] + ";" + line[1] + ";" + (duration / 60.0 / 60.0).ToString("N2") + " hrs;");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arry"></param>
        private void runChart(string[][] arry)
        {
            int max = Math.Max(getUsableHeaderColumns(arry), getUsableTimerColumns(arry)) - 8;
            string[] last = arry[arry.Length - 1].ToArray(); // last row of the array

            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisX.CustomLabels.Clear();

            // Set palette and other chart settings
            chart1.Palette = ChartColorPalette.Pastel;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Transparent;
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.IntervalOffset = -0.5;
            chart1.ChartAreas[0].AxisX.Maximum = max - 1 + 0.5;//xaxis is 0 index
            chart1.ChartAreas[0].AxisX.Minimum = -0.5;

            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gray;
            chart1.ChartAreas[0].AxisY.Interval = 0.25;

            //Initializes the array that will hold the top y value of each bar. //Wut?
            for (int i = 0; i < max; i++) seriesARRAYNUM[i] = 0;

            //add series and add black border on everything
            for (int i = 0; i < seriesArray.Length; i++)
            {
                chart1.Series.Add(seriesArray[i]);
                chart1.Series[i].BorderColor = Color.Black;
                chart1.Series[i]["DrawSideBySide"] = "false";
            }

            //Defines chart type
            foreach (Series s in chart1.Series)
            {
                s.ChartType = SeriesChartType.RangeColumn;
            }

            //Get the axis labels and append the totals.
            string[] axslblsAndTotals = new string[max];
            for (int i = 0; i < max; i++)
            {
                axslblsAndTotals[i] = arry[0][i + 8] + "\nTotal: " + last[i + 8];
            }

            //Used to update the axis labels
            for (int i = 0; i < max; i++)
            {
                //chart1.ChartAreas[0].AxisX.CustomLabels.Add(i - 0.5, i + 0.5, arry[0][i + 8]);
                chart1.ChartAreas[0].AxisX.CustomLabels.Add(i - 0.5, i + 0.5, axslblsAndTotals[i]);
                chart1.Series[0].Points.AddXY(i, 0);
                //chart1.Series[0].Points[i].AxisLabel = arry[0][i + 8];
                chart1.Series[0].Points[i].AxisLabel = axslblsAndTotals[i];
            }

            //Puts the data in the chart
            convert2PT(arry);

            bool hld = true;
            int stp = cnvrtTimeSpanString2sec(last[7], ref hld);
            int tot = cnvrtTimeSpanString2sec(last[6], ref hld);
            labelStoppedTotal.Text = "Stopped Total " + last[7].ToString();
            labelTotal.Text = "Action Total " + last[6].ToString();
            labelDutyCycle.Text = Math.Round((Convert.ToDouble(tot) / Convert.ToDouble(stp + tot)) * 100.0, 0).ToString() + "% (Active/Total)";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        public void runReport(string filepath)
        {
            string[][] arry = FILEMECH.tloadCSVfile(filepath);
            ConvertListToDataTable(arry);
            runChart(arry);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void config_Shown(object sender, EventArgs e)
        {
            runReport(FILEMECH.getDirString(true));            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouseDown_Click(object sender, MouseEventArgs e)
        {
            // Call Hit Test Method
            HitTestResult result = chart1.HitTest(e.X, e.Y);
            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                //Returns the changed chart box back to it's original color
                resetPreviousPoint();

                //Sets the point we want to change to the buffer value
                hldSrs = returnPointSeries(result);
                if (hldSrs == -1) return;
                hldClr = chart1.Series[hldSrs].Points[result.PointIndex].Color;
                hldPt = result;
                chart1.Series[hldSrs].Points[hldPt.PointIndex].Color = Color.Red;

                //Gets the values from the tool tip
                runManicTimeReports(result);
            }
        }

        private int returnPointSeries(HitTestResult result)
        {
            string seriesName = result.Series.ToString();
            seriesName = seriesName.Replace("Series-", ""); //This is weird
            return Array.IndexOf(seriesArray, seriesName);   
        }

        /// <summary>
        /// 
        /// </summary>
        private void resetPreviousPoint()
        {
            //Returns the changed chart box back to it's original color
            if (hldPt != null)
            {
                chart1.Series[hldSrs].Points[hldPt.PointIndex].Color = hldClr;
            }
            hldPt = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pt"></param>
        private void runManicTimeReports(HitTestResult pt)
        {
            if (pt == null) return;
            int srs = returnPointSeries(pt);
            string[] fl = chart1.Series[srs].Points[pt.PointIndex].ToolTip.ToString().Split(';');
            if (fl.Length < 3) return;
            CultureInfo provider = CultureInfo.InvariantCulture;
            string str1 = fl[0];
            string str2 = fl[1];
            string str3 = fl[2];
            DateTime start = DateTime.ParseExact(str1, "M/d/yyyy h:m:s tt", null);
            DateTime end = DateTime.ParseExact(str2, "M/d/yyyy h:m:s tt", null);

            if (Properties.Settings.Default.ManicTimeEnabled)
            {
                if (radioButtonManicTimeAll.Checked)
                {
                    StaticUtility.listAll3(start, end, displayGrid, Properties.Settings.Default.ManicTimePath);
                }
                if (radioButtonManicTimeGrouped.Checked)
                {
                    StaticUtility.aggregate3(start, end, displayGrid, Properties.Settings.Default.ManicTimePath);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reportDiffFileButton_Click(object sender, EventArgs e)
        {
            // Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Filter = "CSV Files|*.csv";
            openFileDialog1.FilterIndex = 1;

            openFileDialog1.Multiselect = false;

            // Call the ShowDialog method to show the dialog box.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                runReport(openFileDialog1.FileName);
            }
        }
    }
}
