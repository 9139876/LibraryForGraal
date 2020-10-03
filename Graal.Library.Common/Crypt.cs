using System;
using System.Security.Cryptography;
using System.Text;

namespace Graal.Library.Common
{
    public class Crypt
    {
        readonly string EntropyString;
        readonly Action<string> Debug;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="entropyString">Строка энтропии - что-нибудь неизменное для сущности данного пароля</param>
        public Crypt(string entropyString, Action<string>debug)
        {
            Debug = debug;
            EntropyString = entropyString;
        }

        private byte[] GetEntropy()
        {
            try
            {
                using (var sha = SHA512.Create())
                    return sha.ComputeHash(Encoding.UTF8.GetBytes(EntropyString));
            }
            catch (Exception ex)
            {
                Debug?.Invoke(ex.Message);
                return new byte[0];
            }
        }

        /// <summary>
        /// Возвращает зашифрованную строку
        /// </summary>
        /// <param name="password">Шифруемая строка</param>
        /// <returns></returns>
        public string Encrypt(string password)
        {
            try
            {
                return Convert.ToBase64String(ProtectedData.Protect(Encoding.UTF8.GetBytes(password), GetEntropy(), DataProtectionScope.CurrentUser));
            }
            catch (Exception ex)
            {
                Debug?.Invoke(ex.Message);
                return "";
            }
        }

        /// <summary>
        /// Возвращает расшифрованную строку
        /// </summary>
        /// <param name="password">Зашифрованная строка</param>
        /// <returns></returns>
        public string Decrypt(string encrypted)
        {
            try
            {
                return Encoding.UTF8.GetString(ProtectedData.Unprotect(Convert.FromBase64String(encrypted), GetEntropy(), DataProtectionScope.CurrentUser));
            }
            catch (Exception ex)
            {
                Debug?.Invoke(ex.Message);
                return "";
            }
        }
    }
}
