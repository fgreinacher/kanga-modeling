using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;

namespace KangaModeling.Visuals.Test
{
	[TestFixture]
	class VisualChildrenTests
	{
		[Test]
		public void The_child_collection_of_a_new_visual_is_not_null()
		{
			// Arrange
			var parent = new Visual();

			// Assert
			Assert.That(parent.Children, Is.Not.Null);
		}

		[Test]
		public void The_child_collection_of_a_new_visual_is_empty()
		{
			// Arrange
			var parent = new Visual();
			
			// Assert
			Assert.That(parent.Children, Is.Empty);
		}

		[Test]
		public void After_adding_a_child_the_child_collection_contains_one_item()
		{
			// Arrange
			var parent = new Visual();
			var child = new Visual();

			// Act
			parent.AddChild(child);

			// Assert
			Assert.That(parent.Children.Count(), Is.EqualTo(1));
		}

		[Test]
		public void After_adding_a_child_the_child_collection_contains_the_added_child()
		{
			// Arrange
			var parent = new Visual();
			var child = new Visual();

			// Act
			parent.AddChild(child);

			// Assert
			Assert.That(parent.Children.ElementAt(0), Is.SameAs(child));
		}

		[Test]
		public void After_adding_a_child_the_child_has_the_correct_parent()
		{
			// Arrange
			var parent = new Visual();
			var child = new Visual();

			// Act
			parent.AddChild(child);

			// Assert
			Assert.That(child.Parent, Is.SameAs(parent));
		}

		[Test]
		public void Adding_a_child_twice_throws_an_ArgumentException()
		{
			// Arrange
			var parent = new Visual();
			var child = new Visual();
			parent.AddChild(child);

			// Act & Assert
			Assert.Catch<ArgumentException>(() => parent.AddChild(child));
		}

		[Test]
		public void Adding_a_child_that_already_has_a_parent_throws_an_ArgumentException()
		{
			// Arrange
			var parent1 = new Visual();
			var parent2 = new Visual();
			var child = new Visual();

			parent1.AddChild(child);

			// Act & Assert
			Assert.Catch<ArgumentException>(() => parent2.AddChild(child));
		}

		[Test]
		public void Adding_null_throws_an_ArgumentNullException()
		{
			// Arrange
			var parent = new Visual();

			// Act & Assert
			Assert.Catch<ArgumentException>(() => parent.AddChild(null));
		}

		[Test]
		public void Adding_the_parent_as_its_own_child_throws_an_ArgumentException()
		{
			// Arrange
			var parent = new Visual();

			// Act & Assert
			Assert.Catch<ArgumentException>(() => parent.AddChild(parent));
		}

		[Test]
		public void Removing_a_child_from_its_parent_sets_its_parent_to_null()
		{
			// Arrange
			var parent = new Visual();
			var child = new Visual();
			parent.AddChild(child);

			// Act
			parent.RemoveChild(child);

			// Assert
			Assert.That(child.Parent, Is.Null);
		}

		[Test]
		public void Removing_a_child_from_its_parent_decrements_the_child_count()
		{
			// Arrange
			var parent = new Visual();
			var child = new Visual();
			parent.AddChild(child);

			// Act
			parent.RemoveChild(child);

			// Assert
			Assert.That(parent.Children, Is.Empty);
		}

		[Test]
		public void Removing_null_throws_and_ArgumentNullException()
		{
			// Arrange
			var parent = new Visual();

			// Act & Assert
			Assert.Catch<ArgumentNullException>(() => parent.RemoveChild(null));
		}

		[Test]
		public void Removing_a_visual_that_is_not_a_child_throws_an_ArgumentException()
		{
			// Arrange
			var parent = new Visual();
			var child = new Visual();

			// Act & Assert
			Assert.Catch<ArgumentException>(() => parent.RemoveChild(child));
		}
	}
}
