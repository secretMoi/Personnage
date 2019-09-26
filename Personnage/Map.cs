using System;
using System.Drawing;

namespace RPG
{
    public abstract class Map
    {
        protected static int tailleX;
        protected static int tailleY;
        private const string point = ".";
        private const string mur = "#";
        private const string porte = "|";
        private const string or = "G";
        private const string tresor = "$";
        protected const string sortie = "X";
        
        protected string[,] tab;
        
        public Map(int tailleX = 50, int tailleY = 50)
        {
            Map.tailleX = tailleX;
            Map.tailleY = tailleY;
            tab = new string[tailleX, tailleY];
            
            GenereMap();
            GenereMur();
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
            for (int y = 0; y < tailleY; y++)
            {
                for (int x = 0; x < tailleX; x++)
                {
                    if (tab[x, y] == point)
                    {
                        GenereMursAlentours(x, y);
                    }
                }
            }
        }

        private void GenereMursAlentours(int x, int y)
        {
            if (tab[x - 1, y - 1] == null) tab[x - 1, y - 1] = mur; // haut gauche
            if (tab[x, y - 1] == null) tab[x, y - 1] = mur; // haut
            if (tab[x + 1, y - 1] == null) tab[x + 1, y - 1] = mur; // haut droite
            if (tab[x + 1, y] == null) tab[x + 1, y] = mur; // droite
            if (tab[x + 1, y + 1] == null) tab[x + 1, y + 1] = mur; // bas droite
            if (tab[x, y + 1] == null) tab[x, y + 1] = mur; // bas
            if (tab[x - 1, y + 1] == null) tab[x - 1, y + 1] = mur; // bas gauche
            if (tab[x - 1, y] == null) tab[x - 1, y] = mur; // gauche
        }

        private void ChangeCouleur(string message, ConsoleColor? texte, ConsoleColor? arrierePlan, bool RetourLigne = false)
        {
            ConsoleColor ancienneCouleur = Console.ForegroundColor;
            ConsoleColor ancienArrierePlan = Console.BackgroundColor;
            
            if (texte == null) texte = Console.ForegroundColor;
            if (arrierePlan == null) arrierePlan = Console.BackgroundColor;
            
            Console.ForegroundColor = (ConsoleColor) texte;
            Console.BackgroundColor = (ConsoleColor) arrierePlan;
            
            if(RetourLigne == false)
                Console.Write(message);
            else
                Console.WriteLine(message);

            Console.ForegroundColor = ancienneCouleur;
            Console.BackgroundColor = ancienArrierePlan;
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