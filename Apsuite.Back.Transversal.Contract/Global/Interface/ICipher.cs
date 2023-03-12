using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsuite.Back.Transversal.Contract.Global.Interface
{
    public interface ICipher
    {
        string Encrypt(string plainText);
        string EncryptPass(string plainText);
        string Decrypt(string encryptedText);
    }
}
