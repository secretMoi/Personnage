using System;
using System.Collections.Generic;

namespace RPG.Etre.Personnage
{
    public abstract class Personnage : Etre
    {
        protected Arme arme;
        protected Ressource ressource;
        protected Banque banque;
        
        protected int mana;
        
        protected const int potionVie = 20;
        protected const int manaMax = 100;
        protected const int coutPotionVie = 20;
        protected int buffDegats;
        
        public const string GUERRIER = "Guerrier";
        public const string MAGICIEN = "Magicien";
        public const string VOLEUR = "Voleur";
        
        public Personnage(ManagerEtre managerEtre = null, int arme = Arme.MAINS) : base(managerEtre)
        {
            InitConstructeur(arme);
            InitCommun();
        }
        
        protected abstract void InitConstructeur(int arme);
        
        protected void InitCommun()
        {
            buffDegats = 0;
            
            banque = new Banque();
            ressource = new Ressource();
        }
        
        public void Soigne(int montantSoins)
        {
            Vie += montantSoins;
            
            if (Vie > VieMax)
                Vie = VieMax;
        }
        public void AfficherEtat(ElementGraphique elementGraphique)
        {
            int offsetX = Console.CursorLeft; // enregistre le décalage gauche pour ne pas le perdre lors du retour à la ligne
            
            Console.WriteLine("Classe : " + NomClasse());
            Console.CursorLeft = offsetX;
            Console.WriteLine("Nom : " + nom);
            Console.CursorLeft = offsetX;
            
            Console.Write("Vie : {0} / {1} ", vie, VieMax);
            elementGraphique.BarreProgression(vie, VieMax, ConsoleColor.Red);
            Console.WriteLine();
            Console.CursorLeft = offsetX;
            
            Console.Write("Mana : {0} / {1} ", mana, ManaMax);
            elementGraphique.BarreProgression(mana, ManaMax, ConsoleColor.Blue);
            Console.WriteLine();
            Console.CursorLeft = offsetX;
            
            Console.WriteLine("Arme : {0} ({1})", Arme.Nom, Arme.Degats);
            Console.CursorLeft = offsetX;
            Console.WriteLine("Portée arme : " + Arme.Portee);
            Console.CursorLeft = offsetX;

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
        
        public bool APortee(Personnage personnage)
        {
            if (personnage.Existe())
                return managerEtre.Distance(id, personnage.id) <= Arme.Portee;
            
            return false;
        }
        
        public void BoirePotion()
        {
            if(etat == true && mana > coutPotionVie)
            {
                Soigne(potionVie);

                mana -= coutPotionVie;
            }
        }
        
        public void BuffDegats(int buff)
        {
            buffDegats = buff;
        }

        public abstract List<string> ListeCompetences();
        
        public abstract string NomClasse();
        
        public virtual Arme Arme => arme;
        
        public virtual int ManaMax => manaMax;
    }
}