﻿using System;

namespace RPG
{
    public class Magicien : Personnage
    {
        protected new const int vieMax = 80; // new car on override Personnage
        protected new const int manaMax = 180;
        private const int montantBuffDegats = 10;
        private const int montantSoins = 15;
        
        public Magicien(Ring ringObject, int arme = Arme.MAINS) : base(ringObject, arme)
        {
        }

        public Magicien(Ring ringObject, int x, int y = 0, int arme = Arme.MAINS) : base(ringObject, x, y, arme)
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
            return "Magicien";
        }
        
        public override int VieMax => vieMax;
        public override int Vie
        {
            get => vie;
            set => vie = value;
        }
        public override int ManaMax => manaMax;

        public void LancerSort(Personnage cible)
        {
            cible.RecevoirDegats(20);
        }

        public void SelectionnerSort(Personnage cible)
        {
            Console.WriteLine("1 - Boule de feu");
            Console.WriteLine("2 - Soin");
            Console.WriteLine("3 - Buff dégâts");
            
            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    LancerSort(cible);
                    break;
                case "2":
                    cible.Soigne(montantSoins);
                    break;
                case "3":
                    cible.BuffDegats(montantBuffDegats);
                    break;
                default:
                    Message.Add("Choix non disponible");
                    break;
            }
        }
    }
}