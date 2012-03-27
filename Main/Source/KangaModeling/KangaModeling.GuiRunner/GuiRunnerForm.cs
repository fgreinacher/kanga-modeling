using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics.Theming;
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
			//@Stefan had problems with returning errors and passing Lines -> reverted to old implementation. Need to find solution.
			var scanner = new Scanner(inputTextBox.Lines);
			var sequenceDiagram = new SequenceDiagram();
			var astBuilder = new ModelBuilder(sequenceDiagram);

			var parser = new Parser(scanner, new StatementParserFactory());
			foreach (var statement in parser.Parse())
			{
				try
				{
					statement.Build(astBuilder);
				}
				catch (NotImplementedException)
				{

				}
			}

			ShowErrors(astBuilder.Errors);

            var sequenceDiagramVisual = new SequenceDiagramVisual(sequenceDiagram);
			var theme = new SimpleTheme();

			using (var measureBitmap = new Bitmap(1, 1))
			using (var measureGraphics = System.Drawing.Graphics.FromImage(measureBitmap))
			{
			    var graphicContextFactory = new GdiPlusGraphicContextFactory(measureGraphics);
			    var graphicContext = graphicContextFactory.CreateGraphicContext(theme);

			    sequenceDiagramVisual.Layout(graphicContext);
                
				var renderBitmap = new Bitmap(
                    (int)Math.Ceiling(sequenceDiagramVisual.Width + 1),
                    (int)Math.Ceiling(sequenceDiagramVisual.Height + 1));

                using (var renderGraphics = System.Drawing.Graphics.FromImage(renderBitmap))
                {
                    renderGraphics.Clear(Color.White);

                    graphicContextFactory = new GdiPlusGraphicContextFactory(renderGraphics);
                    graphicContext = graphicContextFactory.CreateGraphicContext(theme);

                    sequenceDiagramVisual.Draw(graphicContext);

                }
                
			    outputPictureBox.Image = renderBitmap;
			}
		}

        private void ShowErrors(IEnumerable<AstError> errors)
        {
            FillErrorList(errors);
            HighlightErrorsInEditor(errors);
        }

        private void HighlightErrorsInEditor(IEnumerable<AstError> errors)
        {
            int rememberStart = inputTextBox.SelectionStart;
            int rememberLength = inputTextBox.SelectionLength;

            inputTextBox.SelectAll();
            inputTextBox.SelectionColor = Color.Navy;
            inputTextBox.SelectionBackColor = Color.White;

            foreach (AstError astError in errors)
            {
                SelectTokenInEditor(astError.Token);
                inputTextBox.SelectionColor = Color.Red;
            }

            inputTextBox.Select(rememberStart, rememberLength);
            inputTextBox.SelectionColor = Color.Navy;
        }

        private void FillErrorList(IEnumerable<AstError> errors)
        {
            listBoxErrors.Items.Clear();
            foreach (AstError error in errors)
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

            AstError error = listBoxErrors.SelectedItems[0].Tag as AstError;
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
