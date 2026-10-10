class EnemyMovePattern : IVectorReturner
{
    GameObject targetObject;

    public EnemyMovePattern(GameObject spaceship)
    {
        this.targetObject = spaceship;
    }

    public Vector2f MovePattern(Vector2f direction)
    {
        if (targetObject.position.X > Program.WindowSize.X - (targetObject.size.X / 2))
        {
            direction.X = -1;
        }
        else if (targetObject.position.X < (targetObject.size.X / 2))
        {
            direction.X = 1;
        }

        return direction;

    }

}