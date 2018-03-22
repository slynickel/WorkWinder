using System;
using System.Linq;
using System.Windows.Forms;

namespace WorkWinder
{
    public class TimerEngine
    {
        WorkWinderDisplay DISPLAY;
        FileMechanism FILEMECH;

        public TimerEngine(WorkWinderDisplay w, FileMechanism f)
        {
            DISPLAY = w;
            FILEMECH = new FileMechanism(this, DISPLAY);
            for (int i = 0; i < StaticUtility.maximumRows; i++) activeTimeArray[i] = new TimeSpan(0, 0, 0);
            initializeTimers();
            engineInitialized();
        }

        /// <summary>
        /// State variable: -2 system action, -1 stopped, 0 < time, 0 index
        /// </summary>
        int active = -2;

        /// <summary>
        /// Holds inital time stamp
        /// </summary>
        DateTime activeTimeStamp = DateTime.Now;

        /// <summary>
        /// Elapsing time span variable
        /// </summary>
        TimeSpan currentDiff, tempTotal, stopElapsed = TimeSpan.Zero;

        /// <summary>
        /// Holds the current timer values. Index 0.
        /// Note that it doesn't hold the current ticking value.
        /// </summary>
        TimeSpan[] activeTimeArray = new TimeSpan[StaticUtility.maximumRows];

        /// <summary>
        /// Get a timer's value, 0 index. NONACTIVE!
        /// </summary>
        /// <param name="timer"></param>
        /// <returns></returns>
        public TimeSpan getTimeValue(int timer)
        {
            return activeTimeArray[timer];
        }

        /// <summary>
        /// Calculates the total for all visible timers.
        /// </summary>
        /// <returns></returns>
        public TimeSpan engCalcArrayTotal()
        {
            return activeTimeArray.Aggregate((t1, t2) => t1.Add(t2));
        }

        /// <summary>
        /// Gets the value of active.
        /// </summary>
        /// <returns></returns>
        public int getActiveRow()
        {
            return active;
        }

        /// <summary>
        /// Resets the timer value to zero.
        /// </summary>
        /// <param name="timer">Index Zero</param>
        public void resetTimerValue(int timer)
        {
            //TODO: AN ACTION SHOULD BE WRITTEN TO FILE. -2 maybe? Something to note in the file that a timer was reset or removed? Sort of a gray area.
            activeTimeArray[timer] = TimeSpan.Zero;
        }

        /// <summary>
        /// Changes the running timer or current action to a different action.
        /// </summary>
        /// <param name="newActive">Action to change to.</param>
        public void changeActive(int newActive)
        {
            if (active == newActive) return; //nothing changed
            currentDiff = StaticUtility.SubTime(DateTime.Now, activeTimeStamp);

            if (active > -1) // At button press something was running. Turn the active timer off and write to file.
            {
                DISPLAY.disSetRunTimer(active, false);
                activeTimeArray[active] = StaticUtility.AddTime(currentDiff, activeTimeArray[active]);
                DISPLAY.disUpd1Row(active, activeTimeArray[active]);
                FILEMECH.writeRow(DateTime.Now, activeTimeStamp, currentDiff, "[TIME]", active, stopElapsed);
            }

            if (active == -1) // system was stopped
            {
                stopElapsed = StaticUtility.AddTime(currentDiff, stopElapsed);
                FILEMECH.writeRow(DateTime.Now, activeTimeStamp, currentDiff, "[STOP]", active, stopElapsed);
                DISPLAY.disSetStopped(false);
            }

            // Update the total to window
            DISPLAY.disUpdTotal(engCalcArrayTotal());

            activeTimeStamp = DateTime.Now;
            active = newActive;

            if (newActive > -1) // Thing to go to is a timer
            {
                tempTotal = engCalcArrayTotal();
                DISPLAY.disUpd1Row(active, activeTimeArray[active]);
                DISPLAY.disSetRunTimer(active, true);
                windowUpdateTimer.Start();
            }

            if (newActive == -1) // Thing to go to is stopped
            {
                DISPLAY.disSetStopped(true);
                windowUpdateTimer.Stop();
            }
        }

        /// <summary>
        /// Called when the system is closing.
        /// </summary>
        public void engineClosing()
        {
            changeActive(-1);
            FILEMECH.writeRow(DateTime.Now, DateTime.Now, new TimeSpan(0, 0, 0), "[EXIT]", -2, stopElapsed);
        }

        /// <summary>
        /// Used to set the application to the the most recent value in the file.
        /// </summary>
        /// <param name="names"></param>
        /// <param name="counters"></param>
        /// <param name="stoppedTotal"></param>
        /// <param name="numCntrs"></param>
        public void setAll(string[] names, string[] counters, string stoppedTotal, int numCntrs)
        {
            string[] tmpSpn = new string[3];
            int currentTimerTexts = DISPLAY.returnTextBoxList().Length;

            //set correct number of timers
            if (numCntrs < currentTimerTexts)
            {
                //I commented this out. I can't figure out why it can't delete rows so now it just defaults in. 
                //for (int i = 0; i < currentTimerTexts - numCntrs; i++) DISPLAY.deleteRow();
            }
            else
            {
                for (int i = currentTimerTexts; i < numCntrs; i++) DISPLAY.createRow(names[i]);
            }

            //set actual time array values for timers
            for (int i = 0; i < StaticUtility.maximumRows; i++)
            {
                if (counters[i] == "")
                {
                    activeTimeArray[i] = new TimeSpan(0, 0, 0);
                    continue;
                }
                activeTimeArray[i] = StaticUtility.stringToTimeSpan(counters[i]);
            }

            //update the text boxes
            for (int i = 0; i < numCntrs; i++)
            {
                DISPLAY.setTimerTextBox(i, names[i]);
                DISPLAY.disUpd1Row(i, activeTimeArray[i]);
            }

            //update the total and stopped value
            DISPLAY.disUpdTotal(engCalcArrayTotal());
            stopElapsed = StaticUtility.stringToTimeSpan(stoppedTotal);
            FILEMECH.writeRow(DateTime.Now, DateTime.Now, new TimeSpan(0, 0, 0), "[LOAD]", -2, stopElapsed);
        }

        /// <summary>
        /// Clock tick for all the timers (1 sec)
        /// </summary>
        Timer windowUpdateTimer = new Timer();

        /// <summary>
        /// Set up the clock ticks
        /// </summary>
        private void initializeTimers()
        {
            windowUpdateTimer.Tick += windowUpdateTimer_Tick;
            windowUpdateTimer.Interval = 1000; //1.000 sec
        }

        //TODO: The case where you remove a few rows from the default then close it and reopen will not maintain the previous counters.
        // I don't for the life of me know why.
        /// <summary>
        /// Used to initalize the system.
        /// </summary>
        private void engineInitialized()
        {
            FILEMECH.loadExistingFile();
            FILEMECH.writeRow(DateTime.Now, DateTime.Now, new TimeSpan(0, 0, 0), "[OPEN]", -2, stopElapsed);
            changeActive(-1);
        }

        /// <summary>
        /// Define action that occurs each timer tick.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void windowUpdateTimer_Tick(object sender, EventArgs e)
        {
            if (active > -1)
            {
                currentDiff = StaticUtility.SubTime(DateTime.Now, activeTimeStamp);
                DISPLAY.disUpd1Row(active, StaticUtility.AddTime(activeTimeArray[active], currentDiff));

                TimeSpan newTotal = StaticUtility.AddTime(StaticUtility.SubTime(StaticUtility.AddTime(tempTotal, currentDiff), activeTimeArray[active]), activeTimeArray[active]);
                DISPLAY.disUpdTotal(newTotal);
            }
        }
    }
}
