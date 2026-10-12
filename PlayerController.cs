class PlayerController : IMoveControllable, IShootable
{
    float projectilePositionOffset;
    ControlLayout controlLayout;
    readonly Timer shootTimer;
    volatile bool canShoot = true;

    float bulletMoveSpeed;
    float damage;
    Vector2f bulletSize;
    string bulletSprite;
    int shootCooldown;

    void StartTimer()
    {
        canShoot = false;
        shootTimer.Change(dueTime: shootCooldown, period: Timeout.Infinite);
    }

    public PlayerController(int shootCooldown, float damage, float bulletMoveSpeed, Vector2f bulletSize, string bulletSprite, ControlLayout controlLayout, float projectilePositionOffset)
    {
        this.controlLayout = controlLayout;
        this.shootCooldown = shootCooldown;
        this.bulletMoveSpeed = bulletMoveSpeed;
        this.bulletSize = bulletSize;
        this.damage = damage;
        this.bulletSprite = bulletSprite;
        shootTimer = new Timer(callback => canShoot = true, state: null, dueTime: Timeout.Infinite, period: Timeout.Infinite);
        this.projectilePositionOffset = projectilePositionOffset;
    }

    public Vector2f GetMoveDirection()
    {
        Vector2f direction = new Vector2f(0, 0);
        if (KeyboardHandler.IsKeyDown(controlLayout.left))
            direction.X -= 1;
        if (KeyboardHandler.IsKeyDown(controlLayout.right))
            direction.X += 1;
        if (KeyboardHandler.IsKeyDown(controlLayout.down))
            direction.Y += 1;
        if (KeyboardHandler.IsKeyDown(controlLayout.up))
            direction.Y -= 1;

        return direction;
    }

    public Vector2f GetFacingDirection()
    {
        return new(0, -1);
    }

    public bool ShootRequirements()
    {
        if (canShoot && KeyboardHandler.IsKeyDown(controlLayout.shoot))
        {
            StartTimer();
            return true;
        }
        return false;
    }

    public void Shoot(Vector2f direction, Vector2f position, string targetTag)
    {
        new Projectile(bulletMoveSpeed, damage, new(position.X - projectilePositionOffset, position.Y), bulletSize, direction, bulletSprite, targetTag);
        new Projectile(bulletMoveSpeed, damage, new(position.X + projectilePositionOffset, position.Y), bulletSize, direction, bulletSprite, targetTag);
    }
}