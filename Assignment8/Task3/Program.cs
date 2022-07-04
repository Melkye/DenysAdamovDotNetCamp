using Task3;

try
{
    Product[] products = new Product[6]
    {
        new Product("product 1", 1.1, 1.1),
        new Product("product 2", 2.2, 2.2),
        new Meat("product 3 - meat", 3.3, 3.3, MeatCategory.First, MeatType.Chicken),
        new Meat("product 4 - meat", 4.4, 4.4, MeatCategory.Highest, MeatType.Veal),
        new DairyProduct("product 5 - dairy product", 5.5, 5.5, 10),
        new DairyProduct("product 6 - dairy product", 6.6, 6.6, 10)
    };
    Storage storage1 = new Storage(products);
    Storage storage2 = new Storage(products[1..5]);
    Storage storage3 = new Storage(products[3..]);

    Storage storageUnion1 = storage1 + storage2;
    Storage storageUnion2 = storage2 + storage3;
    Storage storageUnion3 = storage3 + storage1;

    Storage storageIntersect1 = storage1 * storage2;
    Storage storageIntersect2 = storage2 * storage3;
    Storage storageIntersect3 = storage3 * storage1;

    Storage storageExcept1 = storage1 / storage2;
    Storage storageExcept2 = storage1 / storage3;
    Storage storageExcept3 = storage2 / storage1;
    Storage storageExcept4 = storage2 / storage3;
    Storage storageExcept5 = storage3 / storage1;
    Storage storageExcept6 = storage3 / storage2;

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
