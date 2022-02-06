using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace endOfTerm
{
    class Context
    {
        public Form1 form;
        public List<Monster> monsters;
        public Rectangle baseLeft;
        public Rectangle baseMiddle;
        public Rectangle baseRight;
        public Vector2 basePosition = new Vector2(1, 1);
        public Player player=new Player();
        public Vector2 directory=new Vector2(1,0);
        public int pixelSize=50;


        public void Update()
        {
            directory =new Vector2 (form.direction.X,form.direction.Y);
            Debug.WriteLine(form.direction.X+"aaaa"+ form.direction.Y);
        }

        public void Initialize()
        {
            player.Initialize(this);
        }
    }
}
