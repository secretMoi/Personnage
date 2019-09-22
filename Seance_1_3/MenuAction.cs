﻿using System;
using System.Collections;

namespace RPG
{
    class MenuAction
    {
        private const int consoleX = 60;
        private const int consoleY = 30;

        private Ring ring = new Ring();
        private ArrayList player = new ArrayList();

        private int tourJoueur;
        private int nombreJoueurs = 2;

        private bool error;

        private ArrayList choix = new ArrayList();
        private int compteur;

        public MenuAction()
        {
            Console.SetWindowSize(consoleX, consoleY); // taille console
            Console.SetBufferSize(consoleX * 2, consoleY * 4); // taille buffer

            player.Add(new Guerrier(ring));
            player.Add(new Magicien(ring, 0, 1));

            nombreJoueurs = 2;
            error = false;
            tourJoueur = 0;
        }

        public void ListeActions()
        {
            Personnage joueur = (player[tourJoueur] as Personnage);

            if (error == false) // si il n'y a pas d'erreur
            {
                ring.DisplayGrid();
                joueur.AfficherEtat();
            }
            
            AfficheChoix(joueur);
            Action(joueur);
            
            error = !(Message.IsEmpty());
            Message.AfficheInfo();

            if (error == false)
            {
                IncrementeTour();
                Console.Clear();
            }

            ListeActions();
        }

        private void Action(Personnage joueur)
        {
            string choix = Console.ReadLine();
            string choixConverti = ConvertiChoix(choix);
            switch (choixConverti)
            {
                case "1":
                    Console.WriteLine("Déplacements : 4 = Gauche, 6 = Droite, 2 = Bas, 8 = Haut");
                    joueur.ActionDeplacement();
                    break;
                case "2":
                    Console.WriteLine("Indiquez le n° de joueur a attaquer");
                    joueur.ActionAttaque((player[StringToInt()] as Personnage));
                    break;
                case "3":
                    joueur.BoirePotion();
                    break;
                case "4":
                    Magicien magicien = (Magicien) joueur;
                    Console.WriteLine("Indiquez le n° de joueur a attaquer");
                    magicien.LancerSort((player[StringToInt()] as Personnage));
                    break;
                case "5":
                    Voleur voleur = (Voleur) joueur;
                    Console.WriteLine("Indiquez le n° de joueur a dérober");
                    voleur.VolerArme((player[StringToInt()] as Personnage));
                    break;
                case "6":
                    Guerrier guerrier = (Guerrier) joueur;
                    Console.WriteLine("Indiquez le n° de l'arme voulue");
                    guerrier.AjouterArme();
                    break;
                case "Q":
                    Environment.Exit(1);
                    break;
                default:
                    Console.WriteLine("Cette option n'est pas dans le menu, veuillez refaire votre choix");
                    Action(joueur);
                    break;
            }
        }

        private string ConvertiChoix(string choix)
        {
            if (choix == "Q")
                return choix;

            int choixNombre = Convert.ToInt32(choix) - 1;
            object position = this.choix[choixNombre];
            
            return position.ToString();
        }

        public void AfficheChoix(Personnage joueur)
        {
            compteur = 1;
            choix.Clear();
            Console.WriteLine("Actions possibles : ");
            
            AfficheMessageCompteur("Se déplacer d'une case", "1");
            AfficheMessageCompteur("Attaquer à max " + joueur.Arme.Portee + " cases", "2");
            AfficheMessageCompteur("Boire potion de vie contre mana", "3");
            
            if(joueur.NomClasse() == "Magicien")
                AfficheMessageCompteur("Lancer un sort", "4");
            if(joueur.NomClasse() == "Voleur")
                AfficheMessageCompteur("Voler une arme", "5");
            if(joueur.NomClasse() == "Guerrier")
                AfficheMessageCompteur("Changer d'arme", "6");
            
            Console.WriteLine("Q - Quitter");
        }

        private void AfficheMessageCompteur(string message, string choix)
        {
            Console.WriteLine("{0} - {1}", compteur, message);
            this.choix.Add(choix);
            
            compteur++;
        }

        private void IncrementeTour()
        {
            tourJoueur++;
            if (tourJoueur == nombreJoueurs) tourJoueur = 0;
        }

        private int StringToInt()
        {
            string texte = Console.ReadLine();

            if (!Int32.TryParse(texte, out int nombre)) nombre = -1; // si le texte n'est pas un nombre

            return nombre-1;
        }
    }
}
