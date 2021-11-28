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
        case TrafficLight.Green:
            return TrafficLight.Amber;
        case TrafficLight.Amber:
            return TrafficLight.Red;
        case TrafficLight.Red:
            return TrafficLight.Green;
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
        TrafficLight.Green => TrafficLight.Amber,
        TrafficLight.Amber => TrafficLight.Red,
        TrafficLight.Red => TrafficLight.Green,
        _ => throw new InvalidOperationException()
    };

enum TrafficLight
{
    Red,
    Amber,
    Green
}
