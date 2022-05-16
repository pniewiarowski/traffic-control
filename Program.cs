using TrafficController.Classes;

void Main()
{
    Map map = GetMap();
    Menu menu = GetMenu();

    Boolean run = true;
    while (run)
    {
        Logger.Reset();

        Logger.LogWarning($"{Environment.NewLine}R_A_D_A_R {Environment.NewLine}");
        map.Print();

        Logger.LogWarning($"{Environment.NewLine}M_E_N_U {Environment.NewLine}");
        menu.Print();

        Logger.LogWarning("Your choice: ");
        switch (Console.ReadLine()?.ToLower())
        {
            case "exit":
            case "e":
                run = false;
                break;
            case "next":
            case "n":
                map.UpdateItems();
                break;
            case "c":
            case "create":
                Plane plane = GetPlaneFromUser();
                map.AddItemToRender(plane);
                break;
        }
    }
}


Map GetMap()
{
    return args.Length switch
    {
        0 => MapLoader.LoadMapFromString(new MapBuilder(100, 30, .98f).Build()),
        1 => MapLoader.LoadMapFromTextFile(args[0]),
        _ => throw new Exception("Invalid usage...")
    };
}

Menu GetMenu()
{
    return new Menu(new[]
        {
            $"[e] exit from menu{Environment.NewLine}",
            $"[n] go next{Environment.NewLine}",
            $"[c] set new plane{Environment.NewLine}",
        }
    );
}

Plane GetPlaneFromUser()
{
    Logger.LogWarning("Enter new plane x: ");
    var newX = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

    Logger.LogWarning("Enter new plane y: ");
    var newY = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

    Logger.LogWarning("Enter path plane[Exp: NNNEWWE]: ");
    var newPath = new Queue<char>(Console.ReadLine()?.ToCharArray() ?? Array.Empty<char>());

    return new Plane(newX, newY, newPath);
}

if (!new[] {0, 1}.Contains(args.Length))
{
    Logger.LogError($"Invalid arguments!{Environment.NewLine}");
    Logger.LogError($"Load map from file: <Program> <path_to_file>{Environment.NewLine}");
    Logger.LogError($"Generate map with default parameters: <Program>{Environment.NewLine}");
}
else Main();