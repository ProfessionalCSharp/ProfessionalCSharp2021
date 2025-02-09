﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

using Windows.Foundation;

namespace Shapes;

public sealed partial class MainWindow : Window
{
    public MainWindow() => InitializeComponent();

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

        PathFigure figure = new()
        {
            StartPoint = _mouthPoints[index, 0],
            Segments = []
        };
        QuadraticBezierSegment segment1 = new()
        {
            Point1 = _mouthPoints[index, 1],
            Point2 = _mouthPoints[index, 2]
        };

        figure.Segments.Add(segment1);
        PathGeometry geometry = new()
        {
            Figures = []
        };
        geometry.Figures.Add(figure);

        mouth.Data = geometry;
        _laugh = !_laugh;
    }
}
