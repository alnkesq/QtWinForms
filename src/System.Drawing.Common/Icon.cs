using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace System.Drawing
{
    [TypeConverter(typeof(IconConverter))]
    public class Icon : IDisposable
    {
        public byte[] IcoBytes { get; init; }
        
        // Lazily-initialized native Qt icon pointer for reuse
        public IntPtr _nativeQIcon = IntPtr.Zero;

        public void Dispose()
        {
            // Note: We don't dispose the native QIcon here because it may be shared
            // across multiple tree nodes. The ImageList will manage the lifecycle.
        }
    }
}
