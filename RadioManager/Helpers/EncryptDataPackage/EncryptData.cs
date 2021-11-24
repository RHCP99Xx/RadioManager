using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioManager.Helpers.EncryptDataPackage
{
    public static class EncryptData
    {
        public static string Encrypt(string data)
        {
            byte[] byt = System.Text.Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(byt);
        }

        public static string Decrypt(string data)
        {
            byte[] b = Convert.FromBase64String(data);
            return System.Text.Encoding.UTF8.GetString(b);
        }
    }
}
