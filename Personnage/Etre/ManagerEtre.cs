using System;
using System.Collections.Generic;
using RPG.Monstres;

namespace RPG.Etre
{
    public class ManagerEtre
    {
        private static Map map;
        private static List<Monstre> monstres = new List<Monstre>();
        
        public ManagerEtre(Map map)
        {
            ManagerEtre.map = map;
        }

        public string AjouterElement()
        {
            //return map.AjouterElement("J");
            return "J";
        }

        public bool Deplace(string id, int x, int y = 0)
        {
            return map.Deplace(id, x, y);
        }

        public int Distance(string id1, string id2)
        {
            return map.Distance(id1, id2);
        }

        public void GenereMonstres()
        {
            List<List<int>> liste = new List<List<int>>(); // liste des monstres contenant la liste de leur coord
            liste = map.ListeMonstres();
            
            for (int i = 0; i < liste.Count; i++) // parcours la liste de monstres
            {
                monstres.Add(new Monstre()); // crée un monstre pour chaque élément
                monstres[i].Id = map.LieElement(liste[i]); // lui associe un id
            }
        }

        public void DeplaceMonstres()
        {
            for (int i = 0; i < monstres.Count; i++) // parcours la liste de monstres
            {
                monstres[i].SeDeplaceAuto();
            }
        }
    }
}