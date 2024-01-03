using System;
using System.Timers;

namespace xmascd
{
    public class Countdown
    {
        public TimeSpan tCountdownTimeRemaining;
        public DateTime dCurrentDate;
        public DateTime dEndDate;
        private TimeSpan tTimerDuration;

        public delegate void CountdownTickEventHandler(object sender, EventArgs e);
        public event CountdownTickEventHandler eCountdownTick;

        public Countdown(DateTime thisCurrentDate, DateTime thisEndDate, int thisTimerDuration)
        {
            dCurrentDate = thisCurrentDate;
            dEndDate = dNextAnnualOccurenceOfEndDate(dCurrentDate, thisEndDate);
            tTimerDuration = TimeSpan.FromMilliseconds(thisTimerDuration);
            Timer timer = new Timer(thisTimerDuration); //Creates the Timer instance...
            timer.Elapsed += CountdownTick;
            timer.AutoReset = true;
            timer.Start();
        }
        public void CountdownTick(object sender, ElapsedEventArgs e)
        {
            dCurrentDate = dCurrentDate.Add(tTimerDuration);
            tCountdownTimeRemaining = tCalculateRemainingTime(dCurrentDate, dEndDate);
            eCountdownTick?.Invoke(this, EventArgs.Empty); //Raise event.
        }
        TimeSpan tCalculateRemainingTime(DateTime dStartDate, DateTime dEndDate)
        {
            if (dEndDate < dStartDate)
            {
                throw new ArgumentException("dEndDate must occur after dStartDate."); //Throws an exception if an invalid EndDate is defined.
            }
            else
            {
                return dEndDate - dStartDate; //Gets the time between the two.
            }
        }
        DateTime dNextAnnualOccurenceOfEndDate(DateTime dStartDate, DateTime dEndDate)
        {
            // Adjust endDate to the next occurrence in the current or future years
            DateTime nextOccurrence = new DateTime(
                dStartDate.Year,
                dEndDate.Month,
                dEndDate.Day,
                dEndDate.Hour,
                dEndDate.Minute,
                dEndDate.Second
                );

            if (dStartDate > nextOccurrence)
            {
                // If the adjusted date is in the past, move to the next year
                nextOccurrence = nextOccurrence.AddYears(1);
            }
            return nextOccurrence;
        }
    }
}