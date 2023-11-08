using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comport.ORM
{
    internal class Branch
    {
        public List<Crotch> Crotches { get; private set; }

        public Dictionary<string, object> RowData { get; private set; }

        public Branch()
        {
            Crotches = new List<Crotch>();
            RowData = new Dictionary<string, object>();
        }

        public void SetValue(string[] address, string column, object value)
        {
            foreach (Crotch crotch in Crotches)
            {
                if (crotch.GetRelation(address) == Crotch.Relation.不同分支)
                {
                    return;
                }
            }

            RowData[column] = value;
        }

        public static void GenerateBranch(List<Branch> branches, List<Crotch> crotches)
        {
            if (crotches == null || crotches.Count < 2)
            {
                return;
            }

            List<Branch> list = new List<Branch>();
            for (int i = 1; i < crotches.Count; i++)
            {
                Crotch item = crotches[i];
                foreach (Branch branch2 in branches)
                {
                    Branch branch = StaticResources.Clone(branch2);
                    branch.Crotches.Add(item);
                    list.Add(branch);
                }
            }

            branches.ForEach(delegate (Branch b)
            {
                b.Crotches.Add(crotches[0]);
            });
            branches.AddRange(list);
        }
    }
}
