using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace ProjectTank
{
    public class EnemyAttributes : PictureBox
    {

        public bool move_left, move_right, move_up, move_down;
        public int edirection;

        String enemy_down = Application.StartupPath + "\\Assets\\Enemy_Tank\\enemy_down.png";
        String enemy_up = Application.StartupPath + "\\Assets\\Enemy_Tank\\enemy_up.png";
        String enemy_right = Application.StartupPath + "\\Assets\\Enemy_Tank\\enemy_right.png";
        String enemy_left = Application.StartupPath + "\\Assets\\Enemy_Tank\\enemy_left.png";

        Panel panel1;
        public Bullet_Attributes bullet = new Bullet_Attributes();
        public bool isfired;
        public ArrayList bullets = new ArrayList();


        public EnemyAttributes(Panel mainpanel)
        {

            panel1 = mainpanel;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.BackColor = Color.Transparent;
            move_left = false;
            move_right = false;
            move_up = false;
            move_down = false;
            Random rndm = new Random();
            edirection = rndm.Next(1, 5);
            isfired = false;
            

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
                this.Image = Image.FromFile(enemy_right);
            }
            if (edirection == 2&& move_left == true) // going left
            {
                this.Location = new Point(this.Left - 5, this.Top);
                this.Image = Image.FromFile(enemy_left);
            }
            if (edirection == 3&& move_down==true) // going down
            {
                this.Location = new Point(this.Left, this.Top + 5);
                this.Image = Image.FromFile(enemy_down);
            }
            if (edirection == 4 && move_up == true) //going up
            {
                this.Location = new Point(this.Left, this.Top - 5);
                this.Image = Image.FromFile(enemy_up);
            }

        }

        public void bulletCreate()
        {
            isfired = true;
            
            Console.WriteLine("bullet create and showing on the screen");
            if (edirection == 1)
            {
                //Bullet_Attributes bullet = new Bullet_Attributes();
                //PictureBox bullet = new PictureBox();
                bullet.Name = "bullet";
                bullet.Location = new Point(this.Left + this.Width + 10, this.Top + this.Height / 2 - 5);
                bullet.Size = new Size(15, 15);
                bullet.BackColor = Color.Red;
                //bullet.Image = (Bitmap)(e.Data.GetData(DataFormats.Bitmap));
                bullet.Visible = true;
                bullet.direction = "right";
                panel1.Controls.Add(bullet);
                bullets.Add(bullet);
                // bulletMove("rightkey",bullet);
            }

            if (edirection == 2)
            {
                //Bullet_Attributes bullet = new Bullet_Attributes();
                //PictureBox bullet = new PictureBox();
                bullet.Name = "bullet";
                bullet.Location = new Point(this.Left - 10, this.Top + this.Height / 2 - 5);
                bullet.Size = new Size(15, 15);
                bullet.BackColor = Color.Red;
                //bullet.Image = (Bitmap)(e.Data.GetData(DataFormats.Bitmap));
                bullet.Visible = true;
                bullet.direction = "left";
                panel1.Controls.Add(bullet);
                bullets.Add(bullet);
                //  bulletMove("leftkey",bullet);
            }
            if (edirection == 4)
            {
                //Bullet_Attributes bullet = new Bullet_Attributes();
                //PictureBox bullet = new PictureBox();
                bullet.Name = "bullet";
                bullet.Location = new Point(this.Left + this.Width / 2 - 5, this.Top - 10);
                bullet.Size = new Size(15, 15);
                bullet.BackColor = Color.Red;
                //bullet.Image = (Bitmap)(e.Data.GetData(DataFormats.Bitmap));
                bullet.Visible = true;
                bullet.direction = "up";
                panel1.Controls.Add(bullet);
                bullets.Add(bullet);
                //bulletMove("upkey",bullet);
            }
            if (edirection == 3)
            {
                //Bullet_Attributes bullet = new Bullet_Attributes();
                //PictureBox bullet = new PictureBox();
                bullet.Name = "bullet";
                bullet.Location = new Point(this.Left + this.Width / 2 - 5, this.Top + this.Height + 10);
                bullet.Size = new Size(15, 15);
                bullet.BackColor = Color.Red;
                //bullet.Image = (Bitmap)(e.Data.GetData(DataFormats.Bitmap));
                bullet.Visible = true;
                bullet.direction = "down";
                //String path_down = Application.StartupPath + "\\Assets\\Player_Tank\\idle_down.png";

                panel1.Controls.Add(bullet);
                bullets.Add(bullet);
                // bulletMove("downkey",bullet);
            }


        }

        public void bulletMove()
        {
            if (this.bullet.direction == "right")
            {
                this.bullet.Location = new Point(this.bullet.Left + 10, this.bullet.Top);

            }
            else if (this.bullet.direction == "left")
            {
                this.bullet.Location = new Point(this.bullet.Left - 10, this.bullet.Top);

            }
            else if (this.bullet.direction == "up")
            {

                this.bullet.Location = new Point(this.bullet.Left, this.bullet.Top - 10);

            }
            else if (this.bullet.direction == "down")
            {

                this.bullet.Location = new Point(this.bullet.Left, this.bullet.Top + 10);

            }
        }

        //public void bulletCollision()
        //{
        //    foreach (PictureBox wallmember in walls)
        //    {
        //        Bullet_Attributes fired_bullet = new Bullet_Attributes();
        //        if (bullets[bullets.Count - 1] != null)
        //            fired_bullet = bullets[bullets.Count - 1] as Bullet_Attributes;
        //        if (wallmember.Bounds.IntersectsWith(new Rectangle(fired_bullet.Location.X, fired_bullet.Location.Y, fired_bullet.Width, fired_bullet.Height)))
        //        {
        //            isfired = false;
        //            #region Destroy and Damage Processes
        //            if (wallmember.Tag == "Enemy")
        //            {
        //                Console.WriteLine("Enemy hit");
        //            }
        //            if (wallmember.Tag == "d_Wall")
        //            {
        //                panel1.Controls.Remove(fired_bullet);
        //                panel1.Controls.Remove(wallmember);
        //                //walls.RemoveAt(walls.IndexOf(wallmember));
        //                bullets.Remove(fired_bullet);
        //                walls.Remove(wallmember);
        //                break;
        //                //Console.WriteLine("Bricket has been hit");
        //            }
        //            if (wallmember.Tag == "BorderWalls" || wallmember.Tag == "nd_Wall")
        //            {
        //                panel1.Controls.Remove(fired_bullet);
        //                bullets.Remove(fired_bullet);
        //                break;
        //            }
        //            #endregion
        //        }
        //    }
        //}

    }
}
