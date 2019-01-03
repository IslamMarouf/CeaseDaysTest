using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Telerik.WinControls.UI;

namespace TelerikWinFormsApp1
{
    public partial class RadForm1 : RadForm {
        private CeaseDaysFormatter CeaseDays;

        public RadForm1() {
            InitializeComponent();
            
        }

        private void RadForm1_Load(object sender, EventArgs e) {
        }

        private void radButton1_Click(object sender, EventArgs e) {
            radCalendar1.Text = "";
            CeaseDays = new CeaseDaysFormatter(radCalendar1.SelectedDates);
            radTextBoxControl1.Text = CeaseDays.ToString();
        }
    }
}