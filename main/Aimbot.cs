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

private static Vector2 CalcAngle(Vector3 localPos, Vector3 enemyPos) => 
    new Vector2(Mathf.Asin((enemyPos - localPos).normalized.y) * Mathf.Rad2Deg, 
                Mathf.Atan2((enemyPos - localPos).normalized.x, (enemyPos - localPos).normalized.z) * Mathf.Rad2Deg);

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

public static Entity FindNearestEnemy()
{
    Entity nearestEnemy = null;
    float closestDistance = float.MaxValue;
    Vector3 localPlayerPosition = LocalPlayer.Position;

    foreach (Entity entity in EntitiesUpdater.EntityList)
    {
        if (entity.IsLocalPlayer || entity.Health <= 0)
        {
            continue;
        }

        float distance = Vector3.Distance(localPlayerPosition, entity.Position);
        if (distance > 300 || !IsEnemyVisible(entity))
        {
            continue;
        }

        if (distance < closestDistance)
        {
            closestDistance = distance;
            nearestEnemy = entity;
        }
    }

    return nearestEnemy;
}

private static bool IsEnemyVisible(Entity entity)
{
    Vector3 localPlayerPosition = LocalPlayer.Position;
    Vector3 entityPosition = entity.Position;

    // Check if the entity is too far away to be visible
    if (Vector3.Distance(localPlayerPosition, entityPosition) > 300)
    {
        return false;
    }

    // Check if the entity is behind a wall or obstacle
    RaycastHit hit;
    if (Physics.Linecast(localPlayerPosition, entityPosition, out hit))
    {
        if (hit.transform != entity.transform)
        {
            return false;
        }
    }

    // Check if the entity is within the field of view of the local player
    Vector3 directionToEntity = (entityPosition - localPlayerPosition).normalized;
    float angle = Vector3.Angle(directionToEntity, LocalPlayer.Forward);
    if (angle > 60)
    {
        return false;
    }

    return true;
}


