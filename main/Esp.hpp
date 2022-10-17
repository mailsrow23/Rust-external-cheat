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

		pViewMatrix = Read<Matrix4x4>(entity + 0x2E4); //camera


		uintptr_t m_skyDome = NULL;
		uintptr_t m_camer = NULL;
		uintptr_t last_object = NULL;
		auto current_tagged_base = Read<uintptr_t>(gBase + 0x08);
		auto current_tagged_obj = Read<uintptr_t>(current_tagged_base + 0x120);


		if (Settings::drawCrosshair)
			DrawCrosshair();

		if (Settings::enableAimbot)
			Render::Circle(ImVec2(screenWidth / 2, screenHeight / 2), Settings::aimbotFov, ImColor(255, 255, 255));

		std::unique_ptr<std::vector<BasePlayer>> local_players = std::make_unique<std::vector<BasePlayer>>();
		std::unique_ptr<std::vector<PlayerCorpse>> local_corpse = std::make_unique<std::vector<PlayerCorpse>>();
		std::unique_ptr<std::vector<BaseResource>> local_ore = std::make_unique<std::vector<BaseResource>>();

		Mutex->PlayerSync->lock();
		*local_players = *entityList;
		*local_corpse = *corpseList;
		*local_ore = *oreList;
		Mutex->PlayerSync->unlock();

void setToDay()
{
	ULONG_PTR objectClass = read<ULONG_PTR>(entity[1].gameObject + 0x30);
	ULONG_PTR entityPtr = read<ULONG_PTR>(objectClass + 0x18);
	ULONG_PTR skyDome = read<ULONG_PTR>(entityPtr + 0x28);
	ULONG_PTR todCycle = read<ULONG_PTR>(skyDome + 0x18);
	write<float>(todCycle + 0x10, 12);
}

