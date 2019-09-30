namespace RPG.Etre
{
    public class ManagerEtre
    {
        private Map map;
        
        public ManagerEtre(Map map)
        {
            this.map = map;
        }

        public int AjouterElement()
        {
            return map.AjouterElement();
        }

        public bool Deplace(int id, int x, int y = 0)
        {
            return map.Deplace(id, x, y);
        }

        public int Distance(int id1, int id2)
        {
            return map.Distance(id1, id2);
        }
    }
}