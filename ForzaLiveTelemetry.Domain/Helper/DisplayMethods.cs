namespace ForzaLiveTelemetry.Domain.Helper;

public static class DisplayMethods
{
    public static string PrintFloat(float value, string format, bool showSign)
    {
        string str = Math.Abs(value).ToString(format);

        string computedStr = string.Create(str.Length, str, (span, value) =>
        {
            value.AsSpan().CopyTo(span);
            span[value.Length..].Fill(' ');
        });

        char sign = value >= 0 ? '+' : '-';

        return showSign ? sign + computedStr : ' ' + computedStr;
    }
}
