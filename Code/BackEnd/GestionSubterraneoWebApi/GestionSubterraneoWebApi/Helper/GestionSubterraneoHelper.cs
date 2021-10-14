using System;
using System.IO;
using System.Security.Cryptography;

namespace GestionSubterraneoWebApi.Helper
{
    public static class GestionSubterraneoHelper
    {
        /// <summary>
        /// Metodo publico para ambos controllers que tiene como responsabilidad cifrar el nombre usuario recibido por query string
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <returns></returns>
        public static string ObtenerClienteId(string nombreUsuario)
        {
            try
            {
                using (RijndaelManaged miRijndael = new RijndaelManaged())
                {
                    miRijndael.GenerateKey();
                    miRijndael.GenerateIV();
                    // Encrypt the string to an array of bytes.
                    byte[] encrypted = EncriptarSringABytes(nombreUsuario, miRijndael.Key, miRijndael.IV);

                    Console.WriteLine("Original:   {0}", nombreUsuario);
                    var nombreUsuarioEncriptado = Convert.ToBase64String(encrypted);
                    return nombreUsuarioEncriptado;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return "";

            }
        }
        /// <summary>
        /// Metodo que realiza la encriptacion del nombre de usuario pasado por parametro
        /// </summary>
        /// <param name="textoCifrar"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        private static byte[] EncriptarSringABytes(string textoCifrar, byte[] Key, byte[] IV)
        {

            if (textoCifrar == null || textoCifrar.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(textoCifrar);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        }
    }
}
