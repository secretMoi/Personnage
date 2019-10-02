﻿﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RPG.Etre;
using RPG.Maps;
using RPG.Etre.Personnage;
  
namespace RPG
{
    class MenuAction
    {
        private ManagerEtre managerEtre;
        
        private Map map;
        
        private ElementGraphique elementGraphique = new ElementGraphique();
        private ArrayList player = new ArrayList();
        
        private MenuListe menuListe = new MenuListe();
        List<string> cles = new List<string>();
        
        private int nombreJoueurs = 2;

        private bool error;
        private bool run;

        private ArrayList choix = new ArrayList();
        
        private int compteur;

        private const int tailleX = 230; private const int tailleY = 60;
        private const int offsetMapX = 0; private const int offsetMapY = 7;
        private const int offsetActionX = tailleX - 25; private const int offsetActionY = tailleY - 10;
        private const int offsetCompetencesX = tailleX / 2 - 20; private const int offsetCompetencesY = 0;
        private const int offsetJoueurX = tailleX - 30; private const int offsetJoueurY = 0;

        public MenuAction()
        {
            Console.SetWindowSize(tailleX, tailleY);
            Console.SetBufferSize(tailleX, tailleY);
            Console.SetWindowPosition(0, 0);
            
            map = new Map1();
            map.SetOffsets(offsetMapX, offsetMapY);
            managerEtre = new ManagerEtre(map);
            player.Add(new Magicien(managerEtre));

            nombreJoueurs = 2;
            error = false;

            run = true;
        }

        public void ListeActions()
        {
            AfficheCompetences();
            AffichePerso();
            AfficheMap();
            
            while (run)
            {
                Action();
            }
        }

        private void AffichePerso()
        {
            Console.SetCursorPosition(offsetJoueurX, offsetJoueurY);
            (player[0] as Personnage).AfficherEtat(elementGraphique);
        }

        private void AfficheCompetences()
        {
            Console.SetCursorPosition(offsetCompetencesX, offsetCompetencesY);
            
            AjouteClesLettres("a", "b", "c", "d", "e");
            menuListe.AjouterItems(cles, (player[0] as Personnage).ListeCompetences());
            
            menuListe.AfficherItems();
        }
        
        private void AjouteClesLettres(params string[] cle)
        {
            foreach (string element in cle)
            {
                cles.Add(element);
            }
        }

        private void AfficheMap()
        {
            Console.SetCursorPosition(offsetMapX, offsetMapY);
            map.AfficheMap();
        }

        private void Action()
        {
            Console.SetCursorPosition(offsetActionX, offsetActionY);
            
            Console.Write("Choix action : ");
            
            ConsoleKeyInfo _Key = Console.ReadKey();
            Personnage joueur = (player[0] as Personnage);
            switch (_Key.Key)
            {
                case ConsoleKey.RightArrow:
                    map.Deplace(joueur.Id, Map.RIGHT);
                    break;
                case ConsoleKey.LeftArrow:
                    map.Deplace(joueur.Id, Map.LEFT);
                    break;
                case ConsoleKey.UpArrow:
                    map.Deplace(joueur.Id, 0, Map.UP);
                    break;
                case ConsoleKey.DownArrow:
                    map.Deplace(joueur.Id, 0, Map.DOWN);
                    break;
                case ConsoleKey.Q:
                    run = false;
                    break;
                default:
                    break;
            }
        }

        /*public void ListeActions()
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
                    //joueur.ActionDeplacement();
                    break;
                case "2":
                    Console.WriteLine("Indiquez le n° de joueur à attaquer");
                    //joueur.ActionAttaque((player[StringToInt()] as Personnage));
                    break;
                case "3":
                    joueur.BoirePotion();
                    break;
                case "4":
                    Magicien magicien = (Magicien) joueur;
                    Console.WriteLine("Indiquez le n° de joueur à ensorceler");
                    magicien.SelectionnerSort((player[StringToInt()] as Personnage));
                    break;
                case "5":
                    Voleur voleur = (Voleur) joueur;
                    Console.WriteLine("Indiquez le n° de joueur à dérober");
                    voleur.VolerArme((player[StringToInt()] as Personnage));
                    break;
                case "6":
                {
                    Guerrier guerrier = (Guerrier) joueur;
                    Console.WriteLine("Indiquez le n° de l'arme voulue");
                    guerrier.AjouterArme();
                    break;
                }
                case "7":
                {
                    Guerrier guerrier = (Guerrier) joueur;
                    guerrier.JeterArme();
                    break;
                }
                    
                case "Q":
                case "q":
                    run = false;
                    break;
                default:
                    Message.Add("Cette option n'est pas dans le menu, veuillez refaire votre choix");
                    break;
            }
            
            error = !(Message.IsEmpty()); // vérifie qu'il n'y a pas eu d'erreur sur cette fonction
        }*/

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
            
            if(joueur.NomClasse() == Personnage.MAGICIEN)
                AfficheMessageCompteur("Lancer un sort", "4");
            if(joueur.NomClasse() == Personnage.VOLEUR)
                AfficheMessageCompteur("Voler une arme", "5");
            if (joueur.NomClasse() == Personnage.GUERRIER)
            {
                AfficheMessageCompteur("Ajouter une arme", "6");
                AfficheMessageCompteur("Jeter une arme", "7");
            }
            
            Console.WriteLine("Q - Quitter");
        }

        private void AfficheMessageCompteur(string message, string choix)
        {
            Console.WriteLine("{0} - {1}", compteur, message);
            this.choix.Add(choix);
            
            compteur++;
        }

        private int StringToInt()
        {
            string texte = Console.ReadLine();

            if (!Int32.TryParse(texte, out int nombre)) nombre = -1; // si le texte n'est pas un nombre

            return nombre-1;
        }
    }
}
