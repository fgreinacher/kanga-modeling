using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace KangaModeling.Visuals.Test
{
	[TestFixture]
	class VisualTests
	{
        //[TestCase(-10, -10, 5, 5, -5, -5)]
        //[TestCase(0, 0, 5, 5, 5, 5)]
        //[TestCase(10, 10, 5, 5, 15, 15)]
        //[TestCase(10, 10, -5, -5, 5, 5)]
        //public void Local_points_to_global_points(
        //    float childLocationX, float childLocationY,
        //    float childLocalPointX, float childLocalPointY,
        //    float expectedGlobalPointX, float expectedGlobalPointY)
        //{
        //    // Arrange
        //    var parent = new Visual();
        //    var child = new Visual();
        //    child.Location = new Point(childLocationX, childLocationY);
        //    parent.AddChild(child);

        //    // Act
        //    var childLocalPoint = new Point(childLocalPointX, childLocalPointY);
        //    var globalPoint = child.LocalPointToGlobalPoint(childLocalPoint);

        //    // Assert
        //    var expectedGlobalPoint = new Point(expectedGlobalPointX, expectedGlobalPointY);
        //    Assert.That(globalPoint, Is.EqualTo(expectedGlobalPoint));
        //}

        //[TestCase(10, 10, 20, 20, 10, 10)]
        //[TestCase(-10, -10, 20, 20, 30, 30)]
        //[TestCase(0, 0, 20, 20, 20, 20)]
        //[TestCase(10, 10, -20, -20, -30, -30)]
        //public void Global_points_to_local_points(
        //    float childLocationX, float childLocationY,
        //    float globalPointX, float globalPointY,
        //    float expectedChildLocalPointX, float expectedChildLocalPointY)
        //{
        //    // Arrange
        //    var parent = new Visual();
        //    var child = new Visual();
        //    child.Location = new Point(childLocationX, childLocationY);
        //    parent.AddChild(child);

        //    // Act
        //    var globalPoint = new Point(globalPointX, globalPointY);
        //    var childLocalPoint = child.GlobalPointToLocalPoint(globalPoint);

        //    // Assert
        //    var expectedChildLocalPoint = new Point(expectedChildLocalPointX, expectedChildLocalPointY);
        //    Assert.That(childLocalPoint, Is.EqualTo(expectedChildLocalPoint));
        //}
	}
}
