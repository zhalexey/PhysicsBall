
using System;
using WinApp.Classes.Base;
using System.Drawing;

namespace WinApp.Classes
{
	public class Vector
	{
		public float x, y;
		
		public Vector(){}
		
		public Vector(float x, float y)
		{
			this.x = x;
			this.y = y;
		}
		
		public Vector(Vector vect)
		{
			this.x = vect.x;
			this.y = vect.y;
		}
		
		public override string ToString() {
			return "(" + x + ", " + y + ")";
		}
		
		public override bool Equals(object obj)
		{
			Vector other = obj as Vector;
			if (other == null)
				return false;
			return object.Equals(this.x, other.x) && object.Equals(this.y, other.y);
		}
		
		public override int GetHashCode()
		{
			int hashCode = 0;
			unchecked {
				hashCode += 1000000007 * x.GetHashCode();
				hashCode += 1000000009 * y.GetHashCode();
			}
			return hashCode;
		}
		
		public static Vector Zero = new Vector(0, 0);

		
	}
}
