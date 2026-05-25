using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Text;
using System.IO;
using System.Data.SqlClient;



namespace Project
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			
			//string connection = "Server=DIMITAR\\SQLEXPRESS;Database=WL;Integrated Security=True;";
			
			SetDoubleBuffered(panel2);
	        panel2.Paint += Panel2_Paint;
			
			panel1.Visible = false;
			panel2.Visible = false;
			
			label1.Visible = false;
			label2.Visible = false;
			label3.Visible = false;
			label4.Visible = false;
			label5.Visible = false;
			label6.Visible = false;
			label7.Visible = false;
			richTextBox1.Visible = false;
			
			textBox2.Visible = false;
			textBox3.Visible = false;
			textBox4.Visible = false;
			textBox5.Visible = false;
			textBox6.Visible = false;
			
			comboBox1.Visible = false;
			button2.Visible = false;
			button3.Visible = false;
			button4.Visible = false;
			button5.Visible = false;
			button7.Visible = false;
			button8.Visible = false;
			button10.Visible = false;
			button11.Visible = false;
			button12.Visible = false;
			button13.Visible = false;
			button14.Visible = false;
			button15.Visible = false;
			button16.Visible = false;
			
			button1.Visible = false;
			textBox1.Visible = false;
			textBox7.Visible = false;
			
			comboBox1.Visible = false;
			comboBox2.Visible = false;
			label8.Visible = false;
			
			button9.Visible = false;
			
			int w1 = this.Width;
			int h1 = this.Height;
			button6.Width = w1/2;
			button6.Height = h1/2;
			button6.Location = new Point((int)(w1/4),(int)(h1/5));
			
			Time.Interval = 10; // ms
	        Time.Tick += new EventHandler(OnTick);
	        Time.Stop();
	        
	        
	        float cx = this.Width/2;
	        float cy = 0;
	        float vx = 30;
	        float vy = 14;
	        float r = 50;
	        var B1 = new Ball(cx-r,cy,2*vx,vy,r);
	        var B2 = new Ball(cx-r*8/2,cy+2*r,vx,vy,r*3/2);
	        var B3 = new Ball(cx+r*8/2,cy+4*r,vx,2*vy,r*5/4);
	        /*L.Add(B1);
	        L.Add(B2);
	        L.Add(B3);*/
			
			/*button6.Visible = false;
			
			this.Height = 700;
			this.Width = 1200;*/
			
			/*int w = this.Width;
			int h = this.Height;
			button3.Location = new Point((int)(w*0.875),(int)(h*0.4+2*label6.Height));
			button3.Width = (int)(w*0.1);
			button3.Height = (int)(h*0.08);*/
			
		}
		
		/*
		 
		panel1 - graphing
		
		comboBox1 - graph
		comboBox2 - ball
		
		button1 - Enter
		button2 - Set Plain
		button3 - Graph
		button4 - All
		button5 - Clear
		button6 - Hello
		button7 - Login
		button8 - Register
		button9 - Ball
		button10 - Generate
		button11 - Start
		button12 - Stop
		button13 - Clear ball
		
		
		label1 - holds buttons in graph mode
		label2 - bottom info graph mode
		
		label3 - dx
		label4 - hd
		label5 - depth
		
		label6 - Comment
		label7 - Get From
		label8 - All set / Invalid login
		
		richTextbox1 - polynomial add
		
		textBox7 - main login
		
		textbox2 - dx type
		textbox3 - hd typedeim
		textbox4 - depth type
		
		texbBox5 - Comment
		textBox6 - Get From
 		
 		panel1 - graphing
 		panel2 - ball
 		
 		
		*/
		
		static string path = "Server=DIMITAR\\SQLEXPRESS;Database=Pol;Integrated Security=True;";
		static SqlConnection conn = new SqlConnection(path);
		static int id = 1;
		static bool k = false;
		static bool l = false;
		static int br = 0;
		
		static List<Ball> L = new List<Ball>();
		static float g = 0.3f;
		static float r = 50;
		static Timer Time = new Timer();
	    static bool Started = false;
	    static bool guides = true;
		
		
		static Pen[] Pens = new Pen[]{new Pen(Color.Red), new Pen(Color.Blue), new Pen(Color.Purple),
		    new Pen(Color.Orange), new Pen(Color.DeepPink), new Pen(Color.DarkGoldenrod),
		    new Pen(Color.ForestGreen), new Pen(Color.DarkBlue), new Pen(Color.Firebrick),
		    new Pen(Color.Brown), new Pen(Color.Olive)
		};

		public void Graph(Pol P, int start, int end, double depth, int w, int h, Pen p)
        {
			Graphics g = panel1.CreateGraphics();
        	double dx = (end-start)/2;
            double lx = (double)(w/dx);
            float max = P.D.Keys.Max();
            for(int i = 0; i<max; i++)
            {
                if(P.D.ContainsKey(i) == false)
                {
                    P.D.Add(i,0);
                }
            }
            P.D = P.D.OrderByDescending(w1 => w1.Key).ToDictionary(w1 => w1.Key, w1 => w1.Value);
            if(P.Z != null)
            {
                max = P.Z.D.Keys.Max();
                for(int i = 0; i<max; i++)
                {
                    if(P.Z.D.ContainsKey(i) == false)
                    {
                        P.Z.D.Add(i,0);
                    }
                }
                P.Z.D = P.Z.D.OrderByDescending(w1 => w1.Key).ToDictionary(w1 => w1.Key, w1 => w1.Value);
            }
            int f = 0;
            for(double i = start; i<=end; i += depth)
            {
                try
                {
                    f = (int)(lx*Pol.Horner(P,i));
                }
                catch(OverflowException)
                {
                    f = 0;
                }
                g.DrawRectangle(p,new Rectangle(w+(int)(lx*i),h-(int)(f),1,1));
                
            }
            
            br++;
			if(P.Z != null)
			{
				var P1 = new Pol(P);
		        var Z1 = new Pol(P.Z);
		        P1.Z = null;
		        P = Pol.Div(P1,Z1)[0];
		      Graph(P,start,end,depth,panel1.Width/2,panel1.Height/2,Pens[br%Pens.Length]);
			}
        }
		
		// Hello
		void Button6Click(object sender, EventArgs e)
		{
			button6.Visible = false;
			button1.Visible = true;
			textBox7.Visible = true;
			button7.Visible = true;
			button8.Visible = true;
			label8.Visible = true;
			
			int w = this.Width;
			int h = this.Height;
			
			textBox7.Width = w/2;
			textBox7.Location = new Point((int)(w/4),(int)(h*2/9));
			
			button1.Width = w/4;
			button1.Height = h/10;
			button1.Location = new Point((int)(w*3/8),(int)(h*3/4));
			
			button7.Width = w/4;
			button7.Height = h/10;
			button7.Location = new Point((int)(w/5),(int)(h/2));
			
			button8.Width = w/4;
			button8.Height = h/10;
			button8.Location = new Point((int)(w/2+w/20),(int)(h/2));
			
			label8.Height = h/6;
			label8.Width = w/4;
			label8.Location = new Point((int)(w*3/5+w/12),(int)(h*3/4-h/25));
			
			
		}
		void Panel1Paint(object sender, PaintEventArgs e)
		{
			
		}
		static int dx = 15;
		static int hd = 5;
		static double depth = 0.01;
		static bool added1 = false;
		
		// Enter
		void Button1Click(object sender, EventArgs e)
		{
			if(k == false)
			{
				return;
			}
			Time.Stop();
			Started = false;
			
			button1.Visible = false;
			textBox7.Visible = false;
			button7.Visible = false;
			button8.Visible = false;
			label8.Visible = false;
			comboBox2.Visible = false;
			button10.Visible = false;
			button11.Visible = false;
			button12.Visible = false;
			button13.Visible = false;
			button14.Visible = false;
			button15.Visible = false;
			button16.Visible = true;
			
			panel1.Visible = true;
			panel2.Visible = false;
			
			label3.Visible = true;
			label4.Visible = true;
			label5.Visible = true;
			label6.Visible = true;
			label7.Visible = true;
			richTextBox1.Visible = true;
			
			textBox2.Visible = true;
			textBox3.Visible = true;
			textBox4.Visible = true;
			textBox5.Visible = true;
			textBox6.Visible = true;
			
			comboBox1.Visible = true;
			button2.Visible = true;
			button3.Visible = true;
			button4.Visible = true;
			button5.Visible = true;
			button9.Visible = true;
			
			label2.Visible = true;
			label1.Visible = true;
			
			
			int w = this.Width;
			int h = this.Height;
			
			
			panel1.Location = new Point(0,0);
			panel1.Width = (int)(w*0.75);
			panel1.Height = h;
			
			label1.Location = new Point((int)(w*0.75),0);
			label1.Width = (int)(w*0.25);
			label1.Height = h;
			label2.Location = new Point((int)(w*0.75),(int)(h*0.8));
			label2.Width = (int)(w*0.25);
			label2.Height = (int)(h*0.2);
			
			label3.Width = (int)(w*0.05);
			label3.Location = new Point((int)(w*0.76),(int)(h*0.17 + 2*textBox1.Height+textBox3.Height));
			label3.Height = (int)(h*0.05);
			label4.Width = (int)(w*0.05);
			label4.Location = new Point((int)(w*0.845),(int)(h*0.17 + 2*textBox1.Height+textBox3.Height));
			label4.Height = (int)(h*0.05);
			label5.Width = (int)(w*0.05);
			label5.Location = new Point((int)(w*0.93),(int)(h*0.17 + 2*textBox1.Height+textBox3.Height));
			label5.Height = (int)(h*0.05);
			
			label6.Location = new Point((int)(w*0.76),(int)(h*0.36));
			label6.Width = (int)(w*0.07);
			label7.Location = new Point((int)(w*0.76),(int)(h*0.37+label6.Height));
			label7.Width = (int)(w*0.07);
			
			richTextBox1.Location = new Point((int)(w*0.76),(int)(h*0.015));
			richTextBox1.Width = (int)(w*0.22);
			richTextBox1.Height = (int)(h*0.1);
			
			textBox2.Location = new Point((int)(w*0.76),(int)(h*0.16+2*textBox1.Height));
			textBox2.Width = (int)(w*0.05);
			textBox3.Location = new Point((int)(w*0.845),(int)(h*0.16+2*textBox1.Height));
			textBox3.Width = (int)(w*0.05);
			textBox4.Location = new Point((int)(w*0.93),(int)(h*0.16+2*textBox1.Height));
			textBox4.Width = (int)(w*0.05);
			
			textBox5.Location = new Point((int)(w*0.77+label6.Width),(int)(h*0.36+label6.Height/4));
			textBox5.Width = (int)(w*0.14);
			textBox6.Location = new Point((int)(w*0.77+label6.Width),(int)(h*0.37+label6.Height*5/4));
			textBox6.Width = (int)(w*0.14);
			
			comboBox1.Location = new Point((int)(w*0.76),(int)(h*0.045+richTextBox1.Height));
			comboBox1.Width = (int)(w*0.22);
			
			button2.Location = new Point((int)(w*0.76),(int)(h*0.4+2*label6.Height));
			button2.Width = (int)(w*0.08);
			button2.Height = (int)(h*0.08);
			button3.Location = new Point((int)(w*0.875),(int)(h*0.4+2*label6.Height));
			button3.Width = (int)(w*0.1);
			button3.Height = (int)(h*0.08);
			
			button9.Location = new Point((int)(w*0.825),(int)(h*0.7+2*label6.Height));
			button9.Width = (int)(w*0.1);
			button9.Height = (int)(h*0.08);
			
			button16.Location = new Point((int)(w*0.765),(int)(h*0.7+2*label6.Height));
			button16.Width = (int)(w*0.05);
			button16.Height = (int)(h*0.08);
			
			button4.Location = new Point((int)(w*0.76),(int)(h*0.65));
			button4.Width = (int)(0.08*w);
			button4.Height = (int)(h*0.08);
			button5.Location = new Point((int)(w*0.875),(int)(h*0.65));
			button5.Width = (int)(w*0.1);
			button5.Height = (int)(h*0.08);
			richTextBox1.Text = "";
			
			if(!added1)
			{
				var ans = new List<string>();
				conn.Open();
				string query = "SELECT * FROM Table" + id;
				SqlCommand command = new SqlCommand(query,conn);
				SqlDataReader reader = command.ExecuteReader();
				while(reader.Read())
				{
					comboBox1.Items.Add(reader.GetString(0));
				}
				conn.Close();
				added1 = true;
				conn.Open();
				query = "SELECT * FROM TableB" + id;
				var reader1 = (new SqlCommand(query,conn)).ExecuteReader();
				while(reader1.Read())
				{
					string s = reader1.GetDouble(0) + " " + reader1.GetDouble(1) + " ";
					s += reader1.GetDouble(2) + " " + reader1.GetDouble(3) + " " + reader1.GetDouble(4);
					comboBox2.Items.Add(s);
				}
			}
		}
		
		// Ball
		void Button9Click(object sender, EventArgs e)
		{
			button9.Visible = false;
			
			panel1.Visible = false;
			
			label3.Visible = false;
			label4.Visible = false;
			label5.Visible = false;
			label6.Visible = false;
			label7.Visible = false;
			button16.Visible = false;
			//richTextBox1.Visible = false;
			
			textBox2.Visible = false;
			textBox3.Visible = false;
			textBox4.Visible = false;
			textBox5.Visible = false;
			textBox6.Visible = false;
			
			comboBox1.Visible = false;
			button2.Visible = false;
			button3.Visible = false;
			button4.Visible = false;
			button5.Visible = false;
			button9.Visible = false;
			label2.Visible = false;
			label1.Visible = false;
			
			int w = this.Width;
			int h = this.Height;
			
			
			label2.Visible = true;
			label1.Visible = true;
			panel2.Visible = true;
			
			richTextBox1.Text = "";
			
			button1.Visible = true;
			button1.Location = new Point((int)(w*0.825),(int)(h*0.7+2*label6.Height));
			button1.Width = (int)(w*0.1);
			button1.Height = (int)(h*0.08);
			button1.Text = "Graph";
			button1.BringToFront();
			
			button10.Visible = true;
			button11.Visible = true;
			button12.Visible = true;
			button13.Visible = true;
			button14.Visible = true;
			button15.Visible = true;
			
			
			button10.Location = new Point((int)(w*0.76),(int)(h*0.4+2*label6.Height));
			button10.Width = (int)(w*0.08);
			button10.Height = (int)(h*0.08);
			
			button11.Location = new Point((int)(w*0.875),(int)(h*0.4+2*label6.Height));
			button11.Width = (int)(w*0.1);
			button11.Height = (int)(h*0.08);
			
			button12.Location = new Point((int)(w*0.875),(int)(h*0.65));
			button12.Width = (int)(w*0.1);
			button12.Height = (int)(h*0.08);
			
			button13.Location = new Point((int)(w*0.76),(int)(h*0.65));
			button13.Width = (int)(0.08*w);
			button13.Height = (int)(h*0.08);
			
			button14.Location = new Point((int)(w*0.8075),(int)(h*0.4));
			button14.Width = (int)(0.08*w);
			button14.Height = (int)(h*0.08);
			
			button15.Location = new Point((int)(w*0.8075),(int)(h*0.3));
			button15.Width = (int)(0.08*w);
			button15.Height = (int)(h*0.08);
			
			comboBox2.Location = new Point((int)(w*0.76),(int)(h*0.045+richTextBox1.Height));
			comboBox2.Width = (int)(w*0.22);
			comboBox2.Visible = true;
			comboBox2.BringToFront();
			
			panel2.Location = new Point((int)(0.025*w),(int)(0.025*h));
			panel2.Width = (int)(w*0.7);
			panel2.Height = (int)(h*0.9);
		}
		
		// Set Plane
		void Button2Click(object sender, EventArgs e)
		{
			Graphics g = panel1.CreateGraphics();
			var p = new Pen(Color.Black);
			
			if(textBox2.Text.Length != 0)
			{
				dx = int.Parse(textBox2.Text);
			}
			if(textBox3.Text.Length != 0)
			{
				hd = int.Parse(textBox3.Text);
			}
			if(textBox4.Text.Length != 0)
			{
				depth = double.Parse(textBox4.Text);
			}
			int w = panel1.Width/2;
            int h = panel1.Height/2;
            double lx = (double)(w/dx);
                
            g.DrawLine(p,0,h,2*w,h);
            g.DrawLine(p,w,0,w,2*h);
            
            for(float i = h; i>=0; i -= (float)(w/dx))
            {
                g.DrawLine(p,w-hd,i,w+hd,i);
                g.DrawLine(p,w-hd,2*h-i,w+hd,2*h-i);
            }
            for(float i = w; i>=0; i -= (float)(w/dx))
            {
                g.DrawLine(p,i,h-hd,i,h+hd);
            }
            for(float i = w; i<=2*w; i += (float)(w/dx))
            {
                g.DrawLine(p,i,h-hd,i,h+hd);
            }
		}
		
		// texbBox5 - Comment
		// textBox6 - Get From
		
		// Graph
		void Button3Click(object sender, EventArgs e)
		{
			if(textBox4.Text.Length != 0)
			{
				depth = double.Parse(textBox4.Text);
			}
			var A = richTextBox1.Lines;
			for(int i = 0; i<A.Length; i++)
			{
				var P = Pol.Polify(A[i]);
				Graph(P,-dx,dx,depth,panel1.Width/2,panel1.Height/2,Pens[br%Pens.Length]);
				conn.Close();
				conn.Open();
				var S = new StringBuilder();
				for(int j = 0; j<A[i].Length; j++)
				{
					S.Append(A[i][j]);
					if(A[i][j] == '\'')
					{
						S.Append('\'');
					}
				}
				string query = "SELECT * FROM Table" + id + " WHERE pol = '" + S.ToString() + "'";
				string comment = "";
				if(textBox5.Text.Length != 0)
				{
					comment = textBox5.Text;
				}
				SqlCommand command = new SqlCommand(query,conn);
				SqlDataReader reader = command.ExecuteReader();
				if(reader.Read() == false)
				{
					conn.Close();
					conn.Open();
					(new SqlCommand("INSERT INTO Table" + id + " VALUES('" +
					S.ToString() + "','"+ comment +"')",conn)).ExecuteNonQuery();
					comboBox1.Items.Add(A[i]);
				}
				conn.Close();
			}
			string s = comboBox1.Text;
			if(s.Length != 0)
			{
				var P = Pol.Polify(s);
				Graph(P,-dx,dx,depth,panel1.Width/2,panel1.Height/2,Pens[br%Pens.Length]);
			}
			string s1 = textBox6.Text;
			if(s1.Length != 0)
			{
				conn.Close();
				conn.Open();
				string query = "SELECT * FROM Table" + id + " WHERE status = '" + s1 + "'";
				SqlCommand command = new SqlCommand(query,conn);
				SqlDataReader reader = command.ExecuteReader();
				while(reader.Read())
				{
					var P = Pol.Polify(reader.GetString(0));
			      Graph(P,-dx,dx,depth,panel1.Width/2,panel1.Height/2,Pens[br%Pens.Length]);
				}
				conn.Close();
			}
			if(A.Length == 0 && s.Length == 0 && s1.Length == 0)
			{
				conn.Close();
				conn.Open();
				string query = "SELECT COUNT(*) FROM Table" + id;
				SqlCommand command = new SqlCommand(query,conn);
				SqlDataReader reader = command.ExecuteReader();
				reader.Read();
				int count = reader.GetInt32(0);
				conn.Close();
				conn.Open();
				query = " SELECT * FROM Table" + id;
				var command1 = new SqlCommand(query,conn);
				SqlDataReader reader1 = command1.ExecuteReader();
				int rand = new Random().Next(0,count);
				int curr = 0;
				while(reader1.Read())
				{
					if(curr == rand)
					{
						string p = reader1.GetString(0);
						var P = Pol.Polify(p);
				Graph(P,-dx,dx,depth,panel1.Width/2,panel1.Height/2,Pens[br%Pens.Length]);
				conn.Close();
				return;
					}
					curr++;
				}
				conn.Close();
			}
		}
		
		// All
		void Button4Click(object sender, EventArgs e)
		{
			conn.Close();
			conn.Open();
			string query = "SELECT * FROM Table" + id;
			SqlCommand command = new SqlCommand(query,conn);
			SqlDataReader reader = command.ExecuteReader();
			while(reader.Read())
			{
				string s = reader.GetString(0);
				var P = Pol.Polify(s);
				Graph(P,-dx,dx,depth,panel1.Width/2,panel1.Height/2,Pens[br%Pens.Length]);
			}
			conn.Close();
		}
		
		// Clear
		void Button5Click(object sender, EventArgs e)
		{
			panel1.Invalidate();
			br = 0;
		}
		
		// Login
		void Button7Click(object sender, EventArgs e)
		{
			if(textBox7.Text.Length == 0)
			{
				label8.Text = "Enter Data";
				return;
			}
			var a = textBox7.Text.Split();
			if(a.Length<2)
			{
				label8.Text = "Enter Name and Password";
				return;
			}
			conn.Open();
			string query = "SELECT * FROM Users WHERE Name = '" + a[0] + 
			"' AND Pass = '" + a[1] + "'";
			SqlCommand command = new SqlCommand(query,conn);
			SqlDataReader reader = command.ExecuteReader();
			if(reader.Read() == false)
			{
				label8.Text = "Error login";
				conn.Close();
				return;
			}
			id = reader.GetInt32(0);
			conn.Close();
			k = true;
			label8.Text = "All good login";
		}
		
		// Register
		void Button8Click(object sender, EventArgs e)
		{
			if(textBox7.Text.Length == 0)
			{
				label8.Text = "Enter Data";
				return;
			}
			var a = textBox7.Text.Split();
			if(a.Length<2)
			{
				label8.Text = "Enter Name and Password";
				return;
			}
			conn.Open();
			string query = "SELECT * FROM Users WHERE Name = '" + a[0] + "'";
			SqlCommand command = new SqlCommand(query,conn);
			SqlDataReader reader = command.ExecuteReader();
			if(reader.Read())
			{
				label8.Text = "Username taken";
				conn.Close();
				return;
			}
			conn.Close();
			conn.Open();
			k = true;
			
			(new SqlCommand("INSERT INTO Users VALUES('" +
			a[0] + "','"+a[1]+"')",conn)).ExecuteNonQuery();
			conn.Close();
			conn.Open();
			query = "SELECT MAX(Id) FROM Users";
			command = new SqlCommand(query,conn);
			reader = command.ExecuteReader();
			reader.Read();
			id = reader.GetInt32(0);
			conn.Close();
			conn.Open();
			(new SqlCommand("EXEC AddT " + id, conn)).ExecuteNonQuery();
			conn.Close();
			conn.Open();
			(new SqlCommand("EXEC AddTB " + id, conn)).ExecuteNonQuery();
			conn.Close();
			label8.Text = "All good register";
			
			
			//(new SqlCommand("EXEC sp_executesql 'SELECT * FROM " +
			//"QUOTENAME('Table" + id + "')'", conn)).ExecuteNonQuery();
			
		}
		
		// Generate
		void Button10Click(object sender, EventArgs e)
		{
			var A = richTextBox1.Lines;
			string query = "SELECT * FROM TableB" + id;
			var H = new HashSet<string>();
			conn.Close();
			conn.Open();
			var reader = (new SqlCommand(query,conn)).ExecuteReader();
			while(reader.Read())
			{
				string line = reader.GetDouble(0) + " " + reader.GetDouble(1) + " ";
				line += reader.GetDouble(2) + " " + reader.GetDouble(3) + " " + reader.GetDouble(4);
				H.Add(line);
			}
			
			/*100 200 10 12 50
			400 500 13 17 100
			700 300 12 5 40
			800 100 -25 16 70
			330 130 17 24 70
			350 350 14 21 50*/
			
			conn.Close();
			conn.Open();
			for(int i = 0; i<A.Length; i++)
			{
				var curr = A[i].Split().Select(float.Parse).ToArray();
				if(curr.Length != 5)
				{
					continue;
				}
				if(!H.Contains(A[i]))
				{
					conn.Close();
					conn.Open();
					(new SqlCommand("INSERT INTO TableB" + id + " VALUES(" +
					curr[0] + ","+curr[1] +","+curr[2]+","+curr[3]+","+curr[4]+")",conn)).ExecuteNonQuery();
					comboBox2.Items.Add(A[i]);
				}
				L.Add(new Ball(curr[0],curr[1],curr[2],curr[3],curr[4]));
				
			}
			if(comboBox2.Text.Length != 0)
			{
				var curr = comboBox2.Text.Split().Select(float.Parse).ToArray();
				if(curr.Length == 5)
				{
					L.Add(new Ball(curr[0],curr[1],curr[2],curr[3],curr[4]));
				}
			}
			panel2.Invalidate();
		}
		
		// Start
		void Button11Click(object sender, EventArgs e)
		{
			Started = true;
			Time.Start();
		}
		
		// Stop
		void Button12Click(object sender, EventArgs e)
		{
			Started = false;
			Time.Stop();
		}
		
		// Ball Clear
		void Button13Click(object sender, EventArgs e)
		{
			L.Clear();
			panel2.Invalidate();
			Started = false;
			Time.Stop();
		}
		
		// Paint void
		void Panel2_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
	        foreach(Ball B in L)
	        {
	        	g.FillEllipse(Brushes.Red, B.cx, B.cy, B.r*2, B.r*2);
	        	foreach(var point in B.Q)
		        {
		        	g.DrawRectangle(new Pen(Color.Blue),point.X,point.Y,1,1);
		        }
	        }
	        
	        if(guides)
	        {
	        	for(int i = 200; i<panel2.Height; i += 200)
	        	{
	        		g.DrawLine(new Pen(Color.Black),0,i,panel2.Width,i);
	        	}
	        	for(int i = 200; i<panel2.Width; i += 200)
	        	{
	        		g.DrawLine(new Pen(Color.Black),i,0,i,panel2.Height);
	        	}
	        }
	
	        
	        
		}
		
		// Timer Tick
		private void OnTick(object sender, EventArgs e)
	    {
			foreach(Ball B in L)
	    	{
	    		if(B.cy+2*B.r+B.vy >= panel2.Height || B.cy+B.vy < 0)
		        {
		            B.vy = -B.vy;
		        }
	    		else
	    		{
	    			B.vy += g;
	    		}
	    		if(B.cx+2*B.r+B.vx >= panel2.Width || B.cx+B.vx < 0)
		        {
		            B.vx = -B.vx;
		        }
	    	}
	    	for(int i = 0; i<L.Count; i++)
	    	{
	    		for(int j = i+1; j<L.Count; j++)
	    		{
	    			float dx = L[j].cx+L[j].vx - L[i].cx-L[i].vx;
	    			float dy = L[j].cy+L[j].vy - L[i].cy-L[i].vy;
	    			if (dx*dx + dy*dy < (L[i].r + L[j].r)*(L[i].r + L[j].r))
	    			{
	    				/*L[i].cx += vx;
	    				L[i].cy += vy;
	    				L[j].cx += vx;
	    				L[j].cy += vy;*/
	    				Ball.Crash(L[i],L[j]);
					}
	    		}
	    	}
	    	foreach(Ball B in L)
	    	{
	    		B.cy += B.vy;
	    		B.cx += B.vx;
	    		/*if(B.Q.Count == 50)
		        {
		        	B.Q.Dequeue();
		        }
		        B.Q.Enqueue(new PointF(B.cx+r,B.cy+r));*/
	    	}
	        
	        panel2.Invalidate();
		}
		
		// Prevent Flickering
		private void SetDoubleBuffered(Control control)
	    {
	        typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
	                       .SetValue(control, true, null);
	    }
		
		// Guides
		void Button14Click(object sender, EventArgs e)
		{
			guides = !guides;
			panel2.Invalidate();
		}
		
		// All Balls
		void Button15Click(object sender, EventArgs e)
		{
			conn.Close();
			conn.Open();
			string query = "SELECT * FROM TableB" + id;
			var reader1 = (new SqlCommand(query,conn)).ExecuteReader();
			while(reader1.Read())
			{
				float cx1 = (float)(reader1.GetDouble(0));
				float cy1 = (float)(reader1.GetDouble(1));
				float vx1 = (float)(reader1.GetDouble(2));
				float vy1 = (float)(reader1.GetDouble(3));
				float r1 = (float)(reader1.GetDouble(4));
				L.Add(new Ball(cx1,cy1,vx1,vy1,r1));
			}
			conn.Close();
		}
		bool d = false;
		// Order
		void Button16Click(object sender, EventArgs e)
		{
			conn.Close();
			conn.Open();
			string query = "SELECT * FROM Table" + id + " ORDER BY pol";
			if(!d)
			{
				query += " DESC";
				d = true;
			}
			else
			{
				d = false;
			}
			var reader = (new SqlCommand(query,conn)).ExecuteReader();
			comboBox1.Items.Clear();
			while(reader.Read())
			{
				comboBox1.Items.Add(reader.GetString(0));
			}
			conn.Close();
		}
	}
}
