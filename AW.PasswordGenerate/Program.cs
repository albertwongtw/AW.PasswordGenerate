// See https://aka.ms/new-console-template for more information
using System.Text;

while (true)
{
    Console.Write("Please enter randam string length (0 to exit): ");

    var input = Console.ReadLine();
    var sb = new StringBuilder();
    if (uint.TryParse(input, out var length))
    {
        if (length != 0)
        {
            for (var i = 0; i < length; i++)
                sb.Append(GetRandamPasswordCharacter(PasswordFlag.Alphabet | PasswordFlag.Numermic | PasswordFlag.Symbol));
            Console.WriteLine("Generated password is: {0}", sb.ToString());
            TextCopy.ClipboardService.SetText(sb.ToString());
            
            Console.WriteLine("The password is copied to clipboard.");
            Console.WriteLine();
#if !DEBUG
            Console.Write("Press ANY KEY to exit...");
            Console.ReadLine();
#endif
        }
        break;
    }
}

char GetRandamPasswordCharacter(PasswordFlag passwordFlag = PasswordFlag.Any)
{
    var randam = new Random((int)DateTime.UtcNow.Ticks);
    var bytes = new byte[1];
    randam.NextBytes(bytes);

    if (passwordFlag == PasswordFlag.Any
        || (passwordFlag.HasFlag(PasswordFlag.Alphabet) && bytes.Any(b => (b >= 65 && b <= 90) || (b >= 97 && b <= 122)))
        || (passwordFlag.HasFlag(PasswordFlag.Numermic) && bytes.Any(b => b >= 48 && b <= 57))
        || (passwordFlag.HasFlag(PasswordFlag.Symbol) && bytes.Any(b => (b >= 33 && b <= 47) || (b >= 58 && b <= 64) || (b >= 91 && b <= 96) || (b >= 123 && b <= 126))))
        return Convert.ToChar(bytes[0]);
    return GetRandamPasswordCharacter(passwordFlag);
}

enum PasswordFlag
{
    Any = 0,
    Alphabet = 1,
    Numermic = 2,
    Symbol = 4,
}