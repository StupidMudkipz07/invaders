interface IShootable
{
    public bool ShootRequirements();

    public void Shoot(Vector2f facingDirection, Vector2f position,string targetTag);
}