using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comport.ORM
{
    public interface IConverter
    {
        NullValue NullValue { get; set; }

        Type ObjectType { get; }

        object DefaultValue { get; }

        object FromPLC(object plcValue);

        object FromRow(string rowValue);

        object ToRow(object objectValue);
    }
}
