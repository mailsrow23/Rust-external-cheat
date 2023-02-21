namespace Dissector.Helpers
{
    public enum Layer
    {
        AI = 11,
        Clutter = 25,
        Construction = 21,
        Construction_Socket = 22,
        Debris = 26,
        Default = 0,
        Deployed = 8,
        Game_Trace = 14,
        Ignore_Raycast = 2,
        Invisible = 10,
        Occluder = 27,
        Player_Model_Rendering = 19,
        Player_Movement = 12,
        Player_Server = 17,
        Prevent_Building = 29,
        Prevent_Movement = 28,
        Ragdoll = 9,
        Reflections = 15,
        Terrain = 23,
        Transparent = 24,
        TransparentFX = 1,
        Tree = 30,
        Trigger = 18,
        UI = 5,
        Vehicle_Movement = 13,
        Water = 4,
        World = 16
    }
}

// Check if the projectile hits an object
bool CheckHit(Projectile* projectile, DWORD64 hitInfo, vector3 hitPoint, vector3 hitNormal, TraceInfo traceInfo, bool& exit) {
    bool hitSuccessful = false;
    
    // Check if the object being hit is alive
    if (!IsObjectAlive()) {
        return hitSuccessful;
    }

    // Determine the material of the object being hit
    auto materialName = traceInfo.material != 0 ? GetName(traceInfo.material)->str : MaterialType::Generic;
    
    // Check if the object being hit is visible
    bool isVisible = IsObjectVisible();
    if (!isVisible) {
        // If the object is not visible, set the integrity to 0 and return true
        SetObjectIntegrity(0);
        return true;
    }

    // Simulate the attack and set the attack start point
    float org;
    if (isVisible) {
        vector3 attackStart = SimulateAttack(false, true);
        safe_write(hitInfo + 0x14, Ray(attackStart, vector3()), Ray);
    }

    // If the material of the object being hit is "Flesh", set the name of the object to "head"
    if (isVisible && materialName == MaterialType::Flesh) {
        DWORD64 object = safe_read(hitInfo + 0xB0, DWORD64);
        if (object) {
            SetObjectName(object, ObjectName::Head);
        }
        
        // Call a function to handle the hit event
        hitSuccessful = HandleHitEvent(projectile, hitInfo, hitPoint, hitNormal);
        
        // Update the position of the object
        SetObjectPosition(CurrentPosition());
    }
    
    return hitSuccessful;
}
