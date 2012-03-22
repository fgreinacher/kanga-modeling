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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GuiRunnerForm));
            this.compileButton = new System.Windows.Forms.Button();
            this.outputPictureBox = new System.Windows.Forms.PictureBox();
            this.checkBoxImidiateCompile = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonSample5 = new System.Windows.Forms.Button();
            this.buttonSample4 = new System.Windows.Forms.Button();
            this.buttonSample3 = new System.Windows.Forms.Button();
            this.buttonSample2 = new System.Windows.Forms.Button();
            this.buttonSample1 = new System.Windows.Forms.Button();
            this.inputTextBox = new System.Windows.Forms.RichTextBox();
            this.listBoxErrors = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageListGrid = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.outputPictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // compileButton
            // 
            this.compileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.compileButton.Location = new System.Drawing.Point(10, 173);
            this.compileButton.Name = "compileButton";
            this.compileButton.Size = new System.Drawing.Size(102, 29);
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
            this.outputPictureBox.Location = new System.Drawing.Point(13, 221);
            this.outputPictureBox.Name = "outputPictureBox";
            this.outputPictureBox.Size = new System.Drawing.Size(776, 258);
            this.outputPictureBox.TabIndex = 2;
            this.outputPictureBox.TabStop = false;
            // 
            // checkBoxImidiateCompile
            // 
            this.checkBoxImidiateCompile.AutoSize = true;
            this.checkBoxImidiateCompile.Checked = true;
            this.checkBoxImidiateCompile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxImidiateCompile.Location = new System.Drawing.Point(10, 150);
            this.checkBoxImidiateCompile.Name = "checkBoxImidiateCompile";
            this.checkBoxImidiateCompile.Size = new System.Drawing.Size(106, 17);
            this.checkBoxImidiateCompile.TabIndex = 4;
            this.checkBoxImidiateCompile.Text = "Compile as I type";
            this.checkBoxImidiateCompile.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonSample5);
            this.panel1.Controls.Add(this.buttonSample4);
            this.panel1.Controls.Add(this.buttonSample3);
            this.panel1.Controls.Add(this.checkBoxImidiateCompile);
            this.panel1.Controls.Add(this.compileButton);
            this.panel1.Controls.Add(this.buttonSample2);
            this.panel1.Controls.Add(this.buttonSample1);
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(116, 205);
            this.panel1.TabIndex = 5;
            // 
            // buttonSample5
            // 
            this.buttonSample5.Location = new System.Drawing.Point(4, 120);
            this.buttonSample5.Name = "buttonSample5";
            this.buttonSample5.Size = new System.Drawing.Size(109, 23);
            this.buttonSample5.TabIndex = 4;
            this.buttonSample5.Text = "Sample \'deactivate\'";
            this.buttonSample5.UseVisualStyleBackColor = true;
            this.buttonSample5.Click += new System.EventHandler(this.buttonSample5_Click);
            // 
            // buttonSample4
            // 
            this.buttonSample4.Location = new System.Drawing.Point(4, 91);
            this.buttonSample4.Name = "buttonSample4";
            this.buttonSample4.Size = new System.Drawing.Size(109, 23);
            this.buttonSample4.TabIndex = 3;
            this.buttonSample4.Text = "Sample \'activate\'";
            this.buttonSample4.UseVisualStyleBackColor = true;
            this.buttonSample4.Click += new System.EventHandler(this.buttonSample4_Click);
            // 
            // buttonSample3
            // 
            this.buttonSample3.Location = new System.Drawing.Point(4, 62);
            this.buttonSample3.Name = "buttonSample3";
            this.buttonSample3.Size = new System.Drawing.Size(109, 23);
            this.buttonSample3.TabIndex = 2;
            this.buttonSample3.Text = "Sample \'B-->A\'";
            this.buttonSample3.UseVisualStyleBackColor = true;
            this.buttonSample3.Click += new System.EventHandler(this.buttonSample3_Click);
            // 
            // buttonSample2
            // 
            this.buttonSample2.Location = new System.Drawing.Point(3, 33);
            this.buttonSample2.Name = "buttonSample2";
            this.buttonSample2.Size = new System.Drawing.Size(109, 23);
            this.buttonSample2.TabIndex = 1;
            this.buttonSample2.Text = "Sample \'A->B\'";
            this.buttonSample2.UseVisualStyleBackColor = true;
            this.buttonSample2.Click += new System.EventHandler(this.buttonSample2_Click);
            // 
            // buttonSample1
            // 
            this.buttonSample1.Location = new System.Drawing.Point(4, 4);
            this.buttonSample1.Name = "buttonSample1";
            this.buttonSample1.Size = new System.Drawing.Size(109, 23);
            this.buttonSample1.TabIndex = 0;
            this.buttonSample1.Text = "Sample \'title\'";
            this.buttonSample1.UseVisualStyleBackColor = true;
            this.buttonSample1.Click += new System.EventHandler(this.buttonSample1_Click);
            // 
            // inputTextBox
            // 
            this.inputTextBox.Font = new System.Drawing.Font("Lucida Console", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputTextBox.Location = new System.Drawing.Point(135, 13);
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(654, 96);
            this.inputTextBox.TabIndex = 6;
            this.inputTextBox.Text = "";
            this.inputTextBox.WordWrap = false;
            this.inputTextBox.TextChanged += new System.EventHandler(this.inputTextBox_TextChanged);
            // 
            // listBoxErrors
            // 
            this.listBoxErrors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxErrors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listBoxErrors.GridLines = true;
            this.listBoxErrors.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listBoxErrors.Location = new System.Drawing.Point(135, 115);
            this.listBoxErrors.Name = "listBoxErrors";
            this.listBoxErrors.Size = new System.Drawing.Size(654, 100);
            this.listBoxErrors.SmallImageList = this.imageListGrid;
            this.listBoxErrors.TabIndex = 7;
            this.listBoxErrors.UseCompatibleStateImageBehavior = false;
            this.listBoxErrors.View = System.Windows.Forms.View.Details;
            this.listBoxErrors.SelectedIndexChanged += new System.EventHandler(this.listBoxErrors_SelectedValueChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Description";
            this.columnHeader2.Width = 358;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Line";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Column";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Token";
            // 
            // imageListGrid
            // 
            this.imageListGrid.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListGrid.ImageStream")));
            this.imageListGrid.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListGrid.Images.SetKeyName(0, "Close.ico");
            // 
            // GuiRunnerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 491);
            this.Controls.Add(this.listBoxErrors);
            this.Controls.Add(this.inputTextBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.outputPictureBox);
            this.Name = "GuiRunnerForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.outputPictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Button compileButton;
        private System.Windows.Forms.PictureBox outputPictureBox;
        private System.Windows.Forms.CheckBox checkBoxImidiateCompile;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonSample5;
        private System.Windows.Forms.Button buttonSample4;
        private System.Windows.Forms.Button buttonSample3;
        private System.Windows.Forms.Button buttonSample2;
        private System.Windows.Forms.Button buttonSample1;
        private System.Windows.Forms.RichTextBox inputTextBox;
        private System.Windows.Forms.ListView listBoxErrors;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ImageList imageListGrid;
	}
}

