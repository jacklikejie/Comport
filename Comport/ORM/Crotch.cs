namespace Comport.ORM
{
    internal class Crotch
    {
        public enum Relation
        {
            无关,
            不同分支,
            相同分支
        }

        public string[] address { get; set; }

        public Crotch()
        {
        }

        public Crotch(string address)
        {
            this.address = DataItem.AnalyzeAddress(address);
        }

        public Relation GetRelation(string[] addr)
        {
            for (int i = 0; i < addr.Length && i < address.Length; i++)
            {
                if (addr[i] != address[i] && !(address[i] == "*") && !(addr[i] == "*"))
                {
                    if (i < address.Length - 1)
                    {
                        return Relation.无关;
                    }

                    return Relation.不同分支;
                }
            }

            return Relation.相同分支;
        }
    }
}