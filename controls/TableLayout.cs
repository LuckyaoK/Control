using System.Drawing;
using System.Windows.Forms;

namespace CXPro001.controls
{
    public partial class TableLayout : TableLayoutPanel
    {
        public TableLayout()
        {
            // 防止闪屏
            this.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(this, true, null);
        }

        private Color borderColor = Color.Black;

        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }
        /// <summary>
        /// 绘制矩形
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellPaint(TableLayoutCellPaintEventArgs e)
        {
            //绘制边框
            base.OnCellPaint(e);
            Pen pp = new Pen(BorderColor);
            e.Graphics.DrawRectangle(pp, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.X + this.Width - 1, e.CellBounds.Y + this.Height - 1);
        }

    }
}
