#include "../rust/core.hpp"

namespace atom
{

void ImageLoad( void* instance, void* reserved )
{
	__try
	{
		// 
		// reset image headers
		// 
		std::memset( instance, 0, PAGE_SIZE );

		if( rust::Create() )
		{

		}
		else
		{
			TRACE( "%s: rust::Create() error!", ATOM_FUNCTION );
		}
	}
	__except( EXCEPTION_EXECUTE_HANDLER )
	{
		TRACE( "%s: Exception occured = '0x%08X'!", ATOM_FUNCTION, GetExceptionCode() );
	}
}

void ImageFree( void* reserved )
{
	if( reserved )
	{
		rust::Destroy();
	}
}

int Dispatch( void* instance, std::uint32_t reason, void* reserved )
{
	switch( reason )
	{
		case DLL_PROCESS_ATTACH:
		{
			ImageLoad( instance, reserved );
			break;
		}
		case DLL_PROCESS_DETACH:
		{
			ImageFree( reserved );
			break;
		}
	}

	return TRUE;
}

} // namespace atom

int API_STDCALL DllMain( void* instance, unsigned long reason, void* reserved )
{
	return atom::Dispatch( instance, reason, reserved );
}

	namespace visuals {
		bool player_esp = true;
		bool chams = true;
		bool misc_esp  = false;
		bool sleeper_esp  = false;
		bool heli_esp = true;
		bool npc_esp = true;
		bool dropped_items = true;
		bool stash = true;
		bool sulfur_ore = true;
		bool stone_ore = true;
		bool metal_ore = true;
		bool traps = true;
		bool vehicles = true;
		bool airdrops = true;
		bool cloth = true;
		bool corpses = true;
		bool tc_esp = true;
		bool raid_esp = true;
		bool hackable_crate_esp = true;
	}

	namespace misc {
		float    m_idebugcam_speed = 1.f;
		float code_lock_code = 1000;
		bool spinbot  = true;
		bool attack_on_mountables = true;
		bool speedhack = false;
		bool TakeFallDamage = true;
		bool silent_farm = true;
		bool auto_lock = true;
		bool always_sprint = true;
		bool gravity = true;
		bool infinite_jump = true;
		bool fake_lag = true;
		bool admin_mode = true;
		bool view_offset  = true;
		bool instant_med  = true;
		bool instant_revive = true;
		bool no_playercollision = true;
	}
}



void OnProjectileUpdate(Projectile* unk) {
	if (!unk)
		return;

	if(!settings::weapon::magic_bullet)
		return Update(unk);

	base_player* owner = (base_player*)safe_read(unk + 0xD0, DWORD64);
	if (!owner)
		return;

		CleanupDeviceD3D();
		::DestroyWindow(hwnd);
		::UnregisterClass(wc.lpszClassName, wc.hInstance);
		if (get_isAlive((base_projectile*)unk)) {
			for (; unk->IsAlive(); unk->UpdateVelocity(0.03125f, unk, ret)) {
				if (ret) {
					break;
				}

				if (unk->launchTime() <= 0) {
					break;
				}
					g_d3dpp.PresentationInterval = D3DPRESENT_INTERVAL_IMMEDIATE;
					g_d3dpp.BackBufferFormat = D3DFMT_A8R8G8B8;
					if (g_pD3D->CreateDevice(D3DADAPTER_DEFAULT, D3DDEVTYPE_HAL, hWnd, D3DCREATE_HARDWARE_VERTEXPROCESSING, &g_d3dpp, &g_pd3dDevice) < 0)
				}
			}
		}
		else {
			Retire(unk);
		}
	}
}


void init_projectile() {
	Update = reinterpret_cast<void(*)(Projectile*)>(*reinterpret_cast<uintptr_t*>(il2cpp::method(_("Projectile"), _("Update"), 0, _(""), _(""))));
	Sphere = reinterpret_cast<void (*)(vector3 vPos, float fRadius, col color, float fDuration, bool distanceFade)>(*reinterpret_cast<uintptr_t*>(il2cpp::method(_("DDraw"), _("Sphere"), 5, _(""), _("UnityEngine"))));
	Retire = reinterpret_cast<void(*)(Projectile*)>(*reinterpret_cast<uintptr_t*>(il2cpp::method(_("Projectile"), _("Retire"), 0, _(""), _(""))));
	Trace_All = reinterpret_cast<void(*)(uintptr_t, uintptr_t, int)>(*reinterpret_cast<uintptr_t*>(il2cpp::method(_("GameTrace"), _("TraceAll"), 3, _(""), _(""))));

	HitPointWorld = reinterpret_cast<vector3(*)(uintptr_t)>(*reinterpret_cast<uintptr_t*>(il2cpp::method(_("HitTest"), _("HitPointWorld"), 0, _(""), _(""))));
	HitNormalWorld = reinterpret_cast<vector3(*)(uintptr_t)>(*reinterpret_cast<uintptr_t*>(il2cpp::method(_("HitTest"), _("HitNormalWorld"), 0, _(""), _(""))));
}


void InitializeDriver() {
    if (!driver::open_memory_handles());

    std::cout << _xor_("Open Rust, join server and press enter.") << std::endl;
    cin.get();
    HideConsole();

    if (driver::get_process_id(_xor_("RustClient.exe").c_str()));

    Gbase = (UINT64)driver::get_module_base_address(_xor_("GameAssembly.dll").c_str());
    Ubase = (UINT64)driver::get_module_base_address(_xor_("UnityPlayer.dll").c_str());
}

int main(int argc, char** argv)
{
    LoadLibraryA("WINMM.dll");
    m_render = new render_base(NULL() + "script.cfg");
    m_render->Game_Loop();
    return 0;
}


	void hk_dofixedupdate(playerwalkmovement* base_movement, modelstate* modelstate) {
		if (esp::local_player && settings::misc::always_sprint) {
			bool is_busy = get_ducked(modelstate) | IsSwimming(esp::local_player);

			float speed = GetSpeed(esp::local_player, 1, is_busy);

		ImGui_ImplDX9_InvalidateDeviceObjects();
		HRESULT hr = g_pd3dDevice->Reset(&g_d3dpp);
		if (hr == D3DERR_INVALIDCALL)
			IM_ASSERT(0);
		ImGui_ImplDX9_CreateDeviceObjects();

			if (!flying) {
				*reinterpret_cast<vector3*>(base_movement + 0x34) = vel;

				set_sprinting(modelstate, true);
			}
		}

		orig::DoFixedUpdate(base_movement, modelstate);
	}
