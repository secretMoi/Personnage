﻿using System;
using System.Collections;

namespace RPG.Etre
{
    public abstract class Etre
    {
        protected int vie;
        protected int vieMax;
        protected bool etat; // si le perso est vivant
        protected string nom;
        protected string id;
        protected static ArrayList ids = new ArrayList();
        protected static ManagerEtre managerEtre;
        
        public Etre(ManagerEtre managerEtre = null)
        {
            if(Etre.managerEtre == null)
                Etre.managerEtre = managerEtre;

            id = managerEtre.AjouterElement();
            
            etat = true;
        }
        
        public bool Existe()
        {
            if (!ids.Contains(id)) return false;
            
            return etat;
        }
        
        public void SeDeplace(int x, int y = 0)
        {
            bool resultat = managerEtre.Deplace(id, x, y);

            if (resultat == false)
                Message.Add("Déplacement impossible");
        }
        
        public virtual void RecevoirDegats(int degats)
        {
            if (degats > 0)
            {
                vie -= degats;

                if (vie <= 0){
                    etat = false; // si la cible est morte
                    vie = 0;
                }
            }
        }
        
        public bool APortee(Etre etre)
        {
            if (etre.Existe())
                return managerEtre.Distance(id, etre.id) <= 1;
            
            return false;
        }

        public string Id => id;

        public virtual int VieMax => vieMax;
        public virtual int Vie
        {
            get => vie;
            set => vie = value;
        }
        
        public override string ToString()
        {
            return nom;
        }
    }
}