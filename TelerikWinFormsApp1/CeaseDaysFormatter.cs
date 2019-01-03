using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly List<DateTime> _datesList;

        public CeaseDaysFormatter(DateTimeCollection dates) {
            Dates = dates;
            _datesList = new List<DateTime>();
            ArrangeDates(Dates);
        }

        private void ArrangeDates(DateTimeCollection dates) {
            IOrderedEnumerable<DateTime> ordered = dates.OrderBy(dt => dt.Month).ThenBy(dt => dt.Day);
            IOrderedEnumerable<DateTime> orderedByYear = ordered.OrderBy(dt => dt.Year);


            foreach (DateTime date in orderedByYear) {
                _datesList.Add(date);
            }
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

            foreach (DateTime date in _datesList) {
                datesStr += date.ToShortDateString() + "-";
            }


            return datesStr.TrimEnd('-');
        }
    }
}
