using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace Cryptor
{
    
    public interface ISerializerOption
    {
        public object PreprocessorSave(object list);
        public Stream PostprocessorSave(Stream stream);
        public object PreprocessorLoad(object list);
        public Stream PostprocessorLoad(Stream stream);
    }
    
    
    public partial class CryptField : ISerializerOption
    {
        public string Name { get; protected set; } = "Cryption";
        public Aes aes = Aes.Create();

        public object PreprocessorSave(object list) { return list; }
        public object PreprocessorLoad(object list) { return list; }
        public Stream PostprocessorLoad(Stream stream)
        {
            CryptoStream cryptoStream = new(
                stream,
                aes.CreateDecryptor(),
                CryptoStreamMode.Read);
            return cryptoStream;
            return stream;

        }
        public Stream PostprocessorSave(Stream stream)
        {
          
            CryptoStream cryptoStream = new(
                stream,
                aes.CreateEncryptor(),
                CryptoStreamMode.Read);
            return cryptoStream;
            
            return stream;
        }
    }
}