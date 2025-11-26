using System;
using System.Runtime.InteropServices;
using Cdecl = System.Runtime.CompilerServices.CallConvCdecl;

namespace System.Windows.Forms
{
    internal static class NativeMethods
    {
        internal const string LibName = "QtWinFormsNative";

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QApplication_Create();

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QApplication_Run();

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern void QApplication_InvokeOnMainThread(delegate* unmanaged[Cdecl]<IntPtr, void> callback, IntPtr userData);


        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QWidget_Create(IntPtr parent);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QWidget_Destroy(IntPtr widget);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QWidget_Show(IntPtr widget);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QWidget_Hide(IntPtr widget);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool QWidget_IsVisible(IntPtr widget);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QWidget_SetParent(IntPtr widget, IntPtr parent);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QWidget_Move(IntPtr widget, int x, int y);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QWidget_Resize(IntPtr widget, int width, int height);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QWidget_SetTitle(IntPtr widget, [MarshalAs(UnmanagedType.LPStr)] string title);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QWidget_SetIcon(IntPtr widget, IntPtr icon);

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
        public static extern void QWidget_SetFont(IntPtr widget, [MarshalAs(UnmanagedType.LPStr)] string family, float size, bool bold, bool italic, bool underline, bool strikeout);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QLineEdit_Create(IntPtr parent, [MarshalAs(UnmanagedType.LPStr)] string text);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QLineEdit_SetText(IntPtr lineEdit, [MarshalAs(UnmanagedType.LPStr)] string text);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern void QLineEdit_GetText_Invoke(IntPtr label, delegate* unmanaged[Cdecl]<void*, int, nint*, void> callback, IntPtr userData);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QLineEdit_SetEchoMode(IntPtr lineEdit, int mode);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QPlainTextEdit_Create(IntPtr parent, [MarshalAs(UnmanagedType.LPStr)] string text);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QPlainTextEdit_SetText(IntPtr widget, [MarshalAs(UnmanagedType.LPStr)] string text);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern void QPlainTextEdit_GetText_Invoke(IntPtr widget, delegate* unmanaged[Cdecl]<void*, int, nint*, void> callback, IntPtr userData);

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
        public static extern void QTabWidget_ConnectCurrentChanged(IntPtr tabWidget, IntPtr callback, IntPtr userData);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QMessageBox_Show(IntPtr parent, [MarshalAs(UnmanagedType.LPStr)] string text, [MarshalAs(UnmanagedType.LPStr)] string caption, int buttons, int icon, int defaultButton, int options);



        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QWidget_SetWindowState(IntPtr widget, int state);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QWidget_GetWindowState(IntPtr widget);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QWidget_SetFormProperties(IntPtr widget, bool minimizeBox, bool maximizeBox, bool showInTaskbar, bool showIcon, bool topMost, int formBorderStyle);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QProgressBar_Create(IntPtr parent);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QProgressBar_SetRange(IntPtr progressBar, int min, int max);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QProgressBar_SetValue(IntPtr progressBar, int value);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QWidget_ConnectCloseEvent(IntPtr widget, IntPtr closeCallback, IntPtr closedCallback, IntPtr userData);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern void QWidget_ConnectKeyEvent(IntPtr widget, delegate* unmanaged[Cdecl]<IntPtr, int, int, byte> callback, IntPtr userData);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QWidget_Close(IntPtr widget);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QWidget_SetContextMenuPolicy(IntPtr widget, int policy);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QWidget_ConnectCustomContextMenuRequested(IntPtr widget, IntPtr callback, IntPtr userData);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QMenu_Popup(IntPtr menu, int x, int y);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QWidget_MapToGlobal(IntPtr widget, int x, int y, out int rx, out int ry);

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
        public static extern void QAction_SetIcon(IntPtr action, IntPtr icon);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QAction_SetToolTip(IntPtr action, [MarshalAs(UnmanagedType.LPStr)] string toolTip);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QAction_SetVisible(IntPtr action, bool visible);

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
        public static extern void QMenu_ConnectAboutToShow(IntPtr menu, IntPtr callback, IntPtr userData);

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

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern void QFileDialog_GetOpenFileName(IntPtr parent, [MarshalAs(UnmanagedType.LPStr)] string initialDirectory, [MarshalAs(UnmanagedType.LPStr)] string title, [MarshalAs(UnmanagedType.LPStr)] string filter, delegate* unmanaged[Cdecl]<void*, int, IntPtr, void> callback, IntPtr userData);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern void QFileDialog_GetSaveFileName(IntPtr parent, [MarshalAs(UnmanagedType.LPStr)] string initialDirectory, [MarshalAs(UnmanagedType.LPStr)] string title, [MarshalAs(UnmanagedType.LPStr)] string filter, delegate* unmanaged[Cdecl]<void*, int, IntPtr, void> callback, IntPtr userData);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QColorDialog_GetColor(IntPtr parent, int initialColor, bool showAlphaChannel);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QDoubleSpinBox_Create(IntPtr parent);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QDoubleSpinBox_SetValue(IntPtr spinBox, double value);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern double QDoubleSpinBox_GetValue(IntPtr spinBox);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QDoubleSpinBox_SetRange(IntPtr spinBox, double min, double max);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QDoubleSpinBox_SetSingleStep(IntPtr spinBox, double step);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern void QDoubleSpinBox_ConnectValueChanged(IntPtr spinBox, delegate* unmanaged[Cdecl]<IntPtr, double, void> callback, IntPtr userData);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QSlider_Create(IntPtr parent);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QSlider_SetRange(IntPtr slider, int min, int max);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QSlider_SetValue(IntPtr slider, int value);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern void QSlider_ConnectValueChanged(IntPtr slider, delegate* unmanaged[Cdecl]<IntPtr, int, void> callback, IntPtr userData);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QListWidget_Create(IntPtr parent);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QListWidget_AddItem(IntPtr listWidget, [MarshalAs(UnmanagedType.LPStr)] string text);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QListWidget_Clear(IntPtr listWidget);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QListWidget_InsertItem(IntPtr listWidget, int index, [MarshalAs(UnmanagedType.LPStr)] string text);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QListWidget_RemoveItem(IntPtr listWidget, int index);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QListWidget_SetSelectionMode(IntPtr listWidget, int mode);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QListWidget_GetCurrentRow(IntPtr listWidget);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QListWidget_SetCurrentRow(IntPtr listWidget, int row);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern void QListWidget_GetSelectedRows(IntPtr listWidget, delegate* unmanaged[Cdecl]<int*, int, void*, void> callback, IntPtr userData);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern void QListWidget_ConnectCurrentRowChanged(IntPtr listWidget, IntPtr callback, IntPtr userData);
        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QDateTimeEdit_Create(IntPtr parent);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QDateTimeEdit_SetDateTime(IntPtr dateTimeEdit, int year, int month, int day, int hour, int minute, int second);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QDateTimeEdit_GetDateTime(IntPtr dateTimeEdit, out int year, out int month, out int day, out int hour, out int minute, out int second);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QDateTimeEdit_SetMinimumDateTime(IntPtr dateTimeEdit, int year, int month, int day, int hour, int minute, int second);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QDateTimeEdit_SetMaximumDateTime(IntPtr dateTimeEdit, int year, int month, int day, int hour, int minute, int second);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern void QDateTimeEdit_ConnectDateTimeChanged(IntPtr dateTimeEdit, delegate* unmanaged[Cdecl]<IntPtr, int, int, int, int, int, int, void> callback, IntPtr userData);
        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool QFontDialog_GetFont(
            IntPtr parent,
            [MarshalAs(UnmanagedType.LPStr)] string initialFamily,
            float initialSize,
            bool initialBold,
            bool initialItalic,
            bool initialUnderline,
            bool initialStrikeout,
            IntPtr outFamily,
            int outFamilyMaxLen,
            ref float outSize,
            ref bool outBold,
            ref bool outItalic,
            ref bool outUnderline,
            ref bool outStrikeout
        );

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QPictureBox_Create(IntPtr parent);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QPictureBox_SetImage(IntPtr pictureBox, IntPtr pixmap);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QPixmap_CreateFromData(byte[] data, int length);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QPixmap_Destroy(IntPtr pixmap);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QPictureBox_SetImageLocation(IntPtr pictureBox, [MarshalAs(UnmanagedType.LPStr)] string path);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QPictureBox_SetSizeMode(IntPtr pictureBox, int mode);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QToolBar_Create(IntPtr parent);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QToolBar_AddAction(IntPtr toolBar, IntPtr action);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QToolBar_SetToolButtonStyle(IntPtr toolBar, int style);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QTreeWidget_Create(IntPtr parent);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QTreeWidget_AddTopLevelItem(IntPtr treeWidget, [MarshalAs(UnmanagedType.LPStr)] string text);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QTreeWidgetItem_AddChild(IntPtr parentItem, [MarshalAs(UnmanagedType.LPStr)] string text);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QTreeWidgetItem_SetText(IntPtr item, int column, [MarshalAs(UnmanagedType.LPStr)] string text);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QTreeWidgetItem_RemoveChild(IntPtr parentItem, IntPtr childItem);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QTreeWidget_GetCurrentItem(IntPtr treeWidget);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QTreeWidget_SetCurrentItem(IntPtr treeWidget, IntPtr item);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern void QTreeWidget_ConnectItemSelectionChanged(IntPtr treeWidget, IntPtr callback, IntPtr userData);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QTreeWidgetItem_SetExpanded(IntPtr item, bool expanded);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern void QTreeWidget_ConnectItemExpanded(IntPtr treeWidget, IntPtr callback, IntPtr userData);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr QIcon_CreateFromData(byte[] data, int length);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QIcon_Destroy(IntPtr icon);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QTreeWidgetItem_SetIcon(IntPtr item, int column, IntPtr icon);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QTreeWidgetItem_SetToolTip(IntPtr item, int column, [MarshalAs(UnmanagedType.LPStr)] string toolTip);
    }
}
