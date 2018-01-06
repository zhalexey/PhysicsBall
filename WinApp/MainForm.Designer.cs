using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace WinApp
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(15, 13);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Restart";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// trackBar1
			// 
			this.trackBar1.Location = new System.Drawing.Point(15, 81);
			this.trackBar1.Minimum = 1;
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.trackBar1.Size = new System.Drawing.Size(45, 252);
			this.trackBar1.TabIndex = 1;
			this.trackBar1.Value = 1;
			this.trackBar1.Scroll += new System.EventHandler(this.TrackBar1Scroll);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(15, 52);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "dt =";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(44, 52);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(46, 23);
			this.label2.TabIndex = 3;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.trackBar1);
			this.panel1.Location = new System.Drawing.Point(510, 25);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(118, 358);
			this.panel1.TabIndex = 4;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(640, 480);
			this.Controls.Add(this.panel1);
			this.DoubleBuffered = true;
			this.Name = "MainForm";
			this.Text = "EngineApp";
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
		}

		void InitializeEvents()
		{
			// Tick events
			Application.Idle += TickWhileIdle;

			// Mouse events
			this.MouseDown += MouseDownEventHandler;
			this.MouseUp += MouseUpEventHandler;
			this.MouseMove += MouseMoveEventHandler;
		}
		
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TrackBar trackBar1;
		private System.Windows.Forms.Button button1;
		
		
		static class NativeMethods
		{
			[StructLayout(LayoutKind.Sequential)]
			public struct Message
			{
				public IntPtr hWnd;
				public uint Msg;
				public IntPtr wParam;
				public IntPtr lParam;
				public uint Time;
				public System.Drawing.Point Point;
			}

			[DllImport("User32.dll")]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool PeekMessage(out Message message, IntPtr hWnd, uint filterMin, uint filterMax, uint flags);
		}
		
		void TickWhileIdle(object sender, EventArgs e)
		{
			NativeMethods.Message message;

			while (!NativeMethods.PeekMessage(out message, IntPtr.Zero, 0, 0, 0))
			{
				Tick(sender, e);
			}
		}
		
		
		Stopwatch stopWatch = Stopwatch.StartNew();

		readonly TimeSpan TargetElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 60);
		readonly TimeSpan MaxElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 10);
		
		readonly TimeSpan PhysTargetElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 10000);
		readonly TimeSpan PhysMaxElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 2000);

		TimeSpan accumulatedTime;
		TimeSpan lastTime;
		
		TimeSpan physAccumulatedTime;
		TimeSpan physLastTime;

		
		void Tick(object sender, EventArgs e)
		{
			TimeSpan currentTime = stopWatch.Elapsed;
			TimeSpan elapsedTime = currentTime - lastTime;
			lastTime = currentTime;
			
			TimeSpan physElapsedTime = currentTime - physLastTime;
			physLastTime = currentTime;
			

			if (elapsedTime > MaxElapsedTime){
				elapsedTime = MaxElapsedTime;
			}
			
			if (physElapsedTime > PhysMaxElapsedTime) {
				physElapsedTime = PhysMaxElapsedTime;
			}

			accumulatedTime += elapsedTime;
			physAccumulatedTime += physElapsedTime;
			
			
			while (physAccumulatedTime >= PhysTargetElapsedTime)
			{
				physAccumulatedTime -= PhysTargetElapsedTime;
				PerformMath();
			}
			
			
			while (accumulatedTime >= TargetElapsedTime)
			{
				accumulatedTime -= TargetElapsedTime;
				Invalidate();
			}

		}
		
		
		

	}
}
