﻿﻿using System;
using System.Collections;
  using System.Linq;

  namespace RPG
{
    class MenuAction
    {
        private const int consoleX = 60;
        private const int consoleY = 30;

        private bool run;

        private Ring ring = new Ring();
        private ElementGraphique elementGraphique = new ElementGraphique();
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

            run = true;
        }

        public void ListeActions()
        {
            while (run)
            {
                Personnage joueur = (player[tourJoueur] as Personnage);
                
                ring.DisplayGrid(tourJoueur + 1, ConsoleColor.DarkGray);
                joueur.AfficherEtat(elementGraphique);

                error = !(Message.IsEmpty());
                Message.AfficheInfo();
                
                AfficheChoix(joueur);
                Action(joueur);

                if (error == false)
                {
                    IncrementeTour();
                }
                
                Console.Clear();
            }
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
                    run = false;
                    break;
                default:
                    Message.Add("Cette option n'est pas dans le menu, veuillez refaire votre choix");
                    break;
            }
            
            error = !(Message.IsEmpty());
        }

        private string ConvertiChoix(string choix)
        {
            if (choix == "Q" || choix == "q")
                return choix;
            if (!choix.All(char.IsDigit))
                return null;
            
            int choixNombre = Convert.ToInt32(choix) - 1;
            string position;
            
            if (choixNombre >= 0 && choixNombre < this.choix.Count)
                position = this.choix[choixNombre].ToString();
            else
                position = null;
            
            return position;
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
