﻿﻿using System;
 using System.Collections;

 namespace RPG
{
    public abstract class Personnage : Etre.Etre
    {
        protected Arme arme;
        protected Ressource ressource;
        protected Banque banque;

        protected int mana;
        protected static Ring ring;
        protected static int nombreJoueurs = 0;
        protected static ArrayList ids = new ArrayList();

        protected int id;

        
        protected const int potionVie = 20;
        protected const int manaMax = 100;
        protected const int coutPotionVie = 20;
        protected int buffDegats;
        protected string dateConstruction;
        public const string GUERRIER = "Guerrier";
        public const string MAGICIEN = "Magicien";
        public const string VOLEUR = "Voleur";

        public Personnage(Ring ringObject, int arme = Arme.MAINS)
        {
            ring = ringObject;
            InitConstructeur(arme);
            InitCommun();
        }

        public Personnage(Ring ringObject, int x, int y = 0, int arme = Arme.MAINS)
        {
            ring = ringObject;
            InitConstructeur(arme);
            InitCommun(x, y);
        }

        protected void InitCommun(int x = 0, int y = 0)
        {
            etat = true;
            buffDegats = 0;
            
            banque = new Banque();
            ressource = new Ressource();
            
            nombreJoueurs++;
            
            id = ring.AjouterElement(nombreJoueurs, x, y);
            ids.Add(id);
            nom = "Player" + id;
            
            dateConstruction = DateTime.Now.ToString("dd/MM/yyyy");
        }

        protected abstract void InitConstructeur(int arme);

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
                Soigne(potionVie);

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
                personnage.RecevoirDegats(Arme.Degats + buffDegats);
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
        
        public void AfficherEtat(ElementGraphique elementGraphique)
        {
            Console.WriteLine("Classe : " + NomClasse());
            Console.WriteLine("Nom : " + nom);
            
            Console.Write("Vie : {0} / {1} ", vie, VieMax);
            elementGraphique.BarreProgression(vie, VieMax, ConsoleColor.Red);
            Console.WriteLine();
            
            Console.Write("Mana : {0} / {1} ", mana, ManaMax);
            elementGraphique.BarreProgression(mana, ManaMax, ConsoleColor.Blue);
            Console.WriteLine();
            
            Console.WriteLine("Arme : {0} ({1})", Arme.Nom, Arme.Degats);
            Console.WriteLine("Portée arme : " + Arme.Portee);

            AfficherBuff();
            
            Console.WriteLine("");
        }

        public void AfficherBuff()
        {
            ConsoleColor ancienneCouleur = Console.ForegroundColor;
            if (buffDegats > 0)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Buff dégâts : " + buffDegats);
            }

            Console.ForegroundColor = ancienneCouleur;
        }

        public abstract string NomClasse();

        public bool Existe()
        {
            if (!ids.Contains(id)) return false;
            
            return etat;
        }
        
        public void Soigne(int montantSoins)
        {
            Vie += montantSoins;
            
            if (Vie > VieMax)
                Vie = VieMax;
        }

        public override string ToString()
        {
            return nom;
        }

        public virtual Arme Arme => arme;
        
        public virtual int VieMax => vieMax;
        public virtual int Vie
        {
            get => vie;
            set => vie = value;
        }

        public virtual int ManaMax => manaMax;

        public void BuffDegats(int buff)
        {
            buffDegats = buff;
        }

        public string DateConstruction => dateConstruction;
    }
}
