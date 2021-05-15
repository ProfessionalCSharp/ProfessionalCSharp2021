using Microsoft.UI.Xaml.Media.Animation;
using System.Collections.Generic;
using System.Linq;

namespace Animations
{
    public class EasingFunctionsManager
    {
        private readonly static List<EasingFunctionBase> s_easingFunctions = new()
        {
            new BackEase(),
            new SineEase(),
            new BounceEase(),
            new CircleEase(),
            new CubicEase(),
            new ElasticEase(),
            new ExponentialEase(),
            new PowerEase(),
            new QuadraticEase(),
            new QuinticEase()
        };

        public IEnumerable<EasingFunctionModel> EasingFunctionModels =>
            s_easingFunctions.Select(f => new EasingFunctionModel(f));
    }
}
