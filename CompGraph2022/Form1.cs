using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompGraph2022
{
    public partial class Form1 : Form
    {
        private enum ShapeId
        {
            ROUTE, RUNNER
        }

        private Graphics _gr;

        private readonly Shape _route;
        private int _routeRotationDeg = 0;

        private readonly Shape _runner;
        private int _runnerRotationDeg = 0;
        private double _runnerEdgePosition = 0;
        private int _runnerCurrentEdge = 0;

        private int _indx = -1;

        private readonly int _controlSize = 5;
        private bool _moved = false;
        private bool _editCenter = false;
        private ShapeId _currentObject = ShapeId.ROUTE;
        private bool Animating { get { return timer1.Enabled; } set { timer1.Enabled = value; } }

        public Form1()
        {
            _route = new Shape(new List<Point>(new Point[] { new Point(100, 100), new Point(100, 300), new Point(300, 300), new Point(300, 100) }), new Point(200, 200), Pens.Red);
            _runner = new Shape(new List<Point>(new Point[] { new Point(200, 150), new Point(100, 250), new Point(250, 250) }), new Point(200, 200), Pens.Blue);
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Animating = !Animating;
            UpdateAnimationButtonText();
            if (!Animating)
            {
                _runnerCurrentEdge = 0;
                _runnerEdgePosition = 0;
                _runnerRotationDeg = 0;
                _routeRotationDeg = 0;
            }
            RedrawShapes();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _gr = CreateGraphics();
            _gr.Clear(Color.White);
            UpdateShapeButtonText();
            RedrawShapes();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int routeRotationSpeed = trackBar1.Value;
            _routeRotationDeg = NormalizeDegrees(_routeRotationDeg + routeRotationSpeed);
            int runnerRotationSpeed = trackBar1.Value;
            _runnerRotationDeg = NormalizeDegrees(_runnerRotationDeg + runnerRotationSpeed);
            double runnerMovementSpeed = 5;
            (Point a, Point b) = GetRouteEdgePoints(_runnerCurrentEdge);
            _runnerEdgePosition += runnerMovementSpeed / GetLineLength(a, b);
            FixRunnerPosition();
            RedrawShapes();
        }

        private int NormalizeDegrees(int deg)
        {
            return -180 + ((deg + 180) % 360);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            _currentObject = _currentObject == ShapeId.ROUTE ? ShapeId.RUNNER : ShapeId.ROUTE;
            UpdateShapeButtonText();
        }

        private void UpdateShapeButtonText()
        {
            button2.Text = _currentObject == ShapeId.ROUTE ? "Route" : "Runner";
        }

        private void UpdateAnimationButtonText()
        {
            button1.Text = Animating ? "Stop" : "Start";
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            _indx = -1;
            _moved = false;
            var point = new Point(e.X, e.Y);
            Shape shape = GetCurrentShape();
            if (e.Button == MouseButtons.Left)
            {
                _editCenter = false;
                List<Point> points = GetFixedShapeList(shape);
                for (int i = 0; i < points.Count; i++)
                    if (IsMouseInPointControl(e, points[i]))
                    {
                        _indx = i;
                    }
                if (_indx == -1)
                {
                    _gr.Clear(Color.White);
                    shape.Points.Add(point);
                    _indx = shape.Points.Count - 1;
                    RedrawShapes();
                }
            }
            else if (e.Button == MouseButtons.Right && !Animating)
            {
                _editCenter = true;
                shape.Center = point;
                RedrawShapes();
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            var point = new Point(e.X, e.Y);
            Shape shape = GetCurrentShape();
            if (!_editCenter && _indx != -1)
            {
                point = MoveAndRotatePoint(
                    point,
                    shape.Center,
                    - GetRotationDeg(shape),
                    shape.Center
                    );
                _moved = true;
                _gr.Clear(Color.White);
                List<Point> points = shape.Points;
                points[_indx] = point;
                RedrawShapes();
            }
            else if (_editCenter)
            {
                shape.Center = point;
                RedrawShapes();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_moved && _indx != -1)
            {
                List<Point> points = GetFixedShapeList(GetCurrentShape());
                points.RemoveAt(_indx);
            }
            _indx = -1;
            _editCenter = false;
            RedrawShapes();
        }

        private void RedrawShapes()
        {
            _gr.Clear(Color.White);

            var routePoints = new List<Point>(_route.Points);
            RotatePoints(routePoints, _route.Center, _routeRotationDeg);
            PaintShape(routePoints, _route.PreferredPen);

            var runnerPoints = new List<Point>(_runner.Points);
            Point runnerPosition = GetRunnerPosition();
            if (Animating)
            {
                MoveAndRotatePoints(runnerPoints, _runner.Center, _runnerRotationDeg, runnerPosition);
            }
            else
            {
                RotatePoints(_runner.Points, _runner.Center, _runnerRotationDeg);
            }
            PaintShape(runnerPoints, _runner.PreferredPen);

            if (!Animating)
            {
                PaintControl(_route.Center);
                PaintControl(_runner.Center);
            }
            else
            {
                PaintPosition(runnerPosition);
            }
        }

        private Point GetRunnerPosition()
        {
            FixRunnerPosition();
            (Point startPoint, Point endPoint) = GetRouteEdgePoints(_runnerCurrentEdge);
            //R = (1-t)*P + t*Q; 0 <= t <= 1       t=1/2  (P+Q)/2
            int x = (int)((1 - _runnerEdgePosition) * startPoint.X + _runnerEdgePosition * endPoint.X);
            int y = (int)((1 - _runnerEdgePosition) * startPoint.Y + _runnerEdgePosition * endPoint.Y);
            return new Point(x, y);
        }

        private (Point, Point) GetRouteEdgePoints(int edge)
        {
            List<Point> routePoints = GetFixedShapeList(_route);
            Point startPoint = routePoints[edge];
            Point endPoint = routePoints[(edge + 1) % routePoints.Count];
            return (startPoint, endPoint);
        }

        private void FixRunnerPosition()
        {
            if (_runnerEdgePosition > 1)
            {
                _runnerCurrentEdge++;
                _runnerEdgePosition = 0;
            }
            _runnerCurrentEdge %= _route.Points.Count;
        }

        private void RotatePoints(List<Point> points, Point center, int rotateDeg)
        {
            MoveAndRotatePoints(points, center, rotateDeg, center);
        }

        private double GetLineLength(Point a, Point b)
        {
            int x = b.X - a.X;
            int y = b.Y - a.Y;
            return Math.Sqrt(x * x + y * y);
        }

        private void MoveAndRotatePoints(List<Point> points, Point center, int rotateDeg, Point pos)
        {
            double fi = Math.PI * rotateDeg / 180.0;
            double cos = Math.Cos(fi);
            double sin = Math.Sin(fi);
            int dx = center.X;
            int dy = center.Y;
            int rx = pos.X;
            int ry = pos.Y;
            for (int i = 0; i < points.Count; i++)
            {
                Point pt = points[i];
                int x = (int)((pt.X - dx) * cos - (pt.Y - dy) * sin) + rx;
                int y = (int)((pt.X - dx) * sin + (pt.Y - dy) * cos) + ry;
                pt.X = x;
                pt.Y = y;
                points[i] = pt;
            }
        }

        private Point MoveAndRotatePoint(Point pt, Point center, int rotateDeg, Point pos)
        {
            double fi = Math.PI * rotateDeg / 180.0;
            double cos = Math.Cos(fi);
            double sin = Math.Sin(fi);
            int dx = center.X;
            int dy = center.Y;
            int rx = pos.X;
            int ry = pos.Y;
            int x = (int)((pt.X - dx) * cos - (pt.Y - dy) * sin) + rx;
            int y = (int)((pt.X - dx) * sin + (pt.Y - dy) * cos) + ry;
            return new Point(x, y);
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

        private List<Point> GetFixedShapeList(Shape shape)
        {
            int rotationDeg = GetRotationDeg(shape);
            var shapeList = new List<Point>(shape.Points);
            RotatePoints(shapeList, shape.Center, rotationDeg);
            return shapeList;
        }

        private ShapeId ToShapeId(Shape shape)
        {
            return shape == _route ? ShapeId.ROUTE : ShapeId.RUNNER;
        }

        private int GetRotationDeg(Shape shape)
        {
            return ToShapeId(shape) == ShapeId.ROUTE ? _routeRotationDeg : _runnerRotationDeg;
        }

        private Shape GetCurrentShape()
        {
            return _currentObject == ShapeId.ROUTE ? _route : _runner;
        }

        private bool IsMouseInPointControl(MouseEventArgs e, Point point)
        {
            return (e.X - _controlSize < point.X) && (point.X < e.X + _controlSize) &&
                        (e.Y - _controlSize < point.Y) && (point.Y < e.Y + _controlSize);
        }
    }
    class Shape
    {
        public List<Point> Points { set; get; }
        public Point Center { get; set; }
        public Pen PreferredPen { get; set; }

        public Shape(Pen preferredPen) : this(new List<Point>(), new Point(0, 0), preferredPen) { }
        public Shape(Point center, Pen preferredPen) : this(new List<Point>(), center, preferredPen) { }
        public Shape(List<Point> points, Point center, Pen preferredPen)
        {
            Points = points;
            Center = center;
            PreferredPen = preferredPen;
        }
    }
}
