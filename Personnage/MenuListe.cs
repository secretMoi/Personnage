using System;
using System.Collections.Generic;

namespace RPG
{
    public class MenuListe
    {
        private Dictionary<string, string> item;
        private int tailleMax = 5;
        
        public MenuListe()
        {
            item = new Dictionary<string, string>();
        }

        public void AjouterItem(string cle, string phrase)
        {
            item.Add(cle, phrase);
        }

        public void AfficherItems()
        {
            int compteur = 0;
            
            foreach (KeyValuePair<string, string> menu in item)
            {
                if (compteur < tailleMax)
                {
                    Console.WriteLine("{0} - {1}", menu.Key, menu.Value);
                }
                
                compteur++;
            }
            
            if(compteur >= 5)
                Console.WriteLine("Suite");
        }
    }
}