class PlayerController : IMoveController, IShootable
{
    float projectilePositionOffset;
    ControlLayout controlLayout;
    readonly Timer shootTimer;
    volatile bool canShoot = true;

    int shootCooldown;

    void StartTimer()
    {
        canShoot = false;
        shootTimer.Change(dueTime: shootCooldown, period: Timeout.Infinite);
    }

    public PlayerController(int shootCooldown, ControlLayout controlLayout, float projectilePositionOffset)
    {
        this.controlLayout = controlLayout;
        this.shootCooldown = shootCooldown;
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
        new Projectile(new(position.X - projectilePositionOffset, position.Y), direction, targetTag);
        new Projectile(new(position.X + projectilePositionOffset, position.Y), direction, targetTag);
    }
}