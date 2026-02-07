namespace FrontEnd.Components.Classes
{
    public class Euklides
    {
        public int Eukl(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                {
                    var buff = a % b;
                    a = buff;
                }
                else
                {
                    var buff = b % a;
                    b = buff;
                }
            }

            if (a == 0)
            {
                return b;
            }
            else
            {
                return a;
            }
        }
        public int NWW(int a, int b)
        {
            var nwd = Eukl(a, b);
            return (a * b) / nwd;
        }

    }
}
