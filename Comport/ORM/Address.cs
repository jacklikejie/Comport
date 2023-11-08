namespace Comport.ORM
{
    public struct Address
    {
        public string area { get; private set; }

        public int address { get; private set; }

        public int point { get; private set; }

        public string RawString { get; private set; }

        public bool IsValid { get; private set; }

        public string AddressWithoutPoint => (!IsValid) ? "" : $"{area}{address}";

        public bool IgnoreArea { get; private set; }

        public Address(string address, bool ignoreArea = false)
        {
            this = default(Address);
            RawString = address;
            IgnoreArea = ignoreArea;
            if (address == null)
            {
                return;
            }

            if (!ignoreArea && address.Length > 0)
            {
                area = address[0].ToString();
            }

            int num = ((!ignoreArea) ? 1 : 0);
            if (address.Length > num)
            {
                string[] array = address.Substring(num).Split('.');
                int.TryParse(array[0], out var result);
                this.address = result;
                if (array.Length == 2)
                {
                    point = StringToInt(array[1]);
                }
            }

            IsValid = (ignoreArea || (!string.IsNullOrWhiteSpace(area) && ((area[0] >= 'a' && area[0] <= 'z') || (area[0] >= 'A' && area[0] <= 'Z')))) && this.address > 0 && point >= 0;
            if (!IsValid)
            {
                this = new Address
                {
                    RawString = address
                };
            }
        }

        private static int StringToInt(string p)
        {
            p = p.ToLower();
            if (p.Length != 1)
            {
                return -1;
            }

            if (p[0] >= '0' && p[0] <= '9')
            {
                return p[0] - 48;
            }

            if (p[0] >= 'a' && p[0] <= 'f')
            {
                return p[0] - 97 + 10;
            }

            return -1;
        }
    }
}