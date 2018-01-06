
using System;
using System.Drawing;
using WinApp.Classes.Base;

namespace WinApp.Classes
{

	public class Circle : RigidBody, Shape
	{
		public Vector center;
		public int rad;
		private int diam = 0;
		
		public Circle(float x, float y, int rad)
		{
			center = new Vector(x, y);
			this.rad = rad;
		}
		
		public Circle(Vector center, int rad)
		{
			this.center = center;
			this.rad = rad;
		}
		
		public Circle(Circle circle)
		{
			CopyFrom(circle);
		}
		
		
		public void CopyFrom(Circle circle) {
			this.center = new Vector(circle.center);
			this.rad = circle.rad;
			this.velocity = new Vector(circle.velocity);
		}
		
		private int GetDiam() {
			if (diam == 0) {
				diam = rad * 2;
			}
			return diam;
		}
		
		public Rectangle GetRect() {
			return new Rectangle((int)(center.x - rad), (int)(center.y - rad), GetDiam(), GetDiam());
		}
		
	}
}
