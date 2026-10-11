struct SpaceShipData
{
    public float moveSpeed { get; init; }
    public float health { get; init; }
    public float damage { get; init; }
    public int shootCooldown { get; init; }
    public string sprite { get; init; }
    public Vector2f size { get; init; }

    public void Print()
    {
        System.Console.WriteLine(moveSpeed);
        System.Console.WriteLine(health);
        System.Console.WriteLine(shootCooldown);
        System.Console.WriteLine(damage);
        System.Console.WriteLine(sprite);
        System.Console.WriteLine(size);
    }

    public SpaceShipData(string filePath)
    {
        string data = File.ReadAllText(filePath);
        this = JsonSerializer.Deserialize<SpaceShipData>(data, new JsonSerializerOptions() { IncludeFields = true });
    }
}