using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace System.Windows.Forms
{
    public class DataGridViewSelectedCellCollection : IEnumerable<DataGridViewCell>
    {
        internal DataGridView _owner = null!;
        
        public int Count
        {
            get
            {
                if (!_owner.IsHandleCreated)
                    return 0;
                
                return GetSelectedCells().Count;
            }
        }
        
        public DataGridViewCell this[int index]
        {
            get
            {
                var cells = GetSelectedCells();
                if (index < 0 || index >= cells.Count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                
                return cells[index];
            }
        }
        
        private List<DataGridViewCell> GetSelectedCells()
        {
            if (!_owner.IsHandleCreated)
                return new List<DataGridViewCell>();
            
            var cells = new List<DataGridViewCell>();
            var data = (_owner, cells);
            var handle = GCHandle.Alloc(data);
            try
            {
                unsafe
                {
                    var callback = (delegate* unmanaged[Cdecl]<int*, int*, int, IntPtr, void>)&OnGetSelectedCellsCallback;
                    NativeMethods.QTableWidget_GetSelectedCells(_owner.QtHandle, callback, GCHandle.ToIntPtr(handle));
                }
            }
            finally
            {
                handle.Free();
            }
            return cells;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        private static unsafe void OnGetSelectedCellsCallback(int* rows, int* columns, int count, IntPtr userData)
        {
            var data = ((DataGridView, List<DataGridViewCell>))GCHandle.FromIntPtr(userData).Target!;
            var grid = data.Item1;
            var cells = data.Item2;
            
            for (int i = 0; i < count; i++)
            {
                cells.Add(grid.Rows[rows[i]].Cells[columns[i]]);
            }
        }
        
        public IEnumerator<DataGridViewCell> GetEnumerator()
        {
            return GetSelectedCells().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
