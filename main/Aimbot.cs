class Aimbot
{
    // Constants
    private const float M_PI = 3.14159265358979323846f;

    // Variables
    private Entity localPlayer;
    private Entity nearestPlayer;

    // Utility methods
    private static double RAD2DEG(double x)
    {
        return x / Math.PI * 180.0;
    }

    private static float GetLength(Vector3 a)
    {
        return (float)Math.Sqrt(a.X * a.X + a.Y * a.Y + a.Z * a.Z);
    }

    private static float ToRadian(float degree)
    {
        return degree * M_PI / 180f;
    }

    private static float ToDegree(float radian)
    {
        return radian * 180f / M_PI;
    }

private static Vector2 CalcAngle(Vector3 localPos, Vector3 enemyPos)
{
    // Normalize the difference between the two positions
    Vector3 dir = (enemyPos - localPos).normalized;

    // Calculate the pitch angle
    float pitch = Mathf.Asin(dir.y) * Mathf.Rad2Deg;

    // Calculate the yaw angle
    float yaw = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

    // Return a Vector2 object with the pitch and yaw angles as its x and y components, respectively
    return new Vector2(pitch, yaw);
}


    private static Vector2 Normalize(Vector2 angle)
    {
        while (angle.X < -180f)
        {
            angle.X += 360f;
        }

        while (angle.X > 180f)
        {
            angle.X -= 360f;
        }

        while (angle.Y < -180f)
        {
            angle.Y += 360f;
        }

        while (angle.Y > 180f)
        {
            angle.Y -= 360f;
        }

        return angle;
    }

    private static Vector3 Prediction(Entity enemy)
    {
        Vector3 vel = enemy.Velocity;
        Vector3 bone = enemy.Position;
        float distance = Vector3.Distance(localPlayer.Position, enemy.Position);

        if (distance > 0.001f)
        {
            float bulletTime = distance / 50f; // Replace .50f with bullet speed
            Vector3 predict = vel * bulletTime * 0.75f;
            bone += predict;
            bone.Y += 4.905f * bulletTime * bulletTime;
        }

        return bone;
    }

public static bool ScreenToEnemy(Vector3 position)
{
    Vector2 vec2;
    Visuals.ESP.WorldToScreen(position, out vec2);
    return vec2.X > 0 && vec2.Y > 0 && vec2.X < Screen.PrimaryScreen.Bounds.Width && vec2.Y < Screen.PrimaryScreen.Bounds.Height;
}

// Improved Run method
public static Entity Run()
{
    // Find the nearest enemy within the specified FOV and distance
    float bestFov = Settings.Aimbot;

    // Initialize nearestPlayer as null
    Entities nearestPlayer = null;
    foreach (Entities entity in EntitiesUpdater.EntityUpdater.EntityList)
    {
        if (entity.LocalPlayer)
        {
            continue;
        }

        // Check if the entity is alive and within distance
        if (entity.Health < 0.1 || Vector3.Distance(LocalPlayer.Position, entity.Position) > 300)
        {
            continue;
        }

        // Check if the entity is within the screen bounds
        if (!ScreenToEnemy(entity.Position))
        {
            continue;
        }

        float fov = CalculateFov(LocalPlayer.Position, entity.Position);
        if (fov < bestFov)
        {
            bestFov = fov;
            nearestPlayer = entity;
        }
    }

    return nearestPlayer;
}
