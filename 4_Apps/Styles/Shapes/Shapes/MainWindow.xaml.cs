using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Shapes
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void OnChangeShape() => SetMouth();

        private readonly Point[,] _mouthPoints = new Point[2, 3]
        {
            { new(40, 74), new(57, 95), new(80, 74) },
            { new(40, 82), new(57, 65), new(80, 82) }
        };

        private bool _laugh = false;
        public void SetMouth()
        {
            int index = _laugh ? 0 : 1;

            PathFigure figure = new() { StartPoint = _mouthPoints[index, 0] };
            figure.Segments = new PathSegmentCollection();
            QuadraticBezierSegment segment1 = new()
            {
                Point1 = _mouthPoints[index, 1],
                Point2 = _mouthPoints[index, 2]
            };

            figure.Segments.Add(segment1);
            PathGeometry geometry = new();
            geometry.Figures = new PathFigureCollection();
            geometry.Figures.Add(figure);

            mouth.Data = geometry;
            _laugh = !_laugh;
        }
    }
}
