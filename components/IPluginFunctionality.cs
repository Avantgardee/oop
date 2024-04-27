
namespace components;

public interface IPluginFunctionality
{
    public string Name { get; }
    public List<MySpriteForEncrypt> EncryptData(List<MySprite> data);
    public List<MySprite> DecryptData(List<MySpriteForEncrypt> encryptedData, Dictionary<object, ToolType> ToolArr);
}