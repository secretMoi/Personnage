﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Seance_3_1
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

            player.Add(new Personnage(this.ring));
            player.Add(new Personnage(this.ring, 0, 1));

            this.nombreJoueurs = 2;
            this.error = false;
            this.tourJoueur = 0;
        }

        public void ListeActions()
        {
            Personnage joueur = (player[tourJoueur] as Personnage);

            if (this.error == false)
            {
                this.ring.DisplayGrid();
                joueur.AfficherEtat();
                Console.WriteLine("Actions possibles : ");

                Console.WriteLine("1 - Se déplacer d'une case");
                Console.WriteLine("2 - Attaquer à max " + joueur.Arme.GetPortee() + " cases");
                Console.WriteLine("3 - Boire potion de vie contre mana");
                Console.WriteLine("Q - Quitter");
            }
            
            Action(joueur);
            error = Message.IsEmpty();
            Message.AfficheInfo();

            if (this.error == false)
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
                    joueur.ActonDeplacement();
                    break;
                case "2":
                    Console.WriteLine("Indiquez le n° de joueur a attaquer");
                    joueur.ActionAttaque((player[StringToInt()] as Personnage));
                    break;
                case "3":
                    joueur.BoirePotion();
                    break;
                case "Q":
                    System.Environment.Exit(1);
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

            if (!Int32.TryParse(texte, out int nombre)) nombre = -1;

            return nombre-1;
        }
    }
}
