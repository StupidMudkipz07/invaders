class Projectile : GameObject
{
    float moveSpeed = 800;
    public float health = 3;
    Vector2f direction;

    public Projectile(Vector2f position, Vector2f Direction)
    {
        direction = Direction;
        InitializeVariables("kallo", position, new(80, 50), "player");
    }


    public override void Update(float deltaTime)
    {
        // viktigt att normalisera vector
        Move(deltaTime, Program.Normalize(direction) * moveSpeed);
    }

    public override void Draw(RenderWindow window)
    {
        DrawObject(sprite, size, window);
    }
}