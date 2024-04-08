namespace WinFormsNorthwind
{
    partial class FormNorthwind
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnCustomers = new Button();
            lblCustomers = new Label();
            lstCustomers = new ListBox();
            SuspendLayout();
            // 
            // btnCustomers
            // 
            btnCustomers.Location = new Point(12, 12);
            btnCustomers.Name = "btnCustomers";
            btnCustomers.Size = new Size(205, 23);
            btnCustomers.TabIndex = 0;
            btnCustomers.Text = "Cargar customers";
            btnCustomers.UseVisualStyleBackColor = true;
            btnCustomers.Click += btnCustomers_Click;
            // 
            // lblCustomers
            // 
            lblCustomers.AutoSize = true;
            lblCustomers.Location = new Point(12, 48);
            lblCustomers.Name = "lblCustomers";
            lblCustomers.Size = new Size(64, 15);
            lblCustomers.TabIndex = 1;
            lblCustomers.Text = "Customers";
            // 
            // lstCustomers
            // 
            lstCustomers.FormattingEnabled = true;
            lstCustomers.ItemHeight = 15;
            lstCustomers.Location = new Point(12, 66);
            lstCustomers.Name = "lstCustomers";
            lstCustomers.Size = new Size(205, 214);
            lstCustomers.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(229, 291);
            Controls.Add(lstCustomers);
            Controls.Add(lblCustomers);
            Controls.Add(btnCustomers);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCustomers;
        private Label lblCustomers;
        private ListBox lstCustomers;
    }
}
