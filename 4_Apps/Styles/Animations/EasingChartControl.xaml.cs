using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using XamlShapes = Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;

namespace Animations;

public sealed partial class EasingChartControl : UserControl
{
    private const double SamplingInterval = 0.01;
    public EasingChartControl() => InitializeComponent();

    public void Draw(EasingFunctionBase easingFunction)
    {
        canvas1.Children.Clear();

        PathSegmentCollection pathSegments = new();

        for (double i = 0; i < 1; i += SamplingInterval)
        {
            double x = i * canvas1.Width;
            double y = easingFunction.Ease(i) * canvas1.Height;

            LineSegment segment = new();
            segment.Point = new Point(x, y);

            pathSegments.Add(segment);
        }

        XamlShapes.Path p = new();
        p.Stroke = new SolidColorBrush(Colors.Black);
        p.StrokeThickness = 3;
        PathFigureCollection figures = new();
        figures.Add(new PathFigure { Segments = pathSegments });
        p.Data = new PathGeometry { Figures = figures };
        canvas1.Children.Add(p);
    }
}
