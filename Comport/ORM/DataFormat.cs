using System.Text;

namespace Comport.ORM
{
    public class DataFormat
    {
        public DataOrder Order { get; set; }

        public bool ReverseString { get; set; }

        public bool EndWithZeroChar { get; set; }

        public Encoding Encoding { get; set; }

        public DataFormat()
        {
            Order = DataOrder.CDAB;
            ReverseString = false;
            EndWithZeroChar = true;
            Encoding = Encoding.ASCII;
        }
    }
}