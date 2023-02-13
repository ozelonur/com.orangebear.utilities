namespace OrangeBear.Utilities
{
    public static class Suffix
    {
        public static string WithSuffix(this int value)
        {
            string suffix = value switch
            {
                < 1000 => "",
                >= 1000 and < 1000000 => "K",
                >= 1000000 and < 1000000000 => "M",
                >= 1000000000 => "B"
            };

            return suffix.Equals("") ? $"{value}{suffix}" : $"{value / 1000f:0.0} {suffix}";
        }
    }
}