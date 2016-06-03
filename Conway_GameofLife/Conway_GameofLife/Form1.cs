﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Conway_GameofLife
{
    public partial class Form1 : Form
    {
        bool[,] universe = new bool[50, 25];
        bool[,] UniverseNext = new bool[50, 25];
        Timer timer = new Timer();
        int generation = 0;
        public Form1()
        {
            InitializeComponent();
            timer.Interval = 20;
            timer.Tick += Timer_Tick;

        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            // Call next generation
            generation++;
            NextGeneration();
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    universe[x, y] = UniverseNext[x, y];
                }
            }

            // update toolstrip
            toolStripStatusLabel1.Text = "Generation: " + generation.ToString();

            // Paint
            graphicsPanel1.Invalidate();
        }

        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            float width = (float)graphicsPanel1.ClientSize.Width / (float)universe.GetLength(0);
            float height = (float)graphicsPanel1.ClientSize.Height / (float)universe.GetLength(1);


            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    
                    RectangleF temp = RectangleF.Empty;
                    temp.X = x * width;
                    temp.Y = y * height;
                    temp.Width = width;
                    temp.Height = height;

                    if (universe[x,y] == true)
                    {
                        if (GetNaighbors(x, y) == 2 || GetNaighbors(x, y) == 3)
                        {
                            e.Graphics.FillRectangle(Brushes.Green, temp);
                        }
                        else
                        {
                            e.Graphics.FillRectangle(Brushes.Gray, temp);
                        }
                       
                    }
                    else if (universe[x, y] == false && GetNaighbors(x, y) == 3)
                    {
                        e.Graphics.FillRectangle(Brushes.LightYellow, temp);
                    }
                    


                    e.Graphics.DrawRectangle(Pens.Black,temp.X,temp.Y,temp.Width,temp.Height);
                    if (GetNaighbors(x,y)>0)
                    {
                        float size = Math.Min(width, height);
                        Font font = new Font("Arial", 0.5f*size);

                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;


                        e.Graphics.DrawString(GetNaighbors(x,y).ToString(), font, Brushes.Red, temp, stringFormat);


                    }
                }
            }
        }

        private void graphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            float width = (float)graphicsPanel1.ClientSize.Width / (float)universe.GetLength(0);
            float height = (float)graphicsPanel1.ClientSize.Height / (float)universe.GetLength(1);

            int x = (int)(e.X /width);
            int y = (int)(e.Y /height);


            universe[x, y] = !universe[x, y];
                graphicsPanel1.Invalidate();
        }



        private void NextGeneration()
        {
            Array.Clear(UniverseNext, 0, UniverseNext.Length);

            //for (int y  = 0; y < universe.GetLength(1); y++)
            //{
            //    for (int x = 0; x < universe.GetLength(0); x++)
            //    {
                    //bool test = false;
                    //if (universe[x, y] == true && GetNaighbors(x,y) <2 )
                    //{
                    //    test = false;
                    //}

                    //if (universe[x, y] == true && (GetNaighbors(x, y) == 2 || GetNaighbors(x, y) == 3))
                    //{
                    //    test = true;
                    //}

                    //if (universe[x, y] == true && GetNaighbors(x,y) >3)
                    //{
                    //    test = false;
                    //}
                    //if (universe[x, y] == false && GetNaighbors(x, y) == 3)
                    //{
                    //    test = true;
                    //}
                    //UniverseNext[x, y] = test;

                    for ( int y = 0; y < universe.GetLength(1); y++)
                    {
                        for ( int x = 0; x < universe.GetLength(0); x++)
                        {



                    if (universe[x, y] == true)
                    {
                        if (GetNaighbors(x, y) == 2 || GetNaighbors(x, y) == 3)
                        {
                            UniverseNext[x, y] = true;
                        }
                        else
                        {
                            UniverseNext[x, y] = false;
                        }

                    }
                    else if (universe[x, y] == false && GetNaighbors(x, y) == 3)
                    {
                        UniverseNext[x, y] = true;
                    }
                    else
                        UniverseNext[x, y] = false;

                    }
            }

        }



        private int GetNaighbors(int x, int y)
        {
            
            int count = 0;

            if (x > 0)
            {
                if (universe[x - 1, y] == true)
                    count++;
            }
            if (y > 0)
            {
                if (universe[x, y - 1] == true)
                    count++;
            }
            if (y > 0 && x > 0)
            {
                if (universe[x - 1, y - 1] == true)
                    count++;
            }
            if (x < universe.GetLength(0)-1)
            {
                if (universe[x + 1, y] == true)
                    count++;
            }
            if (y < universe.GetLength(1) - 1)
            {
                if (universe[x, y + 1] == true)
                    count++;
            }
            if (y < universe.GetLength(1)-1 && x < universe.GetLength(0)-1 )
            {
                if (universe[x + 1, y + 1] == true)
                    count++;
            }
            if (x>0 && y < universe.GetLength(1)-1)
            {
                if (universe[x - 1, y + 1] == true)
                    count++;
            }
            if (y > 0 && x < universe.GetLength(0)-1)
            {
                if (universe[x + 1, y - 1] == true)
                    count++;
            }


            return count;
        }











        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            Array.Clear(universe, 0, universe.Length);
            generation = 0;
            timer.Enabled = false;
            toolStripStatusLabel1.Text = "Generation: " + generation.ToString();
            graphicsPanel1.Invalidate();
        }

        private void PlaytoolStripButton_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
        }

        private void PausetoolStripButton_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
        }

        private void SteptoolStripButton_Click(object sender, EventArgs e)
        {
            Timer_Tick(sender, e);
        }
    }
}
