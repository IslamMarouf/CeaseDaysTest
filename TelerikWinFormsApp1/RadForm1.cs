using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Telerik.WinControls.UI;

namespace TelerikWinFormsApp1
{
    public partial class RadForm1 : RadForm
    {
        public class ConsecutiveDate
        {
            public DateTime Date1;
            public DateTime Date2;
        }

        //
        private Random rand = new Random();

        public RadForm1() {
            InitializeComponent();
        }

        private void RadForm1_Load(object sender, EventArgs e) {
            List<DateTime> dList = new List<DateTime>();
            List<ConsecutiveDate> resultList = new List<ConsecutiveDate>();
            //
            for (int i = 1; i <= 2000; i++) {
                int offset = rand.Next(1, 2000);
                DateTime d = DateTime.Today.AddDays(offset);
                if (!dList.Contains(d)) {
                    dList.Add(d);
                }
            }

            //
            string allDaysTextFile = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\AllDays.txt";
            StringBuilder sb = new StringBuilder();
            //
            for (int i = 0; i < dList.Count; i++) {
                sb.AppendLine(dList[i].ToShortDateString());
            }

            //
            File.WriteAllText(allDaysTextFile, sb.ToString());
            //
            dList.Sort();
            //
            for (int i = 0; i < dList.Count; i++) {
                if (i > 0) {
                    DateTime dayBefore = dList[i - 1];
                    DateTime thisDay = dList[i];
                    bool containsFriday =
                        dayBefore.DayOfWeek == DayOfWeek.Friday || thisDay.DayOfWeek == DayOfWeek.Friday;
                    //
                    if (dayBefore.DayOfWeek > DayOfWeek.Sunday && dayBefore.DayOfWeek < DayOfWeek.Saturday) {
                        //
                        if (thisDay.DayOfWeek > DayOfWeek.Sunday && thisDay.DayOfWeek < DayOfWeek.Saturday) {
                            //
                            if (containsFriday) {
                                // Determine which of them is Friday
                                if (dayBefore.DayOfWeek == DayOfWeek.Friday) {
                                    if (thisDay.DayOfWeek == DayOfWeek.Monday) {
                                        ConsecutiveDate thisDateSet = new ConsecutiveDate();
                                        thisDateSet.Date1 = dayBefore;
                                        thisDateSet.Date2 = thisDay;
                                        resultList.Add(thisDateSet);
                                    }
                                }
                                else {
                                    if (dayBefore.DayOfWeek == DayOfWeek.Monday) {
                                        if (Simulate.DateDiff(Simulate.DateInterval.Day, dayBefore, thisDay) == 4) {
                                            ConsecutiveDate thisDateSet = new ConsecutiveDate();
                                            thisDateSet.Date1 = dayBefore;
                                            thisDateSet.Date2 = thisDay;
                                            resultList.Add(thisDateSet);
                                        }
                                    }
                                }
                            }
                            else {
                                if (Simulate.DateDiff(Simulate.DateInterval.Day, dayBefore, thisDay) == 1) {
                                    ConsecutiveDate thisDateSet = new ConsecutiveDate();
                                    thisDateSet.Date1 = dayBefore;
                                    thisDateSet.Date2 = thisDay;
                                    resultList.Add(thisDateSet);
                                }
                            }
                        }
                    }
                }
            }

            //
            string consecutiveDaysFile =
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\ConsecutiveDays.txt";
            sb.Length = 0;
            //
            for (int i = 0; i < resultList.Count; i++) {
                if (i > 0) {
                    sb.AppendLine();
                    sb.AppendLine("------------------------------");
                    sb.AppendLine();
                }

                sb.AppendLine(resultList[i].Date1.ToShortDateString());
                sb.AppendLine(resultList[i].Date2.ToShortDateString());
            }

            //
            File.WriteAllText(consecutiveDaysFile, sb.ToString());
        }

        private void radButton1_Click(object sender, EventArgs e) {
            DateTimeCollection dates = radCalendar1.SelectedDates;

            var qDates = from dt in dates
                    .OrderBy(dt => dt.Month)
                    .ThenBy(dt => dt.Day)
                    .GroupBy(dt => dt.Month)
                    .OrderBy(dt => dt.Key)
                select dt;

            string dateString = string.Empty;

            foreach (var date in qDates) {
                dateString += "days in " + date.Key + " :\n";

                int count = 0;
                foreach (DateTime date1 in date) {
                    dateString += date1.ToShortDateString();
                    count++;

                    if (count != date.Count()) {
                        dateString += " - ";
                    }
                }

                dateString += "\n";
            }

            radTextBoxControl1.Text = dateString;
        }

//----------------------------------------------------------------------------------------
//	Copyright © 2003 - 2017 Tangible Software Solutions Inc.
//	This class can be used by anyone provided that the copyright notice remains intact.
//
//	This class simulates the behavior of the classic VB 'DateDiff' function.
//----------------------------------------------------------------------------------------
        public static class Simulate
        {
            public enum DateInterval
            {
                Day,
                DayOfYear,
                Hour,
                Minute,
                Month,
                Quarter,
                Second,
                Weekday,
                WeekOfYear,
                Year
            }

            public static long DateDiff(DateInterval intervalType, DateTime dateOne, DateTime dateTwo)
            {
                switch (intervalType)
                {
                    case DateInterval.Day:
                    case DateInterval.DayOfYear:
                        TimeSpan spanForDays = dateTwo - dateOne;
                        return (long)spanForDays.TotalDays;
                    case DateInterval.Hour:
                        TimeSpan spanForHours = dateTwo - dateOne;
                        return (long)spanForHours.TotalHours;
                    case DateInterval.Minute:
                        TimeSpan spanForMinutes = dateTwo - dateOne;
                        return (long)spanForMinutes.TotalMinutes;
                    case DateInterval.Month:
                        return ((dateTwo.Year - dateOne.Year) * 12) + (dateTwo.Month - dateOne.Month);
                    case DateInterval.Quarter:
                        long dateOneQuarter = (long)Math.Ceiling(dateOne.Month / 3.0);
                        long dateTwoQuarter = (long)Math.Ceiling(dateTwo.Month / 3.0);
                        return (4 * (dateTwo.Year - dateOne.Year)) + dateTwoQuarter - dateOneQuarter;
                    case DateInterval.Second:
                        TimeSpan spanForSeconds = dateTwo - dateOne;
                        return (long)spanForSeconds.TotalSeconds;
                    case DateInterval.Weekday:
                        TimeSpan spanForWeekdays = dateTwo - dateOne;
                        return (long)(spanForWeekdays.TotalDays / 7.0);
                    case DateInterval.WeekOfYear:
                        DateTime dateOneModified = dateOne;
                        DateTime dateTwoModified = dateTwo;
                        while (System.Globalization.DateTimeFormatInfo.CurrentInfo != null && dateTwoModified.DayOfWeek !=
                               System.Globalization.DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek)
                        {
                            dateTwoModified = dateTwoModified.AddDays(-1);
                        }

                        while (System.Globalization.DateTimeFormatInfo.CurrentInfo != null && dateOneModified.DayOfWeek !=
                               System.Globalization.DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek)
                        {
                            dateOneModified = dateOneModified.AddDays(-1);
                        }

                        TimeSpan spanForWeekOfYear = dateTwoModified - dateOneModified;
                        return (long)(spanForWeekOfYear.TotalDays / 7.0);
                    case DateInterval.Year:
                        return dateTwo.Year - dateOne.Year;
                    default:
                        return 0;
                }
            }
        }
    }
}