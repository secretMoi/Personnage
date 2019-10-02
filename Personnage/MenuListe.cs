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
        
        public void AjouterItems(List<string> cles, List<string> phrases)
        {
            for (int i = 0; i < phrases.Count; i++)
            {
                item.Add(cles[i], phrases[i]);
            }
        }

        public void AfficherItems()
        {
            int offsetX = Console.CursorLeft; // enregistre le décalage gauche pour ne pas le perdre lors du retour à la ligne
            int compteur = 0;
            
            foreach (KeyValuePair<string, string> menu in item)
            {
                if (compteur < tailleMax)
                {
                    Console.CursorLeft = offsetX;
                    Console.WriteLine("{0} - {1}", menu.Key, menu.Value);
                }
                
                compteur++;
            }
            
            if(compteur > 5)
                Console.WriteLine("Suite");
        }
    }
}