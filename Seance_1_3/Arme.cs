﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Seance_3_1
{
    class Arme
    {
        private int typeArme;
        private int degats;
        private int portee;
        private String nom;

        public const int BATON = 0;
        public const int MAINS = 1;

        public Arme(int typeArme)
        {
            this.typeArme = typeArme;
            AssocieDegats();
        }

        public Arme()
        {
            this.typeArme = BATON;
            AssocieDegats();
        }

        protected void AssocieDegats()
        {
            switch (typeArme)
            {
                case BATON:
                    this.degats = 15;
                    this.portee = 1;
                    this.nom = "Bâton";
                    break;
                default:
                    this.degats = 5;
                    this.portee = 1;
                    this.nom = "Mains nues";
                    break;
            }
        }
        
        public int GetPortee()
        {
            return this.portee;
        }

        public string GetNom()
        {
            return this.nom;
        }

        public int GetDegats()
        {
            return this.degats;
        }
    }
}
