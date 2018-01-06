
using System;
using System.Collections.Generic;
using WinApp.Classes.Base;

namespace WinApp.Classes
{

	public class Model
	{
		public Circle player;
		public List<Polygon> objects;
		
		public Model() {
			objects = new List<Polygon>();
		}
		
		public void Add(Polygon obj) {
			objects.Add(obj);
		}

		
	}
}
