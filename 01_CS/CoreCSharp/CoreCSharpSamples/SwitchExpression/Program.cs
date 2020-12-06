using System;

string? input = Console.ReadLine();

string result = input switch
{
    "one" => "the input has the value one",
    "two" or "three" => "the input has the value two or three",
    _ => "any other value"
};

Console.WriteLine(result);

// traffic light with the switch statement

var light = TrafficLight.Red;

for (int i = 0; i < 10; i++)
{
    Console.WriteLine(light);
    light = NextLightClassic(light);
}

TrafficLight NextLightClassic(TrafficLight light) 
{
    switch (light)
    {
        case TrafficLight.Red:
            return TrafficLight.Amber;
        case TrafficLight.Amber:
            return TrafficLight.Green;
        case TrafficLight.Green:
            return TrafficLight.Red;
        default:
            throw new InvalidOperationException();            
    }
}


// traffic light with the swith expression

for (int i = 0; i < 10; i++)
{
    Console.WriteLine(light);
    light = NextLight(light);
}

TrafficLight NextLight(TrafficLight light) =>
    light switch
    {
        TrafficLight.Red => TrafficLight.Amber,
        TrafficLight.Amber => TrafficLight.Green,
        TrafficLight.Green => TrafficLight.Red,
        _ => throw new InvalidOperationException()
    };

enum TrafficLight
{
    Red,
    Amber,
    Green
}
