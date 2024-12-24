using System.Security.Cryptography;
using System.Text;

namespace WpfDormitories.Model.Convertors
{
    /// <summary>
    /// Конвертор в хэш.
    /// </summary>
    public class HashCodeConvertor : IConvertor<string, string>
    {
        /// <summary>
        /// Конвертировать.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Convert(string value)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(value);
            byte[] hashValue = SHA256.HashData(messageBytes);
            return System.Convert.ToHexString(hashValue);
        }
    }
}
