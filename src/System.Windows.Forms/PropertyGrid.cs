using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class PropertyGrid : Control
    {
        private object _selectedObject;

        public PropertyGrid()
        {
        }

        public object SelectedObject
        {
            get { return _selectedObject; }
            set
            {
                if (_selectedObject != value)
                {
                    _selectedObject = value;
                    RefreshProperties();
                }
            }
        }

        protected override void CreateHandle()
        {
            QtHandle = NativeMethods.QTreeWidget_Create(IntPtr.Zero);
            NativeMethods.QTreeWidget_SetColumnCount(QtHandle, 2);
            NativeMethods.QTreeWidget_SetHeaderHidden(QtHandle, false);
            NativeMethods.QTreeWidget_SetHeaderLabels(QtHandle, new string[] { "Property", "Value" }, 2);
            
            RefreshProperties();
        }

        private void RefreshProperties()
        {
            if (QtHandle == IntPtr.Zero) return;

            NativeMethods.QTreeWidget_Clear(QtHandle);

            if (_selectedObject == null) return;

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(_selectedObject);

            foreach (PropertyDescriptor prop in properties)
            {
                if (!prop.IsBrowsable) continue;

                // Add item
                IntPtr item = NativeMethods.QTreeWidget_AddTopLevelItem(QtHandle, prop.Name);
                
                // Set value in second column
                object? val = null;
                try
                {
                    val = prop.GetValue(_selectedObject);
                }
                catch
                {
                    val = "<error>";
                }
                
                string valStr = val?.ToString()?.Replace('\n', ' ') ?? string.Empty;
                if (valStr.Length >= MaxValueLength)
                    valStr = valStr.Substring(0, MaxValueLength) + "…";
                NativeMethods.QTreeWidgetItem_SetText(item, 1, valStr);
            }
        }
        private const int MaxValueLength = 1000;
        [Obsolete(NotImplementedWarning)] public bool ToolbarVisible { get; set; }
        [Obsolete(NotImplementedWarning)] public bool HelpVisible { get; set; }
        [Obsolete(NotImplementedWarning)] public PropertySort PropertySort { get; set; }
    }

    
}
