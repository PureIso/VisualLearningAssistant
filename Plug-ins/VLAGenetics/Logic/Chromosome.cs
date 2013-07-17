
using System;
using System.Threading;

namespace VLAGenetics.Logic
{
    /// <summary>
    /// This is the set of properties which can be mutated and altered.
    /// </summary>
    public class Chromosome : IComparable<Chromosome>
    {
        #region variables
        private readonly string _stringBinaryEncoding;
        private readonly byte[] _bitBinaryEncoding;
        #endregion

        #region properties
        public int Fitness { get; set; }
        public byte[] ByteBinaryEncoding
        {
            get { return _bitBinaryEncoding; }
        }
        public string StringBinaryEncoding
        {
            get { return _stringBinaryEncoding; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public Chromosome()
        {
            //Randomize the chromosomes
            _bitBinaryEncoding = new byte[10];
            Random randomValues = new Random(Convert.ToInt32(DateTime.Now.Ticks %Int16.MaxValue));

            //Modulus operation to produce 1 or 0
            for (int i = 0; i < _bitBinaryEncoding.Length; i++)
            {
                _bitBinaryEncoding[i] = (byte)(randomValues.Next() % 2);
            }
            _stringBinaryEncoding = Helper.BinaryByteToString(_bitBinaryEncoding);
            Thread.Sleep(1);
        }

        public Chromosome(string byteString)
        {
            //Randomize the chromosomes
            _bitBinaryEncoding = Helper.StringToBinayByte(byteString);
            _stringBinaryEncoding = byteString;
            Thread.Sleep(1);
        }

        public Chromosome(byte[] bytes)
        {
            //Randomize the chromosomes
            _stringBinaryEncoding = Helper.BinaryByteToString(bytes);
            _bitBinaryEncoding = bytes;
            Thread.Sleep(1);
        }
        #endregion

        #region Method
        public int CompareTo(Chromosome other)
        {
            return Fitness.CompareTo(other.Fitness);
        }
        #endregion
    }
}
