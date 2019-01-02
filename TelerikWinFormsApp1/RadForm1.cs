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
        

        public RadForm1() {
            InitializeComponent();
        }

        private void RadForm1_Load(object sender, EventArgs e) {
            
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
    }
}