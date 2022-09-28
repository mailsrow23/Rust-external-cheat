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

NTSTATUS NTAPI MmCopyVirtualMemory
(
	PEPROCESS SourceProcess,
	PVOID SourceAddress,
	PEPROCESS TargetProcess,
	PVOID TargetAddress,
	SIZE_T BufferSize,
	KPROCESSOR_MODE PreviousMode,
	PSIZE_T ReturnSize
);

typedef struct _IMAGE_OPTIONAL_HEADER64
{
	USHORT Magic;
	UCHAR MajorLinkerVersion;
	UCHAR MinorLinkerVersion;
	ULONG SizeOfCode;
	ULONG SizeOfInitializedData;
	ULONG SizeOfUninitializedData;
	ULONG AddressOfEntryPoint;
	ULONG BaseOfCode;
	ULONGLONG ImageBase;
	ULONG SectionAlignment;
	ULONG FileAlignment;
	USHORT MajorOperatingSystemVersion;
	USHORT MinorOperatingSystemVersion;
	USHORT MajorImageVersion;
	USHORT MinorImageVersion;
	USHORT MajorSubsystemVersion;
	USHORT MinorSubsystemVersion;
	ULONG Win32VersionValue;
	ULONG SizeOfImage;
	ULONG SizeOfHeaders;
	ULONG CheckSum;
	USHORT Subsystem;
	USHORT DllCharacteristics;
	ULONGLONG SizeOfStackReserve;
	ULONGLONG SizeOfStackCommit;
	ULONGLONG SizeOfHeapReserve;
	ULONGLONG SizeOfHeapCommit;
	ULONG LoaderFlags;
	ULONG NumberOfRvaAndSizes;
	struct _IMAGE_DATA_DIRECTORY DataDirectory[IMAGE_NUMBEROF_DIRECTORY_ENTRIES];
} IMAGE_OPTIONAL_HEADER64, * PIMAGE_OPTIONAL_HEADER64;

