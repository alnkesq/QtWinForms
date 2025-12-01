using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace System.Windows.Forms
{
    public class DataGridViewSelectedColumnCollection : IEnumerable<DataGridViewColumn>
    {
        internal DataGridView _owner = null!;
        
        public int Count
        {
            get
            {
                if (!_owner.IsHandleCreated)
                    return 0;
                
                return GetSelectedColumnIndices().Count;
            }
        }
        
        public DataGridViewColumn this[int index]
        {
            get
            {
                var indices = GetSelectedColumnIndices();
                if (index < 0 || index >= indices.Count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                
                return _owner.Columns[indices[index]];
            }
        }
        
        private List<int> GetSelectedColumnIndices()
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
                    var callback = (delegate* unmanaged[Cdecl]<int*, int, IntPtr, void>)&OnGetSelectedColumnsCallback;
                    NativeMethods.QTableWidget_GetSelectedColumns(_owner.QtHandle, callback, GCHandle.ToIntPtr(handle));
                }
            }
            finally
            {
                handle.Free();
            }
            return indices;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        private static unsafe void OnGetSelectedColumnsCallback(int* columns, int count, IntPtr userData)
        {
            var data = ((DataGridView, List<int>))GCHandle.FromIntPtr(userData).Target!;
            var indices = data.Item2;
            
            for (int i = 0; i < count; i++)
            {
                indices.Add(columns[i]);
            }
        }
        
        public IEnumerator<DataGridViewColumn> GetEnumerator()
        {
            var indices = GetSelectedColumnIndices();
            return indices.Select(i => _owner.Columns[i]).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
