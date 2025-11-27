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
            Handle = NativeMethods.QTreeWidget_Create(IntPtr.Zero);
            NativeMethods.QTreeWidget_SetColumnCount(Handle, 2);
            NativeMethods.QTreeWidget_SetHeaderHidden(Handle, false);
            NativeMethods.QTreeWidget_SetHeaderLabels(Handle, new string[] { "Property", "Value" }, 2);
            
            RefreshProperties();
        }

        private void RefreshProperties()
        {
            if (Handle == IntPtr.Zero) return;

            NativeMethods.QTreeWidget_Clear(Handle);

            if (_selectedObject == null) return;

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(_selectedObject);

            foreach (PropertyDescriptor prop in properties)
            {
                if (!prop.IsBrowsable) continue;

                // Add item
                IntPtr item = NativeMethods.QTreeWidget_AddTopLevelItem(Handle, prop.Name);
                
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
                
                string valStr = val != null ? val.ToString() ?? string.Empty : "(null)";
                NativeMethods.QTreeWidgetItem_SetText(item, 1, valStr);
            }
        }
        [Obsolete(NotImplementedWarning)] public bool ToolbarVisible { get; set; }
        [Obsolete(NotImplementedWarning)] public bool HelpVisible { get; set; }
        [Obsolete(NotImplementedWarning)] public PropertySort PropertySort { get; set; }
    }

    
}
