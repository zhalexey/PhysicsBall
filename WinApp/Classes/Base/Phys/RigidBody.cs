
using System;
using System.Drawing;

namespace WinApp.Classes.Base
{
	public class RigidBody {
		public Vector velocity;
		
		public RigidBody() {
			velocity = new Vector(0, 0);
		}
	}
}
