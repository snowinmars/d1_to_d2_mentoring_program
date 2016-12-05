namespace ExceptionHandling.Task1.Extensions
{
    public static class Extension
    {
        public static bool IsPrintable(this char c)
        {
            return !char.IsControl(c) && !char.IsWhiteSpace(c);
        }
    }
}