using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace System.Windows.Forms
{
    public class TableLayoutPanel : Control
    {
        private IntPtr _gridLayout;
        private int _rowCount = 0;
        private int _columnCount = 0;
        private readonly Dictionary<Control, (int row, int column, int rowSpan, int columnSpan)> _controlPositions = new();

        public TableLayoutPanel()
        {
            base.Controls = new TableLayoutControlCollection(this);
            RowStyles = new TableLayoutRowStyleCollection(this);
            ColumnStyles = new TableLayoutColumnStyleCollection(this);
            Size = new Size(200, 100);
        }

        public new TableLayoutControlCollection Controls => (TableLayoutControlCollection)base.Controls;

        public TableLayoutRowStyleCollection RowStyles { get; }
        public TableLayoutColumnStyleCollection ColumnStyles { get; }

        public int RowCount
        {
            get => _rowCount;
            set
            {
                _rowCount = value;
                ApplyStyles();
            }
        }

        public int ColumnCount
        {
            get => _columnCount;
            set
            {
                _columnCount = value;
                ApplyStyles();
            }
        }

        protected override void CreateHandle()
        {
            QtHandle = NativeMethods.QWidget_Create(IntPtr.Zero);
            _gridLayout = NativeMethods.QGridLayout_Create(QtHandle);
            SetCommonProperties();
            ApplyStyles();
            CreateChildren();
        }

        protected new void CreateChildren()
        {
            EnsureIsQWidget();

            // Add all controls to the grid layout based on their stored positions
            foreach (var child in Controls)
            {
                child.CreateControl();

                if (_controlPositions.TryGetValue(child, out var position))
                {
                    NativeMethods.QGridLayout_AddWidget(_gridLayout, child.QtHandle, 
                        position.row, position.column, position.rowSpan, position.columnSpan);
                }
                else
                {
                    // Default: add to next available cell
                    int totalCells = _rowCount * _columnCount;
                    int cellIndex = Controls.IndexOf(child);
                    if (_columnCount > 0)
                    {
                        int row = cellIndex / _columnCount;
                        int col = cellIndex % _columnCount;
                        NativeMethods.QGridLayout_AddWidget(_gridLayout, child.QtHandle, row, col, 1, 1);
                    }
                }

                // QGridLayout manages visibility, so we need to show the widget
                if (child.Visible)
                {
                    NativeMethods.QWidget_Show(child.QtHandle);
                }
            }
        }

        internal void ApplyStyles()
        {
            if (!IsHandleCreated || _gridLayout == IntPtr.Zero)
                return;

            // Apply row styles
            for (int i = 0; i < RowStyles.Count; i++)
            {
                var style = (RowStyle)RowStyles[i];
                switch (style.SizeType)
                {
                    case SizeType.Absolute:
                        NativeMethods.QGridLayout_SetRowMinimumHeight(_gridLayout, i, (int)style.Height);
                        NativeMethods.QGridLayout_SetRowStretch(_gridLayout, i, 0);
                        break;
                    case SizeType.Percent:
                        NativeMethods.QGridLayout_SetRowStretch(_gridLayout, i, (int)style.Height);
                        break;
                    case SizeType.AutoSize:
                        NativeMethods.QGridLayout_SetRowStretch(_gridLayout, i, 0);
                        NativeMethods.QGridLayout_SetRowMinimumHeight(_gridLayout, i, 0);
                        break;
                }
            }

            // Apply column styles
            for (int i = 0; i < ColumnStyles.Count; i++)
            {
                var style = (ColumnStyle)ColumnStyles[i];
                switch (style.SizeType)
                {
                    case SizeType.Absolute:
                        NativeMethods.QGridLayout_SetColumnMinimumWidth(_gridLayout, i, (int)style.Width);
                        NativeMethods.QGridLayout_SetColumnStretch(_gridLayout, i, 0);
                        break;
                    case SizeType.Percent:
                        NativeMethods.QGridLayout_SetColumnStretch(_gridLayout, i, (int)style.Width);
                        break;
                    case SizeType.AutoSize:
                        NativeMethods.QGridLayout_SetColumnStretch(_gridLayout, i, 0);
                        NativeMethods.QGridLayout_SetColumnMinimumWidth(_gridLayout, i, 0);
                        break;
                }
            }
        }

        public void SetColumn(Control control, int column)
        {
            if (!_controlPositions.TryGetValue(control, out var position))
            {
                position = (0, column, 1, 1);
            }
            else
            {
                position.column = column;
            }
            _controlPositions[control] = position;

            if (IsHandleCreated && control.IsHandleCreated)
            {
                NativeMethods.QGridLayout_AddWidget(_gridLayout, control.QtHandle,
                    position.row, position.column, position.rowSpan, position.columnSpan);
            }
        }

        public void SetRow(Control control, int row)
        {
            if (!_controlPositions.TryGetValue(control, out var position))
            {
                position = (row, 0, 1, 1);
            }
            else
            {
                position.row = row;
            }
            _controlPositions[control] = position;

            if (IsHandleCreated && control.IsHandleCreated)
            {
                NativeMethods.QGridLayout_AddWidget(_gridLayout, control.QtHandle,
                    position.row, position.column, position.rowSpan, position.columnSpan);
            }
        }

        public void SetColumnSpan(Control control, int value)
        {
            if (!_controlPositions.TryGetValue(control, out var position))
            {
                position = (0, 0, 1, value);
            }
            else
            {
                position.columnSpan = value;
            }
            _controlPositions[control] = position;

            if (IsHandleCreated && control.IsHandleCreated)
            {
                NativeMethods.QGridLayout_AddWidget(_gridLayout, control.QtHandle,
                    position.row, position.column, position.rowSpan, position.columnSpan);
            }
        }

        public void SetRowSpan(Control control, int value)
        {
            if (!_controlPositions.TryGetValue(control, out var position))
            {
                position = (0, 0, value, 1);
            }
            else
            {
                position.rowSpan = value;
            }
            _controlPositions[control] = position;

            if (IsHandleCreated && control.IsHandleCreated)
            {
                NativeMethods.QGridLayout_AddWidget(_gridLayout, control.QtHandle,
                    position.row, position.column, position.rowSpan, position.columnSpan);
            }
        }

        public void SetCellPosition(Control control, TableLayoutPanelCellPosition position)
        {
            if (!_controlPositions.TryGetValue(control, out var currentPosition))
            {
                currentPosition = (position.Row, position.Column, 1, 1);
            }
            else
            {
                currentPosition.row = position.Row;
                currentPosition.column = position.Column;
            }
            _controlPositions[control] = currentPosition;

            if (IsHandleCreated && control.IsHandleCreated)
            {
                NativeMethods.QGridLayout_AddWidget(_gridLayout, control.QtHandle,
                    currentPosition.row, currentPosition.column, currentPosition.rowSpan, currentPosition.columnSpan);
            }
        }

        public TableLayoutPanelCellPosition GetCellPosition(Control control)
        {
            if (_controlPositions.TryGetValue(control, out var position))
            {
                return new TableLayoutPanelCellPosition(position.row, position.column);
            }
            return new TableLayoutPanelCellPosition(-1, -1);
        }

        public int GetColumn(Control control)
        {
            if (_controlPositions.TryGetValue(control, out var position))
            {
                return position.column;
            }
            return -1;
        }

        public int GetRow(Control control)
        {
            if (_controlPositions.TryGetValue(control, out var position))
            {
                return position.row;
            }
            return -1;
        }

        public int GetColumnSpan(Control control)
        {
            if (_controlPositions.TryGetValue(control, out var position))
            {
                return position.columnSpan;
            }
            return 1;
        }

        public int GetRowSpan(Control control)
        {
            if (_controlPositions.TryGetValue(control, out var position))
            {
                return position.rowSpan;
            }
            return 1;
        }

        public override void PerformLayout()
        {
            if (!IsHandleCreated) return;

            // Is this necessary?
            //foreach (var child in this.Controls)
            //{
            //    child.PerformLayout();
            //}
        }
    }
}
