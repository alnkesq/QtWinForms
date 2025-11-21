using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class Form : Control
    {
        private string _text = string.Empty;

        public Form() : base()
        {
            Size = new Size(800, 600);
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();
            NativeMethods.QWidget_Resize(Handle, Size.Width, Size.Height);
            NativeMethods.QWidget_SetTitle(Handle, _text);
            
            // Connect resize event to trigger layout updates
            ConnectResizeEvent();
        }

        private unsafe void ConnectResizeEvent()
        {
            delegate* unmanaged[Cdecl]<nint, int, int, void> callback = &OnResizeCallback;
            NativeMethods.QWidget_ConnectResize(Handle, (IntPtr)callback, (IntPtr)(nint)GCHandle.Alloc(this));
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        private static unsafe void OnResizeCallback(nint userData, int width, int height)
        {
            var handle = GCHandle.FromIntPtr((IntPtr)userData);
            if (handle.Target is Form form)
            {
                // Update size and trigger layout using SetBoundsCore
                form.SetBoundsCore(form.Location.X, form.Location.Y, width, height);
            }
        }

        public Size ClientSize
        {
            get => Size;
            set => Size = value;
        }
        public override string Text 
        {
            get => _text;
            set
            {
                _text = value ?? string.Empty;
                if (IsHandleCreated)
                {
                    NativeMethods.QWidget_SetTitle(Handle, _text);
                }
            }
        }
    }
}
