using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    public class PreviewKeyDownEventArgs : EventArgs
    {
        public Keys KeyData { get; }

        public bool Alt => (KeyData & Keys.Alt) == Keys.Alt;

        public bool Control => (KeyData & Keys.Control) == Keys.Control;

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

        public Keys Modifiers => KeyData & Keys.Modifiers;

        public bool Shift => (KeyData & Keys.Shift) == Keys.Shift;

        public bool IsInputKey { get; set; }

        public PreviewKeyDownEventArgs(Keys keyData)
        {
            KeyData = keyData;
        }
    }

}
