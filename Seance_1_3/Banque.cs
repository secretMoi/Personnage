using System.Collections;

namespace RPG
{
    public class Banque
    {
        private int solde;

        public Banque(int solde = 0)
        {
            this.solde = solde;
        }

        public int Solde
        {
            get => solde;
        }

        public void Depot(int montant)
        {
            solde += montant;
        }

        public void Retrait(int montant)
        {
            int total = solde - montant;
            if (total >= 0) solde = total;
        }
    }
}