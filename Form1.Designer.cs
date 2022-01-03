namespace Halleys_Comet_2019
{
    partial class HC_Form
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
            this.txt_Location = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Directions = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.UserCmd = new System.Windows.Forms.TextBox();
            this.txt_Additional = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmd_OK = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_Location
            // 
            this.txt_Location.Location = new System.Drawing.Point(85, 200);
            this.txt_Location.Name = "txt_Location";
            this.txt_Location.ReadOnly = true;
            this.txt_Location.Size = new System.Drawing.Size(412, 20);
            this.txt_Location.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 203);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Location";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 257);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Directions";
            // 
            // Directions
            // 
            this.Directions.Location = new System.Drawing.Point(85, 257);
            this.Directions.Name = "Directions";
            this.Directions.ReadOnly = true;
            this.Directions.Size = new System.Drawing.Size(364, 20);
            this.Directions.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 298);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Command";
            // 
            // UserCmd
            // 
            this.UserCmd.Location = new System.Drawing.Point(85, 294);
            this.UserCmd.Name = "UserCmd";
            this.UserCmd.Size = new System.Drawing.Size(272, 20);
            this.UserCmd.TabIndex = 5;
            // 
            // txt_Additional
            // 
            this.txt_Additional.Location = new System.Drawing.Point(91, 346);
            this.txt_Additional.Name = "txt_Additional";
            this.txt_Additional.ReadOnly = true;
            this.txt_Additional.Size = new System.Drawing.Size(406, 20);
            this.txt_Additional.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 343);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Response";
            // 
            // cmd_OK
            // 
            this.cmd_OK.Location = new System.Drawing.Point(386, 292);
            this.cmd_OK.Name = "cmd_OK";
            this.cmd_OK.Size = new System.Drawing.Size(53, 19);
            this.cmd_OK.TabIndex = 8;
            this.cmd_OK.Text = "OK";
            this.cmd_OK.UseVisualStyleBackColor = true;
            this.cmd_OK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(18, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(500, 155);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // HC_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 420);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cmd_OK);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_Additional);
            this.Controls.Add(this.UserCmd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Directions);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_Location);
            this.Name = "HC_Form";
            this.Text = "Halley\'s Comet";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Location;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Directions;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox UserCmd;
        private System.Windows.Forms.TextBox txt_Additional;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmd_OK;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

