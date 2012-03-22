using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using KangaModeling.Renderer;
using Moq;
using KangaModeling.Layouter.SequenceDiagrams;
using KangaModeling.Compiler.SequenceDiagrams;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Renderables;
using KangaModeling.Graphics.Theming;
using KangaModeling.Renderer.Primitives;

namespace KangaModeling.Layouter.Test.SequenceDiagrams
{
	[TestFixture]
	internal sealed class RenderableFactoryTests
	{
		[Test]
		public void Creating_a_RenderableFactory_without_measurement_engine_throws_an_ArgumentNullException()
		{
			// Arrange
			var theme = new Mock<ITheme>();

			// Act, Assert
			Assert.Catch<ArgumentNullException>(() => new RenderableFactory(null, theme.Object));
		}

		[Test]
		public void Creating_renderables_without_sequence_diagram_throws_an_ArgumentNullException()
		{
			// Arrange			
			var measurer = new Mock<IMeasurer>();
			var theme = new Mock<ITheme>();
			var renderableFactory = new RenderableFactory(measurer.Object, theme.Object);

			// Act, Assert
			Assert.Catch<ArgumentNullException>(() => renderableFactory.CreateRenderables(null));
		}

		[Test]
		public void Creating_renderables_for_an_empty_sequence_diagram_creates_and_empty_renderable_sequence()
		{
			// Arrange
			var measurer = new Mock<IMeasurer>();
			var theme = new Mock<ITheme>();

			var sequenceDiagram = new Mock<ISequenceDiagram>();
			sequenceDiagram.Setup(sd => sd.Title).Returns((string)null);

			var renderableFactory = new RenderableFactory(measurer.Object, theme.Object);

			// Act
			var renderables = renderableFactory.CreateRenderables(sequenceDiagram.Object);

			// Assert
			Assert.That(renderables, Is.Empty);
		}

		[Test]
		public void Creating_renderables_for_a_sequence_diagram_that_contains_only_a_title_creates_a_sequence_containing_one_renderable_text()
		{
			// Arrange
			var measurer = new Mock<IMeasurer>();
			measurer.Setup(m => m.MeasureText(It.IsAny<string>())).Returns(new Size(1, 1));
			var theme = new Mock<ITheme>();

			var sequenceDiagram = new Mock<ISequenceDiagram>();
			sequenceDiagram.Setup(sd => sd.Title).Returns("My title");

			var renderableFactory = new RenderableFactory(measurer.Object, theme.Object);

			// Act			
			var renderables = renderableFactory.CreateRenderables(sequenceDiagram.Object);

			// Assert
			Assert.That(renderables.Count(), Is.EqualTo(1));
			Assert.That(renderables.First(), Is.InstanceOf<RenderableText>());
			Assert.That(((RenderableText)renderables.First()).Text, Is.EqualTo("My title"));
		}

		[Test]
		public void The_location_of_the_title_reflects_the_configured_theme_padding()
		{
			// Arrange
			var measurer = new Mock<IMeasurer>();
			measurer.Setup(m => m.MeasureText(It.IsAny<string>())).Returns(new Size(1, 1));
			var theme = new Mock<ITheme>();
			theme.Setup(t => t.Padding).Returns(5);

			var sequenceDiagram = new Mock<ISequenceDiagram>();
			sequenceDiagram.Setup(sd => sd.Title).Returns("My title");

			var renderableFactory = new RenderableFactory(measurer.Object, theme.Object);

			// Act			
			var renderables = renderableFactory.CreateRenderables(sequenceDiagram.Object);

			// Assert
			Assert.That(((RenderableText)renderables.First()).Location.X, Is.EqualTo(5));
			Assert.That(((RenderableText)renderables.First()).Location.Y, Is.EqualTo(5));
		}
	}
}
