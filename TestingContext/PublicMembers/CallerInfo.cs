namespace TestingContextCore.PublicMembers
{
    public class CallerInfo
    {
        public CallerInfo(string filterString, string file, int line, string member)
        {
            FilterString = filterString;
            File = file;
            Line = line;
            Member = member;
        }

        public string FilterString { get; }

        public string File { get; }

        public int Line { get; }

        public string Member { get; }
    }
}
