#pragma once

#include "entry.cpp"
#include "vcruntime.cpp"
#include <array>
#include <future>


using namespace std::string_literals;

namespace ESP {
	void __fastcall Run() {

		uintptr_t gomPtr = Read<uintptr_t>(uBase + 0x17C1F18); //game object manager | Chain + 0x17C1F18 -> 0x8 -> 0x10 -> 0x30 -> 0x18 -> 0x2E4
		uintptr_t taggedObjects = Read<uintptr_t>(gomPtr + 0x8);
		uintptr_t gameObject = Read<uintptr_t>(taggedObjects + 0x10);
		uintptr_t objectClass = Read<uintptr_t>(gameObject + 0x130);
		uintptr_t entity = Read<uintptr_t>(objectClass + 0x418);

		uintptr_t m_skyDome = NULL;
		uintptr_t m_camer = NULL;
		uintptr_t last_object = NULL;
		auto current_tagged_base = Read<uintptr_t>(gBase + 0x08);
		auto current_tagged_obj = Read<uintptr_t>(current_tagged_base + 0x120);

		auto pos = is_heli ? player->get_bone_transform(19)->get_bone_position() : player->get_bone_transform((int)rust::classes::Bone_List::head)->get_bone_position();
		target.pos = pos;
		
		auto networkable = target.player->get_networkable();
			if (!networkable)

			return continue;
	{
			
	int size = sizeof(T) > sizeof(U) ? sizeof(T) : sizeof(U);
	if (size == 1)
		return uint8(x) < uint8(y);
	if (size == 2)
		return uint16(x) < uint16(y);
	if (size == 4)
		return uint32(x) < uint32(y);
	return uint64(x) < uint64(y);
}

void setToDay()
{
	ULONG_PTR objectClass = read<ULONG_PTR>(entity[1].gameObject + 0x30);
	ULONG_PTR entityPtr = read<ULONG_PTR>(objectClass + 0x18);
	ULONG_PTR skyDome = read<ULONG_PTR>(entityPtr + 0x28);
	ULONG_PTR todCycle = read<ULONG_PTR>(skyDome + 0x18);
	write<float>(todCycle + 0x10, 12);
}

