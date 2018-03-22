using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WorkWinder
{
    public class FileMechanism
    {
        /// <summary>
        /// TRUE: Actions are saved to file
        /// </summary>
        bool saveActionsFlag = Properties.Settings.Default.SaveActions;

        /// <summary>
        /// By default holds a directory one under the executable
        /// </summary>
        public string dirString = (Properties.Settings.Default.SaveFolderOverride == "" ? (Application.StartupPath + @"\TimeFiles") : Properties.Settings.Default.SaveFolderOverride);

        WorkWinderDisplay DISPLAY;
        TimerEngine TIMEENGINE;

        public FileMechanism(TimerEngine t, WorkWinderDisplay w)
        {
            DISPLAY = w;
            TIMEENGINE = t;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="getFullPath"></param>
        /// <returns></returns>
        public string getDirString(bool getFullPath = false)
        {
            if (getFullPath == false)
            {
                return dirString;
            }
            else
            {
                return dirString + @"\" + DateTime.Today.ToString("yyyMMdd") + "_Times.csv";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string[][] tloadCSVfile(string filePath)
        {
            //string filePath = @"C:\Users\Nick\Documents\C sharp\troubleshooting.csv";
            using (StreamReader sr = new StreamReader(filePath))
            {
                List<List<string>> results = new List<List<string>>();

                var list1 = new List<string>(sr.ReadLine().Split(',')); //results.Add(list1); //Don't add it. It's the version row.

                while (!sr.EndOfStream)
                {
                    var list = new List<string>(sr.ReadLine().Split(','));
                    results.Add(list);
                }
                return results.Select(a => a.ToArray()).ToArray();
            }
        }

        /// <summary>
        /// Will load an existing file if one exists.
        /// </summary>
        /// <returns>TRUE if it had to load an existing file; FALSE if existing file doesn't exist or can't be loaded.</returns>
        public bool loadExistingFile()
        {
            FileInfo fi = new FileInfo(dirString + @"\" + DateTime.Today.ToString("yyyMMdd") + "_Times.csv");
            if (!fi.Exists) return false;
            string[] arrLines = File.ReadAllLines(fi.ToString());

            string[] versionRow = StaticUtility.parseFileRow(arrLines[0]);
            string head = returnHeaderLine(1);

            if (head == versionRow[0] + "," + versionRow[1]) //Don't allow version skew
            {
                int highestName = 0;
                int highestCnt = 0;
                string stoppedDuration = "";
                string[] cntrArray = new string[StaticUtility.maximumRows];
                string[] nmArray = new string[StaticUtility.maximumRows];

                string[] headerRow = StaticUtility.parseFileRow(arrLines[1]);
                string[] lastRow = StaticUtility.parseFileRow(arrLines.Last());

                stoppedDuration = lastRow[7];
                for (int i = 8; i < StaticUtility.maximumRows + 8; i++)
                {
                    if (headerRow[i] != "")
                    {
                        highestName = i - 8;
                    }
                    if (lastRow[i] != "")
                    {
                        highestCnt = i - 8;
                    }
                    nmArray[i - 8] = headerRow[i];
                    cntrArray[i - 8] = lastRow[i];
                }
                int needTimerRows = Math.Max(highestName, highestCnt) + 1;
                TIMEENGINE.setAll(nmArray, cntrArray, stoppedDuration, needTimerRows);
                TIMEENGINE.changeActive(-1);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Writes a single row to the file.
        /// </summary>
        /// <param name="nowInstant"></param>
        /// <param name="endInstant"></param>
        /// <param name="eventDuration"></param>
        /// <param name="eventDone"></param>
        /// <param name="activeAction"></param>
        /// <param name="stopElapsed"></param>
        /// <returns></returns>
        public bool writeRow(DateTime nowInstant, DateTime endInstant, TimeSpan eventDuration, string eventDone, int activeAction, TimeSpan stopElapsed)
        {
            //Exit if you aren't supposed to be saving
            if (!Properties.Settings.Default.SaveActions) return true;
            bool writeSuccess = true;

            FileInfo fi = new FileInfo(dirString + @"\" + DateTime.Today.ToString("yyyMMdd") + "_Times.csv");
            if (!fi.Exists) initializeFile();
            checkFileHeader();

            string textToWrite = endInstant.ToString("G") + ","
                + nowInstant.ToString("G") + ","
                + eventDuration.ToString() + ","
                + eventDone + ","
                + activeAction.ToString() + ","
                + (activeAction > -1 ? valCSVstring(DISPLAY.returnTimerTextBox(activeAction)) : "") + ","
                + TIMEENGINE.engCalcArrayTotal().ToString() + ","
                + stopElapsed.ToString() + ",";

            for (int i = 0; i < StaticUtility.maximumRows; i++)
            {
                textToWrite += TIMEENGINE.getTimeValue(i).ToString() == "00:00:00" ? "" + "," : TIMEENGINE.getTimeValue(i).ToString() + ",";
            }
            try
            {
                using (StreamWriter sw = fi.AppendText())
                {
                    sw.Write(textToWrite + Environment.NewLine);
                }
            }
            catch (Exception) { writeSuccess = false; writeThrowException(false); }

            return writeSuccess;
        }

        /// <summary>
        /// Called from Config window. Saves the column settings.
        /// </summary>
        public void saveConfigColumns()
        {
            Properties.Settings.Default.NameList = string.Join(",", DISPLAY.returnTextBoxList());
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        public void initializeFile()
        {
            //Exit if you aren't supposed to be saving
            if (!Properties.Settings.Default.SaveActions) return;

            bool writeSuccess = true;
            string currentHeaderLines = returnHeaderLine();

            FileInfo fi = new FileInfo(dirString + @"\" + DateTime.Today.ToString("yyyMMdd") + "_Times.csv");

            if (!fi.Exists)
            {
                //File doesn't exist. Create file and write the header line to it.
                fi.Directory.Create();
                try
                {
                    using (StreamWriter sw = fi.CreateText())
                    {
                        sw.Write(currentHeaderLines + Environment.NewLine);
                    }
                }
                catch (Exception) { writeSuccess = false; writeThrowException(false); }
            }
            else
            {
                writeSuccess = checkFileHeader();
            }
        }

        /// <summary>
        /// Checks to see if the file header needs to be updated and up dates it if it does.
        /// </summary>
        /// <returns>TRUE if successful. False if failure.</returns>
        private bool checkFileHeader()
        {
            //Exit if you aren't supposed to be saving
            if (!Properties.Settings.Default.SaveActions) return true;

            bool writeSuccess = true;
            string currentHeaderLines = returnHeaderLine();

            FileInfo fi = new FileInfo(dirString + @"\" + DateTime.Today.ToString("yyyMMdd") + "_Times.csv");

            try
            {
                //Read the first line of the file
                string fileHeaderLines;
                using (StreamReader reader = new StreamReader(fi.ToString()))
                {
                    fileHeaderLines = reader.ReadLine() + Environment.NewLine;
                    fileHeaderLines += reader.ReadLine(); //NO carriage return at the end of these
                }

                // if the first two lines doesn't match the file, edit the first two lines, replace the rest of the file
                if (fileHeaderLines != currentHeaderLines)
                {
                    string[] arrLine = File.ReadAllLines(fi.ToString());
                    arrLine[0] = returnHeaderLine(1);
                    arrLine[1] = returnHeaderLine(2);

                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < arrLine.Length - 1; i++)
                    {
                        builder.Append(arrLine[i]);
                        builder.Append(Environment.NewLine);
                    }
                    builder.Append(arrLine[arrLine.Length - 1]);
                    builder.AppendLine(); // I think the last string doesn't have a new line on it. Even though it should... I should have worked enough breaks for a null line.

                    File.WriteAllText(fi.ToString(), builder.ToString());
                }
            }
            catch (Exception) { writeSuccess = false; writeThrowException(false); }

            return writeSuccess;
        }

        /// <summary>
        /// Used to return a string that should populate the first few lines of the form.
        /// </summary>
        /// <returns>CSV header line specifying columns. NO NewLine Character to end last line.</returns>
        private string returnHeaderLine(int numReturn = 0)
        {
            string ln1, ln2 = "";

            ln1 = "Version:" + StaticUtility.version + "," + "MaximumRows:" + StaticUtility.maximumRows;
            if (numReturn == 1) return ln1;

            ln2 += "Event DateTime (Start),Event DateTime (End),Duration,Event,Timer ID,Row Name,Events Total,Stopped Total,";
            string[] existingRowsText = DISPLAY.returnTextBoxList();
            for (int i = 0; i < StaticUtility.maximumRows; i++)
            {
                if (i < existingRowsText.Length) ln2 += valCSVstring(existingRowsText[i]) + ",";
                else ln2 += ",";
                //else ln2 += i + "temp,";
            }
            if (numReturn == 2) return ln2;

            else return ln1 + Environment.NewLine + ln2;
        }

        /// <summary>
        /// Santizing user input from a text box
        /// </summary>
        /// <param name="input">String to validate</param>
        /// <returns>String without ","</returns>
        private string valCSVstring(string input)
        {
            return input.Replace(",", "");
        }

        /// <summary>
        /// Handles write fail.
        /// </summary>
        /// <param name="success">TRUE exits. FALSE throws the exception.</param>
        private void writeThrowException(bool success)
        {
            if (success) return;
            DialogResult result = MessageBox.Show(
                "File could not be written to. Do you have the output file locked?\n\nApplication will continue but the previous action was not logged to file. The report may now be inaccurate",
                "Warning",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }
}
