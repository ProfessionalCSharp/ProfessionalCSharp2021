using System;
using System.Threading.Tasks;
using static TrafficLight;


var previousLight = Red;
var currentLight = Red;
for (int i = 0; i < 10; i++)
{
    (currentLight, previousLight) = NextLightUsingTuples(currentLight, previousLight);
    Console.Write($"{currentLight} - ");
    await Task.Delay(100);
}
Console.WriteLine();

TrafficLightState currentLightState = new(AmberBlink, AmberBlink, 2000);

for (int i = 0; i < 20; i++)
{
    currentLightState = NextLightUsingRecords(currentLightState);
    Console.WriteLine($"{currentLightState.CurrentLight}, {currentLightState.Milliseconds}");
    await Task.Delay(currentLightState.Milliseconds);
}
Console.WriteLine();

// property pattern matching
(TrafficLight Current, TrafficLight Previous) NextLightUsingTuples(TrafficLight current, TrafficLight previous) =>
    (current, previous) switch
    {
        (Red, _) => (Amber, current),
        (Amber, Red) => (Green, current),
        (Green, _) => (Amber, current),
        (Amber, Green) => (Red, current),
        _ => throw new InvalidOperationException()
    };

// tuple pattern matching
TrafficLightState NextLightUsingRecords(TrafficLightState trafficLightState) =>
    trafficLightState switch
    {
        { CurrentLight: AmberBlink } => new TrafficLightState(Red, trafficLightState.PreviousLight, 3000),
        { CurrentLight: Red } => new TrafficLightState(Amber, trafficLightState.CurrentLight, 200),
        { CurrentLight: Amber, PreviousLight: Red} => new TrafficLightState(Green, trafficLightState.CurrentLight, 2000),
        { CurrentLight: Green } => new TrafficLightState(GreenBlink, trafficLightState.CurrentLight, 100, 1),
        { CurrentLight: GreenBlink, BlinkCount: < 3 } => trafficLightState with { BlinkCount = trafficLightState.BlinkCount + 1 },
        { CurrentLight: GreenBlink } => new TrafficLightState(Amber, trafficLightState.CurrentLight, 200),
        { CurrentLight: Amber, PreviousLight: GreenBlink } => new TrafficLightState(Red, trafficLightState.CurrentLight, 3000),
        _ => throw new InvalidOperationException()
    };

public enum TrafficLight
{
    Red,
    Amber,
    Green,
    GreenBlink,
    AmberBlink
}

public record TrafficLightState(TrafficLight CurrentLight, TrafficLight PreviousLight, int Milliseconds, int BlinkCount = 0);

