using System;
using System.Drawing;
using System.Windows.Forms;
using WinderTheme;

namespace WorkWinder
{
    public partial class WorkWinderDisplay : Form
    {
        //Instantiate the time engine? I don't understand how these work.
        TimerEngine TIMEENGINE;
        FileMechanism FILEMECH;
        ReportWindow CONFDIS;
        xColors theme;

        public WorkWinderDisplay()
        {
            InitializeComponent();
            initializeTimers();
            theme = new xColors();
            initializeColors();
            initializeTLP(ref baseTLP);
            TIMEENGINE = new TimerEngine(this, FILEMECH);
            FILEMECH = new FileMechanism(TIMEENGINE, this);
            CONFDIS = new ReportWindow(TIMEENGINE, this, FILEMECH);
        }

        //Temporary variables
        TextBox tempTextBox;
        Label tempLabel;

        //Grid that holds everything
        TableLayoutPanel baseTLP;

        // Holds the text in the textboxes 
        string[] textBoxNamesArray = new string[StaticUtility.maximumRows];

        public bool timerRowInRange(int num)
        {
            return num < returnTmrCnt();
        }

        public int returnTmrCnt()
        {
            return baseTLP.RowCount - StaticUtility.visualNonTimerRows;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timerRow2Change">Row to affect;</param>
        /// <param name="running">TRUE change it to running; FALSE change it stopped;</param>
        public void disSetRunTimer(int timerRow2Change, bool running)
        {
            if (!timerRowInRange(timerRow2Change)) return;
            StaticUtility.setRunningRow(ref baseTLP, running, timerRow2Change, theme);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stopped">TRUE set it stopped; FALSE set it out of false</param>
        public void disSetStopped(bool stopped)
        {
            StaticUtility.setStopped(ref baseTLP, stopped, theme);
            if (stopped)
            {
                stopFlashTimer.Start();
            }
            else
            {
                stopFlashTimer.Stop();
                BackColor = theme.defWindowColor;
            }
        }

        /// <summary>
        /// This should update the timer row specified with the time specified.
        /// </summary>
        /// <param name="TimerRow">0 index; Timer to update;</param>
        /// <param name="time">TimeSpan to pass in.</param>
        public void disUpd1Row(int TimerRow, TimeSpan time)
        {
            if (!timerRowInRange(TimerRow)) return;
            StaticUtility.getLabelYX(baseTLP, TimerRow + StaticUtility.visualNonTimerRows, 3).Text = time.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="total"></param>
        public void disUpdTotal(TimeSpan total)
        {
            StaticUtility.getLabelYX(baseTLP, 0, 2).Text = "Total: " + total.ToString();
        }

        /// <summary>
        /// Public Entry point to add a row.
        /// </summary>
        /// <param name="name">Optional parameter for setting the name of the row.</param>
        public void createRow(string name = "^^^|||^^^")
        {
            if (name == "^^^|||^^^")
            {
                string[] nameLi = Properties.Settings.Default.NameList.Split(new char[] { ',' });
                string nm = (baseTLP.RowCount - 1) > nameLi.Length ? "" : nameLi[returnTmrCnt()];
                addTimerRow(ref baseTLP, nm, true);
            }
            else
            {
                addTimerRow(ref baseTLP, name, true);
            }
        }

        /// <summary>
        /// Public Entry point to delete row.
        /// </summary>
        public void deleteRow()
        {
            removeRow(ref baseTLP, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TimerRow"></param>
        /// <returns>Gets the text in the textbox.</returns>
        public string returnTimerTextBox(int TimerRow)
        {
            if (!timerRowInRange(TimerRow)) return "";
            return StaticUtility.getTextBoxYX(baseTLP, TimerRow + StaticUtility.visualNonTimerRows, 2).Text.Trim();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TimerRow"></param>
        /// <param name="text"></param>
        public void setTimerTextBox(int TimerRow, string text)
        {
            if (!timerRowInRange(TimerRow)) return;
            StaticUtility.setTextBoxYX(baseTLP, TimerRow + StaticUtility.visualNonTimerRows, 2, text);
        }

        /// <summary>
        /// Returns the values of all text boxes.
        /// </summary>
        /// <returns></returns>
        public string[] returnTextBoxList()
        {
            string[] outStringText = new string[returnTmrCnt()];
            for (int i = 0; i < returnTmrCnt(); i++)
            {
                outStringText[i] += StaticUtility.getTextBoxYX(baseTLP, i + StaticUtility.visualNonTimerRows, 2).Text.Trim();
            }
            return outStringText;
        }

        /// <summary>
        /// Used when the config checkbox is checked to make sure it changes background color correctly.
        /// </summary>
        /// <param name="chckd"></param>
        public void flashOnStop(bool chckd)
        {
            if (chckd == false) BackColor = theme.defWindowColor;
        }

        // ++++++++++++++++++++++START++++++++++++++++++++++
        // Timers
        // +++++++++++++++++++++++++++++++++++++++++++++++++

        Timer stopFlashTimer = new Timer();

        private void initializeTimers()
        {
            stopFlashTimer.Tick += stopFlashTimer_Tick;
            stopFlashTimer.Interval = 600; //0.6 sec
        }

        private void stopFlashTimer_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.FlashOnStop == false) return;
            BackColor = (BackColor == theme.defWindowColor ? theme.flashWindowColor : theme.defWindowColor);
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++
        // Timers
        // +++++++++++++++++++++++END+++++++++++++++++++++++

        // ++++++++++++++++++++++START++++++++++++++++++++++
        // Layout Functions
        // +++++++++++++++++++++++++++++++++++++++++++++++++

        private void initializeColors()
        {
            theme.Theme = 0;//This should be a property.
        }

        private void addRow(TableLayoutPanel TLP)
        {
            TLP.RowCount++;
            TLP.RowStyles.Add(new RowStyle(SizeType.Absolute, StaticUtility.rowheight));
        }

        private void addTimerRow(ref TableLayoutPanel TLP, string boxText, bool relayout)
        {
            if (TLP.RowCount - StaticUtility.visualNonTimerRows >= StaticUtility.maximumRows) //don't let rows add past limit. Minus 1 is for zero index.
            {
                DialogResult result = MessageBox.Show(
                    "Maximum number of timers have been added. Modify the application config file and restart the application to change the maximum rows."
                    , ""
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Information
                    , MessageBoxDefaultButton.Button1);
                return;
            }

            TLP.SuspendLayout();
            addRow(TLP);
            int cntIx = TLP.RowCount - 1;
            StaticUtility.addLabel(ref TLP, ref tempLabel, theme.defStartBackColor, theme.defStartForeColor, 1, starts_Click, true, true, 0, cntIx);
            tempLabel.Text = "u";
            StaticUtility.addTextBox(ref TLP, ref tempTextBox, theme.defTxtBxBackColor, theme.defTxtBxForeColor, 0, false, false, 2, cntIx);
            tempTextBox.Text = boxText;
            StaticUtility.addLabel(ref TLP, ref tempLabel, theme.defStartBackColor, theme.defStartForeColor, 0, null, true, false, 3, cntIx);
            tempLabel.Text = "00:00:00";
            TLP.ResumeLayout(false);
            if (relayout) TLP.PerformLayout();
        }

        private void removeRow(ref TableLayoutPanel TLP, bool relayout)
        {
            int bottomTimerIndex = returnTmrCnt() - 1;// -1 is to get to active's zero index.

            if (!timerCanBeRemoved()) return;
            if (bottomTimerIndex == TIMEENGINE.getActiveRow()) TIMEENGINE.changeActive(-1); // If the active row is to be removed stop.
            TIMEENGINE.resetTimerValue(bottomTimerIndex); // make sure the removed value is zero. It might not be.
            disUpdTotal(TIMEENGINE.engCalcArrayTotal());  // if you removed an existing row you want to reset the total.            

            int bottomRowIndex = baseTLP.RowCount - 1;
            TLP.SuspendLayout();
            Control c = TLP.GetControlFromPosition(0, bottomRowIndex);
            TLP.Controls.Remove(c);
            c.Dispose();
            Control d = TLP.GetControlFromPosition(2, bottomRowIndex);
            TLP.Controls.Remove(d);
            d.Dispose();
            Control e = TLP.GetControlFromPosition(3, bottomRowIndex);
            TLP.Controls.Remove(e);
            e.Dispose();
            TLP.RowStyles.RemoveAt(bottomRowIndex);
            TLP.RowCount--;
            TLP.ResumeLayout(false);
            TLP.PerformLayout();
        }

        private void initializeTLP(ref TableLayoutPanel TLP)
        {
            FormClosing += new FormClosingEventHandler(workWinder_FormClosing); //unrelated but I need to do this somewhere

            TLP = new TableLayoutPanel();
            TLP.SuspendLayout();
            SuspendLayout();

            //Set Window Properties
            BackColor = theme.defWindowColor;
            Text = StaticUtility.returnTitleName();
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            MinimumSize = new Size(Properties.Settings.Default.MainFormWindowWidth, 50);
            MaximizeBox = false;

            //Define Column Settings
            TLP.ColumnCount = 4;
            TLP.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            TLP.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            TLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TLP.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));

            //Add Rows
            addRow(TLP);
            addRow(TLP);

            //Set TableLayoutPanel Settings
            TLP.Dock = DockStyle.Fill;
            TLP.Location = new Point(0, 0);
            TLP.Margin = new Padding(0);
            TLP.Padding = new Padding(2);
            TLP.AutoSize = true;
            Controls.Add(TLP);

            //Stop Button
            StaticUtility.addLabel(ref TLP, ref tempLabel, theme.defStopBackColor, theme.defStopForeColor, 2, stop_Click, true, true, 0, 0);
            tempLabel.Text = "Stop";
            //Total label
            StaticUtility.addLabel(ref TLP, ref tempLabel, theme.defTotalBackColor, theme.defTotalForeColor, 0, null, true, true, 2, 0);
            tempLabel.Text = "Total: 00:00:00";
            //Up button
            StaticUtility.addLabel(ref TLP, ref tempLabel, theme.defUpDwBackColor, theme.defUpDwForeColor, 1, up_Click, true, false, 0, 1);
            tempLabel.Text = "p";
            //Down button
            StaticUtility.addLabel(ref TLP, ref tempLabel, theme.defUpDwBackColor, theme.defUpDwForeColor, 1, down_Click, true, false, 1, 1);
            tempLabel.Text = "q";
            //Open Report
            StaticUtility.addLabel(ref TLP, ref tempLabel, theme.defUpDwBackColor, theme.defUpDwForeColor, 2, config_Click, true, true, 2, 1);
            tempLabel.Text = "Report && Configuration";

            //Setup the default name settings
            string[] nameLiDefault = Properties.Settings.Default.NameList.Split(new char[] { ',' });

            for (int i = 0; i < nameLiDefault.Length; i++)
            {
                textBoxNamesArray[i] = nameLiDefault[i];
                addTimerRow(ref TLP, textBoxNamesArray[i], false);
            }

            TLP.ResumeLayout(false);
            TLP.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        //TODO: You could make this static by passing it TLP and active.
        private bool timerCanBeRemoved()
        {
            int bottomTimerIndex = returnTmrCnt() - 1;// -1 is to get to active's zero index.

            if (bottomTimerIndex < 1) // Are you going less than 1 timer?
            {
                DialogResult result = MessageBox.Show(
                    "You can't have less than one timer."
                    , ""
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Information
                    , MessageBoxDefaultButton.Button1);
                return false; //Timer can't be removed
            }

            if (TIMEENGINE.getTimeValue(bottomTimerIndex) > TimeSpan.Zero || (bottomTimerIndex == TIMEENGINE.getActiveRow())) // test: Does the last row have a non-zero time? Or is it running?
            {
                DialogResult result = MessageBox.Show(
                "You are trying to remove a non-zero timer. The "
                    + (string.IsNullOrEmpty(returnTimerTextBox(bottomTimerIndex)) ? "last" : "\"" + returnTimerTextBox(bottomTimerIndex) + "\"")
                    + " timer will be reset and removed from the total elapsed time. Continue?",
                "Warning",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

                return result == DialogResult.OK; //Timer can be removed (clicked okay)
            }
            else return true; // Timer can be removed
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++
        // Layout
        // +++++++++++++++++++++++END+++++++++++++++++++++++

        // ++++++++++++++++++++++Start++++++++++++++++++++++
        // Events
        // +++++++++++++++++++++++++++++++++++++++++++++++++

        private void starts_Click(object sender, EventArgs e)
        {
            Label clicked = (Label)sender;
            TableLayoutPanelCellPosition a = baseTLP.GetPositionFromControl(clicked);
            int clk = a.Row;

            //Check if you just clicked the same row
            if (TIMEENGINE.getActiveRow() == clk - StaticUtility.visualNonTimerRows) return;
            TIMEENGINE.changeActive(clk - StaticUtility.visualNonTimerRows);
        }

        private void stop_Click(object sender, EventArgs e)
        {
            if (TIMEENGINE.getActiveRow() < 0) return; //already stopped exit
            TIMEENGINE.changeActive(-1);
        }

        private void up_Click(object sender, EventArgs e)
        {
            deleteRow();
        }

        private void down_Click(object sender, EventArgs e)
        {
            createRow();
        }

        private void config_Click(object sender, EventArgs e)
        {
            CONFDIS.ShowDialog();
        }

        private void workWinder_FormClosing(object sender, FormClosingEventArgs e)
        {
            TIMEENGINE.changeActive(-1);
            TIMEENGINE.changeActive(-2);
            TIMEENGINE.engineClosing();
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++
        // Events
        // +++++++++++++++++++++++END+++++++++++++++++++++++        

        // +++++This section here allows the timer to be snapped to the edge of a window+++++
        private const int SnapDist = 100;

        private bool DoSnap(int pos, int edge)
        {
            int delta = pos - edge;
            return delta > 0 && delta <= SnapDist;
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
            Screen scn = Screen.FromPoint(this.Location);
            if (DoSnap(this.Left, scn.WorkingArea.Left)) this.Left = scn.WorkingArea.Left;
            if (DoSnap(this.Top, scn.WorkingArea.Top)) this.Top = scn.WorkingArea.Top;
            if (DoSnap(scn.WorkingArea.Right, this.Right)) this.Left = scn.WorkingArea.Right - this.Width;
            if (DoSnap(scn.WorkingArea.Bottom, this.Bottom)) this.Top = scn.WorkingArea.Bottom - this.Height;
        }
        // +++++End Section for timer snap+++++
    }
}
