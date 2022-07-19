using System;
using System.Text;
using System.Security.Cryptography;

namespace HashRandom
{
    /// <summary>
    ///  Creates a cryptographically secure random number generator using the MD5 algorithm
    /// </summary>
    public class MD5Random
    {
        /// <summary>
        ///  The seed used for generating numbers
        /// </summary>
        public string Seed { get; }
        private byte[] lastState { get; set; } = new byte[0];

        /// <summary>
        ///  Creates a cryptographically secure random number generator using the MD5 algorithm with a double seed
        /// </summary>
        public MD5Random(double seed)
        {
            Seed = seed.ToString();
        }

        /// <summary>
        ///  Creates a cryptographically secure random number generator using the MD5 algorithm with an int seed
        /// </summary>
        public MD5Random(int seed)
        {
            Seed = seed.ToString();
        }

        /// <summary>
        ///  Creates a cryptographically secure random number generator using the MD5 algorithm with a long seed
        /// </summary>
        public MD5Random(long seed)
        {
            Seed = seed.ToString();
        }

        /// <summary>
        ///  Creates a cryptographically secure random number generator using the MD5 algorithm with a string seed
        /// </summary>
        public MD5Random(string seed)
        {
            Seed = seed;
        }

        /// <summary>
        ///  Creates a cryptographically secure random number generator using the MD5 algorithm
        /// </summary>
        public MD5Random()
        {
            Seed = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds.ToString();
        }





        /// <summary>
        ///  Generates a random double that is greater than or equal to 0.0, and less than 1.0
        /// </summary>
        public double NextDouble()
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = StaticFunctions.Combine(Encoding.ASCII.GetBytes(Seed), lastState);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                lastState = hashBytes;

                return (double)Math.Abs(BitConverter.ToInt64(hashBytes, 0)) / long.MaxValue % 1;
            }
        }

        /// <summary>
        ///  Generates a random double that is less than the exclusive number
        /// </summary>
        public double NextDouble(double exclusive)
        {
            return NextDouble() * exclusive;
        }

        /// <summary>
        ///  Generates a random double that is greater than or equal to the minimum number and less than the exclusive number
        /// </summary>
        public double NextDouble(double minimum, double exclusive)
        {
            return NextDouble(exclusive - minimum) + minimum;
        }





        /// <summary>
        ///  Generates a random long
        /// </summary>
        public long NextLong()
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = StaticFunctions.Combine(Encoding.ASCII.GetBytes(Seed), lastState);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                lastState = hashBytes;

                return BitConverter.ToInt64(hashBytes, 0);
            }
        }

        /// <summary>
        ///  Generates a random long that is less than the exclusive number
        /// </summary>
        public long NextLong(long exclusive)
        {
            return (long)(NextDouble() * exclusive);
        }

        /// <summary>
        ///  Generates a random long that is greater than or equal to the minimum number and less than the exclusive number
        /// </summary>
        public long NextLong(long minimum, long exclusive)
        {
            return NextLong(exclusive - minimum) + minimum;
        }





        /// <summary>
        ///  Generates a random int
        /// </summary>
        public int NextInt()
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = StaticFunctions.Combine(Encoding.ASCII.GetBytes(Seed), lastState);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                lastState = hashBytes;

                return BitConverter.ToInt32(hashBytes, 0);
            }
        }

        /// <summary>
        ///  Generates a random int that is less than the exclusive number
        /// </summary>
        public int NextInt(int exclusive)
        {
            return (int)(NextDouble() * exclusive);
        }

        /// <summary>
        ///  Generates a random int that is greater than or equal to the minimum number and less than the exclusive number
        /// </summary>
        public int NextInt(int minimum, int exclusive)
        {
            return NextInt(exclusive - minimum) + minimum;
        }
    }
}