using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using KangaModeling.Facade;

namespace KangaModeling.GuiRunner
{
    public partial class GuiRunnerForm : Form
    {
        private string m_LastTitle;

        public GuiRunnerForm()
        {
            m_LastTitle = "noname";

            InitializeComponent();

            styleComboBox.DataSource = Enum.GetValues(typeof(DiagramStyle));
        }
        
        private void Compile()
        {
            var diagramArguments = new DiagramArguments(inputTextBox.Text, DiagramType.Sequence, (DiagramStyle)styleComboBox.SelectedItem, debugCheckBox.Checked);
            var diagramResult = DiagramFactory.Create(diagramArguments);
            ProcessResult(diagramResult);
        }

        private void ProcessResult(DiagramResult diagramResult)
        {
            ProcessName(diagramResult);
            ProcessResultImage(diagramResult.Image);
            ProcessResultErrors(diagramResult.Errors);
        }

        private void ProcessName(DiagramResult diagramResult)
        {
            m_LastTitle = diagramResult.Name;
        }

        private void ProcessResultImage(Image image)
        {
            outputPictureBox.Image = image;
        }

        private void ProcessResultErrors(IEnumerable<DiagramError> errors)
        {
            FillErrorList(errors);
            HighlightErrorsInEditor(errors);
        }

        private void HighlightErrorsInEditor(IEnumerable<DiagramError> errors)
        {
            int rememberStart = inputTextBox.SelectionStart;
            int rememberLength = inputTextBox.SelectionLength;

            inputTextBox.SelectAll();
            inputTextBox.SelectionColor = Color.Navy;
            inputTextBox.SelectionBackColor = Color.White;

            foreach (var error in errors)
            {
                SelectErrorInEditor(error);
                inputTextBox.SelectionColor = Color.Red;
            }

            inputTextBox.Select(rememberStart, rememberLength);
            inputTextBox.SelectionColor = Color.Navy;
        }

        private void FillErrorList(IEnumerable<DiagramError> errors)
        {
            listBoxErrors.Items.Clear();
            foreach (var error in errors)
            {
                var listItem =
                    new ListViewItem(
                        new string[]
	                        {
                                error.Message, 
                                error.TokenLine.ToString(), 
                                error.TokenStart.ToString(), 
                                error.TokenValue},
                        0);
                listItem.Tag = error;
                listBoxErrors.Items.Add(listItem);
            }
        }

        private void SelectErrorInEditor(DiagramError error)
        {
            int zeroBasedLineNumber = error.TokenLine - 1;
            int numberOfNewLineChars = zeroBasedLineNumber;

            int startIndex =
                inputTextBox
                    .Lines
                    .Select(line => line.Length)
                    .Take(zeroBasedLineNumber)
                    .Sum()
                + numberOfNewLineChars
                + error.TokenStart;

            int tokenLength = error.TokenLength;
            inputTextBox.Select(startIndex, (tokenLength == 0) ? 1 : tokenLength);
        }

        private void compileButton_Click(object sender, EventArgs e)
        {
            Compile();
        }

        private void listBoxErrors_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBoxErrors.SelectedItems.Count == 0)
            {
                return;
            }

            DiagramError error = listBoxErrors.SelectedItems[0].Tag as DiagramError;
            if (error == null)
            {
                return;
            }

            SelectErrorInEditor(error);
            Invoke((MethodInvoker)(() => inputTextBox.Focus()));
        }


        private void inputTextBox_TextChanged(object sender, EventArgs e)
        {
            Compile();
        }

        private void buttonSample1_Click(object sender, EventArgs e)
        {
            inputTextBox.AppendText("title Hello world!" + Environment.NewLine);
        }

        private void buttonSample2_Click(object sender, EventArgs e)
        {
            inputTextBox.AppendText("A->B" + Environment.NewLine);
        }

        private void buttonSample3_Click(object sender, EventArgs e)
        {
            inputTextBox.AppendText("B-->A" + Environment.NewLine);
        }

        private void buttonSample4_Click(object sender, EventArgs e)
        {
            inputTextBox.AppendText("activate B" + Environment.NewLine);
        }

        private void buttonSample5_Click(object sender, EventArgs e)
        {
            inputTextBox.AppendText("deactivate B" + Environment.NewLine);
        }

        private void buttonBigSample_Click(object sender, EventArgs e)
        {
            inputTextBox.Lines = new[]
                {
                    @"title Diagram 1",
                    @"A->B : Ping()",
                    @"activate B",
                    @"B-->A : isOk",
                    @"deactivate B",
                    @"alt isOk",
                    @"B->C : More()",
                    @"activate C",
                    @"C-->B : result",
                    @"deactivate C",
                    @"else !isOk",
                    @"B->C : Less()",
                    @"activate C",
                    @"C-->B : result",
                    @"deactivate C",
                    @"end",
                };
        }

        private void buttonNesteAdtivities_Click(object sender, EventArgs e)
        {
            inputTextBox.Lines = new[]
                {
                    @"title Diagram 1",
                    @"A->B : Ping()",
                    @"activate B",
                    @"B->C : call C",
                    @"activate C",
                    @"C->B",
                    @"activate B",
                    @"b-->c",
                    @"deactivate B",
                    @"c->B",
                    @"deactivate C",
                    @"b-->A",
                    @"deactivate B"
                };
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog =  new SaveFileDialog();
            saveFileDialog.DefaultExt = "png";
            saveFileDialog.FileName = m_LastTitle + ".png";
            if (saveFileDialog.ShowDialog()!=DialogResult.OK)
            {
                return;
            }

            outputPictureBox.Image.Save(saveFileDialog.FileName);
        }

        private void styleComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            Compile();
        }

        private void debugCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Compile();
        }
    }
}
