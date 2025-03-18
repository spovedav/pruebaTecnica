using System;
using System.Collections.Generic;
using System.Text;

namespace NUGET.TOOL.CORE._3_1.AES
{
    public interface IAesEncryption
    {
        void GenerateKeyAndIV(out byte[] key, out byte[] iv);
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
    }
}
