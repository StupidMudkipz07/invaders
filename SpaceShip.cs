class SpaceShip : GameObject, IDamageable, IKillable
{
    public float moveSpeed = 500;
    public float health = 3;

    public SpaceShip()
    {
        InitializeVariables("kallo", new(350, 700), new(130, 130), "player");
    }

    void Shoot(Vector2f posistion, Vector2f direction)
    {
        new Projectile(posistion, direction);
    }

    public void OnTakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) OnKill();
    }

    public void OnKill()
    {
        remove = true;
    }


    public override void Update(float deltaTime)
    {
        Vector2f direction = new Vector2f(0, 0);

        if (KeyboardHandler.IsKeyDown(Keyboard.Key.A))
            direction.X -= 1;
        if (KeyboardHandler.IsKeyDown(Keyboard.Key.D))
            direction.X += 1;
        if (KeyboardHandler.IsKeyDown(Keyboard.Key.S))
            direction.Y += 1;
        if (KeyboardHandler.IsKeyDown(Keyboard.Key.W))
            direction.Y -= 1;

        if (KeyboardHandler.IsKeyDown(Keyboard.Key.Space))
        {
            Shoot(position, new(0, -1));
        }

        if (CollidingWithTag("slop"))
        {
            System.Console.WriteLine("träffade slop");
        }

        // viktigt att normalisera vector
        Move(deltaTime, Program.Normalize(direction) * moveSpeed);
    }

    public override void Draw(RenderWindow window)
    {
        DrawObject(sprite, size, window);
    }
}