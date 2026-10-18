static class VectorMath
{

    //Returns the normalized vector
    public static Vector2f Normalize(Vector2f vector)
    {
        float length = MathF.Sqrt(vector.X * vector.X + vector.Y * vector.Y);

        if (length == 0f)
            return new Vector2f(0f, 0f);

        return new Vector2f(vector.X / length, vector.Y / length);
    }

    //Returns the length of the vector
    public static float Length(Vector2f vector) => MathF.Sqrt(vector.X * vector.X + vector.Y * vector.Y);

    //returns the inverse of the vector
    public static Vector2f Inverse(Vector2f vector) => new(vector.X * -1, vector.Y * -1);

    public static float VectorToAngle(Vector2f rotation)
    {
        return MathF.Atan2(rotation.Y, rotation.X) * 180f / MathF.PI;
    }

    public static Vector2f AngleToVector(float angle)
    {
        //matte slop
        float radians = angle * MathF.PI / 180f;
        Vector2f rotatedVector = new(MathF.Cos(radians), MathF.Sin(radians));
        return rotatedVector;
    }

}