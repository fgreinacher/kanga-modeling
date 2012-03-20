using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using KangaModeling.Renderer;
using Moq;
using KangaModeling.Renderer.Core;
using KangaModeling.Layouter.SequenceDiagrams;

namespace KangaModeling.Layouter.Test.SequenceDiagrams
{
	[TestFixture]
	internal sealed class LayoutEngineTests
	{
		[Test]
		public void Creating_a_layout_engine_without_measurement_engine_throws_an_ArgumentNullException()
		{
			// Arrange, Act, Assert
			Assert.Catch<ArgumentNullException>(() => new LayoutEngine(null));
		}

		[Test]
		public void Performing_layout_without_sequence_diagram_throws_an_ArgumentNullException()
		{
			// Arrange
			var measurementEngine = new Mock<IMeasurementEngine>();
			var layoutEngine = new LayoutEngine(measurementEngine.Object);

			// Act, Assert
			Assert.Catch<ArgumentNullException>(() => layoutEngine.PerformLayout(null));
		}

		[Test]
		public void Rendering_an_empty_sequence_diagram_creates_and_empty_renderable_sequence()
		{
			// Arrange
			var measurementEngine = new Mock<IMeasurementEngine>();

			var sequenceDiagram = new Mock<ISequenceDiagram>();
			sequenceDiagram.Setup(sd => sd.HasTitle).Returns(false);

			var layoutEngine = new LayoutEngine(measurementEngine.Object);

			// Act
			var renderableObjects = layoutEngine.PerformLayout(sequenceDiagram.Object);

			// Assert
			Assert.That(renderableObjects, Is.Empty);
		}

		[Test]
		public void Rendering_a_sequence_diagram_that_contains_only_a_title_creates_a_sequence_containing_one_renderable_text()
		{
			// Arrange
			var measurementEngine = new Mock<IMeasurementEngine>();

			var sequenceDiagram = new Mock<ISequenceDiagram>();
			sequenceDiagram.Setup(sd => sd.HasTitle).Returns(true);
			sequenceDiagram.Setup(sd => sd.Title).Returns("My title");

			var layoutEngine = new LayoutEngine(measurementEngine.Object);

			// Act			
			var renderableObjects = layoutEngine.PerformLayout(sequenceDiagram.Object);

			// Assert
			Assert.That(renderableObjects.Count(), Is.EqualTo(1));
			Assert.That(renderableObjects.First(), Is.InstanceOf<RenderableText>());
			Assert.That(((RenderableText)renderableObjects.First()).Text, Is.EqualTo("My title"));
		}
	}
}
