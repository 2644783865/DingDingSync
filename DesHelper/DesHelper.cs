using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace DesHelper
{
    public class DesHelper
    {
        private static string _KEY = "xiaomeng";
        private static string _IV = "xiaomeng";
        private static DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider();

        public static string EncryptString(string str)
        {
            desProvider.Key = ASCIIEncoding.ASCII.GetBytes(_KEY);
            desProvider.IV = ASCIIEncoding.ASCII.GetBytes(_IV);

            byte[] inBlock = Encoding.UTF8.GetBytes(str);
            ICryptoTransform xfrm = desProvider.CreateEncryptor();
            byte[] outBlock = xfrm.TransformFinalBlock(inBlock, 0, inBlock.Length);
            return Convert.ToBase64String(outBlock);
        }

        public static string DecryptString(string str)
        {
            desProvider.Key = ASCIIEncoding.ASCII.GetBytes(_KEY);
            desProvider.IV = ASCIIEncoding.ASCII.GetBytes(_IV);

            byte[] outBlock;
            try
            {
                byte[] inBlock = Convert.FromBase64String(str);
                ICryptoTransform xfrm = desProvider.CreateDecryptor();
                outBlock = xfrm.TransformFinalBlock(inBlock, 0, inBlock.Length);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                return string.Empty;
            }
            return Encoding.UTF8.GetString(outBlock);
        }
    }
}