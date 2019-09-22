﻿using System;
using System.Collections.Generic;
 using System.Drawing;
 using System.Linq;
using System.Text;

namespace RPG
{
    public class Ring
    {
        private string[,] ring;
        private static int compteurItem = 0;
        private int[] dimensions;

        private readonly Dictionary<int, List<int>> items = new Dictionary<int, List<int>>();

        public static int LEFT = -1;
        public static int RIGHT = 1;
        public static int DOWN = 1;
        public static int UP = -1;

        public Ring(int tailleX = 8, int tailleY = 8)
        {
            ring = new string[tailleX, tailleY];
            dimensions = new int[2];
            dimensions[0] = tailleX;
            dimensions[1] = tailleY;

            for(int y = 0; y < tailleY; y++)
                for (int x = 0; x < tailleX; x++)
                    ring[x, y] = "0";
            
            CreerPlanEau();
        }

        public void CreerPlanEau()
        {
            if (dimensions[0] <= 3 || dimensions[1] <= 3) // si le ring est assez grand
                return;

            // Création du point de départ du plan d'eau
            int posDebutX = dimensions[0] / 2 - 1;
            int posDebutY = dimensions[1] / 2 - 1;
            
            // On case les E dans les cases calculées
            ring[posDebutX, posDebutY] = "E";
            ring[posDebutX+1, posDebutY] = "E";
            ring[posDebutX, posDebutY+1] = "E";
            ring[posDebutX+1, posDebutY+1] = "E";
        }

        public bool Deplace(int id, int x, int y = 0)
        {
            // récupère la position courante de l'item
            int xCourant = items[id][0];
            int yCourant = items[id][1];

            if (PositionPossible(x + xCourant, y + yCourant) == false)
                return false;

            ring[xCourant, yCourant] = "0"; // supprime la position actuelle

            // change la position
            items[id][0] += x;
            items[id][1] += y;
            ring[items[id][0], items[id][1]] = id.ToString();

            return true;
        }

        protected bool PositionPossible(int x, int y)
        {
            // si on ne sort pas du ring et que la position est inoccupée
            if (x < dimensions[0] && x >= 0 && y < dimensions[1] && y >= 0)
            {
                if (ring[x, y] == "0") // si la case est vide
                    return true;
            }
                
            return false;
        }

        public int AjouterElement(int etat = 1, int x = 0, int y = 0)
        {
            compteurItem++;
            items.Add(compteurItem, new List<int> { x, y }); // rajoute l'id avec ses coordonnées

            ring[x, y] = etat.ToString();

            return compteurItem;
        }

        public int Distance(int id1, int id2)
        {
            int x1 = items[id1][0];
            int y1 = items[id1][1];
            int x2 = items[id2][0];
            int y2 = items[id2][1];

            int deltaX = x2 - x1;
            int deltaY = y2 - y1;
            return (int) Math.Floor(Math.Sqrt(deltaX * deltaX + deltaY * deltaY));
        }

        public void DisplayGrid(int id, ConsoleColor color)
        {
            string element;
            for (int y = 0; y < dimensions[1]; y++)
            {
                for (int x = 0; x < dimensions[0]; x++)
                {
                    element = ring[x, y];
                    
                    if (element == "E") // si c'est de l'eau
                        Console.ForegroundColor = ConsoleColor.Blue;
                    if(int.TryParse(element, out int n) && n > 0) // si c'est un nombre (un joueur)
                        Console.ForegroundColor = ConsoleColor.Green;
                    if(element == id.ToString()) // si c'est l'élement courant
                        SetBackground(PositionCourante(id), color);
                    else
                        Console.Write(element + " ");
                    
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public int[] PositionCourante(int id)
        {
            int[] tab =
            {
                items[id][0],
                items[id][1]
            };
            
            return tab;
        }

        public void SetBackground(int[] position, ConsoleColor color)
        {
            string caractere = ring[position[0], position[1]];
            ConsoleColor ancienneCouleur = Console.BackgroundColor;
            /*int[] curseurAnciennePosition =
            {
                Console.CursorLeft,
                Console.CursorTop
            };*/
            
            /*if(position[0] > 0)
                Console.SetCursorPosition(position[0] * 2 + 1, position[1]);
            else
                Console.SetCursorPosition(2, position[1]);*/
            
            Console.BackgroundColor = color;
            //Console.Write("\b \b");
            Console.Write(caractere);
            Console.BackgroundColor = ancienneCouleur;
            Console.Write(" ");
            //Console.SetCursorPosition(curseurAnciennePosition[0], curseurAnciennePosition[1]);
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
