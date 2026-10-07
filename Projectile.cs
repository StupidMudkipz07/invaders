class Projectile : GameObject
{
    Vector2f direction;
    float moveSpeed = 800;

    public Projectile(Vector2f position,Vector2f Direction)
    {
        direction = Direction;
        size = new(80, 50);
        this.position = position;
    }

    public override void Update(float deltaTime)
    {

        // viktigt att normalisera vector
        Move(deltaTime, Program.Normalize(direction) * moveSpeed);
    }

    public override void Draw(RenderWindow window)
    {
        SpriteDrawer.DrawSprite(SpriteDrawer.GetStaticSprite("kallo"), position, size, window);
    }
}