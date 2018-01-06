
using System;
using NUnit.Framework;
using WinApp.Classes;
using WinApp.Classes.Base;

namespace WinApp.Test
{
	[TestFixture]
	public class GeomTest
	{
		[Test]
		public void TestMethod()
		{
			Vector a = new Vector(0, 4);
			Vector b = new Vector(-3, -2);
			Vector sum = Geom.Sum(a, b);
			Assert.AreEqual(new Vector(-3, 2), sum);
		}
		
		[Test]
		public void TestLineCrosses() {
			Line l1 = new Line(new Vector(1, 1), new Vector(1, -2));
			Line l2 = new Line(new Vector(-1, -1), new Vector(2, -1));
			
			Assert.True(Geom.LinesCross(l1, l2));
		}
		
		[Test]
		public void TestCrossPosition() {
			
			Vector a = new Vector(0, 400);
			Vector b = new Vector(640, 400);
			Vector c = new Vector(50, 397.8131f);
			Vector e = new Vector(500, 376.573f);
			Line ab = new Line(a, b);
			float dist1 = Geom.Distance(ab, c);
			float dist2 = Geom.Distance(ab, e);
			
			Console.Out.WriteLine("dist1 = " + dist1);
			Console.Out.WriteLine("dist2 = " + dist2);
		}
		
		[Test]
		public void TestPointOnLine() {
			Vector point1 = new Vector(2, 1);
			Vector point2 = new Vector(-2, 0);
			Vector point3 = new Vector(5, 0);
			Vector point4 = new Vector(3, 0);
			Vector point5 = new Vector(4, 0);
			Line line = new Line(Vector.Zero, new Vector(4, 0));
			Assert.False(Geom.PointOnLine(point1, line));
			Assert.False(Geom.PointOnLine(point2, line));
			Assert.False(Geom.PointOnLine(point3, line));
			Assert.True(Geom.PointOnLine(point4, line));
			Assert.True(Geom.PointOnLine(point5, line));
		}
		
		[Test]
		public void TestIntersectCircleLineRectangles() {
			Circle circle = new Circle(364.7018f, 261.0624f, 10);
			Vector a = new Vector(160, 220);
			Vector b = new Vector(360, 270);
			Line line = new Line(a, b);
			
			Assert.True(Geom.IntersectCircleLineRectangles(circle, line));
		}
		
	}
}
