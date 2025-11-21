using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    internal static class NativeMethods
    {
        private const string LibName = "QtBackend";

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QApplication_Create();

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QApplication_Run();

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QWidget_Create(IntPtr parent);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QWidget_Show(IntPtr widget);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QWidget_SetParent(IntPtr widget, IntPtr parent);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QWidget_Move(IntPtr widget, int x, int y);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QWidget_Resize(IntPtr widget, int width, int height);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QWidget_SetTitle(IntPtr widget, [MarshalAs(UnmanagedType.LPStr)] string title);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QPushButton_Create(IntPtr parent, [MarshalAs(UnmanagedType.LPStr)] string text);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QPushButton_SetText(IntPtr button, [MarshalAs(UnmanagedType.LPStr)] string text);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QPushButton_ConnectClicked(IntPtr button, IntPtr callback, IntPtr userData);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QLabel_Create(IntPtr parent, [MarshalAs(UnmanagedType.LPStr)] string text);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QLabel_SetText(IntPtr label, [MarshalAs(UnmanagedType.LPStr)] string text);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QCheckBox_Create(IntPtr parent, [MarshalAs(UnmanagedType.LPStr)] string text);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QCheckBox_SetText(IntPtr checkBox, [MarshalAs(UnmanagedType.LPStr)] string text);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QCheckBox_SetChecked(IntPtr checkBox, bool isChecked);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QCheckBox_ConnectStateChanged(IntPtr checkBox, IntPtr callback, IntPtr userData);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QWidget_SetBackColor(IntPtr widget, byte r, byte g, byte b, byte a);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QWidget_SetEnabled(IntPtr widget, bool enabled);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QLineEdit_Create(IntPtr parent, [MarshalAs(UnmanagedType.LPStr)] string text);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QLineEdit_SetText(IntPtr lineEdit, [MarshalAs(UnmanagedType.LPStr)] string text);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern void QLineEdit_GetText_Invoke(IntPtr label, delegate* unmanaged[Cdecl]<void*, int, nint*, void> callback, IntPtr userData);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QWidget_ConnectResize(IntPtr widget, IntPtr resizeCallback, IntPtr moveCallback, IntPtr userData);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QGroupBox_Create(IntPtr parent, [MarshalAs(UnmanagedType.LPStr)] string title);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QGroupBox_SetTitle(IntPtr groupBox, [MarshalAs(UnmanagedType.LPStr)] string title);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QTabWidget_Create(IntPtr parent);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QTabWidget_AddTab(IntPtr tabWidget, IntPtr page, [MarshalAs(UnmanagedType.LPStr)] string label);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QTabWidget_RemoveTab(IntPtr tabWidget, int index);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QTabWidget_GetCurrentIndex(IntPtr tabWidget);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QTabWidget_SetCurrentIndex(IntPtr tabWidget, int index);

    }
}
