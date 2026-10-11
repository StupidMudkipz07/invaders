interface IDamageable
{
    public void OnTakeDamage(float damage);
}

interface IMoveController
{
    public Vector2f GetMoveDirection();

    public Vector2f GetFacingDirection();
}

interface IShootable
{
    public bool ShootRequirements();

    public void Shoot(Vector2f facingDirection, Vector2f position,string targetTag);
}