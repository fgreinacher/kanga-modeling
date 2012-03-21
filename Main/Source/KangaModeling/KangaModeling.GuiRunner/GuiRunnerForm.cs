using System;
using System.Drawing;
using System.Windows.Forms;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Layouter.SequenceDiagrams;
using KangaModeling.Graphics.Theming;
using KangaModeling.Graphics.Renderables;
using KangaModeling.Graphics.GdiPlus;

namespace KangaModeling.GuiRunner
{
	public partial class GuiRunnerForm : Form
	{
		public GuiRunnerForm()
		{
			InitializeComponent();
		}

		private void compileButton_Click(object sender, EventArgs e)
		{
			var text = inputTextBox.Text;

            var sequenceDiagram = new SequenceDiagram();
            var astBuilder = new AstBuilder(sequenceDiagram);

		    var scanner = new Scanner(text);
		    var parser = new Parser(scanner, new StatementParserFactory());

		    foreach (var statement in parser.Parse())
		    {
                try
                {
                    statement.Build(astBuilder);
                } 
                catch(NotImplementedException)
                {
                        
                }
		    } 
           
			var theme = new SimpleTheme();

			using (var measureBitmap = new Bitmap(1, 1))
			using (var measureGraphics = System.Drawing.Graphics.FromImage(measureBitmap))
			{
				var measurerFactory = new GdiPlusMeasurerFactory(measureGraphics);
				var measurer = measurerFactory.CreateMeasurer(theme);

				var layoutEngine = new LayoutEngine(measurer);
				var layoutResult = layoutEngine.PerformLayout(sequenceDiagram);

				var renderBitmap = new Bitmap(
					(int)Math.Ceiling(layoutResult.Size.Width), 
					(int)Math.Ceiling(layoutResult.Size.Height));
				
				using (var renderGraphics = System.Drawing.Graphics.FromImage(renderBitmap))
				{
					renderGraphics.Clear(Color.White);

					var rendererFactory = new GdiPlusRendererFactory(renderGraphics);
					var renderer = rendererFactory.CreateRenderer(theme);

					foreach (var renderable in layoutResult.Renderables)
					{
						var renderableText = renderable as RenderableText;
						if (renderableText != null)
						{
							renderer.RenderText(renderableText);
						}
					}
				}

				outputPictureBox.Image = renderBitmap;
			}



		}
	}
}
