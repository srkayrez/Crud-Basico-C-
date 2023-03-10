using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/saveproduct", (Product product) => {
    ProductRepository.Add(product);
});

app.MapGet("/getproduct/{code}", ([FromRoute] string code) => {
    var product = ProductRepository.GetBy(code);
    return product;
});

app.MapPut("/editproduct", (Product product) => {
    var productSaved = ProductRepository.GetBy(product.code);
    productSaved.name = product.name;
});

app.MapDelete("deleteproduct/{code}", ([FromRoute] string code) => {
    var productSaved = ProductRepository.GetBy(code);
    ProductRepository.Remove(productSaved);

});

app.Run();
public static class ProductRepository {
    public static List<Product> Products { get; set; }

public static void Add(Product product){
    if(Products == null)
        Products = new List<Product>();
    Products.Add(product);
    }

    public static Product GetBy(string code) {
        return Products.FirstOrDefault(p => p.code == code);
    }

    public static void Remove(Product product) {
        Products.Remove(product);
    }
}

public class Product{
    public string code { get; set; }
    public string name { get; set; }  
}

