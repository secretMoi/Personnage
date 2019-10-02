using System;

namespace RPG.Maps
{
    public class Map1 : Map
    {
        public Map1(int tailleX = 35, int tailleY = 10) : base(tailleX, tailleY)
        {
        }
        
        public Map1()
        {
            tailleX = 35;
            tailleY = 10;
        }

        protected override void GenereMap()
        {
            GenereRectangle(1, 1, 5, 5);
            GenereRectangle(6, 3, 8, 1);
            GenereRectangle(14, 1, 5, 5);
            GenereRectangle(19, 3, 8, 1);

            PlaceElement(25, 3, sortie);
            PlaceElement(1, 1, joueur);
        }
    }
}