using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CompGraph2022;

namespace BarycentricCoordinates
{
    public partial class BaryForm : Form
    {
        private Graphics _gr;
        
        private readonly Shape _triangle;

        private Point _point = new Point(200, 200);

        private readonly int _controlSize = 5;

        private bool _movingTriangle = false;

        private bool _movingPoint = false;

        private int _indx = -1;

        public BaryForm()
        {
            _triangle = new Shape(new List<Point>(new Point[] { new Point(150, 150), new Point(100, 350), new Point(300, 100) }), new Point(0, 0), Pens.Blue);
            InitializeComponent();
        }

        private void BaryForm_Load(object sender, EventArgs e)
        {
            _gr = CreateGraphics();
            OnShapesUpdate();
        }

        private void OnShapesUpdate()
        {
            UpdatePointInTriangeLabel();
            RedrawShapes();
        }

        private void RedrawShapes()
        {
            _gr.Clear(Color.White);
            PaintShape(_triangle.Points, _triangle.PreferredPen);
            PaintPosition(_point);
        }

        private bool IsMouseInPointControl(MouseEventArgs e, Point point)
        {
            return (e.X - _controlSize < point.X) && (point.X < e.X + _controlSize) &&
                        (e.Y - _controlSize < point.Y) && (point.Y < e.Y + _controlSize);
        }

        private void UpdatePointInTriangeLabel()
        {
            OutputLabel.Text = IsPointInTriangle() ? "TRUE" : "FALSE";
        }

        private bool IsPointInTriangle()
        {
            Point p1 = _triangle.Points[0];
            Point p2 = _triangle.Points[1];
            Point p3 = _triangle.Points[2];
            Point p = _point;
            double det = (p2.Y - p3.Y) * (p1.X - p3.X) + (p3.X - p2.X) * (p1.Y - p3.Y);
            double lambda1 = ((p2.Y - p3.Y) * (p.X - p3.X) + (p3.X - p2.X) * (p.Y - p3.Y)) / det;
            double lambda2 = ((p3.Y - p1.Y) * (p.X - p3.X) + (p1.X - p3.X) * (p.Y - p3.Y)) / det;
            double lambda3 = 1 - lambda1 - lambda2;

            bool inside = (0 < lambda1) && (lambda1 < 1)
                && (0 < lambda2) && (lambda2 < 1)
                && (0 < lambda3) && (lambda3 < 1);

            bool onEdge = (0 <= lambda1) && (lambda1 <= 1)
                && (0 <= lambda2) && (lambda2 <= 1)
                && (0 <= lambda3) && (lambda3 <= 1)
                && (TestEquals(lambda1, 0) + TestEquals(lambda2, 0) + TestEquals(lambda3, 0) == 1);

            return inside || onEdge;
        }

        private int TestEquals(double a, double b)
        {
            return Math.Abs(a - b) < 0.00001 ? 1 : 0;
        }

        private void PaintShape(List<Point> points, Pen pen)
        {
            int count = points.Count;
            for (int i = 0; i < count; i++)
            {
                Point currentPoint = points[i], nextPoint = points[(i + 1) % count];
                PaintControl(currentPoint);
                _gr.DrawLine(pen, currentPoint, nextPoint);
            }
        }

        private void PaintPosition(Point point)
        {
            _gr.FillEllipse(Brushes.Aqua, point.X - _controlSize, point.Y - _controlSize, _controlSize * 2 + 1, _controlSize * 2 + 1);
        }

        private void PaintControl(Point point)
        {
            _gr.DrawRectangle(new Pen(Color.Green, 1), point.X - _controlSize, point.Y - _controlSize, _controlSize * 2 + 1, _controlSize * 2 + 1);
        }

        private void BaryForm_Paint(object sender, PaintEventArgs e)
        {
            RedrawShapes();
        }

        private void BaryForm_MouseDown(object sender, MouseEventArgs e)
        {
            _indx = -1;
            _movingTriangle = false;
            _movingPoint = false;
            var point = new Point(e.X, e.Y);
            if (e.Button == MouseButtons.Left)
            {
                _movingTriangle = true;
                List<Point> points = _triangle.Points;
                for (int i = 0; i < points.Count; i++)
                    if (IsMouseInPointControl(e, points[i]))
                    {
                        _indx = i;
                    }
            }
            else if (e.Button == MouseButtons.Right)
            {
                _movingPoint = true;
                _point = point;
                OnShapesUpdate();
            }
        }

        private void BaryForm_MouseMove(object sender, MouseEventArgs e)
        {
            var point = new Point(e.X, e.Y);
            if (_movingTriangle && _indx != -1)
            {
                List<Point> points = _triangle.Points;
                points[_indx] = point;
                OnShapesUpdate();
            }
            else if (_movingPoint)
            {
                _point = point;
                OnShapesUpdate();
            }
        }

        private void BaryForm_MouseUp(object sender, MouseEventArgs e)
        {
            _indx = -1;
            _movingTriangle = false;
            _movingPoint = false;
            OnShapesUpdate();
        }
    }
}
