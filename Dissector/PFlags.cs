using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dissector
{
    public enum PFlags
    {
        Unused1 = 1,
        Unused2 = 2,
        IsAdmin = 4,
        ReceivingSnapshot = 8,
        Sleeping = 16,
        Spectating = 32,
        Wounded = 64,
        IsDeveloper = 128,
        Connected = 256,
        VoiceMuted = 512,
        ThirdPersonViewmode = 1024,
        EyesViewmode = 2048,
        ChatMute = 4096,
        NoSprint = 8192,
        Aiming = 16384,
        DisplaySash = 32768,
        Workbench1 = 1048576,
        Workbench2 = 2097152,
        Workbench3 = 4194304
    }

    /// <summary>
    /// ModelState flags
    /// </summary>
    public enum MFlags
    {
        Ducked = 1,
        Jumped = 2,
        OnGround = 4,
        Sleeping = 8,
        Sprinting = 16, // 0x00000010
        OnLadder = 32, // 0x00000020
        Flying = 64, // 0x00000040
        Aiming = 128, // 0x00000080
        Prone = 256, // 0x00000100
        Mounted = 512, // 0x00000200
        Relaxed = 1024, // 0x00000400
    }
}
