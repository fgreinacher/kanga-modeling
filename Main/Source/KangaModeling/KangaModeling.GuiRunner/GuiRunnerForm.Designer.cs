namespace KangaModeling.GuiRunner
{
	partial class GuiRunnerForm
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
			this.inputTextBox = new System.Windows.Forms.TextBox();
			this.compileButton = new System.Windows.Forms.Button();
			this.outputPictureBox = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.outputPictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// inputTextBox
			// 
			this.inputTextBox.AcceptsReturn = true;
			this.inputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.inputTextBox.Location = new System.Drawing.Point(13, 13);
			this.inputTextBox.Multiline = true;
			this.inputTextBox.Name = "inputTextBox";
			this.inputTextBox.Size = new System.Drawing.Size(371, 92);
			this.inputTextBox.TabIndex = 0;
			// 
			// compileButton
			// 
			this.compileButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.compileButton.Location = new System.Drawing.Point(13, 112);
			this.compileButton.Name = "compileButton";
			this.compileButton.Size = new System.Drawing.Size(371, 32);
			this.compileButton.TabIndex = 1;
			this.compileButton.Text = "Compile";
			this.compileButton.UseVisualStyleBackColor = true;
			this.compileButton.Click += new System.EventHandler(this.compileButton_Click);
			// 
			// outputPictureBox
			// 
			this.outputPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.outputPictureBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.outputPictureBox.Location = new System.Drawing.Point(13, 151);
			this.outputPictureBox.Name = "outputPictureBox";
			this.outputPictureBox.Size = new System.Drawing.Size(371, 192);
			this.outputPictureBox.TabIndex = 2;
			this.outputPictureBox.TabStop = false;
			// 
			// GuiRunnerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(396, 355);
			this.Controls.Add(this.outputPictureBox);
			this.Controls.Add(this.compileButton);
			this.Controls.Add(this.inputTextBox);
			this.Name = "GuiRunnerForm";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.outputPictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox inputTextBox;
		private System.Windows.Forms.Button compileButton;
		private System.Windows.Forms.PictureBox outputPictureBox;
	}
}

