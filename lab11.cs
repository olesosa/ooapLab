using HtmlAgilityPack;

namespace ooap11;

public class IphoneParser
{
    private readonly HtmlWeb _web;

    public IphoneParser(HtmlWeb web)
    {
        _web = web;
    }

    public async Task<string> GetTitleAsync(string url)
    {
        var document = await _web.LoadFromWebAsync(url);

        var title = document.DocumentNode.SelectSingleNode("//title").InnerText.Trim();

        if (title == null)
        {
            throw new Exception("Cant get title");
        }

        return title;
    }
}

public static class Program
{
    private const string RozetkaUrl =
        "https://rozetka.com.ua/apple-iphone-15-pro-max-256gb-natural-titanium/p395461104/";

    private const string FoxtrotUrl =
        "https://www.foxtrot.com.ua/uk/shop/smartfoniy_i_mobilniye_telefoniy_apple_iphone_15_pro_max_256gb_deep_purple.html";

    static async Task Main(string[] args)
    {
        var parser = new IphoneParser(new HtmlWeb());

        var tasks = new List<Task<string>>
        {
            parser.GetTitleAsync(RozetkaUrl),
            parser.GetTitleAsync(FoxtrotUrl)
        };

        var titles = await Task.WhenAll(tasks);

        foreach (var title in titles)
        {
            Console.WriteLine(title);
        }
    }
}
