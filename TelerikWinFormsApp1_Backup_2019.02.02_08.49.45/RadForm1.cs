﻿using System;
using System.Drawing;
using Telerik.WinControls.UI;

namespace TelerikWinFormsApp1
{
    public partial class RadForm1 : RadForm
    {
        private CeaseDaysFormatter _ceaseDays;

        public RadForm1() {
            InitializeComponent();
        }

        private void RadForm1_Load(object sender, EventArgs e) {
        }

        private void radButton1_Click(object sender, EventArgs e) {
            radCalendar1.Text = "";
            _ceaseDays = new CeaseDaysFormatter(radCalendar1.SelectedDates);
            radTextBoxControl1.Text = _ceaseDays.ToString();

        }

        private void btnOK_Click(object sender, EventArgs e) {
            Close();
        }

        private void btnClear_Click(object sender, EventArgs e) {
            radCalendar1.SelectedDates.Clear();

            radTextBoxControl1.Clear();
        }
    }
}