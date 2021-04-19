using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Concurrent;
using System.Collections;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

       ArrayList arr = new ArrayList();
       
       public void newfigures(ArrayList arr, char C)
       {
           int j;int i;
           if (C == 'W') { j = 0; i = 1; }
           else{j=7;i=6;}
           King A = new King(C, 3, j);
           arr.Add(A);
           Queen B = new Queen(C, 4, j);
           arr.Add(B);
           Elefant E = new Elefant(C,5,j);
           arr.Add(E);
            E = new Elefant(C,2,j);
           arr.Add(E);
           Horse H =new Horse(C,1,j);
           arr.Add(H);
            H =new Horse(C,6,j);
           arr.Add(H);
           Ladia L =new Ladia(C,0,j);
           arr.Add(L);
           L =new Ladia(C,7,j);
           arr.Add(L);
           Peshka D;
           for (int Z=0;Z<8;Z++){
           D=new Peshka(C, Z, i);
           arr.Add(D);
           }
       }

       Button RE=new Button();
       int HowManyGames = 0;
        char hod = 'W';//w-white || B-black
        bool faza = false;//фалсе фаза выбора фигуры, тру фаза передвижения
        Bitmap imageW = new Bitmap(Properties.Resources.W);

        Bitmap imageWP = new Bitmap(Properties.Resources.WP);
        Bitmap imageWL = new Bitmap(Properties.Resources.WL);
        Bitmap imageWH = new Bitmap(Properties.Resources.WH);
        Bitmap imageWQ = new Bitmap(Properties.Resources.WQ);
        Bitmap imageWK = new Bitmap(Properties.Resources.WK);
        Bitmap imageWE = new Bitmap(Properties.Resources.WE);

        Bitmap imageBE = new Bitmap(Properties.Resources.BE);
        Bitmap imageBH = new Bitmap(Properties.Resources.BH);
        Bitmap imageBK = new Bitmap(Properties.Resources.BK);
        Bitmap imageBP = new Bitmap(Properties.Resources.BP);
        Bitmap imageBQ = new Bitmap(Properties.Resources.BQ);
        Bitmap imageBL = new Bitmap(Properties.Resources.BL);

        PictureBox[,] PB = new PictureBox[8, 8];

        int witch = -1;

        private void button1_Click(object sender, EventArgs e)
        {
            if (HowManyGames != 0)
            {
                for (int i = 0; i < 8; i++)
                {

                    for (int j = 0; j < 8; j++)
                    {
                        Controls.Remove(PB[i, j]);
                    }
                }
                for (int i = 0; i < 32; i++) 
                {
                    arr.RemoveAt(0);
                    
                }
            }
            hod = 'W';
            faza = false;
            HowManyGames++;
            newfigures(arr, 'W');
            newfigures(arr, 'B');
            for (int i = 0; i < 8; i++)
            {

                for (int j = 0; j < 8; j++)
                {
                    PB[i, j]= new PictureBox();
                    PB[i, j].Location = new Point(12 + 96 * (j), 12 + 96 * (i));
                    PB[i, j].BorderStyle = BorderStyle.None;
                    PB[i, j].Image = imageW;

                    if (i == 1) { PB[i, j].Image = imageWP; };
                    if (i == 6) { PB[i, j].Image = imageBP; };
                    if (i == 0)
                    {
                        if (j == 0 || j == 7) { PB[i, j].Image = imageWL; };
                        if (j == 1 || j == 6) { PB[i, j].Image = imageWH; };
                        if (j == 2 || j == 5) { PB[i, j].Image = imageWE; };
                        if (j == 3) { PB[i, j].Image = imageWK; };
                        if (j == 4) { PB[i, j].Image = imageWQ; };
                    };
                    if (i == 7)
                    {
                        if (j == 0 || j == 7) { PB[i, j].Image = imageBL; };
                        if (j == 1 || j == 6) { PB[i, j].Image = imageBH; };
                        if (j == 2 || j == 5) { PB[i, j].Image = imageBE; };
                        if (j == 3) { PB[i, j].Image = imageBK; };
                        if (j == 4) { PB[i, j].Image = imageBQ; };
                    };
                    PB[i, j].Size=new Size(90,90);
                    PB[i, j].Click += new EventHandler(PBClick);
                    Controls.Add(PB[i, j]);
                }
            }
            RE.Left = 800;
            RE.Top = 100;
            RE.Text = "отменить выбор фигуры";
            RE.Click += new EventHandler(REClick);
            richTextBox1.Text = "Ход белых";
            richTextBox2.Text = "Фаза выбора фигуры";
        }

        public void PBClick(object sender, EventArgs e){
            int X=-1;
            int Y=-1;
            for (int i = 0; i < 8; i++)
            {

                for (int j = 0; j < 8; j++)
                {
                    if ((sender as PictureBox) == PB[i, j]) 
                    {
                        X = j; Y = i; break;
                    }
                    
                }
                if (X != -1) { break; }
            }
            
         //   MessageBox.Show(X.ToString());
         // MessageBox.Show(Y.ToString());
            if (faza == false && X!=-1)
            {
                witch = Proverka.witch(X,Y,arr,hod);
                if (witch != -1)
                {
                    faza = true; 
                    richTextBox2.Text = "Фаза перемещения фигуры";
                    Controls.Add(RE); 
                }
                    
                
                   // arr[witch].vybor();
               
                
            } 
            
   
            
            
            
            else if (faza==true && X!=-1) 
            {
                if (Proverka.mona(X, Y, arr, witch))
                {
                    (arr[witch] as Figure ).HOD(X, Y,arr,  PB); 
                    if (hod=='W'){
                    richTextBox1.Text = "Ход Чёрных";
                    hod='B';}
                    else{richTextBox1.Text = "Ход Белых";
                    hod = 'W';
                    }
                    richTextBox2.Text = "Фаза выбора фигуры";
                    witch = -1;
                    faza = false;
                    Controls.Remove(RE);
                    for (int i = 0; i < 32; i++) {
                        if ((arr[i] as Figure).name == 'K' && (arr[i] as Figure).alive == false) { if ((arr[i] as Figure).colore == 'W') { MessageBox.Show("Чёрные победили"); } else { MessageBox.Show("Белые победили"); } }
                    }
                }
                else { MessageBox.Show("Нельзя сделать такой ход"); }

                
            }



        
        }

        public void REClick(object sender, EventArgs e) 
        {
            richTextBox2.Text = "Фаза выбора фигуры";
            witch = -1;
            faza = false;
            Controls.Remove(RE);
        }
    }


















}









