﻿using System.Collections;

namespace RPG
{
    public class Ressource
    {
        private int bois;
        private int fer;
        private int nourriture;
        private int boisson;
        private int chargeMax;
        private ArrayList items;

        public Ressource(int bois = 0, int fer = 0, int nourriture = 0, int boisson = 0)
        {
            this.bois = bois;
            this.fer = fer;
            this.nourriture = nourriture;
            this.boisson = boisson;
            
            items = new ArrayList();
            
            ChargeMax = 10;
        }

        public int ChargeMax
        {
            get => chargeMax;
            set => chargeMax = value;
        }
    }
}