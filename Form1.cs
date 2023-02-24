using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Colisiones_Corregida
{
        public partial class Form1 : Form
        {
            Bitmap bmp;
            Graphics g;
            Ball ball;
            List<Ball> balls;
            SolidBrush brush;
            static float deltaTime;

            Timer timer = new Timer();

            public Form1()
            {
                InitializeComponent();

                bmp = new Bitmap(PCT_CANVAS.Width, PCT_CANVAS.Height);
                g = Graphics.FromImage(bmp);

                brush = new SolidBrush(Color.White);

                Random rand = new Random(); // declare and initialize the 'rand' variable

                balls = new List<Ball>();
                for (int b = 0; b < 5; b++)
                {
                    balls.Add(new Ball(rand, PCT_CANVAS.Size, b));
                }

                PCT_CANVAS.Image = bmp;
                timer.Interval = 10;
                timer.Tick += new EventHandler(TIMER_Tick);
                timer.Start();
            }




            private void TIMER_Tick(object sender, EventArgs e)
            {
                g.Clear(Color.Black);
                Parallel.For(0, balls.Count, b =>//ACTUALIZAMOS EN PARALELO
                {
                    Ball P;
                    balls[b].Update(deltaTime, balls);
                    P = balls[b];
                });

                Ball p;
                for (int b = 0; b < balls.Count; b++)//PINTAMOS EN SECUENCIA
                {
                    p = balls[b];
                    g.FillEllipse(new SolidBrush(p.c), p.x - p.radio, p.y - p.radio, p.radio * 2, p.radio * 2);
                }

                PCT_CANVAS.Invalidate();
                deltaTime += .1f;
            }

            private void button1_Click(object sender, EventArgs e)
            {
                Random rand = new Random();
                balls.Add(new Ball(rand, PCT_CANVAS.Size, balls.Count));
            }
        }

    }
