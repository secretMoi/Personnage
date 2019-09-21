﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seance_3_1
{
    class Ring
    {
        private int[,] ring;
        private static int compteurItem = 0;
        private int[] dimensions;

        private readonly Dictionary<int, List<int>> items = new Dictionary<int, List<int>>();

        public static int LEFT = -1;
        public static int RIGHT = 1;
        public static int DOWN = 1;
        public static int UP = -1;

        public Ring(int tailleX = 5, int tailleY = 5)
        {
            this.ring = new int[tailleX, tailleY];
            this.dimensions = new int[2];
            this.dimensions[0] = tailleX;
            this.dimensions[1] = tailleY;

            for(int y = 0; y < tailleY; y++)
                for (int x = 0; x < tailleX; x++)
                    this.ring[x, y] = 0;
        }

        public bool Deplace(int id, int x, int y = 0)
        {
            // récupère la position courante de l'item'
            int xCourant = this.items[id][0];
            int yCourant = this.items[id][1];

            //Console.WriteLine("{0}.{1}.{2}.{3}.", x, id, xCourant, yCourant);

            if (PositionPossible(x + xCourant, y + yCourant) == false)
                return false;

            this.ring[xCourant, yCourant] = 0; // supprime la position actuelle

            // change la position
            this.items[id][0] += x;
            this.items[id][1] += y;
            this.ring[this.items[id][0], this.items[id][1]] = id;

            return true;
        }

        protected bool PositionPossible(int x, int y)
        {
            //Console.WriteLine("{0}.{1}.{2}.{3}.", x, y, dimensions[0], dimensions[1]);
            // si on ne sort pas du ring et que la position est inoccupée
            if (x < dimensions[0] && x >= 0 && y < dimensions[1] && y >= 0)
            {
                if (ring[x, y] == 0)
                    return true;
            }
                
            return false;
        }

        public int AjouterElement(int etat = 1, int x = 0, int y = 0)
        {
            compteurItem++;
            items.Add(compteurItem, new List<int> { x, y });

            this.ring[x, y] = etat;

            return compteurItem;
        }

        public int Distance(int id1, int id2)
        {
            int x1 = this.items[id1][0];
            int y1 = this.items[id1][1];
            int x2 = this.items[id2][0];
            int y2 = this.items[id2][1];

            int deltaX = x2 - x1;
            int deltaY = y2 - y1;
            return (int) Math.Floor(Math.Sqrt(deltaX * deltaX + deltaY * deltaY));
        }

        public void DisplayGrid()
        {
            for (int y = 0; y < dimensions[1]; y++)
            {
                for (int x = 0; x < dimensions[0]; x++)
                {
                    Console.Write(ring[x, y] + " ");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public int MaxX()
        {
            return dimensions[0] - 1;
        }
        public int MaxY()
        {
            return dimensions[1] - 1;
        }
    }
}
