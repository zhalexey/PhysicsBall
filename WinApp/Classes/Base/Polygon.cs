
using System;
using System.Collections.Generic;
using WinApp.Classes.Base;

namespace WinApp.Classes
{

	public class Polygon : Shape
	{
		public List<Vector> dots;
		
		public Polygon()
		{
			dots = new List<Vector>();
		}
		
		public void Add(Vector dot) {
			dots.Add(dot);
		}
		
	}
}
