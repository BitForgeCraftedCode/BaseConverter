using System.Numerics;
using System.Text;


namespace BaseConverter
{
    internal class Program
    {
        //positional numeral systems conversion class
        static void Main(string[] args)
        {
            string base62 = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string binary = "01";
            string hex = "0123456789ABCDEF";

            var num = Decode("1Z", base62);
            Console.WriteLine(num); // 123

            // base10 → base62
            var encoded = Encode(123, base62);
            Console.WriteLine(encoded); // 1Z

            // base62 → binary
            var bin = Convert("1Z", base62, binary);
            Console.WriteLine(bin); // 1111011

            var num2 = Decode(bin, binary);
            Console.WriteLine(num2); // 123

            Console.ReadKey();
        }

        //1) Decode: baseX -> Integer (baseX to base10 decoder)
        public static int Decode(string input, string alphabet)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Input is empty");

            int numberBase = alphabet.Length;

            // Build lookup table
            var map = new Dictionary<char, int>();
            for (int i = 0; i < alphabet.Length; i++)
                map[alphabet[i]] = i;

            int value = 0;
            double exponent = 0;
            //Console.WriteLine($"Base Number {numberBase}");
            for (int i = input.Length - 1; i >= 0; i--)
            {
                //Console.WriteLine($"Exponent {exponent}");
                //Console.WriteLine($"String at i {input[i]}");
                //Console.WriteLine($"Base10 of String at position i {map[input[i]]}");
                //Console.WriteLine($"Base number raised to exponent {Math.Pow((double)numberBase, (double)exponent)}");
                //Console.WriteLine($"--------------------------");
                value = value + map[input[i]]*(int)Math.Pow((double)numberBase,(double)exponent);
                exponent = exponent + 1;
            }
            
            return value;
        }

        // 2) Encode: Integer -> baseY (base10 to baseY encoder)
        public static string Encode(int value, string alphabet)
        {
            if (value < 0)
                throw new ArgumentException("Negative values not supported");

            int numberBase = alphabet.Length;

            if (value == 0)
                return alphabet[0].ToString();

            var sb = new StringBuilder();
            /* remainder method https://condor.depaul.edu/psisul/conversionmath.html
             * taking base10 -> base2 conversion as an example. Do a few calcualtions by hand and think about this sentance
             * "Because we divide by 2 repeatedly, each division step moves us up one power of 2, so the last remainder must correspond to the highest power of 2."
             * 
             * 
             * 2 % 4 equals 2, because 4 goes into 2 zero times, with a remainder of 2. 
             * 
             * value = 123
             * remainder = 61 (123 % 62 = 61)
             * Base62Chars[61] = Z
             * value = 123/62 = 1  (integer division truncated)
             * 
             * remainder = 1 (1 % 62 = 1)
             * Base62Chars[1] = 1
             * 
             * value = 0  -- end while
             * 
             * 123 = 1Z
             */
            while (value > 0)
            {
                int remainder = (int)(value % numberBase);
                sb.Insert(0, alphabet[remainder]);
                value = value / numberBase;
            }

            return sb.ToString();

        }

        // 3) Convert: baseX → baseY
        public static string Convert(string input, string sourceAlphabet, string targetAlphabet)
        {
            var value = Decode(input, sourceAlphabet);
            return Encode(value, targetAlphabet);
        }
    }
}
