
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinApp.Classes.Base;
using WinApp.Classes;

namespace WinApp.Classes
{

	public class Controller
	{
		
		private const float PRECISION = 0.1f;
		private const float FADE_Y_INDEX = 0.5f;
		private const float FADE_X_INDEX = 0.99f;
		public const float DEFAULT_DT = 0.0005f;
		
		private Model model;
		private ViewRenderer view;
		
		private Circle copyCircle;
		public float dt = DEFAULT_DT;

		
		public Controller(Model model, ViewRenderer view)
		{
			this.model = model;
			this.view = view;
		}
		
		
		public void Update() {
			copyCircle = new Circle(model.player);
			ApplyPhysics();
			CheckCollisions();
		}
		
		
		private void CheckCollisions() {
			List<Shape> collidedFigures = new List<Shape>();
			
			foreach (Shape figure in model.objects) {
				
				Collision collision = GetCollision(figure);
				
				if (collision != null) {
					ApplyCollision(collision);
				}
			}
		}
		
		
		void ApplyCollision(Collision collision) {
			model.player.CopyFrom(copyCircle);
			
			
			Vector c = model.player.center;
			Vector d;
			
			if (Collision.Type.Corner == collision.type) {
				d = Geom.GetNearestPoint(collision.line, model.player.center);
			} else {
				d = Geom.GetCrossPosition(c, collision.line);
			}
			
			/* view.SetDebugNormal(new Line(d, new Vector(c))); */

			Vector normal = Geom.Normalize(Geom.Sub(c, d));
			float dv = -2.0f * Geom.Dot(model.player.velocity, normal);	
			Vector normalDV = Geom.Mul(normal, dv);
			Vector reflected = Geom.Sum(normalDV, model.player.velocity);		
			

			model.player.velocity.y = reflected.y * FADE_Y_INDEX;
			model.player.velocity.x = reflected.x * FADE_X_INDEX;
			
		}
		
		
		private Collision GetCollision(Shape figure) {
			Circle circle = model.player;
			
			Polygon poly = (Polygon) figure;
			Vector[] dots = poly.dots.ToArray();
			
			
			for(int i = 0; i < dots.Length - 1; i++) {
				Vector a = dots[i];
				Vector b = dots[i + 1];
				Line line = new Line(a, b);
				

				if (Geom.IntersectCircleLineRectangles(circle, line)) {
					float dist = Geom.Distance(line, circle.center);
					
					if ((dist - circle.rad) <= PRECISION ) {
						float prevDist = Geom.Distance(line, copyCircle.center);
						if (dist < prevDist) {
							return new Collision(line, Collision.Type.Normal);
						}
					}
				} else {
					
					float distCorner = Geom.DistanceCorner(circle, line);
					if ((distCorner - circle.rad) <= PRECISION) {
						float prevDistCorner = Geom.DistanceCorner(copyCircle, line);
						if (distCorner < prevDistCorner) {
							return new Collision(line, Collision.Type.Corner);
						}
					}
				}
			}
			
			return null;
		}


		void ApplyPhysics()
		{
			model.player.velocity.y += 9.8f * dt;
			
			RecountPosition(model.player, dt);
			
		}


		void RecountPosition(RigidBody figure, double dt)
		{
			if (figure is Circle) {
				Circle circle = (Circle) figure;

				circle.center.y += (float)(figure.velocity.y * dt);
				circle.center.x += (float)(figure.velocity.x * dt);
			}
		}

		public void UpdateDt(int value) {
			dt = Controller.DEFAULT_DT * value;
		}

		void DebugInfo(string info) {
			MainForm form = (MainForm)Application.OpenForms["MainForm"];
		}

	}
}
