using Domain.Models.Enums;

namespace ApplicationAdmin._Utils;
public static class HelpersFn
{
    public static bool IsInRangeCategories(int value)
    {
        var values = Enum.GetValues(typeof(Categories)).Cast<int>().OrderBy(x => x);
        return value >= values.First() && value <= values.Last();
    }
}
