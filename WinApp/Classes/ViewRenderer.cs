
using System;
using System.Collections.Generic;
using WinApp.Classes.Base;
using System.Drawing;

namespace WinApp.Classes
{
	public class ViewRenderer
	{
		private Model model;
		
		private Pen defaultPen = new Pen(Color.White, 2f);
		private Pen debugPen = new Pen(Color.Red, 1f);
		private Pen debugLinePen = new Pen(Color.White, 1f);
		private Pen debugNormalPen = new Pen(Color.Yellow, 2f);
		
		private const float DEBUG_LINE_SIZE = 10f;
		private Vector debugPoint;
		private Line debugLine;
		private Line debugNormal;
		
		
		public ViewRenderer(Model model)
		{
			this.model = model;
		}
		
		
		public void Render(Graphics g) {
			foreach(Shape obj in model.objects) {
				RenderObject(g, obj);
			}
			RenderObject(g, model.player);
			RenderDebug(g);
		}
		
		
		private void RenderObject(Graphics g, Shape obj) {
			
			if (obj is Circle) {
				Circle circle = (Circle)obj;
				Rectangle rect = new Rectangle((int)(circle.center.x - circle.rad),
				                               (int)(circle.center.y - circle.rad),
				                               circle.rad * 2,
				                               circle.rad * 2);
				g.DrawEllipse(defaultPen, rect);
				
			} else if (obj is Polygon) {
				
				Polygon polygon = (Polygon)obj;
				Vector[] dots = polygon.dots.ToArray();
				
				for (int i = 0; i < dots.Length - 1; i++) {
					DrawLine(dots[i], dots[i + 1], g);
				}
			}
		}

		private void DrawLine(Vector dot1, Vector dot2, Graphics g)
		{
			g.DrawLine(defaultPen, dot1.x, dot1.y, dot2.x, dot2.y);
		}
		
		private void DrawLine(Vector dot1, Vector dot2, Graphics g, Pen pen)
		{
			g.DrawLine(pen, dot1.x, dot1.y, dot2.x, dot2.y);
		}
		
		
		public void SetDebugPoint(Vector point) {
			debugPoint = point;
		}
		
		public void SetDebugLine(Line line) {
			debugLine = line;
		}
		
		public void SetDebugNormal(Line line) {
			debugNormal = line;
		}
		
		public void RenderDebug(Graphics g) {
			if (debugPoint != null) {
				Vector a = new Vector(debugPoint.x - DEBUG_LINE_SIZE, debugPoint.y);
				Vector b = new Vector(debugPoint.x + DEBUG_LINE_SIZE, debugPoint.y);
				DrawLine(a, b, g, debugPen);
				a = new Vector(debugPoint.x, debugPoint.y - DEBUG_LINE_SIZE);
				b = new Vector(debugPoint.x, debugPoint.y + DEBUG_LINE_SIZE);
				DrawLine(a, b, g, debugPen);
			}
			
			if (debugLine != null) {
				DrawLine(debugLine.a, debugLine.b, g, debugLinePen);
			}
			
			if (debugNormal != null) {
				DrawLine(debugNormal.a, debugNormal.b, g, debugNormalPen);
			}
		}
	}
}
