class EnemyShip : GameObject, IDamageable
{
    public float moveSpeed = 300;
    public float health = 3;                      //lustig if sats👇👇👇
    Vector2f direction = new Vector2f(Random.Shared.Next(2) == 0 ? -1 : 1, 1);

    public EnemyShip()
    {
        InitializeVariables("angel2", new(350, -300), new(160, 160), Program.GlobalTags.Enemy);
    }

    void Shoot(Vector2f posistion, Vector2f direction)
    {
        new Projectile(posistion, direction);
    }

    public void OnTakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) remove = true;
    }

    Vector2f BounceOnEdges(Vector2f direction)
    {
        if (position.X > Program.WindowSize.X - (size.X / 2))
        {
            direction.X = -1;
            position.X -= 1;
        }
        else if (position.X < (size.X / 2))
        {
            direction.X = 1;
            position.X += 1;
        }

        return direction;
    }

    public override void Update(float deltaTime)
    {
        // viktigt att normalisera vector
        direction = BounceOnEdges(direction);
        Move(deltaTime, Program.Normalize(direction) * moveSpeed);
    }

    public override void Draw(RenderWindow window)
    {
        DrawObject(sprite, size, window, Program.VectorToAngle(direction) - 90);
    }
}