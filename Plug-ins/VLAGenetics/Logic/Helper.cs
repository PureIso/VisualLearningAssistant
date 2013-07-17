using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VLAGenetics.Logic
{
    public static class Helper
    {
        public static byte[] StringToBinayByte(string stringEncoding)
        {
            List<byte> binaryEncoded = new List<byte>();
            foreach( char bits in stringEncoding)
            {
                binaryEncoded.Add(bits == '1' ? (byte)1 : (byte)0);
            }
            return binaryEncoded.ToArray();
        }

        public static string BinaryByteToString(IEnumerable<byte> binaryEncoded)
        {
            string stringEncoding = String.Empty;
            foreach (byte bits in binaryEncoded)
                stringEncoding += bits;
                
            return stringEncoding;
        }
    }
}
