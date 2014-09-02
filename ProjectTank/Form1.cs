﻿using System;
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
        #region Global Değişkenler
        bool right, left, up, down;
        bool upkey, rightkey, leftkey, downkey;
        bool fire;
        String LastButton;
        ArrayList walls = new ArrayList();
        ArrayList bullets = new ArrayList();
        #endregion
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
            PictureBox sended= new PictureBox();
            if(bullets.Count!=0){
            sended = bullets[bullets.Count-1] as PictureBox;
            bulletMove(LastButton, sended);
            }
        }

        private void tank1_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) { leftkey = true; LastButton="leftkey" ;}
            if (e.KeyCode == Keys.Right) { rightkey = true; LastButton="rightkey" ;}
            if (e.KeyCode == Keys.Up) { upkey = true; LastButton = "upkey";}
            if (e.KeyCode == Keys.Down) { downkey = true; LastButton = "downkey"; }
            if (e.KeyCode == Keys.Space)
            {
                bulletCreate(LastButton);
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) { leftkey = false;  }
            if (e.KeyCode == Keys.Right) { rightkey = false;  }
            if (e.KeyCode == Keys.Up) { upkey = false;  }
            if (e.KeyCode == Keys.Down) { downkey = false; }
            if (e.KeyCode == Keys.Space) { fire = false; }
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void bulletCreate(String direction)
        {
            fire = true; Console.WriteLine("bullet create and showing on the screen");
            if (direction == "rightkey")
            {
                PictureBox bullet = new PictureBox();
                bullet.Name = "bullet";
                bullet.Location = new Point(tank1.Left+tank1.Width+10, tank1.Top+tank1.Height/2-5);
                bullet.Size = new Size(15, 15);
                bullet.BackColor = Color.Black;
                //bullet.Image = (Bitmap)(e.Data.GetData(DataFormats.Bitmap));
                bullet.Visible = true;
                panel1.Controls.Add(bullet);
                bullets.Add(bullet);
               // bulletMove("rightkey",bullet);
            }
            
            if (direction == "leftkey")
            {
                PictureBox bullet = new PictureBox();
                bullet.Name = "bullet";
                bullet.Location = new Point(tank1.Left -10, tank1.Top);
                bullet.Size = new Size(15, 15);
                bullet.BackColor = Color.Black;
                //bullet.Image = (Bitmap)(e.Data.GetData(DataFormats.Bitmap));
                bullet.Visible = true;
                panel1.Controls.Add(bullet);
                bullets.Add(bullet);
              //  bulletMove("leftkey",bullet);
            }
            if (direction == "upkey")
            {
                PictureBox bullet = new PictureBox();
                bullet.Name = "bullet";
                bullet.Location = new Point(tank1.Left, tank1.Top-10);
                bullet.Size = new Size(15, 15);
                bullet.BackColor = Color.Black;
                //bullet.Image = (Bitmap)(e.Data.GetData(DataFormats.Bitmap));
                bullet.Visible = true;
                panel1.Controls.Add(bullet);
                bullets.Add(bullet);
                //bulletMove("upkey",bullet);
            }
            if (direction == "downkey")
            {
                PictureBox bullet = new PictureBox();
                bullet.Name = "bullet";
                bullet.Location = new Point(tank1.Left, tank1.Top+ tank1.Height+10);
                bullet.Size = new Size(15, 15);
                bullet.BackColor = Color.Black;
                //bullet.Image = (Bitmap)(e.Data.GetData(DataFormats.Bitmap));
                bullet.Visible = true;
                panel1.Controls.Add(bullet);
                bullets.Add(bullet);
               // bulletMove("downkey",bullet);
            }
        }
        public void bulletMove(String direction,PictureBox LastBullet)
        {
            if (direction == "rightkey")
            {
                    LastBullet.Location = new Point(LastBullet.Left + 10, LastBullet.Top);
                
            }
            else if (direction == "leftkey")
            {
                        LastBullet.Location = new Point(LastBullet.Left - 10, LastBullet.Top);
                 
            }
             else if (direction == "upkey")
            {
                
                    LastBullet.Location = new Point(LastBullet.Left, LastBullet.Top - 10);
                
            }
            else if (direction == "downkey")
            {
                
                    LastBullet.Location = new Point(LastBullet.Left, LastBullet.Top + 10);
                
            }

        }
        

        

    }
}
