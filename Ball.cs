using System;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Collections.Generic;

namespace Project
{
	/// <summary>
	/// Description of Ball.
	/// </summary>
	public class Ball
	{
		public float cx,cy,vx,vy,r,m;
		public Queue<PointF> Q;
		public Ball()
		{
			
		}
		public Ball(float cx, float cy, float vx, float vy, float r)
		{
			this.cx = cx;
			this.cy = cy;
			this.vx = vx;
			this.vy = vy;
			this.r = r;
			this.m = r*r*r;
			this.Q = new Queue<PointF>();
		}
		public static void Crash(Ball B1, Ball B2)
		{
			float dx = B2.cx - B1.cx;
			float dy = B2.cy - B1.cy;
			
			float v1norm = (B1.vx*dx + B1.vy*dy)/(dx*dx+dy*dy);
			float v2norm = (B2.vx*dx + B2.vy*dy)/(dx*dx+dy*dy);
			
			float v1n = ((B1.m - B2.m)*v1norm + 2*B2.m*v2norm)/(B1.m + B2.m);
			float v2n = ((B2.m - B1.m)*v2norm + 2*B1.m*v1norm)/(B1.m + B2.m);
			
			B1.vx += dx*(v1n-v1norm);
			B1.vy += dy*(v1n-v1norm);
			B2.vx += dx*(v2n-v2norm);
			B2.vy += dy*(v2n-v2norm);
		}
	}
}
