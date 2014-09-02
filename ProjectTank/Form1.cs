using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;


namespace ProjectTank
{
    public partial class Form1 : Form
    {

        bool right, left, up, down;
        bool upkey, rightkey, leftkey, downkey;
        ArrayList walls = new ArrayList();
        //ilist kullan.
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            walls.Add(wall1);
            walls.Add(wall2);
            walls.Add(wall3);
            walls.Add(wall4);
            walls.Add(wall5);
            walls.Add(wall6);
            walls.Add(wall7);
            walls.Add(wall8);
            walls.Add(wall9);
            walls.Add(wall10);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (right == true && rightkey==true) { tank1.Left += 5; }
            if (left == true && leftkey==true ) { tank1.Left -= 5; }
            if (up == true && upkey == true) { tank1.Top -= 5; }
            if (down == true && downkey == true) { tank1.Top += 5; }
            right = true; left = true; up = true; down = true;
            collision();

            
            
        }

        private void tank1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) { leftkey = true; }
            if (e.KeyCode == Keys.Right) { rightkey = true; }
            if (e.KeyCode == Keys.Up) { upkey = true;}
            if (e.KeyCode == Keys.Down) { downkey = true;}
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) { leftkey = false;  }
            if (e.KeyCode == Keys.Right) { rightkey = false;  }
            if (e.KeyCode == Keys.Up) { upkey = false;  }
            if (e.KeyCode == Keys.Down) { downkey = false; }

        }
        public void collision()
        {
            //her collision için foreach döner.
            //hepsi tek bir foreach içinde döner ise kesişme noktalarında sadece tek bir yön çalışır ve foreach kırılır.
            //moving actions
            foreach (PictureBox wallmember in walls){

                if(wallmember.Bounds.IntersectsWith(new Rectangle(tank1.Location.X+5,tank1.Location.Y,tank1.Width,tank1.Height)))
                {
                    //Console.WriteLine("collision detected"+ wallmember.Name);
                    right = false; break;

                } right = true;
            }

                foreach (PictureBox wallmember in walls)
                {
                    if (wallmember.Bounds.IntersectsWith(new Rectangle(tank1.Location.X - 5, tank1.Location.Y, tank1.Width, tank1.Height)))
                    {
                        left = false; break;
                    } left = true;
                }
                foreach (PictureBox wallmember in walls)
                {
                 if (wallmember.Bounds.IntersectsWith(new Rectangle(tank1.Location.X , tank1.Location.Y+5, tank1.Width, tank1.Height)))
                {
                    down = false; break;
                } down = true;
                }
                foreach (PictureBox wallmember in walls)
                {
                 if (wallmember.Bounds.IntersectsWith(new Rectangle(tank1.Location.X, tank1.Location.Y-5, tank1.Width, tank1.Height)))
                {
                    up = false; break;
                } up = true;

                   
                
                //if(tank1.Bounds.IntersectsWith(wallmember.Bounds))
                   //{
                   //    Console.WriteLine("collision ihlal");
                   //}
                }


            #region KoordinatBazlıKontrol (comment)
            //foreach (PictureBox wallmember in walls)
            //{
            //    //aşağıdan yukarıya giriş engeli
            //    if (tank1.Top - 5 <= wallmember.Bottom && tank1.Top - 5 >= wallmember.Top &&
            //       tank1.Left + tank1.Width >= wallmember.Left && tank1.Left <= wallmember.Left + wallmember.Width)
            //    {
            //        up = false;
            //        break;
            //    }
            //    else up = true;

            //    //yukarıdan aşağıya giriş engeli

            //    if (tank1.Bottom + 5 >= wallmember.Top && tank1.Bottom + 5 <= wallmember.Bottom &&
            //        tank1.Left + tank1.Width >= wallmember.Left && tank1.Left <= wallmember.Left + wallmember.Width)
            //    {
            //        down = false;
            //        break;
            //    }
            //    else down = true;

            //    //soldan sağa  giriş engeli
            //    if (tank1.Right + 6 > wallmember.Left && tank1.Right + 6 < wallmember.Right
            //        && tank1.Bottom > wallmember.Top && tank1.Top < wallmember.Bottom)
            //    {
            //        right = false;
            //        break;

            //    }
            //    else right = true;


            //    //sağdan sola giriş engeli
            //    if (tank1.Left - 6 < wallmember.Right && tank1.Left - 6 > wallmember.Left
            //        && tank1.Bottom > wallmember.Top && tank1.Top <= wallmember.Bottom)
            //    {
            //        left = false;
            //        break;
            //    }
            //    else left = true;
            //}
            #endregion

        }

        

        

    }
}
