using SharpDX.Windows;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Dissector.Helpers
{
    public class RenderFormEx : RenderForm
    {
        protected override CreateParams CreateParams
        {
            get
            {
                new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();

                // Extend the CreateParams property of the Button class.
                CreateParams cp = base.CreateParams;
                int ex_transparent = 0x00000020;

                //cp.ExStyle |= ex_composited;
                cp.ExStyle |= ex_transparent;
                return cp;
            }
        }

        public RenderFormEx() : base()
        {

        }
    }
}