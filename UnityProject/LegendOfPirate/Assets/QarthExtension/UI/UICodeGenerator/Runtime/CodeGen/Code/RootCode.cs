
using System.Collections.Generic;

namespace Qarth.Extension
{
    public class RootCode : ICodeScope
    {
        private List<ICode> mCodes = new List<ICode>();

        public List<ICode> Codes
        {
            get { return mCodes; }
            set { mCodes = value; }
        }


        public void Gen(ICodeWriter writer)
        {
            foreach (var code in Codes)
            {
                code.Gen(writer);
            }
        }
    }
}