namespace SimpleActivities
{
    partial class InputIntForm
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
            this.NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.InputNumberLabel = new System.Windows.Forms.Label();
            this.OkButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // NumericUpDown
            // 
            this.NumericUpDown.Location = new System.Drawing.Point(13, 50);
            this.NumericUpDown.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NumericUpDown.Name = "NumericUpDown";
            this.NumericUpDown.Size = new System.Drawing.Size(125, 26);
            this.NumericUpDown.TabIndex = 0;
            // 
            // InputNumberLabel
            // 
            this.InputNumberLabel.AutoSize = true;
            this.InputNumberLabel.Location = new System.Drawing.Point(10, 20);
            this.InputNumberLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.InputNumberLabel.Name = "InputNumberLabel";
            this.InputNumberLabel.Size = new System.Drawing.Size(125, 20);
            this.InputNumberLabel.TabIndex = 1;
            this.InputNumberLabel.Text = "Введите число";
            // 
            // OkButton
            // 
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(163, 50);
            this.OkButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(88, 35);
            this.OkButton.TabIndex = 2;
            this.OkButton.Text = "Готово";
            this.OkButton.UseVisualStyleBackColor = true;
            // 
            // InputIntForm
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 96);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.InputNumberLabel);
            this.Controls.Add(this.NumericUpDown);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputIntForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Введите число";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown NumericUpDown;
        private System.Windows.Forms.Label InputNumberLabel;
        private System.Windows.Forms.Button OkButton;
    }
}