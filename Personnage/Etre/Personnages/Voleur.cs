﻿using RPG.Etre;
 using RPG.Etre.Personnage;

namespace RPG
{
    public class Voleur : Personnage
    {
        protected new const int vieMax = 80; // new  car on override Personnage
        protected new const int manaMax = 60;

        public Voleur(ManagerEtre managerEtre = null, int arme = Arme.MAINS) : base(Etre.Etre.managerEtre, arme)
        {
        }
        
        protected override void InitConstructeur(int arme)
        {
            this.arme = new Arme(arme);
            vie = vieMax;
            mana = manaMax;
        }
        
        public override string NomClasse()
        {
            return VOLEUR;
        }

        public void VolerArme(Personnage cible)
        {
            Arme.TypeArme = cible.Arme.TypeArme;
        }
        
        public override int VieMax => vieMax;
        public override int Vie
        {
            get => vie;
            set => vie = value;
        }
        public override int ManaMax => manaMax;
    }
}