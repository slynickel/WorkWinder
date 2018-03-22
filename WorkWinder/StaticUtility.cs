using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Drawing;
using System.Windows.Forms;
using WinderTheme;

namespace WorkWinder
{
    static class StaticUtility
    {
        public const string version = "0.6.4";
        public const float rowheight = 30F;
        public const int visualNonTimerRows = 2;
        public const int maximumRows = 30;

        //public static Color defWindowColor = Color.Black;
        //public static Color flashWindowColor = Color.Red;
        //
        //public static Color defStartBackColor = Color.Gray;
        //public static Color defStartForeColor = Color.White;
        //public static Color STARTEDBackColor = Color.White;
        //public static Color STARTEDForeColor = Color.Black;
        //
        //public static Color defTxtBxBackColor = Color.Black;
        //public static Color defTxtBxForeColor = Color.White;
        //public static Color ACTIVETxtBxBackColor = Color.White;
        //public static Color ACTIVETxtBxForeColor = Color.Black;
        //
        //public static Color defUpDwBackColor = Color.Gray;
        //public static Color defUpDwForeColor = Color.Black;
        //
        //public static Color defTotalForeColor = Color.White;
        //public static Color defTotalBackColor = Color.Transparent;
        //
        //public static Color defStopBackColor = Color.DarkRed;
        //public static Color defStopForeColor = Color.Red;
        //public static Color STOPPEDBackColor = Color.Red;
        //public static Color STOPPEDForeColor = Color.Black;

        public static TimeSpan AddTime(TimeSpan t1, TimeSpan t2)
        {
            return new TimeSpan(t1.Hours + t2.Hours, t1.Minutes + t2.Minutes, t1.Seconds + t2.Seconds);
        }

        public static TimeSpan AddTime(DateTime d1, DateTime d2)
        {
            return new TimeSpan(d1.Hour + d2.Hour, d1.Minute + d2.Minute, d1.Second + d2.Second);
        }

        public static TimeSpan SubTime(TimeSpan t1, TimeSpan t2)
        {
            return new TimeSpan(t1.Hours - t2.Hours, t1.Minutes - t2.Minutes, t1.Seconds - t2.Seconds);
        }

        public static TimeSpan SubTime(DateTime d1, DateTime d2)
        {
            return new TimeSpan(d1.Hour - d2.Hour, d1.Minute - d2.Minute, d1.Second - d2.Second);
        }

        /// <summary>
        /// Get WorkWinder v#.#
        /// </summary>
        /// <returns></returns>
        public static string returnTitleName()
        {
            return "Work Winder v" + version;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        public static TimeSpan stringToTimeSpan(string st)
        {
            try
            {
                string[] cnt = new string[3];
                cnt = st.Split(':');
                return new TimeSpan(Convert.ToInt32(cnt[0]), Convert.ToInt32(cnt[1]), Convert.ToInt32(cnt[2]));
            }
            catch (Exception) { return new TimeSpan(0, 0, 0); }
        }

        /// <summary>
        /// Separates a string delmited by commas.
        /// </summary>
        /// <param name="rawRow">Comma delimited list of columns.</param>
        /// <returns></returns>
        public static string[] parseFileRow(string rawRow)
        {
            return rawRow.Split(',');
        }

        /// <summary>
        /// Returns a label from the specifed TableLayoutPanel.
        /// </summary>
        /// <param name="TLP"></param>
        /// <param name="row">Y row, index 0</param>
        /// <param name="col">X col, index 0</param>
        /// <returns></returns>
        public static Label getLabelYX(TableLayoutPanel TLP, int row, int col)
        {
            Control c = TLP.GetControlFromPosition(col, row);
            return c as Label;
        }

        /// <summary>
        /// Returns the textbox from specifed TableLayoutPanel.
        /// </summary>
        /// <param name="TLP"></param>
        /// <param name="row">Y row, index 0</param>
        /// <param name="col">X col, index 0</param>
        /// <returns></returns>
        public static TextBox getTextBoxYX(TableLayoutPanel TLP, int row, int col)
        {
            Control c = TLP.GetControlFromPosition(col, row);
            return c as TextBox;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TLP"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="text"></param>
        public static void setTextBoxYX(TableLayoutPanel TLP, int row, int col, string text)
        {
            Control c = TLP.GetControlFromPosition(col, row);
            c.Text = text;
        }

        /// <summary>
        /// Does the visual changes 
        /// </summary>
        /// <param name="TLP"></param>
        /// <param name="running"></param>
        /// <param name="active"></param>
        public static void setRunningRow(ref TableLayoutPanel TLP, bool running, int active, xColors thm)
        {
            TLP.SuspendLayout();
            if (running)
            {
                getLabelYX(TLP, active + visualNonTimerRows, 0).BackColor = getLabelYX(TLP, active + visualNonTimerRows, 3).BackColor = thm.STARTEDBackColor;
                getLabelYX(TLP, active + visualNonTimerRows, 0).ForeColor = getLabelYX(TLP, active + visualNonTimerRows, 3).ForeColor = thm.STARTEDForeColor;
                getLabelYX(TLP, active + visualNonTimerRows, 0).Text = "Running";
                getLabelYX(TLP, active + visualNonTimerRows, 0).Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
                getTextBoxYX(TLP, active + visualNonTimerRows, 2).ForeColor = thm.ACTIVETxtBxForeColor;
                getTextBoxYX(TLP, active + visualNonTimerRows, 2).BackColor = thm.ACTIVETxtBxBackColor;
            }
            else
            {
                getLabelYX(TLP, active + visualNonTimerRows, 0).BackColor = getLabelYX(TLP, active + visualNonTimerRows, 3).BackColor = thm.defStartBackColor;
                getLabelYX(TLP, active + visualNonTimerRows, 0).ForeColor = getLabelYX(TLP, active + visualNonTimerRows, 3).ForeColor = thm.defStartForeColor;
                getLabelYX(TLP, active + visualNonTimerRows, 0).Text = "u";
                getLabelYX(TLP, active + visualNonTimerRows, 0).Font = new Font("Wingdings 3", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 2);
                getTextBoxYX(TLP, active + visualNonTimerRows, 2).ForeColor = thm.defTxtBxForeColor;
                getTextBoxYX(TLP, active + visualNonTimerRows, 2).BackColor = thm.defTxtBxBackColor;
            }
            TLP.ResumeLayout(false);
            TLP.PerformLayout();
        }

        /// <summary>
        /// Handles stop looking correct.
        /// </summary>
        /// <param name="TLP"></param>
        /// <param name="stopped">TRUE means you should show stopped.</param>
        public static void setStopped(ref TableLayoutPanel TLP, bool stopped, xColors thm)
        {
            Label lbl = StaticUtility.getLabelYX(TLP, 0, 0);
            if (stopped)
            {
                lbl.Text = "Stopped";
                lbl.ForeColor = thm.STOPPEDForeColor;
                lbl.BackColor = thm.STOPPEDBackColor;
            }
            else
            {
                lbl.Text = "Stop";
                lbl.ForeColor = thm.defStopForeColor;
                lbl.BackColor = thm.defStopBackColor;
            }
        }

        /// <summary>
        /// Add label to TLP
        /// </summary>
        /// <param name="TLP">TLP</param>
        /// <param name="refLabel">reference temp label</param>
        /// <param name="backColor"></param>
        /// <param name="foreColor"></param>
        /// <param name="fontType"></param>
        /// <param name="click">action call</param>
        /// <param name="pad"></param>
        /// <param name="colSpan"></param>
        /// <param name="col"></param>
        /// <param name="row"></param>
        public static void addLabel(ref TableLayoutPanel TLP, ref Label refLabel, Color backColor, Color foreColor, int fontType, EventHandler click, bool pad, bool colSpan, int col, int row)
        {
            refLabel = new Label();
            refLabel.AutoSize = true;
            refLabel.BackColor = backColor;
            refLabel.ForeColor = foreColor;
            refLabel.Dock = DockStyle.Fill;
            switch (fontType)
            {
                case 0:
                    refLabel.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
                    break;
                case 1:
                    refLabel.Font = new Font("Wingdings 3", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 2);
                    break;
                case 2:
                    refLabel.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
                    break;
            }
            refLabel.TextAlign = ContentAlignment.MiddleCenter;
            if (click != null) refLabel.Click += new EventHandler(click);
            if (pad) refLabel.Margin = new Padding(2);
            else refLabel.Margin = new Padding(0);
            if (colSpan) TLP.SetColumnSpan(refLabel, 2);
            TLP.Controls.Add(refLabel, col, row);
        }

        /// <summary>
        /// Add text box TLP
        /// </summary>
        /// <param name="TLP"></param>
        /// <param name="refTextBox"></param>
        /// <param name="backColor"></param>
        /// <param name="foreColor"></param>
        /// <param name="fontType"></param>
        /// <param name="pad"></param>
        /// <param name="colSpan"></param>
        /// <param name="col"></param>
        /// <param name="row"></param>
        public static void addTextBox(ref TableLayoutPanel TLP, ref TextBox refTextBox, Color backColor, Color foreColor, int fontType, bool pad, bool colSpan, int col, int row)
        {
            refTextBox = new TextBox();
            refTextBox.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
            refTextBox.BackColor = backColor;
            refTextBox.ForeColor = foreColor;
            refTextBox.BorderStyle = BorderStyle.FixedSingle;
            refTextBox.Font = new Font("Calibri", 11.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            refTextBox.Margin = new Padding(2, 0, 2, 0);
            TLP.Controls.Add(refTextBox, col, row);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sdfPath"></param>
        /// <param name="queryString"></param>
        /// <param name="dataGridViewDestination"></param>
        private static void runManicTimeSQLQuery(string sdfPath, string queryString, DataGridView dataGridViewDestination)
        {
            DataTable dtRecord = new DataTable();
            dataGridViewDestination.DataSource = null;
            dataGridViewDestination.Refresh();

            string strConnection; //= @"C:\Users\Nick\AppData\Local\Finkit\ManicTime\ManicTime.sdf;";
            strConnection = sdfPath;
            SqlCeConnection con = new SqlCeConnection("Data Source = " + sdfPath + ";");
            SqlCeCommand sqlCmd = new SqlCeCommand();
            sqlCmd.Connection = con;
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = queryString;
            try
            {
                SqlCeDataAdapter sqlDataAdap = new SqlCeDataAdapter(sqlCmd);
                sqlDataAdap.Fill(dtRecord);
                dataGridViewDestination.DataSource = dtRecord;
                //dataGridViewDestination.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                //int columncnt = dataGridViewDestination.Columns.Count;
                //for (int i = 0; i < columncnt; i++) 
                //{
                //    dataGridViewDestination.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                //}
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            dtRecord.Dispose();
            sqlCmd.Dispose();
            con.Close();
            con.Dispose();
        }

        public static void listAll2(DateTime start, DateTime end, DataGridView dataGridView1, string sdfPath)
        {
            string queryText = @"Select datediff(second,act.StartLocalTime,act.EndLocalTime)/60 as ""Duration(mins)"", act.DisplayName Activity, grp.DisplayName Application, doc.DisplayName Document, grp2.DisplayName ""Document Name"", act.StartLocalTime Start, act.EndLocalTime as ""End""" +
                                    " from Activity act inner join [Group] grp on act.GroupId = grp.GroupId left outer join Activity doc on act.StartLocalTime=doc.StartLocalTime and act.EndLocalTime=doc.EndLocalTime and doc.TimelineId=4 left outer join [Group] grp2 on doc.GroupId=grp2.GroupId" +
                                    " where act.TimelineId=3 and" +
                                    " (( '" + start.ToString("G") + "' < act.EndLocalTime AND act.EndLocalTime < '" + end.ToString("G") + "' ) OR ( '" + start.ToString("G") + "' < act.StartLocalTime AND act.StartLocalTime < '" + end.ToString("G") + "' ))" +
                                    @" order by ""Duration(mins)"" desc";
            runManicTimeSQLQuery(sdfPath, queryText, dataGridView1);
        }

        public static void aggregate2(DateTime start, DateTime end, DataGridView dataGridView1, string sdfPath)
        {
            string queryText = @"Select sum(datediff(second,act.StartLocalTime,act.EndLocalTime)/60) as ""Duration(mins)"", act.DisplayName Activity, grp.DisplayName Application, doc.DisplayName Document, grp2.DisplayName ""Document Name"", Min(act.StartLocalTime) ""First Event Start"", Max(act.EndLocalTime) ""Last Event End""" +
                                    " from Activity act inner join [Group] grp on act.GroupId = grp.GroupId left outer join Activity doc on act.StartLocalTime=doc.StartLocalTime and act.EndLocalTime=doc.EndLocalTime and doc.TimelineId=4 left outer join [Group] grp2 on doc.GroupId=grp2.GroupId" +
                                    " where act.TimelineId=3 and" +
                                    " (( '" + start.ToString("G") + "' < act.EndLocalTime AND act.EndLocalTime < '" + end.ToString("G") + "' ) OR ( '" + start.ToString("G") + "' < act.StartLocalTime AND act.StartLocalTime < '" + end.ToString("G") + "' ))" +
                                    " group by act.DisplayName,grp.DisplayName, doc.DisplayName,grp2.DisplayName" +
                                    @" order by ""Duration(mins)"" desc";
            runManicTimeSQLQuery(sdfPath, queryText, dataGridView1);
        }

        private static string listQueryText3(DateTime start, DateTime end)
        {
            return @"
                SELECT
                  DATEDIFF(SECOND, act.startlocaltime, act.endlocaltime) / 60 AS ""Duration (mins)"",
                  act.displayname Activity,
                  grp.displayname Application,
                  doc.displayname Document,
                  grp2.displayname ""Document Name"",
                  act.startlocaltime Start,
                  act.endlocaltime AS ""End""
                FROM activity act
                INNER JOIN [Group] grp
                  ON act.groupid = grp.groupid
                LEFT OUTER JOIN activity doc
                  ON act.startlocaltime = doc.startlocaltime
                  AND act.endlocaltime = doc.endlocaltime
                  AND doc.timelineid = 4
                LEFT OUTER JOIN [Group] grp2
                  ON doc.groupid = grp2.groupid
                WHERE act.timelineid = 3
                AND (( '" +
                start.ToString("G") + @"' < act.endlocaltime 
                AND act.endlocaltime < '" + end.ToString("G") + @"' )
                OR ( '" +
                start.ToString("G") + @"' < act.startlocaltime
                AND act.startlocaltime < '" + end.ToString("G") + @"' ))
                ORDER BY ""Duration (mins)"" DESC;";
        }

        private static string aggregateQueryText3(DateTime start, DateTime end)
        {
            return @"
                SELECT
                  SUM(DATEDIFF(SECOND, act.startlocaltime, act.endlocaltime) / 60) AS ""Duration (mins)"",
                  act.displayname Activity,
                  grp.displayname Application,
                  doc.displayname Document,
                  grp2.displayname ""Document Name"",
                  MIN(act.startlocaltime) Start,
                  MAX(act.endlocaltime) AS ""End""
                FROM activity act
                INNER JOIN [Group] grp
                  ON act.groupid = grp.groupid
                LEFT OUTER JOIN activity doc
                  ON act.startlocaltime = doc.startlocaltime
                  AND act.endlocaltime = doc.endlocaltime
                  AND doc.timelineid = 4
                LEFT OUTER JOIN [Group] grp2
                  ON doc.groupid = grp2.groupid
                WHERE act.timelineid = 3
                AND (( '" +
                start.ToString("G") + @"' < act.endlocaltime 
                AND act.endlocaltime < '" + end.ToString("G") + @"' )
                OR ( '" +
                start.ToString("G") + @"' < act.startlocaltime
                AND act.startlocaltime < '" + end.ToString("G") + @"' ))
                GROUP BY act.displayname,
                         grp.displayname,
                         doc.displayname,
                         grp2.displayname
                ORDER BY ""Duration (mins)"" DESC;";
        }

        public static void aggregate3(DateTime start, DateTime end, DataGridView dataGridView1, string sdfPath)
        {
            string queryText = aggregateQueryText3(start, end);
            runManicTimeSQLQuery(sdfPath, queryText, dataGridView1);
        }

        public static void listAll3(DateTime start, DateTime end, DataGridView dataGridView1, string sdfPath)
        {
            string queryText = aggregateQueryText3(start, end);
            runManicTimeSQLQuery(sdfPath, queryText, dataGridView1);
        }
    }
}
