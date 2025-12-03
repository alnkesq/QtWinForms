using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace System.Windows.Forms
{
    public class DataGridView : Control
    {
        private DataGridViewColumnCollection _columns;
        private DataGridViewRowCollection _rows;
        private DataGridViewSelectedCellCollection _selectedCells;
        private DataGridViewSelectedRowCollection _selectedRows;
        private DataGridViewSelectedColumnCollection _selectedColumns;
        private bool _virtualMode;
        private int _virtualRowCount;
        private DataGridViewSelectionMode _selectionMode = DataGridViewSelectionMode.FullRowSelect;
        
        protected override Size DefaultSize => new Size(240, 150);
        public DataGridView()
        {
            _columns = new DataGridViewColumnCollection { _owner = this };
            _rows = new DataGridViewRowCollection { _owner = this };
            _selectedCells = new DataGridViewSelectedCellCollection { _owner = this };
            _selectedRows = new DataGridViewSelectedRowCollection { _owner = this };
            _selectedColumns = new DataGridViewSelectedColumnCollection { _owner = this };
        }

        protected override void CreateHandle()
        {
            QtHandle = NativeMethods.QTableWidget_Create(IntPtr.Zero);
            SetCommonProperties();

            if (_columns.Count > 0)
            {
                NativeMethods.QTableWidget_SetColumnCount(QtHandle, _columns.Count);
                for (int i = 0; i < _columns.Count; i++)
                {
                    var col = _columns[i];
                    if (!string.IsNullOrEmpty(col.HeaderText))
                    {
                        NativeMethods.QTableWidget_SetColumnHeaderText(QtHandle, i, col.HeaderText);
                    }
                }
            }

            if (VirtualMode)
            {
                // In virtual mode, set row count and connect to data request callback
                NativeMethods.QTableWidget_SetRowCount(QtHandle, _virtualRowCount);
                ConnectCellDataNeeded();
            }
            else
            {
                // In normal mode, populate with actual data
                if (_rows.Count > 0)
                {
                    NativeMethods.QTableWidget_SetRowCount(QtHandle, _rows.Count);
                }

                UpdateCellsStructure();

                foreach (var row in Rows)
                {
                    foreach (var cell in row.Cells)
                    {
                        cell.ApplyValue();
                    }
                }
            }
            
            // Connect to selection changed signal
            ConnectSelectionChanged();
            
            // Set selection mode
            UpdateSelectionMode();
        }
        
        private void ConnectCellDataNeeded()
        {
            unsafe
            {
                var callback = (delegate* unmanaged[Cdecl]<IntPtr, int, int, void>)&OnCellDataNeededCallback;
                NativeMethods.QTableWidget_ConnectCellDataNeeded(QtHandle, (IntPtr)callback, GCHandlePtr);
            }
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        private static unsafe void OnCellDataNeededCallback(IntPtr userData, int row, int column)
        {
            var grid = ObjectFromGCHandle<DataGridView>(userData);
            grid.OnCellDataNeeded(row, column);
        }

        private void OnCellDataNeeded(int rowIndex, int columnIndex)
        {
            if (CellValueNeeded != null)
            {
                var args = new DataGridViewCellValueEventArgs(columnIndex, rowIndex);
                CellValueNeeded(this, args);

                NativeMethods.QTableWidget_SetCellText(QtHandle, rowIndex, columnIndex, DataGridViewCell.ValueToString(args.Value));
            }
        }
        
        private void ConnectSelectionChanged()
        {
            unsafe
            {
                var callback = (delegate* unmanaged[Cdecl]<IntPtr, void>)&OnSelectionChangedCallback;
                NativeMethods.QTableWidget_ConnectSelectionChanged(QtHandle, (IntPtr)callback, GCHandlePtr);
            }
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        private static unsafe void OnSelectionChangedCallback(IntPtr userData)
        {
            var grid = ObjectFromGCHandle<DataGridView>(userData);
            grid.OnSelectionChanged(EventArgs.Empty);
        }

        protected virtual void OnSelectionChanged(EventArgs e)
        {
            SelectionChanged?.Invoke(this, e);
        }

        internal void UpdateCellsStructure()
        {
            // Create initial cells structure for all rows based on current columns
            // This is only called during CreateHandle
            for (int r = 0; r < _rows.Count; r++)
            {
                var row = _rows[r];
                row.UpdateCellsStructure();
            }
        }

        public DataGridViewColumnCollection Columns => _columns;
        public DataGridViewRowCollection Rows => _rows;

        [Obsolete(NotImplementedWarning)] public DataGridViewRow RowTemplate { get; set; } = new();
        public event EventHandler? SelectionChanged;
        [Obsolete(NotImplementedWarning)] public event DataGridViewCellEventHandler? CellContentClick;
        [Obsolete(NotImplementedWarning)] public event DataGridViewCellEventHandler? CellDoubleClick;
        [Obsolete(NotImplementedWarning)] public event DataGridViewCellFormattingEventHandler? CellFormatting;
        public event DataGridViewCellValueEventHandler? CellValueNeeded;
        [Obsolete(NotImplementedWarning)] public DataGridViewCell? CurrentCell { get; set; }
        
        public bool VirtualMode 
        { 
            get => _virtualMode;
            set
            {
                if (_virtualMode != value)
                {
                    if (IsHandleCreated)
                        throw new InvalidOperationException("Cannot change VirtualMode after the handle has been created.");
                    _virtualMode = value;
                }
            }
        }
        
        public int RowCount
        {
            get
            {
                return VirtualMode ? _virtualRowCount : Rows.Count;
            }
            set
            {
                if (!VirtualMode) 
                    throw new InvalidOperationException("RowCount can only be set when VirtualMode is true.");
                
                _virtualRowCount = value;
                if (IsHandleCreated)
                {
                    NativeMethods.QTableWidget_SetRowCount(QtHandle, _virtualRowCount);
                }
            }
        }

        public int ColumnCount => Columns.Count;
        [Obsolete(NotImplementedWarning)] public bool AllowUserToAddRows { get; set; }
        [Obsolete(NotImplementedWarning)] public bool AllowUserToDeleteRows { get; set; }
        [Obsolete(NotImplementedWarning)] public Color BackgroundColor { get; set; }
        [Obsolete(NotImplementedWarning)] public BorderStyle BorderStyle { get; set; }
        [Obsolete(NotImplementedWarning)] public bool MultiSelect { get; set; }
        [Obsolete(NotImplementedWarning)] public DataGridViewCellStyle? DefaultCellStyle { get; set; }
        [Obsolete(NotImplementedWarning)] public DataGridViewCellStyle? ColumnHeadersDefaultCellStyle { get; set; }
        [Obsolete(NotImplementedWarning)] public bool ReadOnly { get; set; }
        [Obsolete(NotImplementedWarning)] public bool StandardTab { get; set; }
        [Obsolete(NotImplementedWarning)] public int RowHeadersWidth { get; set; }
        [Obsolete(NotImplementedWarning)] public DataGridViewColumnHeadersHeightSizeMode ColumnHeadersHeightSizeMode { get; set; }

        public DataGridViewSelectionMode SelectionMode
        {
            get => _selectionMode;
            set
            {
                if (_selectionMode != value)
                {
                    _selectionMode = value;
                    if (IsHandleCreated)
                    {
                        UpdateSelectionMode();
                    }
                }
            }
        }

        private void UpdateSelectionMode()
        {
            // Map WinForms SelectionMode to Qt SelectionBehavior
            // CellSelect = 0 -> SelectItems
            // FullRowSelect = 1 -> SelectRows
            // FullColumnSelect = 2 -> SelectColumns
            // RowHeaderSelect = 3 -> SelectRows (Qt doesn't distinguish)
            // ColumnHeaderSelect = 4 -> SelectColumns (Qt doesn't distinguish)
            int qtMode = _selectionMode switch
            {
                DataGridViewSelectionMode.CellSelect => 0, // SelectItems
                DataGridViewSelectionMode.FullRowSelect => 1, // SelectRows
                DataGridViewSelectionMode.FullColumnSelect => 2, // SelectColumns
                DataGridViewSelectionMode.RowHeaderSelect => 1, // SelectRows (approximation)
                DataGridViewSelectionMode.ColumnHeaderSelect => 2, // SelectColumns (approximation)
                _ => 1 // Default to SelectRows
            };
            NativeMethods.QTableWidget_SetSelectionBehavior(QtHandle, qtMode);
        }

        public void ClearSelection()
        {
            if (IsHandleCreated)
            {
                NativeMethods.QTableWidget_ClearSelection(QtHandle);
            }
        }

        internal DataGridViewColumn GetColumnByName(string name) => Columns.Single(x => x.Name == name);

        public DataGridViewCell this[int columnIndex, int rowIndex]
        {
            get
            {
                return Rows[rowIndex].Cells[columnIndex];
            }
            set
            {
                Rows[rowIndex].Cells[columnIndex] = value;
            }
        }

        public DataGridViewCell this[string columnName, int rowIndex]
        {
            get
            {
                return Rows[rowIndex].Cells[columnName];
            }
            set
            {
                Rows[rowIndex].Cells[columnName] = value;
            }
        }

        public DataGridViewSelectedCellCollection SelectedCells => _selectedCells;
        public DataGridViewSelectedRowCollection SelectedRows => _selectedRows;
        public DataGridViewSelectedColumnCollection SelectedColumns => _selectedColumns;

        public HitTestInfo HitTest(int x, int y) => throw new NotImplementedException();

        public class HitTestInfo 
        {
            public int ColumnIndex { get; set; }
            public int RowIndex { get; set; }
        }
    }
}
