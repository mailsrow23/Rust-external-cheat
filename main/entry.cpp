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

	if (owner->is_local_player()) {
		bool ret = false;
		if (get_isAlive((base_projectile*)unk)) {
			for (; unk->IsAlive(); unk->UpdateVelocity(0.03125f, unk, ret)) {
				if (ret) {
					break;
				}

				if (unk->launchTime() <= 0) {
					break;
				}

				float time = get_time();

				if (time - unk->launchTime() < unk->traveledTime() + 0.03125f) {
					break;
				}
			}
		}
		else {
			Retire(unk);
		}
	}
}
