using System.Security.Cryptography;
using System.Text;

namespace WpfDormitories.Model.Convertors
{
    class HashCodeConvertor : IConvertor<string, string>
    {
        public string Convert(string value)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(value);
            byte[] hashValue = SHA256.HashData(messageBytes);
            return System.Convert.ToHexString(hashValue);
        }
    }
}
