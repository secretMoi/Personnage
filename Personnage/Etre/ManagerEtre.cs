namespace RPG.Etre
{
    public class ManagerEtre
    {
        private Map map;
        
        public ManagerEtre(Map map)
        {
            this.map = map;
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
    }
}