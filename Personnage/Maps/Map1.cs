using System;

namespace RPG.Maps
{
    public class Map1 : Map
    {
        public Map1(int tailleX = 100, int tailleY = 40)
        {
        }

        protected override void GenereMap()
        {
            GenereRectangle(1, 1, 5, 5);
            GenereRectangle(6, 3, 8, 1);
            GenereRectangle(14, 1, 5, 5);
            GenereRectangle(19, 3, 8, 1);

            tab[25, 3] = sortie;
        }
    }
}