using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace ThreeDimentionalDemo
{
    public partial class ThreeeDimentionalForm : Form
    {
        private Image _buffer;

        private Image _stage;

        private IList<SceneObject> _objs = new List<SceneObject>();

        private readonly ScenePosition _camera = new ScenePosition(
            new Point3Dd(172, 160, 0),
            Utils.ConvertDegreesToRadians(72),
            Utils.ConvertDegreesToRadians(-45),
            Utils.ConvertDegreesToRadians(12)
        );

        //private readonly ScenePosition _camera = new ScenePosition(
        //    new Point3Dd(50, 26, -27),
        //    Utils.ConvertDegreesToRadians(-72),
        //    Utils.ConvertDegreesToRadians(45),
        //    Utils.ConvertDegreesToRadians(-12)
        //);

        public ThreeeDimentionalForm()
        {
            var axisX = new SceneObject
            {
                Shape3D = new Shape3D(new List<Polygon3D>
                {
                    new Polygon3D(new Point3Dd[] { new Point3Dd(0, 0, 0), new Point3Dd(1, 0, 0) }),
                }, new Point3Dd()),
                WirePen = new Pen(Color.Red),
                Sx = 4,
                Sy = 4,
                Sz = 4,
            };
            //_objs.Add(axisX);
            var axisY = new SceneObject
            {
                Shape3D = new Shape3D(new List<Polygon3D>
                {
                    new Polygon3D(new Point3Dd[] { new Point3Dd(0, 0, 0), new Point3Dd(0, 1, 0) }),
                }, new Point3Dd()),
                WirePen = new Pen(Color.Green),
                Sx = 4,
                Sy = 4,
                Sz = 4,
            };
            //_objs.Add(axisY);
            var axisZ = new SceneObject
            {
                Shape3D = new Shape3D(new List<Polygon3D>
                {
                    new Polygon3D(new Point3Dd[] { new Point3Dd(0, 0, 0), new Point3Dd(0, 0, 1) }),
                }, new Point3Dd()),
                WirePen = new Pen(Color.Aqua),
                Sx = 4,
                Sy = 4,
                Sz = 4,
            };
            //_objs.Add(axisZ);

            var cube = new SceneObject
            {
                Shape3D = new Shape3D(new List<Polygon3D>
                {
                    new Polygon3D(new Point3Dd[] { new Point3Dd(0, 0, 0), new Point3Dd(1, 0, 0), new Point3Dd(1, 1, 0), new Point3Dd(0, 1, 0) }),
                    new Polygon3D(new Point3Dd[] { new Point3Dd(0, 0, 0), new Point3Dd(0, 1, 0), new Point3Dd(0, 1, 1), new Point3Dd(0, 0, 1) }),
                    new Polygon3D(new Point3Dd[] { new Point3Dd(0, 0, 0), new Point3Dd(1, 0, 0), new Point3Dd(1, 0, 1), new Point3Dd(0, 0, 1) }),
                    new Polygon3D(new Point3Dd[] { new Point3Dd(1, 0, 0), new Point3Dd(1, 1, 0), new Point3Dd(1, 1, 1), new Point3Dd(1, 0, 1) }),
                    new Polygon3D(new Point3Dd[] { new Point3Dd(0, 1, 0), new Point3Dd(1, 1, 0), new Point3Dd(1, 1, 1), new Point3Dd(0, 1, 1) }),
                    new Polygon3D(new Point3Dd[] { new Point3Dd(0, 0, 1), new Point3Dd(1, 0, 1), new Point3Dd(1, 1, 1), new Point3Dd(0, 1, 1) }),
                }, new Point3Dd(0.5, 0.5, 0.5)),
                WirePen = new Pen(Color.Blue),
                Position = new ScenePosition(new Point3Dd(-1, -1, -1), 0, 0, 0),
                Sx = 2,
                Sy = 2,
                Sz = 2
            };
            _objs.Add(cube);

            InitializeComponent();
        }

        private void InitializeStage()
        {
            Size size = new Size(ImageScene.Width, ImageScene.Height);
            _buffer = new Bitmap(size.Width, size.Height);
            _stage = new Bitmap(size.Width, size.Height);
            CameraX.Value = (decimal)_camera.Coords.X;
            CameraY.Value = (decimal)_camera.Coords.Y;
            CameraZ.Value = (decimal)_camera.Coords.Z;
            CameraRx.Value = (decimal)Utils.ConvertRadiansToDegrees(_camera.Rx);
            CameraRy.Value = (decimal)Utils.ConvertRadiansToDegrees(_camera.Ry);
            CameraRz.Value = (decimal)Utils.ConvertRadiansToDegrees(_camera.Rz);
        }

        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            _objs[0].Position.Rx += Utils.ConvertDegreesToRadians((float)XSpeedUpDown.Value);
            _objs[0].Position.Ry += Utils.ConvertDegreesToRadians((float)YSpeedUpDown.Value);
            _objs[0].Position.Rz += Utils.ConvertDegreesToRadians((float)ZSpeedUpDown.Value);
            FullRenderScene();
        }

        private void ThreeeDimentionalForm_Load(object sender, EventArgs e)
        {
            InitializeStage();
            RenderTimer.Enabled = true;
        }

        private void ThreeeDimentionalForm_Paint(object sender, PaintEventArgs e)
        {
            FullRenderScene();
        }

        private void RenderFrame(Image image)
        {
            using (Graphics gr = Graphics.FromImage(image))
            {
                gr.Clear(Color.White);
                ScenePosition c = _camera;
                var rp = new RenderProps
                {
                    Dx = c.Coords.X,
                    Dy = c.Coords.Y,
                    Dz = c.Coords.Z,
                    Sx = 30,
                    Sy = 30,
                    Sz = 30,
                    Rx = c.Rx,
                    Ry = c.Ry,
                    Rz = c.Rz,
                };
                Matrix<double> cameraMatrix = rp.GetMatrix();
                foreach (SceneObject obj in _objs)
                {
                    Shape3D shape = obj.Shape3D;
                    Pen pen = obj.WirePen;
                    var rpObj = new RenderProps
                    {
                        Dx = obj.Position.Coords.X + c.Coords.X,
                        Dy = obj.Position.Coords.Y + c.Coords.Y,
                        Dz = obj.Position.Coords.Z + c.Coords.Z,
                        Sx = obj.Sx * 30,
                        Sy = obj.Sy * 30,
                        Sz = obj.Sz * 30,
                        Rx = obj.Position.Rx + c.Rx,
                        Ry = obj.Position.Ry + c.Ry,
                        Rz = obj.Position.Rz + c.Rz,
                        Px = -obj.Shape3D.Center.X,
                        Py = -obj.Shape3D.Center.Y,
                        Pz = -obj.Shape3D.Center.Z,
                    };
                    Matrix<double> objMatrix = rpObj.GetMatrix();
                    Matrix<double> transformMatrix = objMatrix;
                    Console.WriteLine(cameraMatrix.ToString());
                    foreach (Polygon3D polygon in shape.Polygon3Ds)
                    {
                        int pointsCount = polygon.Points.Length;
                        PointF[] points = new PointF[pointsCount + 1];
                        for (int i = 0; i < pointsCount; i++)
                        {
                            Point3Dd p = polygon.Points[i];
                            Vector<double> v = Vector<double>.Build.DenseOfArray(new double[] { p.X, p.Y, p.Z, 1 });
                            v = transformMatrix.LeftMultiply(v);
                            points[i] = new PointF((float)v[0], (float)v[1]);
                            Console.WriteLine($"#{(float)v[0]}, #{(float)v[1]}");
                        }
                        points[pointsCount] = points[0];
                        gr.DrawLines(pen, points);
                    }
                }
            }
        }

        private void FullRenderScene()
        {
            RenderFrame(_buffer);
            (_stage, _buffer) = (_buffer, _stage);
            ImageScene.Image = _stage;
            ImageScene.ClientSize = _stage.Size;
        }

        private void CameraX_ValueChanged(object sender, EventArgs e)
        {
            _camera.Coords.X = (double) CameraX.Value;
            FullRenderScene();
        }

        private void CameraY_ValueChanged(object sender, EventArgs e)
        {
            _camera.Coords.Y = (double)CameraY.Value;
            FullRenderScene();
        }

        private void CameraZ_ValueChanged(object sender, EventArgs e)
        {
            _camera.Coords.Z = (double)CameraZ.Value;
            FullRenderScene();
        }

        private void CameraRx_ValueChanged(object sender, EventArgs e)
        {
            _camera.Rx = Utils.ConvertDegreesToRadians((double)CameraRx.Value);
            FullRenderScene();
        }

        private void CameraRy_ValueChanged(object sender, EventArgs e)
        {
            _camera.Ry = Utils.ConvertDegreesToRadians((double)CameraRy.Value);
            FullRenderScene();
        }

        private void CameraRz_ValueChanged(object sender, EventArgs e)
        {
            _camera.Rz = Utils.ConvertDegreesToRadians((double)CameraRz.Value);
            FullRenderScene();
        }
    }

    public static class Utils
    {
        public static double ConvertDegreesToRadians(double degrees)
        {
            return (Math.PI / 180) * degrees;
        }

        public static double ConvertRadiansToDegrees(double degrees)
        {
            return (180 / Math.PI) * degrees;
        }
    }

    public class RenderProps
    {
        public double Sx { get; set; } = 1;
        public double Sy { get; set; } = 1;
        public double Sz { get; set; } = 1;
        public double Dx { get; set; } = 0;
        public double Dy { get; set; } = 0;
        public double Dz { get; set; } = 0;
        public double Px { get; set; } = 0;
        public double Py { get; set; } = 0;
        public double Pz { get; set; } = 0;
        public double Rx { get; set; } = 0;
        public double Ry { get; set; } = 0;
        public double Rz { get; set; } = 0;

        // http://compgraph.tpu.ru/3d.htm
        public Matrix<double> GetMatrix()
        {
            var pMatrix = Matrix<double>.Build.DenseOfArray(new double[,] {
                { 1,                0,              0,              0 },
                { 0,                1,              0,              0 },
                { 0,                0,              1,              0 },
                { Px,               Py,             Pz,             1 }
            });
            var rxMatrix = Matrix<double>.Build.DenseOfArray(new double[,] {
                { 1,                0,              0,              0  },
                { 0,                Math.Cos(Rx),   Math.Sin(Rx),   0  },
                { 0,                -Math.Sin(Rx),  Math.Cos(Rx),   0  },
                { 0,                0,              0,              1  }
            });
            var ryMatrix = Matrix<double>.Build.DenseOfArray(new double[,] {
                { Math.Cos(Ry),     0,              -Math.Sin(Ry),  0  },
                { 0,                1,              0,              0  },
                { Math.Sin(Ry),     0,              Math.Cos(Ry),   0  },
                { 0,                0,              0,              1  }
            });
            var rzMatrix = Matrix<double>.Build.DenseOfArray(new double[,] {
                { Math.Cos(Rz),     Math.Sin(Rz),   0,              0  },
                { -Math.Sin(Rz),    Math.Cos(Rz),   0,              0  },
                { 0,                0,              1,              0  },
                { 0,                0,              0,              1  }
            });
            var sMatrix = Matrix<double>.Build.DenseOfArray(new double[,] {
                { Sx,               0,              0,              0 },
                { 0,                Sy,             0,              0 },
                { 0,                0,              Sz,             0 },
                { 0,                0,              0,              1 }
            });
            var dMatrix = Matrix<double>.Build.DenseOfArray(new double[,] {
                { 1,                0,              0,              0 },
                { 0,                1,              0,              0 },
                { 0,                0,              1,              0 },
                { Dx,               Dy,             Dz,             1  }
            });
            return pMatrix.Multiply(sMatrix).Multiply(rxMatrix).Multiply(ryMatrix).Multiply(rzMatrix).Multiply(dMatrix);
        }
    }
    public class Point3Dd
    {
        public double X { get; set; } = 0;

        public double Y { get; set; } = 0;

        public double Z { get; set; } = 0;

        public Point3Dd()
        {

        }

        public Point3Dd(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public class Polygon3D
    {
        public Point3Dd[] Points { get; set; }

        public Polygon3D(Point3Dd[] point3Ds)
        {
            Points = point3Ds;
        }
    }

    public class Shape3D
    {
        public IList<Polygon3D> Polygon3Ds { get; set; }
        public Point3Dd Center { get; set; }

        public Shape3D(IList<Polygon3D> polygon3Ds, Point3Dd center)
        {
            Polygon3Ds = polygon3Ds;
            Center = center;
        }
    }

    public class ScenePosition
    {
        public Point3Dd Coords { get; set; } = new Point3Dd();

        public double Rx { get; set; } = 0;

        public double Ry { get; set; } = 0;

        public double Rz { get; set; } = 0;

        public ScenePosition()
        {
        }

        public ScenePosition(Point3Dd coords, double rx, double ry, double rz)
        {
            Coords = coords;
            Rx = rx;
            Ry = ry;
            Rz = rz;
        }
    }

    public class SceneObject
    {
        public Shape3D Shape3D { get; set; } = new Shape3D(new List<Polygon3D>(), new Point3Dd());

        public Pen WirePen { get; set; } = new Pen(Color.Red);

        public ScenePosition Position { get; set; } = new ScenePosition();

        public double Sx { get; set; } = 1;

        public double Sy { get; set; } = 1;

        public double Sz { get; set; } = 1;
    }
}
