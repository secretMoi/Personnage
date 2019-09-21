﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Seance_3_1
{
    class Personnage
    {
        private Arme arme;
        private Ressource ressource;
        private Banque banque;
        private int vie;
        private int mana;
        private static Ring ring;
        private static int nombreJoueurs = 0;
        private bool etat; // si le perso est vivant
        private int id;
        private string nom;
        private const int vieMax = 100;
        private const int potionVie = 20;
        private const int manaMax = 100;
        private const int coutPotionVie = 20;
        private string datteConstruction;

        public Personnage(Ring ringObject, int arme = Arme.MAINS)
        {
            ring = ringObject;
            InitConstructeur(arme);
        }

        public Personnage(Ring ringObject, int x, int y = 0, int arme = Arme.MAINS)
        {
            ring = ringObject;
            InitConstructeur(arme, x, y);
            etat = false;
        }

        protected void InitConstructeur(int arme, int x = 0, int y = 0)
            {
            this.arme = new Arme(arme);
            vie = vieMax;
            mana = manaMax;
            etat = true;

            nombreJoueurs++;

            id = ring.AjouterElement(nombreJoueurs, x, y);
            nom = "Player" + id;
            
            datteConstruction = DateTime.Now.ToString("dd/MM/yyyy");
        }

        public void RecevoirDegats(int degats)
        {
            Random aleatoire = new Random();
            int nombreChance = aleatoire.Next(1, 6);

            if (nombreChance > 3)
                vie -= (int) Math.Ceiling(Math.Sqrt(degats));
            else
                vie -= degats;

            if (vie <= 0){
                etat = false; // si la cible est morte
                vie = 0;
            }
        }

        public void BoirePotion()
        {
            if(etat == true && mana > coutPotionVie)
            {
                if (vie <= vieMax - potionVie)
                    vie += potionVie;
                else
                    vie = vieMax;

                mana -= coutPotionVie;
            }
        }

        public void SeDeplace(int x, int y = 0)
        {
            bool resultat = ring.Deplace(id, x, y);

            if (resultat == false)
                Message.AddMessage("Déplacement impossible");
        }

        public void ActonDeplacement()
        {
            switch (Console.ReadLine())
            {
            case "2":
                SeDeplace(0, Ring.DOWN);
                break;
            case "4":
                SeDeplace(Ring.LEFT);
                break;
            case "8":
                SeDeplace(0, Ring.UP);
                break;
            case "6":
                SeDeplace(Ring.RIGHT);
                break;
            default:
                Message.AddMessage("Mouvement non conforme");
                break;
            }
        }
        public void ActionAttaque(Personnage personnage)
        {
            if(APortee(personnage))
                personnage.RecevoirDegats(arme.GetDegats());
            else
                Message.AddMessage("Vous n'avez pas la portée requise : " + ring.Distance(id, personnage.id));
        }

        public bool APortee(Personnage personnage)
        {
            return ring.Distance(id, personnage.id) <= arme.GetPortee();
        }

        public void AfficherEtat()
        {
            Console.WriteLine("Nom : " + nom);
            Console.WriteLine("Arme : " + arme.GetNom());
            Console.WriteLine("Portée arme : " + arme.GetPortee());
            Console.WriteLine("Vie : " + vie);
            Console.WriteLine("Mana : " + mana);
            Console.WriteLine("");
        }

        public override string ToString()
        {
            return nom;
        }

        public bool GetEtat()
        {
            return etat;
        }

        public Arme Arme => arme;

        public string DatteConstruction => datteConstruction;
    }
}
