using System;

namespace RPG
{
    public class ElementGraphique
    {
        public ElementGraphique()
        {
        
        }

        public void BarreProgression(int valeurCourante, int valeurMax, ConsoleColor couleur)
        {
            ConsoleColor ancienneCouleur = Console.ForegroundColor;
            Console.ForegroundColor = couleur;

            double division = (double)valeurCourante / (double)valeurMax;
            int rapport = (int) (division * 10);

            for (int i = 0; i < 10; i++)
            {
                if(i < rapport)
                    Console.Write('|');
                else
                    Console.Write(' ');
            }

            Console.ForegroundColor = ancienneCouleur;
        }
    }
}