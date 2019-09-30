using System;
using System.Collections.Generic;
using System.Drawing;
// todo générer les rectangles et enregistres leur pos + dim pour n'afficher qu'eux si leur porte a ete ouverte
namespace RPG
{
    public abstract class Map
    {
        protected static int tailleX;
        protected static int tailleY;
        
        private const string point = ".";
        private const string mur = "#";
        protected const string porte = "|";
        protected const string or = "G";
        protected const string tresor = "$";
        protected const string sortie = "X";
        protected const string joueur = "J";
        
        protected string[,] tab;
        // items contient un id pour un élément et une liste pour ses positions et autres valeurs
        private readonly Dictionary<int, List<int>> items = new Dictionary<int, List<int>>();
        
        public static int LEFT = -1;
        public static int RIGHT = 1;
        public static int DOWN = 1;
        public static int UP = -1;
        private static int compteurItem = 0;
        
        public Map(int tailleX = 100, int tailleY = 40)
        {
            Map.tailleX = tailleX;
            Map.tailleY = tailleY;
            tab = new string[tailleX, tailleY];
            
            GenereMap();
            GenereMur();
        }
        
        public int AjouterElement(int etat = 1, int x = 0, int y = 0)
        {
            compteurItem++;
            items.Add(compteurItem, new List<int> { x, y }); // rajoute l'id avec ses coordonnées

            tab[x, y] = etat.ToString();

            return compteurItem;
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

        protected abstract void GenereMap();

        protected void GenereRectangle(int x, int y, int longueur, int hauteur)
        {
            for (int yCompteur = y; yCompteur < y + hauteur; yCompteur++)
            {
                for (int xCompteur = x; xCompteur < x + longueur; xCompteur++)
                {
                    tab[xCompteur, yCompteur] = point;
                }
            }
        }

        protected void GenereMur()
        {
            for (int y = 1; y < tailleY-1; y++)
                for (int x = 1; x < tailleX-1; x++)
                    if (tab[x, y] != null && tab[x, y] != mur)
                        GenereMursAlentours(x, y);
        }

        private void GenereMursAlentours(int x, int y)
        {
            if (tab[x - 1, y - 1] == null) tab[x - 1, y - 1] = mur; // haut gauche
            if (tab[x, y - 1]     == null) tab[x, y - 1] = mur; // haut
            if (tab[x + 1, y - 1] == null) tab[x + 1, y - 1] = mur; // haut droite
            if (tab[x + 1, y]     == null) tab[x + 1, y] = mur; // droite
            if (tab[x + 1, y + 1] == null) tab[x + 1, y + 1] = mur; // bas droite
            if (tab[x, y + 1]     == null) tab[x, y + 1] = mur; // bas
            if (tab[x - 1, y + 1] == null) tab[x - 1, y + 1] = mur; // bas gauche
            if (tab[x - 1, y]     == null) tab[x - 1, y] = mur; // gauche
        }
        
        public bool Deplace(int id, int x, int y = 0)
        {
            // récupère la position courante de l'item
            int xCourant = items[id][0];
            int yCourant = items[id][1];

            if (PositionPossible(x + xCourant, y + yCourant) == false)
                return false;

            tab[xCourant, yCourant] = point; // supprime la position actuelle

            // change la position
            items[id][0] += x;
            items[id][1] += y;
            tab[items[id][0], items[id][1]] = id.ToString();

            return true;
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
        
        protected bool PositionPossible(int x, int y)
        {
            // si on ne sort pas du ring et que la position est inoccupée
            if (x < tailleX && x >= 0 && y < tailleY && y >= 0)
            {
                if (tab[x, y] == point) // si la case est vide
                    return true;
            }
                
            return false;
        }

        private void ChangeCouleur(string message, ConsoleColor? texte, ConsoleColor? arrierePlan, bool retourLigne = false)
        {
            ConsoleColor ancienneCouleur = Console.ForegroundColor;
            ConsoleColor ancienArrierePlan = Console.BackgroundColor;
            
            if (texte == null) texte = Console.ForegroundColor;
            if (arrierePlan == null) arrierePlan = Console.BackgroundColor;
            
            Console.ForegroundColor = (ConsoleColor) texte;
            Console.BackgroundColor = (ConsoleColor) arrierePlan;
            
            if(retourLigne == false)
                Console.Write(message);
            else
                Console.WriteLine(message);

            Console.ForegroundColor = ancienneCouleur;
            Console.BackgroundColor = ancienArrierePlan;
        }
        
        public void SetBackground(int[] position, ConsoleColor color)
        {
            string caractere = tab[position[0], position[1]];
            ConsoleColor ancienneCouleur = Console.BackgroundColor;

            Console.BackgroundColor = color;

            Console.Write(caractere);
            Console.BackgroundColor = ancienneCouleur;
        }

        public void AfficheMap()
        {
            for (int y = 0; y < tailleY; y++)
            {
                for (int x = 0; x < tailleX; x++)
                {
                    switch (tab[x, y])
                    {
                        case mur:
                            ChangeCouleur(mur, ConsoleColor.Black, ConsoleColor.White);
                            break;
                        
                        case point:
                            Console.Write(tab[x, y]);
                            break;
                        
                        case sortie:
                            ChangeCouleur(sortie, ConsoleColor.Red, null);
                            break;
                        
                        case joueur:
                            ChangeCouleur(joueur, ConsoleColor.Green, ConsoleColor.Gray);
                            break;
                        
                        default:
                            Console.Write(" ");
                            break;
                    }
                }
                
                Console.WriteLine();
            }
        }
        
        public string Point => point;
        public string Mur => mur;
        public string Porte => porte;
        public string Or => or;
        public string Tresor => tresor;
    }
}