using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Security.Cryptography;
using CodeStage.AntiCheat.ObscuredTypes;

public class Encrypt
{
    public class Key
    {
        public ObscuredString Get()
        {
            return "el2iek3kd02lxxdj2kxoe043ls0d02ks";
        }
    }

    public class Key2
    {
        public ObscuredString Get()
        {
            return "el2i1kxkd02lsxdj2kdde043ls9d02kf";
        }
    }

    public static String AESEncrypt256_Key2(String Input)
    {
        return AESEncrypt256(Input, new Key2().Get());
    }

    public static String AESDecrypt256_Key2(String Input)
    {
        return AESDecrypt256(Input, new Key2().Get());
    }

    public static String AESEncrypt256(String Input)
    {
        return AESEncrypt256(Input, new Key().Get());
    }

    public static String AESDecrypt256(String Input)
    {
        return AESDecrypt256(Input, new Key().Get());
    }

    public static String AESEncrypt256(String Input, String key)
    {
        RijndaelManaged aes = new RijndaelManaged();
        aes.KeySize = 256;
        aes.BlockSize = 128;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
        byte[] xBuff = null;
        using (var ms = new MemoryStream())
        {
            using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
            {
                byte[] xXml = Encoding.UTF8.GetBytes(Input);
                cs.Write(xXml, 0, xXml.Length);
            }

            xBuff = ms.ToArray();
        }

        String Output = Convert.ToBase64String(xBuff);
        return Output;
    }


    //AES_256 복호화
    public static String AESDecrypt256(String Input, String key)
    {
        RijndaelManaged aes = new RijndaelManaged();
        aes.KeySize = 256;
        aes.BlockSize = 128;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        var decrypt = aes.CreateDecryptor();
        byte[] xBuff = null;
        using (var ms = new MemoryStream())
        {
            using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
            {
                byte[] xXml = Convert.FromBase64String(Input);
                cs.Write(xXml, 0, xXml.Length);
            }

            xBuff = ms.ToArray();
        }

        String Output = Encoding.UTF8.GetString(xBuff);
        return Output;
    }

}
