
namespace Qarth.Extension
{
    public class EmptyLineCode : ICode
    {
        public void Gen(ICodeWriter writer)
        {
            writer.WriteLine();
        }
    }

    public static partial class ICodeScopeExtensions
    {
        public static ICodeScope EmptyLine(this ICodeScope self)
        {
            self.Codes.Add(new EmptyLineCode());
            return self;
        }
    }
}