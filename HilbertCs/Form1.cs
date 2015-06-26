using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tao.FreeGlut;
using Tao.OpenGl;
using Tao.Platform.Windows;

namespace Hilbert_XYF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AnT.InitializeContexts();
        }

        private static string axiom = "X";
        private static string newX = "-YF+XFX+FY-";
        private static string newY = "+XF-YFY-FX+";
        private static int angle = 90;
        private static int d_angle = 90;
        private static int level = 0;
        private static double step  = 0;
        private static double X0 = 0, Y0 = 0;
        void Draw_Fractal(ref string  xx, int l )
        {
          //  Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
           // Gl.glClearColor(1, 1, 1, 1);
           // Gl.glColor3b(0, 0, 0);

            if (l > 0)
            {               
                foreach (char c in xx)

                    switch (c)
                    {
                        case '+':
                            {
                                angle -= d_angle;
                                break;
                            }
                        case '-':
                            {
                                angle += d_angle;
                                break;
                            }
                        case 'F':
                            {
                                Gl.glBegin(Gl.GL_LINES);
                                Gl.glColor3d(0.5, 0, 0.5);
                                Gl.glVertex2d(X0, Y0);
                                Gl.glVertex2d(X0 + step * Math.Cos(angle * Math.PI / 180), Y0 + step * Math.Sin(angle * Math.PI / 180));
                                Gl.glEnd();
                                X0 = X0 + step * Math.Cos(angle * Math.PI / 180);
                                Y0 = Y0 + step * Math.Sin(angle * Math.PI / 180);

                                break;
                            }
                        case 'X':
                            {
                                if (l == 1) break;
                                
                                Draw_Fractal(ref newX, l-1);
                                break;
                            }
                        case 'Y':
                            {
                                if (l == 1) break;
                               // --level;
                                Draw_Fractal(ref newY, l - 1);
                                break;
                            }
                        default:
                                break;
                    }


            }
            else return;

            //Gl.glFlush();
            //AnT.Invalidate();
        }

        private void simpleOpenGlControl1_Load(object sender, EventArgs e)
        {

      
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // инициализация библиотеки GLUT 
            Glut.glutInit();
            // инициализация режима окна 
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

            // устанавливаем цвет очистки окна 
            Gl.glClearColor(255, 255, 255, 1);
            Glu.gluOrtho2D(0.0,  AnT.Width, 0.0, AnT.Height);

            // устанавливаем порт вывода, основываясь на размерах элемента управления AnT 
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);

            // устанавливаем проекционную матрицу 
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            // очищаем ее 
            Gl.glLoadIdentity();

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
        }

        private void button1_Click(object sender, EventArgs e)
        {
              Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
             Gl.glClearColor(1, 1, 1, 1);
            X0 = AnT.Width - 5; Y0 = 3;
            step = (AnT.Width - 10) / (Math.Pow(2, level) - 1);
            Draw_Fractal(ref newX, level);

            Gl.glFlush();
            AnT.Invalidate();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            // генерация коэфицента 
            level = trackBar1.Value;
            // вывод значения коэфицента, управляемого данным ползунком. 
            // (под TrackBa'ом) 
            label1.Text = level.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
