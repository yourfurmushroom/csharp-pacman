using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace endOfTerm
{
    class Player
    {
        public Vector2 position;
        public Vector2 velocity;
        public Player()
        {
            position = new Vector2(1, 1);
            velocity = new Vector2(1, 0);
        }

        public void Initialize(Context context)
        {
            position = context.basePosition;
            velocity = context.directory;
            var task = Update(context);
        }

        async static Task Update(Context context)
        {
            while (true)
            {
                context.Update();
                context.player.velocity = context.directory;
                if (Form1.Buffer[context.player.position.x + context.player.velocity.x, context.player.position.y + context.player.velocity.y] == 1)
                {
                    context.player.position += context.player.velocity;
                }
                context.form.pictureBox1.Location = new Point(context.player.position.x*context.pixelSize,context.player.position.y * context.pixelSize);
                Debug.WriteLine(context.player.position.x+","+context.player.position.y);
                await Task.Delay(500);

            }
        }
    }
}
