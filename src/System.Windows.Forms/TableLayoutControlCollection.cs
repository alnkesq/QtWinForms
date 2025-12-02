using System;

namespace System.Windows.Forms
{
    public class TableLayoutControlCollection : ControlCollection
    {
        private readonly TableLayoutPanel _tableLayoutPanel;

        public TableLayoutControlCollection(TableLayoutPanel owner) : base(owner)
        {
            _tableLayoutPanel = owner;
        }

        public void Add(Control control, int column, int row)
        {
            // Add the control to the base collection
            base.Add(control);

            // Set the cell position
            _tableLayoutPanel.SetCellPosition(control, new TableLayoutPanelCellPosition(row, column));
        }

        protected override void PerformQtParenting(Control item)
        {
            // For TableLayoutPanel, we don't use the standard parenting
            // because QGridLayout handles the parenting and positioning
            // The actual Qt parenting is done in TableLayoutPanel.CreateChildren()
            
            // Just ensure the control is created
            item.CreateControl();
        }
    }
}
