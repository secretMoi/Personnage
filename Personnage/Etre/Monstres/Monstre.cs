using System;

namespace RPG.Monstres
{
    public class Monstre : Etre.Etre
    {
        public Monstre()
        {
            
        }

        public void SeDeplaceAuto()
        {
            Random aleatoire = new Random();
            int nombreDirection = aleatoire.Next(1, 5);
            
            switch (nombreDirection)
            {
                case 1:
                    managerEtre.Deplace(id, Map.RIGHT);
                    break;
                case 2:
                    managerEtre.Deplace(id, Map.LEFT);
                    break;
                case 3:
                    managerEtre.Deplace(id, 0, Map.DOWN);
                    break;
                case 4:
                    managerEtre.Deplace(id, 0, Map.UP);
                    break;
            }
        }
    }
}