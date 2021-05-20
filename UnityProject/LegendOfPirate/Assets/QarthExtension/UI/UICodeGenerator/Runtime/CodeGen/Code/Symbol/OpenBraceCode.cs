
namespace Qarth.Extension
{
    // 花括号
    public class OpenBraceCode : ICode
    {
        public void Gen(ICodeWriter writer)
        {
            writer.WriteLine("{");
        }
    }
}