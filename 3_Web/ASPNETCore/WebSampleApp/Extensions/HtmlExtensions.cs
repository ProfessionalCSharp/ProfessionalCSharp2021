namespace WebSampleApp.Extensions;

public static class HtmlExtensions
{
    public static string Div(this string value) =>
        $"<div>{value}</div>";

    public static string Span(this string value) =>
      $"<span>{value}</span>";

    public static string Div(this string key, string value) =>
      $"{key.Span()}:&nbsp;{value.Span()}".Div();

    public static string Li(this string value) =>
      $@"<li>{value}</li>";

    public static string Li(this string value, string url) =>
      $@"<li><a href=""{url}"">{value}</a></li>";

    public static string Ul(this string value) =>
      $"<ul>{value}</ul>";

    public static string HeadingX(this string value, int x) =>
        $"<h{x}>{value}</h{x}>";

    public static string HtmlDocument(this string content, string title) =>
        $"""
        <!DOCTYPE HTML>
            <head><meta charset="utf-8"><title>{title}</title></head>
        <body>
            {content}
        </body>
        """;
}
