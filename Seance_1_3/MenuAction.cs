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

        public MenuAction()
        {
            Console.SetWindowSize(consoleX, consoleY); // taille console
            Console.SetBufferSize(consoleX * 2, consoleY * 2); // taille buffer

            player.Add(new Guerrier(this.ring));
            player.Add(new Magicien(this.ring, 0, 1));

            nombreJoueurs = 2;
            error = false;
            tourJoueur = 0;
        }

        public void ListeActions()
        {
            Personnage joueur = (player[tourJoueur] as Personnage);

            if (error == false)
            {
                ring.DisplayGrid();
                joueur.AfficherEtat();
                Console.WriteLine("Actions possibles : ");

                Console.WriteLine("1 - Se déplacer d'une case");
                Console.WriteLine("2 - Attaquer à max " + joueur.Arme.Portee + " cases");
                Console.WriteLine("3 - Boire potion de vie contre mana");
                if(joueur.NomClasse() == "Magicien")
                    Console.WriteLine("4 - Lancer un sort");
                if(joueur.NomClasse() == "Voleur")
                    Console.WriteLine("5 - Voler une arme");
                if(joueur.NomClasse() == "Guerrier")
                    Console.WriteLine("6 - Changer d'arme");
                Console.WriteLine("Q - Quitter");
            }
            
            Action(joueur);
            error = Message.IsEmpty();
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
            
            switch (Console.ReadLine())
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
                    Console.WriteLine("Indiquez le n° de joueur a dérober");
                    magicien.LancerSort((player[StringToInt()] as Personnage));
                    break;
                case "5":
                    Voleur voleur = (Voleur) joueur;
                    
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

        protected void IncrementeTour()
        {
            tourJoueur++;
            if (tourJoueur == nombreJoueurs) tourJoueur = 0;
        }

        protected int StringToInt()
        {
            string texte = Console.ReadLine();

            if (!Int32.TryParse(texte, out int nombre)) nombre = -1; // si le texte n'est pas un nombre

            return nombre-1;
        }
    }
}
