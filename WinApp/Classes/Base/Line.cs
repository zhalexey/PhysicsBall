
using System;

namespace WinApp.Classes.Base
{
	public class Line
	{
		public Vector a;
		public Vector b;
		
		public Line(Vector a, Vector b)
		{
			this.a = a;
			this.b = b;
		}
	
		public override string ToString()
		{
			return "[" + a + " : " + b + "]";
		}
 
		
	}
}
