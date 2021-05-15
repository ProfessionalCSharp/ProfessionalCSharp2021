using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Animations
{
    public sealed partial class EasingFunctionsControl : UserControl
    {
        private readonly EasingFunctionsManager _easingFunctions = new();
        private const int AnimationTimeSeconds = 6;

        public EasingFunctionsControl()
        {
            InitializeComponent();
            foreach (var easingFunctionModel in _easingFunctions.EasingFunctionModels)
            {
                comboEasingFunctions.Items.Add(easingFunctionModel);
            }
        }

        private EasingMode GetEasingMode()
        {
            if (easingModeIn.IsChecked == true) return EasingMode.EaseIn;
            else if (easingModeOut.IsChecked == true) return EasingMode.EaseOut;
            else return EasingMode.EaseInOut;
        }

        private void OnStartAnimation(object sender, RoutedEventArgs e)
        {
            if (comboEasingFunctions.SelectedItem is EasingFunctionModel easingFunctionModel)
            {
                EasingFunctionBase easingFunction = easingFunctionModel.EasingFunction;
                easingFunction.EasingMode = GetEasingMode();
                StartAnimation(easingFunction);
            }
        }

        private void StartAnimation(EasingFunctionBase easingFunction)
        {
            // show the chart
            chartControl.Draw(easingFunction);

            // animation
            Storyboard storyboard = new();
            DoubleAnimation ellipseMove = new();
            ellipseMove.EasingFunction = easingFunction;
            ellipseMove.Duration = new Duration(TimeSpan.FromSeconds(AnimationTimeSeconds));
            ellipseMove.From = 0;
            ellipseMove.To = 460;
            
            Storyboard.SetTarget(ellipseMove, translate1);
           
            Storyboard.SetTargetProperty(ellipseMove, "X");
            ellipseMove.BeginTime = TimeSpan.FromSeconds(0.5); // start animation in 0.5 seconds
            ellipseMove.FillBehavior = FillBehavior.HoldEnd; // keep position after animation

            storyboard.Children.Add(ellipseMove);
            storyboard.Begin();
        }
    }
}
