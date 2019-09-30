using System;
using System.Collections;

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
        
        public abstract string NomClasse();
        
        public virtual Arme Arme => arme;
        
        public virtual int ManaMax => manaMax;
    }
}