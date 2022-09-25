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
		uintptr_t objectClass = Read<uintptr_t>(gameObject + 0x30);
		uintptr_t entity = Read<uintptr_t>(objectClass + 0x18);

		pViewMatrix = Read<Matrix4x4>(entity + 0x2E4); //camera


		uintptr_t m_skyDome = NULL;
		uintptr_t m_camer = NULL;
		uintptr_t last_object = NULL;
		auto current_tagged_base = Read<uintptr_t>(gBase + 0x08);
		auto current_tagged_obj = Read<uintptr_t>(current_tagged_base + 0x10);


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

		for (unsigned long i = 0; i < local_ore->size(); ++i) {
			std::unique_ptr<BaseResource> curOre = std::make_unique<BaseResource>(local_ore->at(i));

			auto position = Read<Vector3>(curOre->trans + 0x90);
			auto distance = (int)Math::Distance(&localPlayer->Player->position, &position);
			Vector2 pos;

			std::string nameStr = curOre->name;
			std::string distanceStr = std::to_string(distance) + "M";

			if (!Utils::WorldToScreen(position, pos)) continue;

			auto text_size = ImGui::CalcTextSize(nameStr.c_str());
			auto text_sizeDistance = ImGui::CalcTextSize(distanceStr.c_str());

			Render::DrawCornerBox(ImVec2(pos.x - 7, pos.y - 10), ImVec2(10, 10), ImColor(255, 255, 255));

			Render::Text(ImVec2(pos.x - text_size.x / 2, pos.y + 12 - text_size.y), nameStr, ImColor(255, 255, 255), true, Overlay::playerName, Overlay::playerName->FontSize);
			Render::Text(ImVec2(pos.x - text_sizeDistance.x / 2, pos.y + 21 - text_sizeDistance.y), distanceStr, ImColor(255, 255, 255), true, Overlay::playerName, Overlay::playerName->FontSize);
		}
