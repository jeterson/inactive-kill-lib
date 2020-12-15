namespace Lib
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class AESCrypto
    {
        private static string default_iv = "aes256initvector";
        private static string default_key = "ch@veEnorme&32c@r4ct3r3s()654321";

        private static string ByteArrayToString(byte[] bytes) {
           return  BitConverter.ToString(bytes).Replace("-", "");
        }

        public static string Decrypt(string cipherText) {
            return Decrypt(cipherText, null, null);
        }

        public static string Decrypt(string cipherText, string key) {
         return   Decrypt(cipherText, key, null);
        }

        public static string Decrypt(string cipherText, string key, string iv)
        {
            string str;
            if ((cipherText != null) && (cipherText.Length > 0))
            {
                if ((key == null) || (key.Length <= 0))
                {
                    key = default_key;
                }
                if ((iv == null) || (iv.Length <= 0))
                {
                    iv = default_iv;
                }
                str = null;
                AesManaged objA = new AesManaged();
                try
                {
                    objA.Key = StringToASCII(key, 0x100);
                    objA.IV = StringToASCII(iv, 0x80);
                    ICryptoTransform transform = objA.CreateDecryptor(objA.Key, objA.IV);
                    MemoryStream stream = new MemoryStream(StringToByteArray(cipherText));
                    try
                    {
                        CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
                        try
                        {
                            StreamReader reader = new StreamReader(stream2);
                            try
                            {
                                str = reader.ReadToEnd();
                            }
                            finally
                            {
                                if (!ReferenceEquals(reader, null))
                                {
                                    reader.Dispose();
                                }
                            }
                        }
                        finally
                        {
                            if (!ReferenceEquals(stream2, null))
                            {
                                stream2.Dispose();
                            }
                        }
                    }
                    finally
                    {
                        if (!ReferenceEquals(stream, null))
                        {
                            stream.Dispose();
                        }
                    }
                }
                finally
                {
                    if (!ReferenceEquals(objA, null))
                    {
                        ((IDisposable) objA).Dispose();
                    }
                }
            }
            else
            {
                return null;
            }
            return str;
        }

        public static string Encrypt(string text) {
         return   Encrypt(text, null, null);
        }

        public static string Encrypt(string text, string key)
        {
            return Encrypt(text, key, null);
        }

        public static string Encrypt(string text, string key, string iv)
        {
            byte[] buffer;
            if ((text != null) && (text.Length > 0))
            {
                if ((key == null) || (key.Length <= 0))
                {
                    key = default_key;
                }
                if ((iv == null) || (iv.Length <= 0))
                {
                    iv = default_iv;
                }
                AesManaged objA = new AesManaged();
                try
                {
                    objA.Key = StringToASCII(key, 0x100);
                    objA.IV = StringToASCII(iv, 0x80);
                    ICryptoTransform transform = objA.CreateEncryptor(objA.Key, objA.IV);
                    MemoryStream stream = new MemoryStream();
                    try
                    {
                        CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
                        try
                        {
                            StreamWriter writer = new StreamWriter(stream2);
                            try
                            {
                                writer.Write(text);
                            }
                            finally
                            {
                                if (!ReferenceEquals(writer, null))
                                {
                                    writer.Dispose();
                                }
                            }
                            buffer = stream.ToArray();
                        }
                        finally
                        {
                            if (!ReferenceEquals(stream2, null))
                            {
                                stream2.Dispose();
                            }
                        }
                    }
                    finally
                    {
                        if (!ReferenceEquals(stream, null))
                        {
                            stream.Dispose();
                        }
                    }
                }
                finally
                {
                    if (!ReferenceEquals(objA, null))
                    {
                        ((IDisposable) objA).Dispose();
                    }
                }
            }
            else
            {
                return null;
            }
            return ByteArrayToString(buffer);
        }

        private static byte[] StringToASCII(string txt, int bits)
        {
            byte[] buffer = new byte[bits / 8];
            if (ReferenceEquals(txt, null))
            {
                txt = "";
            }
            byte[] bytes = Encoding.ASCII.GetBytes(txt);
            for (int i = 0; (i < bytes.Length) && (i < buffer.Length); i++)
            {
                buffer[i] = bytes[i];
            }
            return buffer;
        }

        private static byte[] StringToByteArray(string hex)
        {
            int length = hex.Length;
            byte[] buffer = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
            {
                buffer[i / 2] = Convert.ToByte(hex.Substring(i, 2), 0x10);
            }
            return buffer;
        }
    }
}

