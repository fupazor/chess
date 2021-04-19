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
  

     class Figure
    {
         public char name;
         public Bitmap pole = Properties.Resources.W;
         public Bitmap icon;
        public char colore;// W or B
        public int KX, KY;//1-8;1-8;
        public bool alive=true;
        public Figure() {alive=true;}
        public Figure(char A,int X,int Y) {alive=true;KX=X;KY=Y;colore=A;}
        public void HOD(int X, int Y, ArrayList arr, PictureBox[,] PB)
        {
            PB[KY, KX].Image = pole;
            PB[Y, X].Image = icon;
            if (Proverka.isempty(X, Y, arr) == false)
            {
                int i = -1;
                i = Proverka.witch(X, Y, arr, 'W');
                if (i == -1) { i = Proverka.witch(X, Y, arr, 'B'); }
                (arr[i] as Figure).alive = false;
            }
            KX = X; KY = Y;

        }
    };

    class Peshka: Figure
    {

        public Peshka() {alive=true;}
        public Peshka(char A, int X, int Y) { name = 'P'; alive = true; KX = X; KY = Y; colore = A; if (colore == 'W') { icon = Properties.Resources.WP; };if (colore == 'B') { icon = Properties.Resources.BP; } }
        public bool mona(int X, int Y, ArrayList arr) 
        {
            if (colore == 'W' && KY == 1 && (Y - KY) == 2 && KX == X && Proverka.isempty(X, Y, arr) && Proverka.isempty(X, Y-1, arr))
            {
                return true;
            }
            if (colore == 'W' && KX == X && (Y - KY) == 1 && Proverka.isempty(X, Y, arr))
            {
                return true;
            }
            if (colore == 'W' && Math.Abs(X - KX) == 1 && (Y - KY) == 1 && Proverka.isempty(X, Y, arr)==false) 
            {
                int i =-1;
                 i = Proverka.witch(X, Y, arr,'W');
                if (i==-1) {return true;}
                else {return false;}
            }
            if (colore == 'B' && KY == 6 && (KY - Y) == 2 && KX == X && Proverka.isempty(X, Y, arr))
            {
                return true;
            }
            if (colore == 'B' && KX == X && (KY - Y) == 1 && Proverka.isempty(X, Y, arr))
            {
                return true;
            }
            if (colore == 'B' && Math.Abs(X - KX) == 1 && (KY - Y) == 1 && Proverka.isempty(X, Y, arr) == false)
            {
                int i = -1;
                 i = Proverka.witch(X, Y, arr,'B');
                if (i==-1) {return true;}
                else {return false;}
            }
            return false;
        }




        







    };
    class King : Figure
    { 
     public King() {alive=true;}
     public King(char A, int X, int Y) { name = 'K'; alive = true; KX = X; KY = Y; colore = A; if (colore == 'W') { icon = Properties.Resources.WK; };if (colore == 'B') { icon = Properties.Resources.BK; } }
        public bool mona(int X, int Y, ArrayList arr)
        {
            if (Math.Abs(X - KX) <= 1 && Math.Abs(Y - KY)<=1 && Proverka.witch(X, Y, arr, colore) == -1) {
            
                return true; }

            return false;
        }
    };
    class Queen : Figure
    {
        public Queen() {alive=true;}
        public Queen(char A, int X, int Y) { name = 'Q'; alive = true; KX = X; KY = Y; colore = A; if (colore == 'W') { icon = Properties.Resources.WQ; };if (colore == 'B') { icon = Properties.Resources.BQ; } }
        public bool mona(int X, int Y, ArrayList arr)
        {
            if (Math.Abs(X - KX) == Math.Abs(Y - KY))
            {
                if (X > KX && Y > KY)
                {
                    int i;
                    for (i = 1; i + KX < X; i++)
                    {
                        if (Proverka.isempty(KX + i, KY + i, arr) == false) { break; }
                    }
                    if (i + KX == X && Proverka.witch(X, Y, arr, colore) == -1) { return true; }

                }
                if (X < KX && Y < KY)
                {
                    int i;
                    for (i = 1; i + X < KX; i++)
                    {
                        if (Proverka.isempty(KX - i, KY - i, arr) == false) { break; }
                    }
                    if (i + X == KX && Proverka.witch(X, Y, arr, colore) == -1) { return true; }

                }
                if (X > KX && Y < KY)
                {
                    int i;
                    for (i = 1; i + KX < X; i++)
                    {
                        if (Proverka.isempty(KX + i, KY - i, arr) == false) { break; }
                    }
                    if (i + KX == X && Proverka.witch(X, Y, arr, colore) == -1) { return true; }

                }
                if (X < KX && Y > KY)
                {
                    int i;
                    for (i = 1; i + X < KX; i++)
                    {
                        if (Proverka.isempty(KX - i, KY + i, arr) == false) { break; }
                    }
                    if (i + X == KX && Proverka.witch(X, Y, arr, colore) == -1) { return true; }

                }
                }
            if (X > KX && Y == KY)
            {
                int i;
                for (i = KX + 1; i < X; i++)
                {
                    if (Proverka.isempty(i, Y, arr) == false) { break; }
                }
                if (i == X && Proverka.witch(X, Y, arr, colore) == -1) { return true; }
            }

            if (X < KX && Y == KY)
            {
                int i;
                for (i = X + 1; i < KX; i++)
                {
                    if (Proverka.isempty(i, Y, arr) == false) { break; }
                }
                if (i == KX && Proverka.witch(X, Y, arr, colore) == -1) { return true; }

            } if (Y > KY && X == KX)
            {
                int i;
                for (i = KY + 1; i < Y; i++)
                {
                    if (Proverka.isempty(X, i, arr) == false) { break; }
                }
                if (i == Y && Proverka.witch(X, Y, arr, colore) == -1) { return true; }


            } if (Y < KY && X == KX)
            {
                int i;
                for (i = Y + 1; i < KY; i++)
                {
                    if (Proverka.isempty(X, i, arr) == false) { break; }
                }
                if (i == KY && Proverka.witch(X, Y, arr, colore) == -1) { return true; }
            }
            
            return false;
        }
    }
    class Elefant : Figure
    {
        public Elefant() {alive=true;}
        public Elefant(char A, int X, int Y) { name = 'E'; alive = true; KX = X; KY = Y; colore = A; if (colore == 'W') { icon = Properties.Resources.WE; };if (colore == 'B') { icon = Properties.Resources.BE; } }
        public bool mona(int X, int Y, ArrayList arr)
        {
            if (Math.Abs(X - KX) == Math.Abs(Y - KY))
            {
                if (X > KX  && Y> KY )
                {
                    int i;
                    for (i = 1; i + KX < X; i++)
                    {
                        if (Proverka.isempty(KX + i, KY + i, arr) == false) { break; }
                    }
                    if (i + KX == X && Proverka.witch(X, Y, arr, colore) == -1) { return true; }

                }
                if (X < KX && Y < KY)
                {
                    int i;
                    for (i = 1; i + X < KX; i++)
                    {
                        if (Proverka.isempty(KX - i, KY - i, arr) == false) { break; }
                    }
                    if (i + X == KX && Proverka.witch(X, Y, arr, colore) == -1) { return true; }

                }
                if (X > KX && Y < KY)
                {
                    int i;
                    for (i = 1; i + KX < X; i++)
                    {
                        if (Proverka.isempty(KX + i, KY - i, arr) == false) { break; }
                    }
                    if (i + KX == X && Proverka.witch(X, Y, arr, colore) == -1) { return true; }

                }
                if (X < KX && Y > KY)
                {
                    int i;
                    for (i = 1; i + X < KX; i++)
                    {
                        if (Proverka.isempty(KX - i, KY + i, arr) == false) { break; }
                    }
                    if (i + X == KX && Proverka.witch(X, Y, arr, colore) == -1) { return true; }

                }
            }
            return false;
        }
    }
    class Horse : Figure
    {
        public Horse() {alive=true;}
        public Horse(char A, int X, int Y) { name = 'H'; alive = true; KX = X; KY = Y; colore = A; if (colore == 'W') { icon = Properties.Resources.WH; };if (colore == 'B') { icon = Properties.Resources.BH; } }
        public bool mona(int X, int Y, ArrayList arr)
        {
            if ((Math.Abs(X - KX) == 1 && Math.Abs(Y - KY) == 2) || (Math.Abs(X - KX) == 2 && Math.Abs(Y - KY) == 1) && Proverka.witch(X, Y, arr, colore) == -1) { return true; }
            return false;
        }
    }
    class Ladia : Figure
    {
        public Ladia() {alive=true;}
        public Ladia(char A, int X, int Y) { name = 'L'; alive = true; KX = X; KY = Y; colore = A; if (colore == 'W') { icon = Properties.Resources.WL; };if (colore == 'B') { icon = Properties.Resources.BL; } }
        public bool mona(int X, int Y, ArrayList arr)
        {
            if (X > KX && Y == KY) {
                int i;
                for (i = KX+1; i < X; i++)
                {
                    if (Proverka.isempty(i, Y, arr) == false) { break; }
                }
            if(i==X && Proverka.witch(X, Y, arr, colore) == -1){return true;}
            }

            if (X < KX && Y == KY)
            {
                int i;
                for (i = X+1; i < KX; i++)
                {
                    if (Proverka.isempty(i, Y, arr) == false) { break; }
                }
                if (i == KX && Proverka.witch(X, Y, arr, colore) == -1) { return true; }

            } if (Y > KY && X == KX)
            {
                int i;
                for (i = KY+1; i < Y; i++)
                {
                    if (Proverka.isempty(X, i, arr) == false) { break; }
                }
                if (i == Y && Proverka.witch(X, Y, arr, colore) == -1) { return true; }


            } if (Y < KY && X == KX)
            {
                int i;
                for (i = Y+1; i < KY; i++)
                {
                    if (Proverka.isempty(X, i, arr) == false) { break; }
                }
                if (i == KY && Proverka.witch(X, Y, arr, colore) == -1) { return true; }
            }




            return false;
        }
    }






    class Proverka 
    { 
       static public bool isempty(int X,int Y,ArrayList arr)
       {
           for (int i = 0; i < 32; i++)
           {
               if ((arr[i] as Figure).KX == X && (arr[i] as Figure).KY == Y && (arr[i] as Figure).alive)
               {
                   return false;
               }
           }
           return true;
       }
       static public int witch(int X,int Y,ArrayList arr,char hod)
        {
            for (int i = 0; i < 32; i++)
            {
                if ((arr[i] as Figure).KX == X && (arr[i] as Figure).KY == Y && (arr[i] as Figure).alive && (arr[i] as Figure).colore == hod)
                {
                    return i; 

                }
            }
            return -1;
        }

      static public bool mona(int X, int Y, ArrayList arr, int i) 
       {
           if ((arr[i] as Figure).name == 'P') { return (arr[i] as Peshka).mona(X, Y, arr); }
           if ((arr[i] as Figure).name == 'K') { return (arr[i] as King).mona(X, Y, arr); }
           if ((arr[i] as Figure).name == 'Q') { return (arr[i] as Queen).mona(X, Y, arr); }
           if ((arr[i] as Figure).name == 'L') { return (arr[i] as Ladia).mona(X, Y, arr); }
           if ((arr[i] as Figure).name == 'E') { return (arr[i] as Elefant).mona(X, Y, arr); }
           if ((arr[i] as Figure).name == 'H') { return (arr[i] as Horse).mona(X, Y, arr); }
           return false;
       }


      





    }
}
