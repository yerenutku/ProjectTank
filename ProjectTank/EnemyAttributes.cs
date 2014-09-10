using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ProjectTank
{
    public class EnemyAttributes : PictureBox
    {

        bool move_left, move_right, move_up, move_down;
        public int edirection;


        public EnemyAttributes()
        {
            move_left = false;
            move_right = false;
            move_up = false;
            move_down = true;
            Random rndm = new Random();
            edirection = rndm.Next(1, 5);
            randomer();

        }
        public void randomer()
        {

        }

        public void move()
        {

            if (edirection == 1) // going right
            {
                this.Location = new Point(this.Left + 5, this.Top);
            }
            if (edirection == 2) // going left
            {
                this.Location = new Point(this.Left - 5, this.Top);
            }
            if (edirection == 3) // going down
            {
                this.Location = new Point(this.Left, this.Top + 5);
            }
            if (edirection == 4) //going up
            {
                this.Location = new Point(this.Left, this.Top - 5);
            }

        }


    }
}
