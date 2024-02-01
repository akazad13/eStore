namespace eStore.Application.Common.Utilities
{
    public class Helper : IHelper
    {
        public Helper() { }

        public string[] SplitString(string source)
        {
            char[] delimiterChars = [' ', ',', '.', ':', '\t'];
            return source.Split(delimiterChars);
        }
    }
}
