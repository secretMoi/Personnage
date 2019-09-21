using System;
using System.Collections;

namespace RPG
{
    public class Guerrier : Personnage
    {
        protected new ArrayList arme = new ArrayList();
        protected new const int vieMax = 150; // new  car on override Personnage
        protected new const int manaMax = 60;
        
        public Guerrier(Ring ringObject, int arme = Arme.MAINS) : base(ringObject, arme)
        {
        }

        public Guerrier(Ring ringObject, int x, int y = 0, int arme = Arme.MAINS) : base(ringObject, x, y, arme)
        {
        }
        
        protected override void InitConstructeur(int arme, int x = 0, int y = 0)
        {
            this.arme.Add(new Arme(arme));
            vie = vieMax;
            mana = manaMax;
            etat = true;

            nombreJoueurs++;

            id = ring.AjouterElement(nombreJoueurs, x, y);
            nom = "Player" + id;
            
            dateConstruction = DateTime.Now.ToString("dd/MM/yyyy");
        }
        
        public void AjouterArme()
        {
            string choix;
            int nombre;
            
            Arme.AfficherListe();

            choix = Console.ReadLine();
            if(!Int32.TryParse(choix, out nombre)) Message.Add("Arme non disponible");
            else
            {
                if (Arme.EstValide(nombre)) arme.Add(new Arme(nombre));
                else Message.Add("Arme non valide");
            }
        }
        
        public override string NomClasse()
        {
            return "Guerrier";
        }

        public override Arme Arme => arme[arme.Count - 1] as Arme;
    }
}