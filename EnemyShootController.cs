class EnemyShootController : IShootable
{
    readonly Timer shootTimer;
    volatile bool canShoot = false;

    int shootCooldown;
    int shootCooldownRandomAddedAmount = 600;

    void StartTimer()
    {
        canShoot = false;
        int cooldownAdder = Random.Shared.Next(0, shootCooldownRandomAddedAmount);
        shootTimer.Change(dueTime: shootCooldown + cooldownAdder, period: Timeout.Infinite);
    }

    public EnemyShootController(int shootCooldown)
    {
        shootTimer = new Timer(callback => canShoot = true, state: null, dueTime: Timeout.Infinite, period: Timeout.Infinite);
        this.shootCooldown = shootCooldown;
        StartTimer();
    }

    public void Shoot(Vector2f direction, Vector2f position, string targetTag)
    {
        new Projectile(position, direction, targetTag);
    }

    public bool ShootRequirements()
    {
        if (canShoot == true)
        {
            canShoot = false;
            StartTimer();

            return true;
        }
        return false;
    }
}