﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RPG.Etre.Personnage
{
    public class Guerrier : Personnage
    {
        private new ArrayList arme = new ArrayList();
        private new const int vieMax = 150; // new  car on override Personnage
        private new const int manaMax = 60;

        public Guerrier(ManagerEtre managerEtre = null, int arme = Arme.EPEE) : base(managerEtre, arme)
        {
        }
        
        protected override void InitConstructeur(int arme)
        {
            this.arme.Add(new Arme(arme));
            vie = vieMax;
            mana = manaMax;
        }
        
        public override void RecevoirDegats(int degats)
        {
            if (degats > 0)
            {
                Random aleatoire = new Random();
                int nombreChance = aleatoire.Next(1, 6);

                if (nombreChance > 3 || arme.Contains(Arme.BOUCLIER)) // si il porte un bouclier
                    vie -= (int) Math.Ceiling(Math.Sqrt(degats));
                else
                    vie -= degats;

                if (vie <= 0){
                    etat = false; // la cible est morte
                    vie = 0;
                }
            }
        }
        
        public void AjouterArme()
        {
            string choix;
            int nombre;
            
            Arme.AfficherListe();

            choix = Console.ReadLine();
            if(!Int32.TryParse(choix, out nombre)) Message.Add("Le choix " + choix + " n'est pas une arme");
            else
            {
                if (Arme.EstValide(nombre-1)) arme.Add(new Arme(nombre-1));
                else Message.Add("Arme non valide");
            }
        }
        
        public override List<string> ListeCompetences()
        {
            List<string> listeSorts = new List<string>();
            listeSorts.Add("Ajouter arme");
            listeSorts.Add("Jeter arme");

            return listeSorts;
        }

        public void JeterArme()
        {
            int compteur = 1;
            
            Console.WriteLine("Jeter quelle arme ?");
            
            foreach (Arme armeCourante in arme)
            {
                Console.WriteLine("{0} - {1}", compteur, armeCourante.Nom);
                compteur++;
            }

            string choix;
            int choixNombre;
            choix = Console.ReadLine();
            if (!choix.All(char.IsDigit))
            {
                Message.Add("Arme non portée");
                return;
            }
                
            choixNombre = Convert.ToInt32(choix);
            
            if(choixNombre > 0 && choixNombre < compteur && arme.Count > 1)
                arme.RemoveAt(choixNombre-1);
            else
                Message.Add("Arme non portée");
        }
        
        public override string NomClasse()
        {
            return GUERRIER;
        }

        public override Arme Arme => arme[arme.Count - 1] as Arme;
        
        public override int VieMax => vieMax;
        public override int Vie
        {
            get => vie;
            set => vie = value;
        }
        public override int ManaMax => manaMax;
    }
}