using System;
using System.Collections.Generic;
using System.Drawing;
// todo générer les rectangles et enregistres leur pos + dim pour n'afficher qu'eux si leur porte a ete ouverte
namespace RPG
{
    public abstract class Map
    {
        protected int tailleX;
        protected int tailleY;
        private int offsetX;
        private int offsetY;
        
        private const string point = ".";
        private const string mur = "#";
        protected const string porte = "|";
        protected const string or = "G";
        protected const string tresor = "$";
        protected const string sortie = "X";
        protected const string joueur = "J";
        protected const string monstre = "M";
        
        protected string[,] tab;
        // items contient un id pour un élément et une liste pour ses positions et autres valeurs
        private readonly Dictionary<string, List<int>> items;
        
        public const int LEFT = -1;
        public const int RIGHT = 1;
        public const int DOWN = 1;
        public const int UP = -1;
        private static int compteurItem = 0;
        
        public Map(int tailleX = 100, int tailleY = 40)
        {
            this.tailleX = tailleX;
            this.tailleY = tailleY;
            tab = new string[tailleX, tailleY];

            items = new Dictionary<string, List<int>>();
            
            GenereMap();
            GenereMur();
        }
        
        public string AjouterElement(string element, int x = 0, int y = 0)
        {
            compteurItem++;
            items.Add(element, new List<int> { x, y }); // rajoute l'id avec ses coordonnées

            tab[x, y] = element;

            return element;
        }

        public string LieElement(List<int> coordonnees)
        {
            foreach(KeyValuePair<string, List<int>> entry in items) // parcours le dictionnaire
            {
                if (entry.Value[0] == coordonnees[0] && entry.Value[1] == coordonnees[1]) // si ses coordonnees correspondent
                {
                    return entry.Key;
                }
            }

            return null; // si pas trouvé
        }
        
        public int[] PositionCourante(string id)
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
        
        public bool Deplace(string id, int x, int y = 0)
        {
            // récupère la position courante de l'item
            int xCourant = items[id][0];
            int yCourant = items[id][1];

            if (PositionPossible(x + xCourant, y + yCourant) == false)
                return false;

            string ancienneValeur = tab[xCourant, yCourant];
            tab[xCourant, yCourant] = point; // supprime la position actuelle

            // change la position
            items[id][0] += x;
            items[id][1] += y;
            tab[items[id][0], items[id][1]] = ancienneValeur;
            
            ReDraw(id, x, y);
            
            return true;
        }

        public void ReDraw(string id, int x, int y = 0)
        {
            // récupère la position courante de l'item
            int xCourant = items[id][0];
            int yCourant = items[id][1];

            int xSuivant = xCourant;
            int ySuivant = yCourant;

            if (x == RIGHT) xSuivant -= RIGHT;
            else if (x == LEFT) xSuivant -= LEFT;
            else if (y == UP) ySuivant -= UP;
            else if (y == DOWN) ySuivant -= DOWN;
            
            Console.SetCursorPosition(offsetX + xCourant, offsetY + yCourant);
            AfficheElement(xCourant, yCourant);
            
            Console.SetCursorPosition(offsetX + xSuivant, offsetY + ySuivant);
            AfficheElement(xSuivant, ySuivant);
        }

        public void SetOffsets(int x, int y)
        {
            offsetX = x;
            offsetY = y;
        }
        
        public int Distance(string id1, string id2)
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
                if (tab[x, y] == point || tab[x, y] == sortie) // si la case est vide
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
                    AfficheElement(x, y);
                }
                
                Console.WriteLine();
            }
        }

        private void AfficheElement(int x, int y)
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
                
                case monstre:
                    ChangeCouleur(monstre, ConsoleColor.Red, null);
                    break;
                        
                default:
                    Console.Write(" ");
                    break;
            }
        }

        protected void PlaceElement(int x, int y, string element)
        {
            if (!CoordonneesOK(x, y))
                return;

            items.Add(element + compteurItem, new List<int> { x, y }); // rajoute l'id avec ses coordonnées
            tab[x, y] = element;

            compteurItem++;
        }

        public List<List<int>> ListeMonstres()
        {
            List<List<int>> liste = new List<List<int>>();

            foreach(KeyValuePair<string, List<int>> entry in items) // parcours le dictionnaire
            {
                if (entry.Key.StartsWith(monstre)) // si c'est un monstre
                {
                    liste.Add(entry.Value); // rajoute ses coordonnées
                }
            }

            return liste;
        }

        private bool CoordonneesOK(int x, int y)
        {
            if (x > 0 && y > 0)
            {
                if (x < tailleX && y < tailleY)
                {
                    return true;
                }
            }

            return false;
        }
    }
}