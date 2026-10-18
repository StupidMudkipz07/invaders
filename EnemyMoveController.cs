class EnemyMoveController : IMoveControllable
{
    MoveableObject targetObject;

    Vector2u borders = new();

    public EnemyMoveController(MoveableObject gameObject, Vector2u borders)
    {
        this.targetObject = gameObject;
        this.borders = borders;
    }

    public Vector2f GetMoveDirection()
    {
        Vector2f direction = targetObject.direction;
        if (targetObject.position.X > borders.X - (targetObject.size.X / 2))
        {
            direction.X = -1;
        }
        else if (targetObject.position.X < (targetObject.size.X / 2))
        {
            direction.X = 1;
        }

        if (targetObject.position.Y >= borders.Y + (targetObject.size.Y / 2))
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