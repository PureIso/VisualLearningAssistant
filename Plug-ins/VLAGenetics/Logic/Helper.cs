using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VLAGenetics.Logic
{
    public static class Helper
    {
        public static byte[] StringToBinayByte(string stringEncoding)
        {
            List<byte> binaryEncoded = new List<byte>();
            foreach (Char bits in stringEncoding)
                binaryEncoded.Add(bits == '1' ? (byte)1 : (byte)0);

            return binaryEncoded.ToArray();
        }

        public static string BinaryByteToString(IEnumerable<byte> binaryEncoded)
        {
            string stringEncoding = string.Empty;
            foreach (byte bits in binaryEncoded)
                stringEncoding += bits;
            return stringEncoding;
        }

    }
}
