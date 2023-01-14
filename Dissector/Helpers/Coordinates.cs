using SharpDX;
using SharpDX.Mathematics.Interop;

public static bool WorldToScreen(SharpDX.Matrix localRotation, Vector3 enemy, out Vector3 screen)
{
    screen = new Vector3(0, 0, 0);

    // Transpose the local rotation matrix instead of creating a new matrix
    var transposedRotation = Matrix.Transpose(localRotation);

    // Use the M41, M42, and M43 elements of the transposed rotation matrix to create the translation vector
    var translationVector = new Vector3(transposedRotation.M41, transposedRotation.M42, transposedRotation.M43);

    // Use the M21, M22, and M23 elements of the transposed rotation matrix to create the 'up' vector
    var up = new Vector3(transposedRotation.M21, transposedRotation.M22, transposedRotation.M23);

    // Use the M11, M12, and M13 elements of the transposed rotation matrix to create the 'right' vector
    var right = new Vector3(transposedRotation.M11, transposedRotation.M12, transposedRotation.M13);

    // Calculate the 'w' value using the dot product of the translation vector and the enemy's position, plus the M44 element of the transposed rotation matrix
    var w = Vector3.Dot(translationVector, enemy) + transposedRotation.M44;

    // If the 'w' value is less than or equal to zero, return false to indicate that the conversion was not successful
    if (w <= 0) return false;

    // Calculate the 'y' value using the dot product of the 'up' vector and the enemy's position, plus the M24 element of the transposed rotation matrix
    var y = Vector3.Dot(up, enemy) + transposedRotation.M24;

    // Calculate the 'x' value using the dot product of the 'right' vector and the enemy's position, plus the M14 element of the transposed rotation matrix
    var x = Vector3.Dot(right, enemy) + transposedRotation.M14;

    // Calculate the 2D screen coordinates of the enemy by applying a transformation to the 'x' and 'y' values
    screen.X = ((ScreenWidth / w) * x + ScreenWidth / 2);
    screen.Y = ((ScreenHeight / w) * y + ScreenHeight / 2);
    screen.Z = w;

    return true;
}



        private static float D3DxVec3Dot(Vector3 a, Vector3 b)
        {
             assert(m_NumHashSlots > 0);

        uint32_t index = Hash % m_NumHashSlots;
        SHashEntry *pEntry = m_rgpHashEntries[index];
        while (nullptr != pEntry)
        {
            
            pEntry = pEntry->pNext;
        }
        return false;
        }
    }
}
