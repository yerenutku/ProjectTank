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

        //Enemy Tank Moves
        bool isenemyMove_left = true;
        bool isenemyMove_right = true;
        bool isenemyMove_down = true;
        bool isenemyMove_up = true;
        int random_direction;

        String LastButton;
        ArrayList temp_form = new ArrayList();
        ArrayList walls = new ArrayList();
        ArrayList bullets = new ArrayList();
        ArrayList enemyTanks = new ArrayList();
        //Player Images Path
        String path_down = Application.StartupPath + "\\Assets\\Player_Tank\\idle_down.png";
        String path_up = Application.StartupPath + "\\Assets\\Player_Tank\\idle_up.png";
        String path_right = Application.StartupPath + "\\Assets\\Player_Tank\\idle_right.png";
        String path_left = Application.StartupPath + "\\Assets\\Player_Tank\\idle_left.png";
        //Walls Images Path
        String path_d_wall = Application.StartupPath + "\\Assets\\Walls\\d_wall.png";


        #endregion
        Panel panel_Temp;

        Form1 form1;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            
            form1 = new Form1();
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
                if (c.Tag == "d_Wall" || c.Tag == "BorderWalls" || c.Tag =="nd_Wall" || c.Tag == "bird" )
                    walls.Add(c);
                
            }
            foreach (Control c in coll)
            {
                temp_form.Add(c);
            }
            //foreach (PictureBox s in walls)
            //{
            //    s.Image = Image.FromFile(path_d_wall);
            //}

            //foreach (Control c in coll)
            //{
            //    if (c.Tag == "Enemy")
            //        enemyTanks.Add(c);

            //}

            EnemyAttributes enemyObj = new EnemyAttributes(panel1);
            enemyObj.Name = "enemy";
            enemyObj.Tag = "Enemy";
            enemyObj.Location = new Point(60, 72);
            enemyObj.Size = new Size(50, 50);
            enemyObj.Visible = true;
            panel1.Controls.Add(enemyObj);
            enemyTanks.Add(enemyObj);

            EnemyAttributes enemyObj2 = new EnemyAttributes(panel1);
            enemyObj2.Name = "enemy";
            enemyObj2.Tag = "Enemy";
            enemyObj2.Location = new Point(1025, 68);
            enemyObj2.Size = new Size(50, 50);
            enemyObj2.Visible = true;
            panel1.Controls.Add(enemyObj2);
            enemyTanks.Add(enemyObj2);


            EnemyAttributes enemyObj3 = new EnemyAttributes(panel1);
            enemyObj3.Name = "enemy";
            enemyObj3.Tag = "Enemy";
            enemyObj3.Location = new Point(1025, 320);
            enemyObj3.Size = new Size(50, 50);
            enemyObj3.Visible = true;
            panel1.Controls.Add(enemyObj3);
            enemyTanks.Add(enemyObj3);



            EnemyAttributes enemyObj4 = new EnemyAttributes(panel1);
            enemyObj4.Name = "enemy";
            enemyObj4.Tag = "Enemy";
            enemyObj4.Location = new Point(60, 315);
            enemyObj4.Size = new Size(50, 50);
            enemyObj4.Visible = true;
            panel1.Controls.Add(enemyObj4);
            enemyTanks.Add(enemyObj4);

            panel_Temp = panel1;
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
            foreach (EnemyAttributes enemytanks in enemyTanks)
            {
                if (enemytanks.isfired == false)
                {
                    enemytanks.bulletCreate();
                    
                    
                }
                enemyBulletCollision();
                enemytanks.bulletMove();
                
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
            
            
            
            
                foreach (PictureBox wallmember in walls)
                {

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
            Bullet_Attributes fired_bullet = new Bullet_Attributes();
            if (bullets[bullets.Count - 1] != null)
            fired_bullet = bullets[bullets.Count - 1] as Bullet_Attributes;
            foreach (PictureBox wallmember in walls)
            {
                //Bullet_Attributes fired_bullet = new Bullet_Attributes();
                //if(bullets[bullets.Count -1] != null)
                //fired_bullet=bullets[bullets.Count-1] as Bullet_Attributes;
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
                    if (wallmember.Tag == "bird")
                    {
                        
                        panel1.Controls.Remove(fired_bullet);

                        panel1.Controls.Remove(wallmember);
                        //walls.RemoveAt(walls.IndexOf(wallmember));
                        bullets.Remove(fired_bullet);
                        walls.Remove(wallmember);
                        timer1.Enabled = false;
                        timer2.Enabled = false;
                        timer3.Enabled = false;
                        timer4.Enabled = false;
                        enemyFireTime.Enabled = false;
                        enemyLookYasin.Enabled = false;
                        MessageBox.Show("NEYSEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE",
        "YASİNİN G...");
                        

                        //DialogResult result1 = MessageBox.Show("YASİN BOOM","YASİN GÜÜM",MessageBoxButtons.YesNo);
                        //if (result1 == DialogResult.Yes) 
                        //{ 
                        //    panel1.Controls.Clear();
                        //    foreach (Control c in temp_form)
                        //    {
                        //        panel1.Controls.Add(c);
                        //    }
                            
                        //}
                        //else if (result1 == DialogResult.No) { Console.WriteLine("İPTAL ETTİİİ"); }
                        break;
                    }
                    #endregion
                }
                
            }
            foreach (EnemyAttributes e_tanks in enemyTanks)
            {
                if (e_tanks.Bounds.IntersectsWith(new Rectangle(fired_bullet.Location.X, fired_bullet.Location.Y, fired_bullet.Width, fired_bullet.Height)))
                {
                    isfired = false;
                    panel1.Controls.Remove(fired_bullet);

                    panel1.Controls.Remove(e_tanks);
                    bullets.Remove(fired_bullet);
                    
                    enemyTanks.Remove(e_tanks);
                    break; 
                    
                  
                }
            }


        }


        public void enemyBulletCollision()
        {
            
                foreach (EnemyAttributes enemytanks in enemyTanks)
                {

                    foreach (PictureBox wallmember in walls)
                    {
                        Bullet_Attributes fired_bullet = new Bullet_Attributes();
                        if (enemytanks.bullets.Count != 0)
                        {
                            fired_bullet = enemytanks.bullets[enemytanks.bullets.Count - 1] as Bullet_Attributes;
                        }
                        if (wallmember.Bounds.IntersectsWith(new Rectangle(fired_bullet.Location.X, fired_bullet.Location.Y, fired_bullet.Width, fired_bullet.Height)))
                        {
                            //isfired = false;
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
                                enemytanks.bullets.Remove(fired_bullet);
                                //enemytanks.isfired = false;
                                walls.Remove(wallmember);
                                break;
                                //Console.WriteLine("Bricket has been hit");
                            }
                            if (wallmember.Tag == "BorderWalls" || wallmember.Tag == "nd_Wall")
                            {
                                panel1.Controls.Remove(fired_bullet);
                                enemytanks.bullets.Remove(fired_bullet);
                                //enemytanks.isfired = false;
                                break;
                            }
                            if (wallmember.Tag == "bird")
                            {
                                MessageBox.Show("Kuş gg");
                                break;
                            }

                            #endregion
                        }

                    }


                    
                    
                    
                    
                    
                    
                    //foreach (PictureBox wallmember in walls)
                    //{
                    //    if (wallmember.Bounds.IntersectsWith(new Rectangle(enemytanks.bullet.Location.X, enemytanks.bullet.Location.Y, enemytanks.bullet.Width, enemytanks.bullet.Height)))
                    //    {

                    //        #region Destroy and Damage Processes
                    //        if (wallmember.Tag == "Enemy")
                    //        {
                    //            Console.WriteLine("Enemy hit");
                    //        }
                    //        if (wallmember.Tag == "d_Wall")
                    //        {
                    //            panel1.Controls.Remove(enemytanks.bullet);
                    //            enemytanks.isfired = false;
                    //            panel1.Controls.Remove(wallmember);
                    //            //walls.RemoveAt(walls.IndexOf(wallmember));
                    //            enemytanks.bullets.Remove(enemytanks.bullet);
                    //            walls.Remove(wallmember);
                    //            break;
                    //            //Console.WriteLine("Bricket has been hit");
                    //        }
                    //        if (wallmember.Tag == "BorderWalls" || wallmember.Tag == "nd_Wall")
                    //        {
                    //            panel1.Controls.Remove(enemytanks.bullet);
                    //            enemytanks.isfired = false;
                    //            enemytanks.bullets.Remove(enemytanks.bullet);
                    //            break;
                    //        }

                    //        #endregion
                    //    }

                    //}
                    if (tank1.Bounds.IntersectsWith(new Rectangle(enemytanks.bullet.Location.X, enemytanks.bullet.Location.Y, enemytanks.bullet.Width, enemytanks.bullet.Height)))
                    {
                        Console.WriteLine("GAME OVER!!!!");
                    }
                }
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            foreach (EnemyAttributes e_tank in enemyTanks)
            {
                foreach (PictureBox wallmember in walls)
                {

                    if (e_tank.move_right == true&&wallmember.Bounds.IntersectsWith(new Rectangle(e_tank.Location.X + 5, e_tank.Location.Y, e_tank.Width, e_tank.Height)))
                    {
                        e_tank.move_right = false;
                        e_tank.randomer();
                    }
                }

                foreach (PictureBox wallmember in walls)
                {
                    if (e_tank.move_left == true&&wallmember.Bounds.IntersectsWith(new Rectangle(e_tank.Location.X - 5, e_tank.Location.Y, e_tank.Width, e_tank.Height)))
                    {
                        e_tank.move_left = false;
                        e_tank.randomer();
                    } 
                }
                foreach (PictureBox wallmember in walls)
                {
                    if (e_tank.move_down == true&&wallmember.Bounds.IntersectsWith(new Rectangle(e_tank.Location.X, e_tank.Location.Y + 5, e_tank.Width, e_tank.Height)))
                    {
                        e_tank.move_down = false;
                        e_tank.randomer();
                    }
                }
                foreach (PictureBox wallmember in walls)
                {
                    if (e_tank.move_up==true&&wallmember.Bounds.IntersectsWith(new Rectangle(e_tank.Location.X, e_tank.Location.Y - 5, e_tank.Width, e_tank.Height)))
                    {
                        e_tank.move_up = false;
                        e_tank.randomer();
                    }
                }
                //enemy tanks collisions with Main Tank.
                if (e_tank.move_right == true&& tank1.Bounds.IntersectsWith(new Rectangle(e_tank.Location.X + 5, e_tank.Location.Y, e_tank.Width, e_tank.Height)))
                    {
                        e_tank.move_right = false;
                        e_tank.randomer();
                    }
                if (e_tank.move_left == true && tank1.Bounds.IntersectsWith(new Rectangle(e_tank.Location.X - 5, e_tank.Location.Y, e_tank.Width, e_tank.Height)))
                {
                    e_tank.move_left = false;
                    e_tank.randomer();
                }

                if (e_tank.move_down == true && tank1.Bounds.IntersectsWith(new Rectangle(e_tank.Location.X, e_tank.Location.Y + 5, e_tank.Width, e_tank.Height)))
                {
                    e_tank.move_down = false;
                    e_tank.randomer();
                }
                if (e_tank.move_up == true && tank1.Bounds.IntersectsWith(new Rectangle(e_tank.Location.X, e_tank.Location.Y - 5, e_tank.Width, e_tank.Height)))
                {
                    e_tank.move_up = false;
                    e_tank.randomer();
                }
                
                
                e_tank.move();
            }
        }

       

        private void timer3_Tick(object sender, EventArgs e)
        {
            foreach (EnemyAttributes e_tank in enemyTanks)
            {
                if (e_tank.Left - yasin.Left > 0) e_tank.move_right = true;
                if (e_tank.Left - yasin.Left > 0) e_tank.move_left = true;
               // e_tank.move_up = true;                
                e_tank.move_down = true;
                
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            foreach (EnemyAttributes e_tank in enemyTanks)
            {
                foreach (EnemyAttributes other_tank in enemyTanks)
                {
                   if (e_tank == other_tank) continue;
                   if (e_tank.move_right == true && other_tank.Bounds.IntersectsWith(new Rectangle(e_tank.Location.X + 5, e_tank.Location.Y, e_tank.Width, e_tank.Height)))
                    {
                        e_tank.move_right = false;
                        e_tank.randomer();
                    }
                
                    if (e_tank.move_left == true && other_tank.Bounds.IntersectsWith(new Rectangle(e_tank.Location.X - 5, e_tank.Location.Y, e_tank.Width, e_tank.Height)))
                    {
                        e_tank.move_left = false;
                        e_tank.randomer();
                    } 
                
                    if (e_tank.move_down == true && other_tank.Bounds.IntersectsWith(new Rectangle(e_tank.Location.X, e_tank.Location.Y + 5, e_tank.Width, e_tank.Height)))
                    {
                        e_tank.move_down = false;
                        e_tank.randomer();
                    }
                    if (e_tank.move_up == true && other_tank.Bounds.IntersectsWith(new Rectangle(e_tank.Location.X, e_tank.Location.Y - 5, e_tank.Width, e_tank.Height)))
                    {
                        e_tank.move_up = false;
                        e_tank.randomer();
                    }
                

                }
            }

        }

        private void enemyFireTime_Tick(object sender, EventArgs e)
        {
            foreach (EnemyAttributes e_tank in enemyTanks)
            {
                e_tank.isfired = false;
            }
        }

        private void enemyLookYasin_Tick(object sender, EventArgs e)
        {
            foreach (EnemyAttributes e_tank in enemyTanks)
            {
                e_tank.move_down = true;
                e_tank.move_up = true;
                e_tank.move_left = true;
                e_tank.move_right = true;
            }
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            foreach (EnemyAttributes e_tank in enemyTanks)
            {
                if (e_tank.Bounds.IntersectsWith(new Rectangle(tank1.Location.X + 5, tank1.Location.Y, tank1.Width, tank1.Height)))
                {
                    rightkey = false; right = false; break;

                }

                if (e_tank.Bounds.IntersectsWith(new Rectangle(tank1.Location.X - 5, tank1.Location.Y, tank1.Width, tank1.Height)))
                {
                    leftkey = false; left = false; break;
                }

                if (e_tank.Bounds.IntersectsWith(new Rectangle(tank1.Location.X, tank1.Location.Y + 5, tank1.Width, tank1.Height)))
                {
                    downkey = false; down = false; break;
                }
                if (e_tank.Bounds.IntersectsWith(new Rectangle(tank1.Location.X, tank1.Location.Y - 5, tank1.Width, tank1.Height)))
                {
                    upkey = false; up = false; break;
                }

            }
        }
        



    }//clas bitişi
}
