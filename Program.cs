using Prueba_Tenica2;

internal class Program
{
    private static void Main(string[] args)
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string filePath = $@"{currentDirectory}/products.txt";
        List<ProductsData> productsCollection = GetProductsData(filePath);
        if (productsCollection.Count > 0)
            ShowInformationData(productsCollection);
        else
            Console.WriteLine("El archivo no contiene datos, favor de verificar");
    }

    public static List<ProductsData> GetProductsData(string filePath)
    {
        string[] fileData = File.ReadAllLines(filePath);

        List<ProductsData> productsCollection = new List<ProductsData>();
        foreach (string data in fileData)
        {
            if (string.IsNullOrEmpty(data)) continue;
            string[] dataInformation = data.Split('|');
            ProductsData productData = SetProductInfo(dataInformation);
            productsCollection.Add(productData);
        }

        return productsCollection;
    }

    private static ProductsData SetProductInfo(string[] dataInformation)
    {
        const int productName = 0;
        const int total = 3;

        return new ProductsData()
        {
            Name = dataInformation[productName],
            Total = Convert.ToDouble(string.IsNullOrEmpty(dataInformation[total]) ? "0" : dataInformation[total])
        };
    }

    private static void ShowInformationData(List<ProductsData> productsInformation)
    {
        double totalPrice = 0;
        productsInformation.ForEach(product => totalPrice += product.Total);

        List<string> uniqueProducts = productsInformation.Select(p => p.Name).Distinct().ToList();

        Console.WriteLine($"Total: {totalPrice}");
        Console.WriteLine("Productos únicos:");
        uniqueProducts.ForEach(product => Console.WriteLine($"* {product}"));
    }
}