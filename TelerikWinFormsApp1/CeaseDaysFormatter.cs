using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace TelerikWinFormsApp1 {
    //-----------------------------------------------------------------------------------------------
    //	Copyright © 2018 - 2017 Tangible Software Solutions Inc.
    //	Created by Islam Marouf based on the classic VB 'DateDiff' function.
    //
    //	This class arranges and formats the cease days of an employee and displays them as a string.
    //-----------------------------------------------------------------------------------------------
    public class CeaseDaysFormatter {
        public DateTimeCollection Dates { get; set; }
        private IEnumerable<IGrouping<int, DateTime>> _groupByMonth;
        private readonly List<DateTime> _datesList;

        public CeaseDaysFormatter(DateTimeCollection dates) {
            Dates = dates;
            _datesList = new List<DateTime>();
            ArrangeDates(Dates);
        }

        private void ArrangeDates(DateTimeCollection dates) {
            IOrderedEnumerable<DateTime> ordered = dates.OrderBy(dt => dt.Month).ThenBy(dt => dt.Day);
            IOrderedEnumerable<DateTime> orderedByYear = ordered.OrderBy(dt => dt.Year);
            _groupByMonth = orderedByYear.GroupBy(dt => dt.Month);
        }

        public enum DateInterval {
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

        public static long DateDiff(DateInterval intervalType, DateTime dateOne, DateTime dateTwo) {
            switch (intervalType) {
                case DateInterval.Day:
                case DateInterval.DayOfYear:
                    TimeSpan spanForDays = dateTwo - dateOne;
                    return (long) spanForDays.TotalDays;
                case DateInterval.Hour:
                    TimeSpan spanForHours = dateTwo - dateOne;
                    return (long) spanForHours.TotalHours;
                case DateInterval.Minute:
                    TimeSpan spanForMinutes = dateTwo - dateOne;
                    return (long) spanForMinutes.TotalMinutes;
                case DateInterval.Month:
                    return ((dateTwo.Year - dateOne.Year) * 12) + (dateTwo.Month - dateOne.Month);
                case DateInterval.Quarter:
                    long dateOneQuarter = (long) Math.Ceiling(dateOne.Month / 3.0);
                    long dateTwoQuarter = (long) Math.Ceiling(dateTwo.Month / 3.0);
                    return (4 * (dateTwo.Year - dateOne.Year)) + dateTwoQuarter - dateOneQuarter;
                case DateInterval.Second:
                    TimeSpan spanForSeconds = dateTwo - dateOne;
                    return (long) spanForSeconds.TotalSeconds;
                case DateInterval.Weekday:
                    TimeSpan spanForWeekdays = dateTwo - dateOne;
                    return (long) (spanForWeekdays.TotalDays / 7.0);
                case DateInterval.WeekOfYear:
                    DateTime dateOneModified = dateOne;
                    DateTime dateTwoModified = dateTwo;
                    while (System.Globalization.DateTimeFormatInfo.CurrentInfo != null && dateTwoModified.DayOfWeek !=
                           System.Globalization.DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek) {
                        dateTwoModified = dateTwoModified.AddDays(-1);
                    }

                    while (System.Globalization.DateTimeFormatInfo.CurrentInfo != null && dateOneModified.DayOfWeek !=
                           System.Globalization.DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek) {
                        dateOneModified = dateOneModified.AddDays(-1);
                    }

                    TimeSpan spanForWeekOfYear = dateTwoModified - dateOneModified;
                    return (long) (spanForWeekOfYear.TotalDays / 7.0);
                case DateInterval.Year:
                    return dateTwo.Year - dateOne.Year;
                default:
                    return 0;
            }
        }

        public override string ToString() {
            string datesStr = string.Empty;

            foreach (var  dates in _groupByMonth)
            {
                _datesList.Clear();
                foreach (DateTime date in dates)
                {
                    _datesList.Add(date);
                }

                // TODO put CeaseDaysAsString() method here.

                datesStr += CeaseDaysAsString(_datesList);
            }

            return datesStr.TrimEnd('-');
        }

        private string CeaseDaysAsString(List<DateTime> list)
        {
            // Return fast if list is null or contains less than 2 items
            if (list == null || !list.Any()) return string.Empty;
            if (list.Count == 1) return list[0].ToShortDateString();

            var rangeString = new StringBuilder();
            bool isRange = false;
            int rangeCount = 0;
            int difference = 0;

            for (int i = 0; i < list.Count; i++)
            {
                while (i < list.Count - 1 && DateDiff(DateInterval.Day, list[i], list[i + 1]) == 1)
                {
                    if (!isRange) rangeString.Append($" # from {list[i]:d/M}");
                    isRange = true;
                    rangeCount++;
                    i++;
                }

                if (isRange)
                {
                    rangeString.Append(" to ");
                    isRange = false;
                    // This line is ok.....
                    rangeString.Replace("#", $"{rangeCount + 1}" + DayToken(rangeCount + 1));

                    rangeString.Replace("@", $"{difference - 1}");
                    rangeCount = 0;
                    difference = 0;
                }

                rangeString.Append($"{list[i]:d},");

                if (difference == 0)
                    rangeString.Append(" (@) ");

                difference++;
            }

            rangeString.Replace(" (0) ", "");
            rangeString.Replace(" (@) ", "");

            return rangeString.ToString().TrimEnd(','); // ... ... 
        }

        private string DayToken(int n)
        {
            if (n == 1)
                return " day";

            return " days";
        }
    }
}
