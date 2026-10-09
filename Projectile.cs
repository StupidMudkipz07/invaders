class Projectile : GameObject
{
    float moveSpeed = 800;
    Vector2f direction;
    float damage = 1;
    string targetTag = Program.GlobalTags.Enemy;

    void CheckForTarget()
    {
        GameObject? gameObject = GetCollidingObject(targetTag);

        if (gameObject != null)
        {
            if (gameObject is IDamageable)
            {
                IDamageable g = gameObject as IDamageable;
                g.OnTakeDamage(damage);
                remove = true;
            }
        }
    }

    public Projectile(Vector2f position, Vector2f Direction)
    {
        direction = Direction;
        InitializeVariables("kallo", position, new(40, 67), "player");
    }

    public override void Update(float deltaTime)
    {
        CheckForTarget();

        // viktigt att normalisera vector
        Move(deltaTime, Program.Normalize(direction) * moveSpeed);
    }

    public override void Draw(RenderWindow window)
    {
        DrawObject(sprite, size, window);
    }
}