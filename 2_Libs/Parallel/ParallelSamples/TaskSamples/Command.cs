namespace TaskSamples;

public record class Command(string Option, string Text, Action Action);
