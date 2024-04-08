namespace WinFormsNorthwind
{
    partial class FormAirports
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
            btnLoadAirports = new Button();
            lstAirports = new ListBox();
            SuspendLayout();
            // 
            // btnLoadAirports
            // 
            btnLoadAirports.Location = new Point(12, 12);
            btnLoadAirports.Name = "btnLoadAirports";
            btnLoadAirports.Size = new Size(420, 23);
            btnLoadAirports.TabIndex = 0;
            btnLoadAirports.Text = "Cargar aeropuertos";
            btnLoadAirports.UseVisualStyleBackColor = true;
            btnLoadAirports.Click += btnLoadAirports_Click;
            // 
            // lstAirports
            // 
            lstAirports.FormattingEnabled = true;
            lstAirports.HorizontalScrollbar = true;
            lstAirports.ItemHeight = 15;
            lstAirports.Location = new Point(12, 41);
            lstAirports.Name = "lstAirports";
            lstAirports.Size = new Size(420, 349);
            lstAirports.TabIndex = 1;
            // 
            // FormAirports
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(444, 408);
            Controls.Add(lstAirports);
            Controls.Add(btnLoadAirports);
            Name = "FormAirports";
            Text = "FormAirports";
            ResumeLayout(false);
        }

        #endregion

        private Button btnLoadAirports;
        private ListBox lstAirports;
    }
}