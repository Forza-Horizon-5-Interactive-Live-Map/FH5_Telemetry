namespace ForzaDynamicMapApi.Helper;

public static class DisplayMethods
{
    public static string PrintFloat(float value, string format, bool showSign)
    {
        var str = Math.Abs(value).ToString(format);

        var computedStr = string.Create(str.Length, str, (span, value) =>
        {
            value.AsSpan().CopyTo(span);
            span[value.Length..].Fill(' ');
        });

        var sign = value >= 0 ? '+' : '-';

        return showSign ? sign + computedStr : ' ' + computedStr;
    }
}
