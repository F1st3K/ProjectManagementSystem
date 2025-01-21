namespace ProjectManagementSystem.External.Extensions;

public static class Io
{
    public static T? TryParseFrom<T>(this string arg, IEnumerable<T> list, Func<T, string> convert)
    {
        return list.FirstOrDefault(i => convert(i).StartsWith(arg, StringComparison.OrdinalIgnoreCase));
    }
    
    public static string ReadOrGet(this string[] args, string flag, int index, Func<string>? getDefault = null)
    {
        if (args.TryGet(index) is { } arg 
            && string.IsNullOrWhiteSpace(arg) == false)
            return arg;
        
        Console.Write($"{flag}: ");
        
        return getDefault?.Invoke() ?? Console.ReadLine() ?? string.Empty;
    }
    
    public static string TryGet(this string[] args, int index)
    {
        return args.Length <= index ? string.Empty : args[index];
    }

    public static void WriteTitle(params string[] titles)
    {
        var main = $"|--[{string.Join("]-[", titles)}]";
            
        Console.WriteLine(main.PadRight(119, '-') + "|");
    }
    
    public static void WriteBlock(string title, string data)
    {
        var main = $"┍--[{title}]";
            
        Console.WriteLine(
            main.PadRight(119, '-') + '┑'
            + "\n" + data
            + "\n┕".PadRight(120, '-') + '┙');
    }
    
    public static string ReadPassword()
    {
        var password = string.Empty;
        while (true)
        { 
            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                break;
            }
            if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                    password = password.Remove(password.Length - 1);
                    Console.Write("\b \b");
            }
            else
            {
                var character = key.KeyChar;
                password += character;
                Console.Write("*");
            }
        }

        return password;
    }
}