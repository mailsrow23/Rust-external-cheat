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

bool DoHit(Projectile* pr, DWORD64 ht, vector3 point, vector3 normal, TraceInfo info, bool& exit) {
		bool result = false;
		if (!IsAlive())
			return result;

		auto material = info.material != 0 ? GetName(info.material)->str : (_(L"generic"));

		bool canIgnore = unity::is_visible(sentPosition(), currentPosition() + currentVelocity().Normalized() * 0.01f);
		if (!canIgnore) {
			integrity(0);
			return true;
		}

		float org;
		if (canIgnore) {
			vector3 attackStart = Simulate(false, true);

			safe_write(ht + 0x14, Ray(attackStart, vector3()), Ray);
		}

		if (canIgnore && m_wcsicmp(material, _(L"Flesh"))) {
			DWORD64 Tra = safe_read(ht + 0xB0, DWORD64);
			if (Tra) {
				auto st = _(L"head");

				set_name(Tra, st);
			}

			result = Do_Hit(pr, ht, point, normal);
			sentPosition(currentPosition());

		}
		return result;
	}
    
