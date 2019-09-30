using System;

namespace RPG.Maps
{
    public class Map1 : Map
    {
        public Map1(int tailleX = 35, int tailleY = 10) : base(tailleX, tailleY)
        {
        }

        protected override void GenereMap()
        {
            GenereRectangle(1, 1, 5, 5);
            GenereRectangle(6, 3, 8, 1);
            GenereRectangle(14, 1, 5, 5);
            GenereRectangle(19, 3, 8, 1);

            tab[25, 3] = sortie;
            tab[1, 1] = joueur;
        }
    }
}