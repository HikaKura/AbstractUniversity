namespace AbstractUniversityView
{
    partial class FormClassroom
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxPov = new System.Windows.Forms.TextBox();
            this.textBoxNumber = new System.Windows.Forms.TextBox();
            this.textBoxStatus = new System.Windows.Forms.TextBox();
            this.labelPov = new System.Windows.Forms.Label();
            this.labelFistNumber = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(226, 145);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(131, 36);
            this.buttonCancel.TabIndex = 19;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(76, 145);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(131, 36);
            this.buttonSave.TabIndex = 18;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxPov
            // 
            this.textBoxPov.Location = new System.Drawing.Point(115, 78);
            this.textBoxPov.Name = "textBoxPov";
            this.textBoxPov.Size = new System.Drawing.Size(242, 20);
            this.textBoxPov.TabIndex = 17;
            // 
            // textBoxNumber
            // 
            this.textBoxNumber.Location = new System.Drawing.Point(115, 10);
            this.textBoxNumber.Name = "textBoxNumber";
            this.textBoxNumber.Size = new System.Drawing.Size(242, 20);
            this.textBoxNumber.TabIndex = 16;
            // 
            // textBoxStatus
            // 
            this.textBoxStatus.Location = new System.Drawing.Point(115, 43);
            this.textBoxStatus.Name = "textBoxStatus";
            this.textBoxStatus.Size = new System.Drawing.Size(242, 20);
            this.textBoxStatus.TabIndex = 15;
            // 
            // labelPov
            // 
            this.labelPov.AutoSize = true;
            this.labelPov.Location = new System.Drawing.Point(63, 78);
            this.labelPov.Name = "labelPov";
            this.labelPov.Size = new System.Drawing.Size(46, 13);
            this.labelPov.TabIndex = 14;
            this.labelPov.Text = "Корпус:";
            // 
            // labelFistNumber
            // 
            this.labelFistNumber.AutoSize = true;
            this.labelFistNumber.Location = new System.Drawing.Point(10, 13);
            this.labelFistNumber.Name = "labelFistNumber";
            this.labelFistNumber.Size = new System.Drawing.Size(99, 13);
            this.labelFistNumber.TabIndex = 13;
            this.labelFistNumber.Text = "Номер аудитории:";
            this.labelFistNumber.Click += new System.EventHandler(this.labelFistName_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(65, 46);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(44, 13);
            this.labelStatus.TabIndex = 12;
            this.labelStatus.Text = "Статус:";
            // 
            // FormClassroom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 208);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxPov);
            this.Controls.Add(this.textBoxNumber);
            this.Controls.Add(this.textBoxStatus);
            this.Controls.Add(this.labelPov);
            this.Controls.Add(this.labelFistNumber);
            this.Controls.Add(this.labelStatus);
            this.Name = "FormClassroom";
            this.Text = "Данные об аудитории";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxPov;
        private System.Windows.Forms.TextBox textBoxNumber;
        private System.Windows.Forms.TextBox textBoxStatus;
        private System.Windows.Forms.Label labelPov;
        private System.Windows.Forms.Label labelFistNumber;
        private System.Windows.Forms.Label labelStatus;
    }
}