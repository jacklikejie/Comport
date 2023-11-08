using System.Collections.Generic;

namespace Comport.ORM
    {
    internal interface IMultiRow
    {
        List<Crotch> GetBranches(object objectValue, string address);
    }
}