﻿using System;

namespace RPG
{
    public class Voleur : Personnage
    {
        protected new const int vieMax = 80; // new  car on override Personnage
        protected new const int manaMax = 60;
        
        public Voleur(Ring ringObject, int arme = Arme.MAINS) : base(ringObject, arme)
        {
        }

        public Voleur(Ring ringObject, int x, int y = 0, int arme = Arme.MAINS) : base(ringObject, x, y, arme)
        {
        }
        
        protected override void InitConstructeur(int arme, int x = 0, int y = 0)
        {
            this.arme = new Arme(arme);
            vie = vieMax;
            mana = manaMax;
            etat = true;
            
            banque = new Banque();
            ressource = new Ressource();

            nombreJoueurs++;

            id = ring.AjouterElement(nombreJoueurs, x, y);
            ids.Add(id);
            nom = "Player" + id;
            
            dateConstruction = DateTime.Now.ToString("dd/MM/yyyy");
        }
        
        public override string NomClasse()
        {
            return "Voleur";
        }

        public void VolerArme(Personnage cible)
        {
            Arme.TypeArme = cible.Arme.TypeArme;
        }
    }
}