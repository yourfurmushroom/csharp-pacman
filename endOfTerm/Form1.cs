using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace endOfTerm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            context.form = this;
            velocity = new Vector2(1, 0);
            context.Initialize();
            task = Update(context);
            pictureBox1.BringToFront();
            context.lifes.Add(pictureBox2);
            context.baseLifes.Add(pictureBox2);
            context.lifes.Add(pictureBox3);
            context.baseLifes.Add(pictureBox3);
            context.lifes.Add(pictureBox4);
            context.baseLifes.Add(pictureBox4);
        }

        Context context = new Context();
        public Vector2 velocity;
        public Task task;
        
        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up && context.isContinue)
            {
                velocity = new Vector2(0, -1);
                pictureBox1.Image = Properties.Resources._1111;
            }
            if (e.KeyCode == Keys.Down && context.isContinue)
            {
                velocity = new Vector2(0, 1);
                pictureBox1.Image = Properties.Resources._2222;
            }
            if (e.KeyCode == Keys.Left && context.isContinue)
            {
                velocity = new Vector2(-1, 0);
                pictureBox1.Image = Properties.Resources.Untitled_1;
            }
            if (e.KeyCode == Keys.Right && context.isContinue)
            {
                velocity = new Vector2(1, 0);
                pictureBox1.Image = Properties.Resources._3333;
            }
            if (e.KeyCode == Keys.R && !context.isContinue)
            {
                foreach (var life in context.lifes.ToList())
                {
                    context.form.Controls.Remove(life);
                }
                foreach (var life in context.baseLifes.ToList())
                {
                    context.form.Controls.Add(life);
                    context.lifes.Add(life);
                }
                context.ghost1.position = new Vector2(context.baseLeft.X,context.baseLeft.Y);
                context.ghost2.position = new Vector2(context.baseMiddle.X,context.baseMiddle.Y);
                context.ghost3.position = new Vector2(context.baseRight.X,context.baseRight.Y);
                context.ghost4.position = new Vector2(context.baseTop.X,context.baseTop.Y);
                context.player.position = context.basePosition;
                context.player.upperRight = new Rectangle(context.basePosition.x + context.form.pictureBox1.Width - 1, context.basePosition.y + 1, 1, 1);
                context.player.bottomRight = new Rectangle(context.basePosition.x + context.form.pictureBox1.Width - 1, context.basePosition.y + context.form.pictureBox1.Height - 1, 1, 1);
                context.player.bottomLeft = new Rectangle(context.basePosition.x, context.basePosition.y + context.form.pictureBox1.Height, 1, 1);

                context.score = 0;
                context.life = 3;
                context.isContinue = true;
                task = Update(context);
            }

        }

        async static Task Update(Context context)
        {
            while(context.isContinue)
            {
                context.form.label4.Text = context.score.ToString();
                context.Update();
                context.player.Update(context);
                GC.Collect();
                await Task.Delay(30);
            }
        }
    }
}
