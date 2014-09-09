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
        #region Global Değişkenler
        bool right, left, up, down;
        bool upkey, rightkey, leftkey, downkey;
        bool fire, isfired;

        String LastButton;

        ArrayList walls = new ArrayList();
        ArrayList bullets = new ArrayList();

        //Player Images Path
        String path_down = Application.StartupPath + "\\Assets\\Player_Tank\\idle_down.png";
        String path_up = Application.StartupPath + "\\Assets\\Player_Tank\\idle_up.png";
        String path_right = Application.StartupPath + "\\Assets\\Player_Tank\\idle_right.png";
        String path_left = Application.StartupPath + "\\Assets\\Player_Tank\\idle_left.png";
        //Walls Images Path
        String path_d_wall = Application.StartupPath + "\\Assets\\Walls\\d_wall.png";


        #endregion
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            isfired = false;
            //walls.Add(wall1);
            //walls.Add(wall2);
            //walls.Add(wall3);
            //walls.Add(wall4);
            //walls.Add(wall5);
            //walls.Add(wall6);
            //walls.Add(wall7);
            //walls.Add(wall8);
            //walls.Add(wall9);
            //walls.Add(wall10);
            Control.ControlCollection coll = panel1.Controls;
            foreach (Control c in coll)
            {
                if (c.Tag == "d_Wall" || c.Tag == "BorderWalls" || c.Tag =="nd_Wall" || c.Tag == "Enemy" )
                    walls.Add(c);
                
            }
            //foreach (PictureBox s in walls)
            //{
            //    s.Image = Image.FromFile(path_d_wall);
            //}

           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (right == true && rightkey==true) { tank1.Left += 2; }
            if (left == true && leftkey==true ) { tank1.Left -= 2; }
            if (up == true && upkey == true) { tank1.Top -= 2; }
            if (down == true && downkey == true) { tank1.Top += 2; }
                

            right = true; left = true; up = true; down = true;
            collision();
            Bullet_Attributes sended= new Bullet_Attributes();
            if(bullets.Count!=0){
            sended = bullets[bullets.Count-1] as Bullet_Attributes;
            bulletMove(sended);
            bulletCollision();
            }
                
        }

        private void tank1_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) { leftkey = true; LastButton = "leftkey"; tank1.Image = Image.FromFile(path_left); }
            if (e.KeyCode == Keys.Right) { rightkey = true; LastButton = "rightkey"; tank1.Image = Image.FromFile(path_right); }
            if (e.KeyCode == Keys.Up) { upkey = true; LastButton = "upkey"; tank1.Image = Image.FromFile(path_up); }
            if (e.KeyCode == Keys.Down) { downkey = true; LastButton = "downkey"; tank1.Image = Image.FromFile(path_down); }
            if (e.KeyCode == Keys.Space )
            {
                
                bulletCreate(LastButton);
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) { leftkey = false;}
            if (e.KeyCode == Keys.Right) { rightkey = false;}
            if (e.KeyCode == Keys.Up) { upkey = false;}
            if (e.KeyCode == Keys.Down) { downkey = false;}
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
            fire = true;
            if (isfired == false )
            {
                Console.WriteLine("bullet create and showing on the screen");
                if (direction == "rightkey")
                {
                    Bullet_Attributes bullet = new Bullet_Attributes();
                    //PictureBox bullet = new PictureBox();
                    bullet.Name = "bullet";
                    bullet.Location = new Point(tank1.Left + tank1.Width + 10, tank1.Top + tank1.Height / 2 - 5);
                    bullet.Size = new Size(15, 15);
                    bullet.BackColor = Color.Red;
                    //bullet.Image = (Bitmap)(e.Data.GetData(DataFormats.Bitmap));
                    bullet.Visible = true;
                    bullet.direction = "right";
                    panel1.Controls.Add(bullet);
                    bullets.Add(bullet);
                    // bulletMove("rightkey",bullet);
                }

                if (direction == "leftkey")
                {
                    Bullet_Attributes bullet = new Bullet_Attributes();
                    //PictureBox bullet = new PictureBox();
                    bullet.Name = "bullet";
                    bullet.Location = new Point(tank1.Left - 10, tank1.Top + tank1.Height/2 -5);
                    bullet.Size = new Size(15, 15);
                    bullet.BackColor = Color.Red;
                    //bullet.Image = (Bitmap)(e.Data.GetData(DataFormats.Bitmap));
                    bullet.Visible = true;
                    bullet.direction = "left";
                    panel1.Controls.Add(bullet);
                    bullets.Add(bullet);
                    //  bulletMove("leftkey",bullet);
                }
                if (direction == "upkey")
                {
                    Bullet_Attributes bullet = new Bullet_Attributes();
                    //PictureBox bullet = new PictureBox();
                    bullet.Name = "bullet";
                    bullet.Location = new Point(tank1.Left +tank1.Width/2 -5 , tank1.Top - 10);
                    bullet.Size = new Size(15, 15);
                    bullet.BackColor = Color.Red;
                    //bullet.Image = (Bitmap)(e.Data.GetData(DataFormats.Bitmap));
                    bullet.Visible = true;
                    bullet.direction = "up";
                    panel1.Controls.Add(bullet);
                    bullets.Add(bullet);
                    //bulletMove("upkey",bullet);
                }
                if (direction == "downkey")
                {
                    Bullet_Attributes bullet = new Bullet_Attributes();
                    //PictureBox bullet = new PictureBox();
                    bullet.Name = "bullet";
                    bullet.Location = new Point(tank1.Left + tank1.Width/2 -5  , tank1.Top + tank1.Height + 10);
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
            isfired = true;
        }
        public void bulletMove(Bullet_Attributes currentBullet)
        {
            if (currentBullet.direction == "right")
            {
                currentBullet.Location = new Point(currentBullet.Left + 10, currentBullet.Top);
                
            }
            else if (currentBullet.direction == "left")
            {
                currentBullet.Location = new Point(currentBullet.Left - 10, currentBullet.Top);
                 
            }
            else if (currentBullet.direction == "up")
            {

                currentBullet.Location = new Point(currentBullet.Left, currentBullet.Top - 10);
                
            }
            else if (currentBullet.direction == "down")
            {

                currentBullet.Location = new Point(currentBullet.Left, currentBullet.Top + 10);
                
            }

        }

        public void bulletCollision()
        {
            foreach (PictureBox wallmember in walls)
            {
                Bullet_Attributes fired_bullet = new Bullet_Attributes();
                if(bullets[bullets.Count -1] != null)
                fired_bullet=bullets[bullets.Count-1] as Bullet_Attributes;
                if(wallmember.Bounds.IntersectsWith(new Rectangle(fired_bullet.Location.X,fired_bullet.Location.Y,fired_bullet.Width,fired_bullet.Height)))
                {
                    isfired = false;
                    #region Destroy and Damage Processes
                    if (wallmember.Tag == "Enemy")
                    {
                        Console.WriteLine("Enemy hit");
                    }
                    if (wallmember.Tag == "d_Wall")
                    {
                        panel1.Controls.Remove(fired_bullet);
                        
                        panel1.Controls.Remove(wallmember);
                        //walls.RemoveAt(walls.IndexOf(wallmember));
                        bullets.Remove(fired_bullet);
                        walls.Remove(wallmember);
                        break;
                        //Console.WriteLine("Bricket has been hit");
                    }
                    if (wallmember.Tag == "BorderWalls" || wallmember.Tag == "nd_Wall")
                    {
                        panel1.Controls.Remove(fired_bullet);
                        bullets.Remove(fired_bullet);
                        break;
                    }

                    #endregion
                }

            }


        }


        



    }//clas bitişi
}
