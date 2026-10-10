class SpaceShip : GameObject, IDamageable
{
    public float moveSpeed;
    public float health;
    public string OpposingTag;         
    Vector2f direction;
    Vector2f facingDirection;
    IVectorReturner arselTratt;

    public SpaceShip(string spriteName, Vector2f position, Vector2f size, Vector2f direction, string tag, float moveSpeed, float health, bool movePattern)
    {
        // InitializeVariables("kallo", new(350, 700), new(130, 130), Program.GlobalTags.Player);
        InitializeVariables(spriteName, position, size, tag);
        this.moveSpeed = moveSpeed;
        this.health = health;
        this.direction = direction;
        if (movePattern) arselTratt = new PlayerMovePattern(Keyboard.Key.W, Keyboard.Key.S, Keyboard.Key.A, Keyboard.Key.D);
        else arselTratt = new EnemyMovePattern(this);
    }

    void Shoot(Vector2f posistion, Vector2f direction)
    {
        new Projectile(posistion, direction, OpposingTag);
        System.Console.WriteLine(OpposingTag);
    }

    public void OnTakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) remove = true;
    }

    public override void Update(float deltaTime)
    {
        if (KeyboardHandler.WasKeyJustPressed(Keyboard.Key.Space))
        {
            Shoot(position, new(0, -1));
        }

        direction = arselTratt.MovePattern(direction);
        Move(deltaTime, Program.Normalize(direction) * moveSpeed);
    }

    public override void Draw(RenderWindow window)
    {
        DrawObject(sprite, size, window);

        // enemy draw function
        // DrawObject(sprite, size, window, Program.VectorToAngle(direction) - 90);
    }
}