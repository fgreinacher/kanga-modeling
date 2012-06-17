using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using KangaModeling.Facade;

namespace KangaModeling.Gui
{
    public partial class MainForm : Form
    {
        private string m_LastTitle;

        public MainForm()
        {
            m_LastTitle = "noname";

            InitializeComponent();

            if (!Properties.Settings.Default.EnableDebugStyle)
            {
                debugCheckBox.Visible = false;
            }

            styleComboBox.DataSource = Enum.GetValues(typeof(DiagramStyle));

            Compile();
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
            errors = errors.ToArray();

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

            var listViewItems = errors
                .Select(error => new ListViewItem(new[] { error.Message, error.TokenLine.ToString(CultureInfo.InvariantCulture), error.TokenStart.ToString(CultureInfo.InvariantCulture), error.TokenValue }, 0) { Tag = error })
                .ToArray();
            listBoxErrors.Items.AddRange(listViewItems);
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

        private void CompileButtonClick(object sender, EventArgs e)
        {
            Compile();
        }

        private void ListBoxErrorsSelectedValueChanged(object sender, EventArgs e)
        {
            if (listBoxErrors.SelectedItems.Count == 0)
            {
                return;
            }

            var error = listBoxErrors.SelectedItems[0].Tag as DiagramError;
            if (error == null)
            {
                return;
            }

            SelectErrorInEditor(error);
            Invoke((MethodInvoker)(() => inputTextBox.Focus()));
        }


        private void InputTextBoxTextChanged(object sender, EventArgs e)
        {
            Compile();
        }

        private void ButtonSample1Click(object sender, EventArgs e)
        {
            inputTextBox.AppendText("title Hello world!" + Environment.NewLine);
        }

        private void ButtonSample2Click(object sender, EventArgs e)
        {
            inputTextBox.AppendText("A->B" + Environment.NewLine);
        }

        private void ButtonSample3Click(object sender, EventArgs e)
        {
            inputTextBox.AppendText("B-->A" + Environment.NewLine);
        }

        private void ButtonSample4Click(object sender, EventArgs e)
        {
            inputTextBox.AppendText("activate B" + Environment.NewLine);
        }

        private void ButtonSample5Click(object sender, EventArgs e)
        {
            inputTextBox.AppendText("deactivate B" + Environment.NewLine);
        }

        private void ButtonBigSampleClick(object sender, EventArgs e)
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
                    @"end"
                };
        }

        private void ButtonNesteAdtivitiesClick(object sender, EventArgs e)
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

        private void ButtonSaveClick(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "PNG files|*.png",
                DefaultExt = "png",
                FileName = m_LastTitle
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            outputPictureBox.Image.Save(saveFileDialog.FileName);
        }

        private void StyleComboBoxSelectedValueChanged(object sender, EventArgs e)
        {
            Compile();
        }

        private void DebugCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            Compile();
        }
    }
}
