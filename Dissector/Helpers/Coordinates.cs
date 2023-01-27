using SharpDX;
using SharpDX.Mathematics.Interop;

public static bool WorldToScreen(SharpDX.Matrix localRotation, Vector3 enemy, out Vector3 screen)
{
    screen = new Vector3();

    var transposedRotation = Matrix.Transpose(localRotation);

    var translation = new Vector3(transposedRotation.M41, transposedRotation.M42, transposedRotation.M43);
    var up = new Vector3(transposedRotation.M21, transposedRotation.M22, transposedRotation.M23);
    var right = new Vector3(transposedRotation.M11, transposedRotation.M12, transposedRotation.M13);

    var w = Vector3.Dot(translation, enemy) + transposedRotation.M44;
    if (w <= 0) return false;

    var y = Vector3.Dot(up, enemy) + transposedRotation.M24;
    var x = Vector3.Dot(right, enemy) + transposedRotation.M14;

    screen.X = ((ScreenWidth / w) * x + ScreenWidth / 2);
    screen.Y = ((ScreenHeight / w) * y + ScreenHeight / 2);
    screen.Z = w;

    return true;
}

