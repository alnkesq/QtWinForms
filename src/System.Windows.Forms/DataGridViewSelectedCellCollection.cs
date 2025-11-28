using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    public class DataGridViewSelectedCellCollection : IEnumerable<DataGridViewCell>
    {
        public DataGridViewCell this[int index] => throw new NotImplementedException();
        internal DataGridView? _owner;
        public IEnumerator<DataGridViewCell> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
