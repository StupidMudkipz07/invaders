class SpaceShip : PhysicalObject, IDamageable, IDrawable, IFloatReturnable
{
    public float moveSpeed;
    public float health;
    public string OpposingTag;

    int CollisionInvincibilityTime;
    int damageInvincibilityTime;

    IMoveControllable moveController;
    IShootable shootController;
    volatile bool Invincible = false;
    readonly Timer invincibiltyTimer;

    public float GetFloat()
    {
        return health;
    }

    SpaceShip(string spriteName, string tag, Vector2f position, Vector2f size)
    {
        InitializeVariables(spriteName, position, size, tag);
        invincibiltyTimer = new Timer(callback => Invincible = false, state: null, dueTime: Timeout.Infinite, period: Timeout.Infinite);
    }

    public static SpaceShip SpawnPlayer(SpaceShipData stats, Vector2f startPosition, ControlLayout controlLayout)
    {
        SpaceShip player = new(stats.sprite, GlobalTags.Player, startPosition, stats.size)
        {
            health = stats.health,
            moveSpeed = stats.moveSpeed,
            facingDirection = new(0, -1),
            OpposingTag = GlobalTags.Enemy,
            damageInvincibilityTime = 2000,
            CollisionInvincibilityTime = 1000,
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
            OpposingTag = GlobalTags.Player,
            damageInvincibilityTime = 0,
            CollisionInvincibilityTime = 1000,
        };

        enemy.moveController = new EnemyMoveController(enemy, handler.PlayArea);
        enemy.shootController = new EnemyShootController(stats.shootCooldown, stats.damage, stats.bulletMoveSpeed, stats.bulletSize, stats.bulletSprite);

        return enemy;
    }

    void StartInvincibility(int waitTime)
    {
        Invincible = true;
        invincibiltyTimer.Change(dueTime: waitTime, period: Timeout.Infinite);
    }

    public void OnTakeDamage(float damage)
    {
        if (!Invincible) health -= damage;
        if (damageInvincibilityTime > 0 && !Invincible) StartInvincibility(damageInvincibilityTime);
        if (health <= 0)
        {
            remove = true;
            new Explosion(position, size);
            AudioManager.PlayAudio("vine-boom");
        }
    }

    public override void Update(float deltaTime)
    {
        if (shootController.ShootRequirements())
        {
            shootController.Shoot(facingDirection, position, OpposingTag);
        }

        if (CollidingWithTag(OpposingTag))
        {
            if (!Invincible)
            {
                OnTakeDamage(1);
                StartInvincibility(CollisionInvincibilityTime);
                //System.Console.WriteLine("träffad");
            }
        }

        facingDirection = moveController.GetFacingDirection();
        direction = moveController.GetMoveDirection();

        Move(deltaTime, VectorMath.Normalize(direction) * moveSpeed);
    }



    public void Draw(RenderWindow window)
    {
        if (Invincible)
        {
            sprite.Color = new(sprite.Color.R, sprite.Color.G, sprite.Color.B, 110);
        }
        else
        {
            sprite.Color = new(sprite.Color.R, sprite.Color.G, sprite.Color.B, 255);
        }
        DrawObject(sprite, size, window, VectorMath.VectorToAngle(facingDirection) + 90);
    }
}