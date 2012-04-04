using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics.GdiPlus;
using KangaModeling.Graphics;
using KangaModeling.Visuals.SequenceDiagrams;

namespace KangaModeling.GuiRunner
{
    public partial class GuiRunnerForm : Form
    {
        public GuiRunnerForm()
        {
            InitializeComponent();
        }

        private void Compile()
		{
            IEnumerable<ModelError> errors;
            var sequenceDiagram = DiagramCreator.CreateFrom(inputTextBox.Text, out errors);
            ShowErrors(errors);

            var sequenceDiagramVisual = new SequenceDiagramVisual(sequenceDiagram);

			using (var measureBitmap = new Bitmap(1, 1))
			using (var measureGraphics = System.Drawing.Graphics.FromImage(measureBitmap))
			{
			    var graphicContext = new GdiPlusGraphicContext(measureGraphics);

			    sequenceDiagramVisual.Layout(graphicContext);
                
				var renderBitmap = new Bitmap(
                    (int)Math.Ceiling(sequenceDiagramVisual.Width + 1),
                    (int)Math.Ceiling(sequenceDiagramVisual.Height + 1));

                using (var renderGraphics = System.Drawing.Graphics.FromImage(renderBitmap))
                {
                    renderGraphics.Clear(Color.White);

					graphicContext = new GdiPlusGraphicContext(renderGraphics);

                    sequenceDiagramVisual.Draw(graphicContext);

                }
                
			    outputPictureBox.Image = renderBitmap;
			}
		}

        private void ShowErrors(IEnumerable<ModelError> errors)
        {
            FillErrorList(errors);
            HighlightErrorsInEditor(errors);
        }

        private void HighlightErrorsInEditor(IEnumerable<ModelError> errors)
        {
            int rememberStart = inputTextBox.SelectionStart;
            int rememberLength = inputTextBox.SelectionLength;

            inputTextBox.SelectAll();
            inputTextBox.SelectionColor = Color.Navy;
            inputTextBox.SelectionBackColor = Color.White;

            foreach (ModelError astError in errors)
            {
                SelectTokenInEditor(astError.Token);
                inputTextBox.SelectionColor = Color.Red;
            }

            inputTextBox.Select(rememberStart, rememberLength);
            inputTextBox.SelectionColor = Color.Navy;
        }

        private void FillErrorList(IEnumerable<ModelError> errors)
        {
            listBoxErrors.Items.Clear();
            foreach (ModelError error in errors)
            {
                var listItem =
                    new ListViewItem(
                        new string[]
	                        {
                                error.Message, 
                                error.Token.Line.ToString(), 
                                error.Token.Start.ToString(), 
                                error.Token.Value},
                        0);
                listItem.Tag = error;
                listBoxErrors.Items.Add(listItem);
            }
        }

        private void YellowTokenInEditor(Token token)
        {
            inputTextBox.SelectAll();
            inputTextBox.SelectionBackColor = Color.White;
            SelectTokenInEditor(token);
            inputTextBox.SelectionBackColor = Color.Green;
            inputTextBox.Select(inputTextBox.TextLength, 0);
        }

        private void SelectTokenInEditor(Token token)
        {

            int zeroBasedLineNumber = token.Line - 1;
            int numberOfNewLineChars = zeroBasedLineNumber;

            int startIndex =
                inputTextBox
                    .Lines
                    .Select(line => line.Length)
                    .Take(zeroBasedLineNumber)
                    .Sum()
                + numberOfNewLineChars
                + token.Start;

            inputTextBox.Select(startIndex, token.IsEmpty() ? 1 : token.Length);
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

            ModelError error = listBoxErrors.SelectedItems[0].Tag as ModelError;
            if (error == null)
            {
                return;
            }

            SelectTokenInEditor(error.Token);
            Invoke((MethodInvoker)(() => inputTextBox.Focus()));
        }


        private void inputTextBox_TextChanged(object sender, EventArgs e)
        {
            if (checkBoxImidiateCompile.Checked)
            {
                Compile();
            }
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
            inputTextBox.AppendText("activate A" + Environment.NewLine);
        }

        private void buttonSample5_Click(object sender, EventArgs e)
        {
            inputTextBox.AppendText("deactivate A" + Environment.NewLine);
        }
    }
}
