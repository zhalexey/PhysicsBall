
using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.SqlServer.Server;
using WinApp.Classes;
using WinApp.Classes.Base;

namespace WinApp
{

	public partial class MainForm : Form
	{
		
		Controller ctrl;
		Model model;
		ViewRenderer view;
		
		
		private Vector defaultPosition = new Vector(450, 20);
		private Vector toVelocityPosition = new Vector(0, 0);
		private Vector defaultVelocity = new Vector(0, 0);
		private int rad = 10;
		
		private bool mousePulling = false;
		
		
		public MainForm()
		{
			InitializeComponent();
			InitializeEvents();
			
			model = new Model();
			view = new ViewRenderer(model);
			ctrl = new Controller(model, view);
			
			InitScene();
		}

		private void InitScene()
		{
			ResetPlayer();
			
			RefreshDt();

			Size screen = this.ClientSize;
			int leftCorner = screen.Width / 4;
			int rightCorner = leftCorner * 3;
			
			// box
			
			Polygon poly = new Polygon();
			poly.Add(new Vector(leftCorner, 10));
			poly.Add(new Vector(leftCorner, screen.Height - 10));
			poly.Add(new Vector(rightCorner, screen.Height - 10));
			poly.Add(new Vector(rightCorner, 10));
			poly.Add(new Vector(leftCorner, 10));
			model.Add(poly);
			
			// obstacles
			poly = new Polygon();
			poly.Add(new Vector(rightCorner, 50));
			poly.Add(new Vector(rightCorner - 130, 100));
			poly.Add(new Vector(rightCorner - 150, 180));
			poly.Add(new Vector(rightCorner, 180));
			model.Add(poly);
			
			poly = new Polygon();
			poly.Add(new Vector(leftCorner + 70, 100));
			poly.Add(new Vector(leftCorner + 120, 130));
			poly.Add(new Vector(leftCorner + 120, 200));
			poly.Add(new Vector(leftCorner + 70, 180));
			poly.Add(new Vector(leftCorner + 70, 100));
			model.Add(poly);
			
			poly = new Polygon();
			poly.Add(new Vector(leftCorner, 220));
			poly.Add(new Vector(leftCorner + 200, 270));
			poly.Add(new Vector(leftCorner + 200, 270));
			poly.Add(new Vector(leftCorner, 320));
			model.Add(poly);
			
			poly = new Polygon();
			poly.Add(new Vector(rightCorner, 270));
			poly.Add(new Vector(rightCorner - 150, 370));
			poly.Add(new Vector(rightCorner - 150, 380));
			poly.Add(new Vector(rightCorner, 320));
			model.Add(poly);
		}
		
		void PerformMath() {
			ctrl.Update();
		}
		
		void ResetPlayer() {
			model.player = new Circle(new Vector(defaultPosition), rad);
			model.player.velocity = new Vector(defaultVelocity);
			view.SetDebugPoint(defaultPosition);
		}
		
		protected override void OnPaint( PaintEventArgs e )
		{
			Graphics graphicsObj = e.Graphics;
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			graphicsObj.Clear(Color.Blue);
			
			view.Render(graphicsObj);
			base.OnPaint( e );
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			ResetPlayer();
		}
		
		void TrackBar1Scroll(object sender, EventArgs e)
		{
			ctrl.UpdateDt(this.trackBar1.Value);
			RefreshDt();
		}
		
		void RefreshDt() {
			this.label2.Text = this.trackBar1.Value.ToString();
		}
		
		
		void MouseDownEventHandler(object sender, EventArgs e) {
			if (mousePulling) {
				return;
			}
			
			MouseEventArgs args = (MouseEventArgs)e;
			if (args.Button == MouseButtons.Left) {
				defaultPosition.x = args.X;
				defaultPosition.y = args.Y;
				view.SetDebugPoint(defaultPosition);
				view.SetDebugLine(null);
				defaultVelocity = Vector.Zero; 
				mousePulling = true;
			}
		}
		
		void MouseUpEventHandler(object sender, EventArgs e) {
			if (!mousePulling) {
				return;
			}
			
			MouseEventArgs args = (MouseEventArgs)e;
			toVelocityPosition.x = args.X;
			toVelocityPosition.y = args.Y;
			view.SetDebugLine(new Line(defaultPosition, toVelocityPosition));
			
			defaultVelocity.x = toVelocityPosition.x - defaultPosition.x;
			defaultVelocity.y = toVelocityPosition.y - defaultPosition.y;
			mousePulling = false;
		}
		
		void MouseMoveEventHandler(object sender, EventArgs e) {
			if (mousePulling) {
				MouseEventArgs args = (MouseEventArgs)e;
				toVelocityPosition.x = args.X;
				toVelocityPosition.y = args.Y;
				view.SetDebugLine(new Line(defaultPosition, toVelocityPosition));
			}
		}
		
	}
}
