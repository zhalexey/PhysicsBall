
using System;

namespace WinApp.Classes.Base
{
	public class Collision
	{
		public enum Type {
			Normal, Corner
		}
		
		public Line line;
		public Type type;
		
		
		public Collision(Line line, Type type)
		{
			this.line = line;
			this.type = type;
		}
	}
}
