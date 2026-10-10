interface IDamageable
{
    public void OnTakeDamage(float damage);
}

interface IVectorReturner
{
    public Vector2f MovePattern(Vector2f s);
}