using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace System.Windows.Forms
{
    public class DataGridViewSelectedRowCollection : IEnumerable<DataGridViewRow>
    {
        internal DataGridView _owner = null!;
        
        public int Count
        {
            get
            {
                if (!_owner.IsHandleCreated)
                    return 0;
                
                return GetSelectedRowIndices().Count;
            }
        }
        
        public DataGridViewRow this[int index]
        {
            get
            {
                var indices = GetSelectedRowIndices();
                if (index < 0 || index >= indices.Count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                
                return _owner.Rows[indices[index]];
            }
        }
        
        private List<int> GetSelectedRowIndices()
        {
            if (!_owner.IsHandleCreated)
                return new List<int>();
            
            var indices = new List<int>();
            var data = (_owner, indices);
            var handle = GCHandle.Alloc(data);
            try
            {
                unsafe
                {
                    var callback = (delegate* unmanaged[Cdecl]<int*, int, IntPtr, void>)&OnGetSelectedRowsCallback;
                    NativeMethods.QTableWidget_GetSelectedRows(_owner.QtHandle, callback, GCHandle.ToIntPtr(handle));
                }
            }
            finally
            {
                handle.Free();
            }
            return indices;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        private static unsafe void OnGetSelectedRowsCallback(int* rows, int count, IntPtr userData)
        {
            var data = ((DataGridView, List<int>))GCHandle.FromIntPtr(userData).Target!;
            var indices = data.Item2;
            
            for (int i = 0; i < count; i++)
            {
                indices.Add(rows[i]);
            }
        }
        
        public IEnumerator<DataGridViewRow> GetEnumerator()
        {
            var indices = GetSelectedRowIndices();
            return indices.Select(i => _owner.Rows[i]).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
