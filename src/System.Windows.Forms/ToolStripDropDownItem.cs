using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    public abstract class ToolStripDropDownItem : ToolStripItem
    {
        private ToolStripDropDown? _dropDown;
        internal override bool IsQWidgetCreated => false;

        public ToolStripDropDown DropDown
        {
            get
            {
                if (_dropDown == null)
                {
                    _dropDown = CreateDefaultDropDown();
                    if (IsHandleCreated)
                    {
                        // If we are already created, we might need to recreate handle to attach menu?
                        // Or just ensure dropdown is created.
                    }
                }
                return _dropDown;
            }
            set
            {
                _dropDown = value;
            }
        }

        protected virtual ToolStripDropDown CreateDefaultDropDown()
        {
            return new ToolStripDropDownMenu();
        }
        [Obsolete(NotImplementedWarning)] public event EventHandler? DropDownOpening;
    }
}
