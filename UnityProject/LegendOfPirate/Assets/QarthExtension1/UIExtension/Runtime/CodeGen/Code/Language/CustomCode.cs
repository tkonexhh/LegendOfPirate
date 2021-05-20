
namespace Qarth.Extension
{
    public class CustomCode : ICode
    {
        private string mLine;

        public CustomCode(string line)
        {
            mLine = line;
        }
        
        public void Gen(ICodeWriter writer)
        {
            writer.WriteLine(mLine);
        }
    }
    
    public static partial class ICodeScopeExtensions
    {
        public static ICodeScope Custom(this ICodeScope self, string line)
        {
            self.Codes.Add(new CustomCode(line));
            return self;
        }
    }
    
    
}