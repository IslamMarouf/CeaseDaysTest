namespace TelerikWinFormsApp1
{
    partial class RadForm1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.radCalendar1 = new Telerik.WinControls.UI.RadCalendar();
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            this.radTextBoxControl1 = new Telerik.WinControls.UI.RadTextBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.radCalendar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radCalendar1
            // 
            this.radCalendar1.AllowMultipleSelect = true;
            this.radCalendar1.AllowViewSelector = true;
            this.radCalendar1.FirstDayOfWeek = Telerik.WinControls.UI.FirstDayOfWeek.Sunday;
            this.radCalendar1.Location = new System.Drawing.Point(275, 12);
            this.radCalendar1.Name = "radCalendar1";
            this.radCalendar1.Size = new System.Drawing.Size(257, 227);
            this.radCalendar1.TabIndex = 0;
            // 
            // radButton1
            // 
            this.radButton1.Location = new System.Drawing.Point(12, 245);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(110, 24);
            this.radButton1.TabIndex = 2;
            this.radButton1.Text = "Display Dates";
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // radTextBoxControl1
            // 
            this.radTextBoxControl1.Location = new System.Drawing.Point(12, 12);
            this.radTextBoxControl1.Multiline = true;
            this.radTextBoxControl1.Name = "radTextBoxControl1";
            this.radTextBoxControl1.Size = new System.Drawing.Size(257, 227);
            this.radTextBoxControl1.TabIndex = 3;
            // 
            // RadForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 275);
            this.Controls.Add(this.radTextBoxControl1);
            this.Controls.Add(this.radButton1);
            this.Controls.Add(this.radCalendar1);
            this.Name = "RadForm1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Display Dates";
            this.Load += new System.EventHandler(this.RadForm1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radCalendar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadCalendar radCalendar1;
        private Telerik.WinControls.UI.RadButton radButton1;
        private Telerik.WinControls.UI.RadTextBoxControl radTextBoxControl1;
    }
}