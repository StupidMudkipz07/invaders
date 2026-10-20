class SpaceShip : PhysicalObject, IDamageable
{
    public float moveSpeed;
    public float health;
    public string OpposingTag;
    IMoveControllable moveController;
    IShootable shootController;

    SpaceShip(string spriteName, string tag, Vector2f position, Vector2f size)
    {
        InitializeVariables(spriteName, position, size, tag);
    }

    public static SpaceShip SpawnPlayer(SpaceShipData stats, Vector2f startPosition, ControlLayout controlLayout)
    {
        SpaceShip player = new(stats.sprite, GlobalTags.Player, startPosition, stats.size)
        {
            health = stats.health,
            moveSpeed = stats.moveSpeed,
            facingDirection = new(0, -1),
            OpposingTag = GlobalTags.Enemy
        };
        PlayerController playerController = new PlayerController(stats.shootCooldown, stats.damage, stats.bulletMoveSpeed, stats.bulletSize, stats.bulletSprite, controlLayout, stats.size.X * 0.4f);

        //player.moveController = new PlayerRotateController(controlLayout);
        player.moveController = playerController;
        player.shootController = playerController;

        return player;
    }

    public static SpaceShip SpawnEnemy(SpaceShipData stats, Vector2f position)
    {                //                             lustig if sats👇👇👇
        Vector2f enemyDirection = new(Random.Shared.Next(2) == 0 ? -1 : 1, 1);
        SpaceShip enemy = new(stats.sprite, GlobalTags.Enemy, position, stats.size)
        {
            health = stats.health,
            moveSpeed = stats.moveSpeed,
            direction = enemyDirection,
            facingDirection = enemyDirection,
            OpposingTag = GlobalTags.Player
        };

        enemy.moveController = new EnemyMoveController(enemy, handler.PlayArea);
        enemy.shootController = new EnemyShootController(stats.shootCooldown, stats.damage, stats.bulletMoveSpeed, stats.bulletSize, stats.bulletSprite);

        return enemy;
    }

    public void OnTakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) remove = true;
    }

    public override void Update(float deltaTime)
    {
        if (shootController.ShootRequirements())
        {
            shootController.Shoot(facingDirection, position, OpposingTag);
        }

        facingDirection = moveController.GetFacingDirection();
        direction = moveController.GetMoveDirection();

        Move(deltaTime, VectorMath.Normalize(direction) * moveSpeed);
    }

    public override void Draw(RenderWindow window)
    {
        DrawObject(sprite, size, window, VectorMath.VectorToAngle(facingDirection) + 90);
    }
}