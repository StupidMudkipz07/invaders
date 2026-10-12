class EnemyMoveController : IMoveControllable
{
    GameObject targetObject;

    public EnemyMoveController(GameObject gameObject)
    {
        this.targetObject = gameObject;
    }

    public Vector2f GetMoveDirection()
    {
        Vector2f direction = targetObject.direction;
        if (targetObject.position.X > Program.WindowSize.X - (targetObject.size.X / 2))
        {
            direction.X = -1;
        }
        else if (targetObject.position.X < (targetObject.size.X / 2))
        {
            direction.X = 1;
        }

        if (targetObject.position.Y >= Program.WindowSize.Y + (targetObject.size.Y / 2))
        {
            targetObject.position.Y = 0 - targetObject.size.Y / 2;
        }

        return direction;
    }

    public Vector2f GetFacingDirection()
    {
        return targetObject.direction;
    }

}