namespace KangaModeling.Gui
{
	partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.compileButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.debugCheckBox = new System.Windows.Forms.CheckBox();
            this.styleComboBox = new System.Windows.Forms.ComboBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonNesteAdtivities = new System.Windows.Forms.Button();
            this.buttonBigSample = new System.Windows.Forms.Button();
            this.buttonSample5 = new System.Windows.Forms.Button();
            this.buttonSample4 = new System.Windows.Forms.Button();
            this.buttonSample3 = new System.Windows.Forms.Button();
            this.buttonSample2 = new System.Windows.Forms.Button();
            this.buttonSample1 = new System.Windows.Forms.Button();
            this.inputAndOutputSplitContainer = new System.Windows.Forms.SplitContainer();
            this.inputAndErrorsSplitContainer = new System.Windows.Forms.SplitContainer();
            this.inputTextBox = new System.Windows.Forms.RichTextBox();
            this.listBoxErrors = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.outputImagePanel = new System.Windows.Forms.Panel();
            this.outputPictureBox = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputAndOutputSplitContainer)).BeginInit();
            this.inputAndOutputSplitContainer.Panel1.SuspendLayout();
            this.inputAndOutputSplitContainer.Panel2.SuspendLayout();
            this.inputAndOutputSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputAndErrorsSplitContainer)).BeginInit();
            this.inputAndErrorsSplitContainer.Panel1.SuspendLayout();
            this.inputAndErrorsSplitContainer.Panel2.SuspendLayout();
            this.inputAndErrorsSplitContainer.SuspendLayout();
            this.outputImagePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // compileButton
            // 
            this.compileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.compileButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.compileButton.Location = new System.Drawing.Point(-1, 323);
            this.compileButton.Name = "compileButton";
            this.compileButton.Size = new System.Drawing.Size(79, 45);
            this.compileButton.TabIndex = 1;
            this.compileButton.Text = "Compile";
            this.compileButton.UseVisualStyleBackColor = true;
            this.compileButton.Click += new System.EventHandler(this.CompileButtonClick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.debugCheckBox);
            this.panel1.Controls.Add(this.styleComboBox);
            this.panel1.Controls.Add(this.buttonSave);
            this.panel1.Controls.Add(this.buttonNesteAdtivities);
            this.panel1.Controls.Add(this.buttonBigSample);
            this.panel1.Controls.Add(this.buttonSample5);
            this.panel1.Controls.Add(this.buttonSample4);
            this.panel1.Controls.Add(this.buttonSample3);
            this.panel1.Controls.Add(this.compileButton);
            this.panel1.Controls.Add(this.buttonSample2);
            this.panel1.Controls.Add(this.buttonSample1);
            this.panel1.Location = new System.Drawing.Point(13, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(79, 368);
            this.panel1.TabIndex = 5;
            // 
            // debugCheckBox
            // 
            this.debugCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.debugCheckBox.AutoSize = true;
            this.debugCheckBox.Location = new System.Drawing.Point(0, 210);
            this.debugCheckBox.Name = "debugCheckBox";
            this.debugCheckBox.Size = new System.Drawing.Size(57, 17);
            this.debugCheckBox.TabIndex = 9;
            this.debugCheckBox.Text = "Debug";
            this.debugCheckBox.UseVisualStyleBackColor = true;
            this.debugCheckBox.CheckedChanged += new System.EventHandler(this.DebugCheckBoxCheckedChanged);
            // 
            // styleComboBox
            // 
            this.styleComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.styleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.styleComboBox.FormattingEnabled = true;
            this.styleComboBox.Location = new System.Drawing.Point(0, 233);
            this.styleComboBox.Name = "styleComboBox";
            this.styleComboBox.Size = new System.Drawing.Size(79, 21);
            this.styleComboBox.TabIndex = 8;
            this.styleComboBox.SelectedValueChanged += new System.EventHandler(this.StyleComboBoxSelectedValueChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonSave.Image = global::KangaModeling.Gui.Properties.Resources.Save_icon1;
            this.buttonSave.Location = new System.Drawing.Point(-1, 260);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(79, 57);
            this.buttonSave.TabIndex = 7;
            this.buttonSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSaveClick);
            // 
            // buttonNesteAdtivities
            // 
            this.buttonNesteAdtivities.Location = new System.Drawing.Point(-1, 174);
            this.buttonNesteAdtivities.Name = "buttonNesteAdtivities";
            this.buttonNesteAdtivities.Size = new System.Drawing.Size(80, 23);
            this.buttonNesteAdtivities.TabIndex = 6;
            this.buttonNesteAdtivities.Text = "Nested";
            this.buttonNesteAdtivities.UseVisualStyleBackColor = true;
            this.buttonNesteAdtivities.Click += new System.EventHandler(this.ButtonNesteAdtivitiesClick);
            // 
            // buttonBigSample
            // 
            this.buttonBigSample.Location = new System.Drawing.Point(-1, 145);
            this.buttonBigSample.Name = "buttonBigSample";
            this.buttonBigSample.Size = new System.Drawing.Size(80, 23);
            this.buttonBigSample.TabIndex = 5;
            this.buttonBigSample.Text = "Big Sample";
            this.buttonBigSample.UseVisualStyleBackColor = true;
            this.buttonBigSample.Click += new System.EventHandler(this.ButtonBigSampleClick);
            // 
            // buttonSample5
            // 
            this.buttonSample5.Location = new System.Drawing.Point(-1, 116);
            this.buttonSample5.Name = "buttonSample5";
            this.buttonSample5.Size = new System.Drawing.Size(80, 23);
            this.buttonSample5.TabIndex = 4;
            this.buttonSample5.Text = "Deactivate";
            this.buttonSample5.UseVisualStyleBackColor = true;
            this.buttonSample5.Click += new System.EventHandler(this.ButtonSample5Click);
            // 
            // buttonSample4
            // 
            this.buttonSample4.Location = new System.Drawing.Point(-1, 87);
            this.buttonSample4.Name = "buttonSample4";
            this.buttonSample4.Size = new System.Drawing.Size(80, 23);
            this.buttonSample4.TabIndex = 3;
            this.buttonSample4.Text = "Activate";
            this.buttonSample4.UseVisualStyleBackColor = true;
            this.buttonSample4.Click += new System.EventHandler(this.ButtonSample4Click);
            // 
            // buttonSample3
            // 
            this.buttonSample3.Location = new System.Drawing.Point(-1, 58);
            this.buttonSample3.Name = "buttonSample3";
            this.buttonSample3.Size = new System.Drawing.Size(80, 23);
            this.buttonSample3.TabIndex = 2;
            this.buttonSample3.Text = "Return";
            this.buttonSample3.UseVisualStyleBackColor = true;
            this.buttonSample3.Click += new System.EventHandler(this.ButtonSample3Click);
            // 
            // buttonSample2
            // 
            this.buttonSample2.Location = new System.Drawing.Point(0, 29);
            this.buttonSample2.Name = "buttonSample2";
            this.buttonSample2.Size = new System.Drawing.Size(80, 23);
            this.buttonSample2.TabIndex = 1;
            this.buttonSample2.Text = "Call";
            this.buttonSample2.UseVisualStyleBackColor = true;
            this.buttonSample2.Click += new System.EventHandler(this.ButtonSample2Click);
            // 
            // buttonSample1
            // 
            this.buttonSample1.Location = new System.Drawing.Point(-1, 0);
            this.buttonSample1.Name = "buttonSample1";
            this.buttonSample1.Size = new System.Drawing.Size(80, 23);
            this.buttonSample1.TabIndex = 0;
            this.buttonSample1.Text = "Title";
            this.buttonSample1.UseVisualStyleBackColor = true;
            this.buttonSample1.Click += new System.EventHandler(this.ButtonSample1Click);
            // 
            // inputAndOutputSplitContainer
            // 
            this.inputAndOutputSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputAndOutputSplitContainer.Location = new System.Drawing.Point(99, 12);
            this.inputAndOutputSplitContainer.Name = "inputAndOutputSplitContainer";
            // 
            // inputAndOutputSplitContainer.Panel1
            // 
            this.inputAndOutputSplitContainer.Panel1.Controls.Add(this.inputAndErrorsSplitContainer);
            // 
            // inputAndOutputSplitContainer.Panel2
            // 
            this.inputAndOutputSplitContainer.Panel2.Controls.Add(this.outputImagePanel);
            this.inputAndOutputSplitContainer.Size = new System.Drawing.Size(632, 368);
            this.inputAndOutputSplitContainer.SplitterDistance = 241;
            this.inputAndOutputSplitContainer.TabIndex = 9;
            // 
            // inputAndErrorsSplitContainer
            // 
            this.inputAndErrorsSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputAndErrorsSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.inputAndErrorsSplitContainer.Name = "inputAndErrorsSplitContainer";
            this.inputAndErrorsSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // inputAndErrorsSplitContainer.Panel1
            // 
            this.inputAndErrorsSplitContainer.Panel1.Controls.Add(this.inputTextBox);
            // 
            // inputAndErrorsSplitContainer.Panel2
            // 
            this.inputAndErrorsSplitContainer.Panel2.Controls.Add(this.listBoxErrors);
            this.inputAndErrorsSplitContainer.Size = new System.Drawing.Size(241, 368);
            this.inputAndErrorsSplitContainer.SplitterDistance = 174;
            this.inputAndErrorsSplitContainer.TabIndex = 0;
            // 
            // inputTextBox
            // 
            this.inputTextBox.AcceptsTab = true;
            this.inputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputTextBox.Location = new System.Drawing.Point(0, 0);
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(241, 174);
            this.inputTextBox.TabIndex = 11;
            this.inputTextBox.Text = "title Kanga Modeling\n\nApp->User: Enjoy Kanga Modeling!";
            this.inputTextBox.WordWrap = false;
            this.inputTextBox.TextChanged += new System.EventHandler(this.InputTextBoxTextChanged);
            // 
            // listBoxErrors
            // 
            this.listBoxErrors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listBoxErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxErrors.GridLines = true;
            this.listBoxErrors.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listBoxErrors.Location = new System.Drawing.Point(0, 0);
            this.listBoxErrors.Name = "listBoxErrors";
            this.listBoxErrors.Size = new System.Drawing.Size(241, 190);
            this.listBoxErrors.TabIndex = 11;
            this.listBoxErrors.UseCompatibleStateImageBehavior = false;
            this.listBoxErrors.View = System.Windows.Forms.View.Details;
            this.listBoxErrors.SelectedIndexChanged += new System.EventHandler(this.ListBoxErrorsSelectedValueChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Description";
            this.columnHeader2.Width = 189;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Line";
            this.columnHeader3.Width = 42;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Column";
            this.columnHeader4.Width = 54;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Token";
            // 
            // outputImagePanel
            // 
            this.outputImagePanel.AutoScroll = true;
            this.outputImagePanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.outputImagePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.outputImagePanel.Controls.Add(this.outputPictureBox);
            this.outputImagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputImagePanel.Location = new System.Drawing.Point(0, 0);
            this.outputImagePanel.Name = "outputImagePanel";
            this.outputImagePanel.Padding = new System.Windows.Forms.Padding(10);
            this.outputImagePanel.Size = new System.Drawing.Size(387, 368);
            this.outputImagePanel.TabIndex = 9;
            // 
            // outputPictureBox
            // 
            this.outputPictureBox.BackColor = System.Drawing.SystemColors.Control;
            this.outputPictureBox.Location = new System.Drawing.Point(10, 10);
            this.outputPictureBox.Name = "outputPictureBox";
            this.outputPictureBox.Size = new System.Drawing.Size(97, 51);
            this.outputPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.outputPictureBox.TabIndex = 3;
            this.outputPictureBox.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 392);
            this.Controls.Add(this.inputAndOutputSplitContainer);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 430);
            this.Name = "MainForm";
            this.Text = "Kanga Modeling 1.0";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.inputAndOutputSplitContainer.Panel1.ResumeLayout(false);
            this.inputAndOutputSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.inputAndOutputSplitContainer)).EndInit();
            this.inputAndOutputSplitContainer.ResumeLayout(false);
            this.inputAndErrorsSplitContainer.Panel1.ResumeLayout(false);
            this.inputAndErrorsSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.inputAndErrorsSplitContainer)).EndInit();
            this.inputAndErrorsSplitContainer.ResumeLayout(false);
            this.outputImagePanel.ResumeLayout(false);
            this.outputImagePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputPictureBox)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button compileButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonSample5;
        private System.Windows.Forms.Button buttonSample4;
        private System.Windows.Forms.Button buttonSample3;
        private System.Windows.Forms.Button buttonSample2;
        private System.Windows.Forms.Button buttonSample1;
		private System.Windows.Forms.Button buttonBigSample;
		private System.Windows.Forms.SplitContainer inputAndOutputSplitContainer;
		private System.Windows.Forms.Panel outputImagePanel;
		private System.Windows.Forms.PictureBox outputPictureBox;
		private System.Windows.Forms.SplitContainer inputAndErrorsSplitContainer;
		private System.Windows.Forms.RichTextBox inputTextBox;
		private System.Windows.Forms.ListView listBoxErrors;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button buttonNesteAdtivities;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ComboBox styleComboBox;
        private System.Windows.Forms.CheckBox debugCheckBox;
	}
}

