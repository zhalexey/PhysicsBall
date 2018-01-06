
using System;
using System.Drawing;
using System.Windows.Forms;
using WinApp.Classes.Base;

namespace WinApp.Classes
{
	public class Geom
	{
		
		public static Vector Inverse(Vector a) {
			return new Vector(-a.x, -a.y);
		}
		
		public static float Distance(Line ab, Vector c) {
			Vector d = GetCrossPosition(c, ab);
			Vector cd = Sub(c, d);
			return Mod(cd);
		}
		
		public static float Distance(Vector a, Vector b) {
			return Mod(Sub(b, a));
		}
		
		
		public static Vector Normalize(Vector a) {
			float mul =  1.0f / (float)Math.Sqrt(a.x * a.x + a.y * a.y);
			
			Vector normalized = new Vector();
			normalized.x = a.x * mul;
			normalized.y = a.y * mul;
			
			return normalized;
		}
		
		public static float Dot(Vector a, Vector norm) {
			return a.x * norm.x + a.y * norm.y;
		}
		
		
		public static Vector GetCrossPosition(Vector c, Line line) {
			Vector ab = Sub(line.b, line.a);
			Vector norm = Normalize(ab);
			Vector ac = Sub(c, line.a);
			float d = Dot(ac, norm);
			return new Vector(line.a.x + norm.x * d, line.a.y + norm.y * d);
		}
		
		public static Vector Sum(Vector a, Vector b) {
			return new Vector(a.x + b.x, a.y + b.y);
		}
		
		public static Vector Sub(Vector a, Vector b) {
			return new Vector(a.x - b.x, a.y - b.y);
		}
		
		public static float Mod(Vector a) {
			return (float)Math.Sqrt(a.x * a.x + a.y * a.y);
		}
		
		public static Vector Mul(Vector a, float n) {
			return new Vector(a.x * n, a.y * n);
		}
		
		public static float Mul(Vector a, Vector b) {
			return a.x * b.y - a.y * b.x;
		}
		
		public static bool LinesCross(Line l1, Line l2) {
			Vector p34 = Sub (l2.a, l2.b);
			Vector p31 = Sub (l2.a, l1.a);
			Vector p32 = Sub (l2.a, l1.b);
			float v1 = Mul (p34, p31);
			float v2 = Mul (p34, p32);

			Vector p12 = Sub (l1.a, l1.b);
			Vector p13 = Sub (l1.a, l2.a);
			Vector p14 = Sub (l1.a, l2.b);
			float v3 = Mul (p12, p13);
			float v4 = Mul (p12, p14);
			
			return ((v1 * v2 < 0) && (v3 * v4 < 0));
		}
		
		
		public static bool InRectangle(Rectangle rect1, Vector a, Vector b) {
			Vector left, right, up, down;
			if (a.x < b.x) {
				left = a;
				right = b;
			} else {
				left = b;
				right = a;
			}
			
			if (a.y < b.y) {
				up = a;
				down = b;
			} else {
				up = b;
				down = a;
			}
			
			Rectangle rect2 = new Rectangle((int)left.x, (int)up.y, (int)(right.x - left.x), (int)(down.y - up.y));
			bool result = !Rectangle.Intersect(rect1, rect2).IsEmpty;
			return result;
		}
		
		
		public static bool IntersectCircleLineRectangles(Circle circle, Line line) {
			if (Geom.InRectangle(circle.GetRect(), line.a, line.b)) {
				
				Vector d = Geom.GetCrossPosition(circle.center, line);
				if (Geom.PointOnLine(d, line)) {
					return true;
				}
				
			}
			return false;
		}
		
		public static float DistanceCorner(Circle circle, Line line) {
			float ac = Distance(circle.center, line.a);
			float bc = Distance(circle.center, line.b);
			return ac < bc ? ac : bc;
		}
		
		public static bool PointOnLine(Vector point, Line line) {
			Vector a, b;

			Vector ap = Geom.Sub(point, line.a);
			Vector ab = Geom.Sub(line.b, line.a);
			
			// colinear check
			if (Geom.Mul(ap, ab) != 0) {
				return false;
			}


			// check x
			if (line.a.x < line.b.x) {
				a = line.a;
				b = line.b;
			} else {
				a = line.b;
				b = line.a;
			}
			
			if (point.x < a.x || point.x > b.x) {
				return false;
			}

			// check y
			if (line.a.y < line.b.y) {
				a = line.a;
				b = line.b;
			} else {
				a = line.b;
				b = line.a;
			}
			
			if (point.y < a.y || point.y > b.y) {
				return false;
			}

			return true;
		}
		
		
		public static Vector GetNearestPoint(Line line, Vector point) {
			return Distance(line.a, point) < Distance(line.b, point) ? line.a : line.b;
		}
		
	}
}
