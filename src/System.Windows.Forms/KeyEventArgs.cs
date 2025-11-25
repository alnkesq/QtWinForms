using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    public class KeyEventArgs : EventArgs
    {
        private bool _suppressKeyPress;

        public virtual bool Alt => (KeyData & Keys.Alt) == Keys.Alt;

        public bool Control => (KeyData & Keys.Control) == Keys.Control;

        public bool Handled { get; set; }

        public Keys KeyCode
        {
            get
            {
                Keys keys = KeyData & Keys.KeyCode;
                if (!Enum.IsDefined(keys))
                {
                    return Keys.None;
                }
                return keys;
            }
        }

        public int KeyValue => (int)(KeyData & Keys.KeyCode);

        public Keys KeyData { get; }

        public Keys Modifiers => KeyData & Keys.Modifiers;

        public virtual bool Shift => (KeyData & Keys.Shift) == Keys.Shift;

        public bool SuppressKeyPress
        {
            get
            {
                return _suppressKeyPress;
            }
            set
            {
                _suppressKeyPress = value;
                Handled = value;
            }
        }

        public KeyEventArgs(Keys keyData)
        {
            KeyData = keyData;
        }
    }
}
