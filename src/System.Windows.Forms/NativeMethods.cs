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
        public static extern void QWidget_Destroy(IntPtr widget);

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
        public static extern IntPtr QRadioButton_Create(IntPtr parent, [MarshalAs(UnmanagedType.LPStr)] string text);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QRadioButton_SetText(IntPtr radioButton, [MarshalAs(UnmanagedType.LPStr)] string text);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QRadioButton_SetChecked(IntPtr radioButton, bool isChecked);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QRadioButton_ConnectToggled(IntPtr radioButton, IntPtr callback, IntPtr userData);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QWidget_SetBackColor(IntPtr widget, byte r, byte g, byte b, byte a);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QWidget_SetForeColor(IntPtr widget, byte r, byte g, byte b, byte a);

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

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QMessageBox_Show(IntPtr parent, [MarshalAs(UnmanagedType.LPStr)] string text, [MarshalAs(UnmanagedType.LPStr)] string caption, int buttons, int icon, int defaultButton, int options);



        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QWidget_SetWindowState(IntPtr widget, int state);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QWidget_GetWindowState(IntPtr widget);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QProgressBar_Create(IntPtr parent);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QProgressBar_SetRange(IntPtr progressBar, int min, int max);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QProgressBar_SetValue(IntPtr progressBar, int value);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QWidget_ConnectCloseEvent(IntPtr widget, IntPtr closeCallback, IntPtr closedCallback, IntPtr userData);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QWidget_Close(IntPtr widget);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QMenuBar_Create(IntPtr parent);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QMenuBar_AddAction(IntPtr menuBar, IntPtr action);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QAction_Create([MarshalAs(UnmanagedType.LPStr)] string text);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QAction_SetText(IntPtr action, [MarshalAs(UnmanagedType.LPStr)] string text);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QAction_ConnectTriggered(IntPtr action, IntPtr callback, IntPtr userData);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QAction_CreateSeparator();

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QWidget_SetMenuBar(IntPtr widget, IntPtr menuBar);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QMenu_Create([MarshalAs(UnmanagedType.LPStr)] string title);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QMenu_AddAction(IntPtr menu, IntPtr action);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QMenu_AddMenu(IntPtr menu, IntPtr submenu);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QMenuBar_AddMenu(IntPtr menuBar, IntPtr menu);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QAction_SetMenu(IntPtr action, IntPtr menu);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QLinkLabel_Create(IntPtr parent, [MarshalAs(UnmanagedType.LPStr)] string text);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QLinkLabel_ConnectLinkClicked(IntPtr label, IntPtr callback, IntPtr userData);
        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QComboBox_Create(IntPtr parent);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QComboBox_AddItem(IntPtr comboBox, [MarshalAs(UnmanagedType.LPStr)] string text);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QComboBox_SetEditable(IntPtr comboBox, bool editable);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QComboBox_GetSelectedIndex(IntPtr comboBox);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QComboBox_SetSelectedIndex(IntPtr comboBox, int index);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QComboBox_ConnectSelectedIndexChanged(IntPtr comboBox, IntPtr callback, IntPtr userData);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QComboBox_Clear(IntPtr comboBox);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QComboBox_InsertItem(IntPtr comboBox, int index, [MarshalAs(UnmanagedType.LPStr)] string text);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QComboBox_RemoveItem(IntPtr comboBox, int index);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QComboBox_SetText(IntPtr comboBox, [MarshalAs(UnmanagedType.LPStr)] string text);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern void QComboBox_GetText_Invoke(IntPtr comboBox, delegate* unmanaged[Cdecl]<void*, int, nint*, void> callback, IntPtr userData);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern void QComboBox_ConnectCurrentTextChanged(IntPtr comboBox, delegate* unmanaged[Cdecl]<void*, int, nint, void> callback, IntPtr userData);

        public unsafe delegate void ReadQStringCallback(void* data, int length, IntPtr userData);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern void QFileDialog_GetExistingDirectory(IntPtr parent, [MarshalAs(UnmanagedType.LPStr)] string initialDirectory, [MarshalAs(UnmanagedType.LPStr)] string title, bool showNewFolderButton, delegate* unmanaged[Cdecl]<void*, int, IntPtr, void> callback, IntPtr userData);
    }
}
