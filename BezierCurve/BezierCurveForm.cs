using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BezierCurve
{
    public partial class BezierCurveForm : Form
    {
        private Graphics _gr;

        private IList<Point> _points;

        private int _indx = -1;

        private readonly int _controlSize = 5;

        public BezierCurveForm()
        {
            _points = new List<Point> { new Point(200, 200), new Point(200, 300), new Point(300, 300) };
            InitializeComponent();
        }

        private void BezierCurveForm_Load(object sender, EventArgs e)
        {
            _gr = CreateGraphics();
            RedrawShapes();
        }

        private void RedrawShapes()
        {
            _gr.Clear(Color.White);
            PaintControls(_points);
            PaintCurve(_points[0], _points[1], _points[2], new Pen(Color.Blue));
            PaintBorders(_points, new Pen(Color.Black));
        }

        private void PaintBorders(IList<Point> points, Pen pen)
        {
            _gr.DrawLines(pen, points.ToArray());
        }

        private void PaintControl(Point point)
        {
            _gr.DrawRectangle(new Pen(Color.Green, 1), point.X - _controlSize, point.Y - _controlSize, _controlSize * 2 + 1, _controlSize * 2 + 1);
        }

        private void PaintControls(IList<Point> points)
        {
            foreach (Point point in points)
            {
                PaintControl(point);
            }
        }

        private void PaintCurve(Point start, Point middle, Point end, Pen pen)
        {
            float x1 = start.X, y1 = start.Y;
            float x2 = middle.X, y2 = middle.Y;
            float x3 = end.X, y3 = end.Y;
            int steps = Math.Max(Math.Abs(end.X - start.X), Math.Abs(end.Y - start.Y)) / 2;
            Point prevPoint = start;
            for (int i = 0; i <= steps; i++)
            {
                float t = (float)i / steps;
                if (!(0 <= t && t <= 1)) break;
                float x = (1 - t) * (1 - t) * x1 + 2 * t * (1 - t) * x2 + t * t * x3;
                float y = (1 - t) * (1 - t) * y1 + 2 * t * (1 - t) * y2 + t * t * y3;
                var p = new Point((int)x, (int)y);
                _gr.DrawLine(pen, prevPoint, p);
                prevPoint = p;
            }
        }
        private bool IsMouseInPointControl(MouseEventArgs e, Point point)
        {
            return (e.X - _controlSize < point.X) && (point.X < e.X + _controlSize) &&
                        (e.Y - _controlSize < point.Y) && (point.Y < e.Y + _controlSize);
        }

        private void BezierCurveForm_Paint(object sender, PaintEventArgs e)
        {
            RedrawShapes();
        }

        private void BezierCurveForm_MouseDown(object sender, MouseEventArgs e)
        {
            _indx = -1;
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < _points.Count; i++)
                    if (IsMouseInPointControl(e, _points[i]))
                    {
                        _indx = i;
                    }
            }
        }

        private void BezierCurveForm_MouseMove(object sender, MouseEventArgs e)
        {
            var point = new Point(e.X, e.Y);
            if (_indx != -1)
            {
                _points[_indx] = point;
                RedrawShapes();
            }
        }

        private void BezierCurveForm_MouseUp(object sender, MouseEventArgs e)
        {
            _indx = -1;
            RedrawShapes();
        }
    }
}
