﻿﻿using System;
 using System.Collections;

 namespace RPG
{
    public class Arme
    {
        private int typeArme;
        private int degats;
        private int portee;
        private int durabilite;
        private String nom;
        
        private const int armeMax = 3;
        public const int BATON = 0;
        public const int MAINS = 1;
        public const int EPEE = 2;
        public const int BOUCLIER = 3;
        
        private static ArrayList liste = new ArrayList();

        public Arme(int typeArme)
        {
            if (EstValide(typeArme)) this.typeArme = typeArme;
            else this.typeArme = MAINS;
            
            AssocieDegats();
        }

        public Arme()
        {
            typeArme = MAINS;
            AssocieDegats();
        }

        private void AssocieDegats()
        {
            switch (typeArme)
            {
                case BATON:
                    degats = 10;
                    portee = 2;
                    nom = "Bâton";
                    durabilite = 5;
                    break;
                case MAINS:
                    degats = 5;
                    portee = 1;
                    nom = "Mains nues";
                    durabilite = 0;
                    break;
                case EPEE:
                    degats = 20;
                    portee = 1;
                    nom = "Epée";
                    durabilite = 20;
                    break;
                case BOUCLIER:
                    degats = 7;
                    portee = 1;
                    nom = "Bouclier";
                    durabilite = 15;
                    break;
                default:
                    degats = 0;
                    portee = 0;
                    nom = "Aucune";
                    durabilite = 0;
                    break;
            }
        }

        public bool EstValide(int arme)
        {
            return arme > 0 && arme <= armeMax;
        }

        private ArrayList Liste()
        {
            if (liste.Count == 0)
            {
                liste.Add("Bâton");
                liste.Add("Mains nues");
                liste.Add("Epée");
                liste.Add("Bouclier");
            }

            return liste;
        }

        public void AfficherListe()
        {
            int compteur = 1;
            foreach (string nom in Liste())
            {
                Console.WriteLine("{0} - {1}", compteur, nom);
                compteur++;
            }
        }

        public int Portee => portee;

        public string Nom => nom;

        public int Degats => degats;

        public int Durabilite => durabilite;

        public int TypeArme
        {
            get => typeArme;
            set => typeArme = value;
        }
    }
}
