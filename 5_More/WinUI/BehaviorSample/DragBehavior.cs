using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.Xaml.Interactivity;

using Windows.Foundation;

namespace BehaviorSample;

public class DragBehavior : Behavior<FrameworkElement>
{
    private TranslateTransform _transform = new();
    private FrameworkElement? _parent;
    private Point _prevPoint;
    private long _pointerId;

    protected override void OnAttached()
    {
        base.OnAttached();

        AssociatedObject.RenderTransform = _transform;

        AssociatedObject.PointerPressed += (sender, e) =>
        {
            if (AssociatedObject.Parent is FrameworkElement parent)
            {
                _parent = parent;

                _prevPoint = e.GetCurrentPoint(_parent).Position;
                _parent.PointerMoved += (sender, e) =>
                {
                    if (e.Pointer.PointerId != _pointerId) return;

                    var pos = e.GetCurrentPoint(_parent).Position;
                    _transform.X += pos.X - _prevPoint.X;
                    _transform.Y += pos.Y - _prevPoint.Y;
                    _prevPoint = pos;
                };
                _pointerId = e.Pointer.PointerId;
            }
        };

        AssociatedObject.PointerReleased += (sender, e) =>
        {
            if (e.Pointer.PointerId != _pointerId) return;
            _pointerId = -1;
        };
    }

    protected override void OnDetaching()
    {
        _parent = null;
        _pointerId = -1;        
    }
}
