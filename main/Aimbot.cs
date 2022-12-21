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
        Vector3 dir = new Vector3(localPos.X - enemyPos.X, localPos.Y - enemyPos.Y, localPos.Z - enemyPos.Z);
        float pitch = ToDegree((float)Math.Asin(dir.Y / GetLength(dir)));
        float yaw = ToDegree((float)-Math.Atan2(dir.X, -dir.Z));
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
        Visuals.ESP.WorldToScreen(position, out vec2);
        return vec2.X > 0 && vec2.Y > 0 && vec2.X < Screen.PrimaryScreen.Bounds.Width && vec2.Y < Screen.PrimaryScreen.Bounds.Height;
    }
    
    // Improved Run method
    public static void Run()
    {
        // Find the nearest enemy within the specified FOV and distance
        float bestFov = Settings.Aimbot

        Entity nearestPlayer = null;
        foreach (Entity entity in EntityUpdater.EntityUpdater.EntityList)
        {
            if (entity.LocalPlayer)
            {
                LocalPlayer = entity;
                continue;
            }

            float distance = Vector3.Distance(LocalPlayer.Position, entity.Position);
            if (distance > 300 || entity.Health < 0.1)
            {
                continue;
            }

            float fov = ScreenToEnemy(entity.Position);
            if (fov < bestFov)
            {
                bestFov = (int)fov;
                nearestPlayer = entity;
            }
        }

        // Aim at the nearest enemy if it was found
        if (LocalPlayer != null && nearestPlayer != null)
        {
            Vector3 aimPos = nearestPlayer.Position;
            Vector2 currentAngle = LocalPlayer.ViewAngle;
            Vector2 recoilAngle = LocalPlayer.RecoilAngle;

            // Check if the right mouse button is pressed
            if (Convert.ToBoolean(Memory.Memory.GetAsyncKeyState(System.Windows.Forms.Keys.RButton) & 0x8000))
            {
                Vector2 angle = CalcAngle(LocalPlayer.Position, aimPos) - LocalPlayer.ViewAngle;
                Vector2 finalAngle = LocalPlayer.ViewAngle + angle;
                finalAngle = ClampAngles(finalAngle);
                recoilAngle = ClampAngles(recoilAngle);

                // Apply RCS
                finalAngle.X -= recoilAngle.X;
                finalAngle.Y -= recoilAngle.Y;

                Vector2 delta = ClampAngle(finalAngle - currentAngle);
                delta = ClampAngles(delta);
                finalAngle.X = currentAngle.X += delta.X / Settings.Aimbot.Smoothness;
                finalAngle.Y = currentAngle.Y += delta.Y / Settings.Aimbot.Smoothness;
                finalAngle = ClampAngles(finalAngle);
                finalAngle = Normalize(finalAngle);

                // Set the view angles
                LocalPlayer.ViewAngle = finalAngle;
            }
        }

        // Sleep for a short time to prevent CPU overload
        Thread.Sleep(5);
    }
}
