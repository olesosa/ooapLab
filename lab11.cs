using HtmlAgilityPack;

namespace ooap11;

public class Parser
{
    private readonly HtmlWeb _web;

    public Parser(HtmlWeb web)
    {
        _web = web;
    }

    public async Task<string> GetTitleAsync(string url)
    {
        var document = await _web.LoadFromWebAsync(url);

        var titleNode = document.DocumentNode.SelectSingleNode("//title")?.InnerText.Trim();

        if (titleNode == null)
        {
            throw new Exception("Title not found");
        }

        return titleNode;
    }

    public async Task<string> GetNameAsync(string url)
    {
        var document = await _web.LoadFromWebAsync(url);

        var productNameNode = document.DocumentNode.SelectSingleNode("//script[@data-hid='dataLayer']");
        var productNameScript = productNameNode.InnerText;
        var productNameIndex = productNameScript.IndexOf("name:");

        if (productNameIndex == -1)
        {
            throw new Exception("Name not found");
        }

        var productNameStartIndex = productNameScript.IndexOf("\"", productNameIndex) + 1;
        var productNameEndIndex = productNameScript.IndexOf("\"", productNameStartIndex);
        var productName =
            productNameScript.Substring(productNameStartIndex, productNameEndIndex - productNameStartIndex);

        return productName;
    }

    public async Task<string> GetPriceAsync(string url)
    {
        var document = await _web.LoadFromWebAsync(url);

        var priceNode = document.DocumentNode.SelectSingleNode("//script[@data-hid='dataLayer']");
        var priceScript = priceNode.InnerText;
        var priceIndex = priceScript.IndexOf("price:");

        if (priceIndex == -1)
        {
            throw new Exception("Price not found");
        }

        var priceStartIndex = priceScript.IndexOf("\"", priceIndex) + 1;
        var priceEndIndex = priceScript.IndexOf("\"", priceStartIndex);
        var price = priceScript.Substring(priceStartIndex, priceEndIndex - priceStartIndex);

        return price;
    }
}

public class Phone
{
    public string Title { get; set; }
    public string Name { get; set; }
    public string Price { get; set; }

    public override string ToString()
    {
        return $"Title: {Title} \n\nName: {Name} \n\nPrice: {Price}";
    }
}

public static class Program
{
    private const string IphoneUrl =
        "https://allo.ua/ru/products/mobile/apple-iphone-15-pro-max-256gb-natural-titanium.html";

    static async Task Main(string[] args)
    {
        var parser = new Parser(new HtmlWeb());

        var iphone = new Phone()
        {
            Title = await parser.GetTitleAsync(IphoneUrl),
            Name = await parser.GetNameAsync(IphoneUrl),
            Price = await parser.GetPriceAsync(IphoneUrl),
        };

        Console.WriteLine(iphone);
    }
}
