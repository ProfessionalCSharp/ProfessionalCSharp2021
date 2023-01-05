namespace AsyncStreaming.Shared;

public record SomeData(string Text, int SomeDataId = default);

public record DeviceData(string Text, DateTime Time, int SomeDataId = default);
