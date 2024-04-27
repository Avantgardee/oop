using System.IO;

namespace components;

public interface IPluginFunctionality
{
    public string Name { get; }
    public List<MySpriteForEncrypt> PreprocessorEncryptData(List<MySprite> data);
    public List<MySprite> PreprocessorDecryptData(List<MySpriteForEncrypt> encryptedData, Dictionary<object, ToolType> ToolArr);
    public Stream PostprocessorEncryptData(Stream stream);
    public Stream PostprocessorDecryptData(Stream stream);
}