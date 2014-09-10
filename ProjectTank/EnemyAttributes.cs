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

        public bool move_left, move_right, move_up, move_down;
        public int edirection;


        public EnemyAttributes()
        {
            move_left = false;
            move_right = false;
            move_up = false;
            move_down = false;
            Random rndm = new Random();
            edirection = rndm.Next(1, 5);
            

        }
        public void randomer()
        {
            Random func_rndm = new Random();
            edirection=func_rndm.Next(1, 5);
        }

        public void move()
        {

            if (edirection == 1&& move_right ==true) // going right
            {
                this.Location = new Point(this.Left + 5, this.Top);
            }
            if (edirection == 2&& move_left == true) // going left
            {
                this.Location = new Point(this.Left - 5, this.Top);
            }
            if (edirection == 3&& move_down==true) // going down
            {
                this.Location = new Point(this.Left, this.Top + 5);
            }
            if (edirection == 4 && move_up == true) //going up
            {
                this.Location = new Point(this.Left, this.Top - 5);
            }

        }


    }
}
