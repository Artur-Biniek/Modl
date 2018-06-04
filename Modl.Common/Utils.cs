using System;
using System.Linq;

namespace Modl.Common {
    public static class Utils {
        public static byte[] GetBytes (int i) {
            var bytes = BitConverter.GetBytes (i);
            if (BitConverter.IsLittleEndian)
                return bytes;
            else
                return bytes.Reverse ().ToArray ();
        }
    }
}