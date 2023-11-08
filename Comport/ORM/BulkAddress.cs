namespace Comport.ORM
{
    internal class BulkAddress
    {
        public Address Start { get; set; }

        public int Length { get; set; }

        public DataPoint[] Points { get; set; }

        public BulkAddress()
        {
        }

        public BulkAddress(Address start, int length, DataPoint[] points)
        {
            Start = start;
            Length = length;
            Points = points;
        }
    }
}