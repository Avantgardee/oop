using System.IO;
using System.Text;
using components;
using Cryptor;
namespace AdapterPlugin;

public class AdapterPlugin : IPluginFunctionality
{
    CryptField Adaptee = new CryptField();
    public string Name => Adaptee.Name;
    
    public List<MySpriteForEncrypt> PreprocessorEncryptData(List<MySprite> data)
    {
        Convertor convertor = new Convertor();
        List<MySpriteForEncrypt> list = new List<MySpriteForEncrypt>();
        foreach (var item in data)
        {
            MySpriteForEncrypt listItem = convertor.ConvertToMySpriteForEncrypt(item);
            list.Add(listItem);
        }
        return list;
    }

    public List<MySprite> PreprocessorDecryptData(List<MySpriteForEncrypt> encryptedData,
        Dictionary<object, ToolType> ToolArr)
    {
        Convertor convertor = new Convertor();
        List<MySprite> list = new List<MySprite>();
        foreach (var item in encryptedData)
        {
            MySprite listItem = convertor.ConvertToMySprite(item, ToolArr);
            list.Add(listItem);
        }
        return list;
    }

    public Stream PostprocessorEncryptData(Stream stream)
    {
    
        Adaptee.aes.IV = Encoding.UTF8.GetBytes("1234567890123456");
        Adaptee.aes.Key = Encoding.UTF8.GetBytes("12345678901234561234567890123456");
        return (Adaptee.PostprocessorSave(stream));

    }
    public Stream PostprocessorDecryptData(Stream stream)
    {
        Adaptee.aes.IV = Encoding.UTF8.GetBytes("1234567890123456");
        Adaptee.aes.Key = Encoding.UTF8.GetBytes("12345678901234561234567890123456");
        return (Adaptee.PostprocessorLoad(stream));
    }
}