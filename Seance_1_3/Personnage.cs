﻿using System;
 using System.Collections;

 namespace RPG
{
    public abstract class Personnage
    {
        protected Arme arme;
        protected Ressource ressource;
        protected Banque banque;
        protected int vie;
        protected int mana;
        protected static Ring ring;
        protected static int nombreJoueurs = 0;
        protected static ArrayList ids = new ArrayList();
        protected bool etat; // si le perso est vivant
        protected int id;
        protected string nom;
        protected const int vieMax = 100;
        protected const int potionVie = 20;
        protected const int manaMax = 100;
        protected const int coutPotionVie = 20;
        protected string dateConstruction;

        public Personnage(Ring ringObject, int arme = Arme.MAINS)
        {
            ring = ringObject;
            InitConstructeur(arme);
        }

        public Personnage(Ring ringObject, int x, int y = 0, int arme = Arme.MAINS)
        {
            ring = ringObject;
            InitConstructeur(arme, x, y);
        }

        protected virtual void InitConstructeur(int arme, int x = 0, int y = 0)
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

        public virtual void RecevoirDegats(int degats)
        {
            if (degats > 0)
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
                Message.Add("Déplacement impossible");
        }

        public void ActionDeplacement()
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
                Message.Add("Mouvement non conforme");
                break;
            }
        }
        public void ActionAttaque(Personnage personnage)
        {
            if (!personnage.Existe())
            {
                Message.Add(personnage.NomClasse());
                Message.Add(personnage.etat.ToString());
                
                return;
            }
                
            if(APortee(personnage))
                personnage.RecevoirDegats(Arme.Degats);
            else
            {
                Message.Add("Vous n'avez pas la portée requise : " + ring.Distance(id, personnage.id));
                Message.Add("Votre portée est de : " + Arme.Portee);
            }
        }

        public bool APortee(Personnage personnage)
        {
            if (personnage.Existe())
                return ring.Distance(id, personnage.id) <= Arme.Portee;
            
            return false;
        }

        public void AfficherEtat()
        {
            Console.WriteLine("Classe : " + NomClasse());
            Console.WriteLine("Nom : " + nom);
            Console.WriteLine("Vie : " + vie);
            Console.WriteLine("Mana : " + mana);
            Console.WriteLine("Arme : {0} ({1})", Arme.Nom, Arme.Degats);
            Console.WriteLine("Portée arme : " + Arme.Portee);
            Console.WriteLine("");
        }

        public abstract string NomClasse();

        public bool Existe()
        {
            if (!ids.Contains(id)) return false;
            
            return etat;
        }

        public override string ToString()
        {
            return nom;
        }

        public virtual Arme Arme => arme;

        public string DateConstruction => dateConstruction;
    }
}
