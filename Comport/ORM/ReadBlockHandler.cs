namespace Comport.ORM
{
    public delegate bool ReadBlockHandler(string address, int num, out short[] arrData);
}