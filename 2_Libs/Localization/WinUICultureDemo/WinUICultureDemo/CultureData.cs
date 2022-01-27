using System.Globalization;

namespace WinUICultureDemo;

public record CultureData(CultureInfo CultureInfo)
{
    public IList<CultureData> SubCultures { get; } = new List<CultureData>();

    private double _numberSample = 9876543.21;
    public string NumberSample => _numberSample.ToString("N", CultureInfo);
    public string DateSample => DateTime.Today.ToString("D", CultureInfo);
    public string TimeSample => DateTime.Now.ToString("T", CultureInfo);
    private RegionInfo? _regionInfo;
    public RegionInfo? RegionInfo
    {
        get
        {
            try
            {
                return _regionInfo ??= new RegionInfo(CultureInfo.Name);
            }
            catch (ArgumentException)
            {
                // with some neutral cultures regions are not available
                return null;
            }
        }
    }
}
