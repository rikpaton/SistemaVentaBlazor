using ERP.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using SistemaVentaBlazor.Server.Models;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ERP.Api;

internal static class ProductApi
{
    public static RouteGroupBuilder MapProductApi(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/erp")
            .WithTags("Product Api");


        // TODO: Mover a config
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            //PropertyNameCaseInsensitive = false,
            //PropertyNamingPolicy = null,
            WriteIndented = true,
            //IncludeFields = false,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            //ReferenceHandler = ReferenceHandler.Preserve
        };

        group.MapGet("/products", async (AppDbContext db) =>
        await db.Producto.ToListAsync() is IList<Producto> products
            ? Results.Json(products, options)
            : Results.NotFound())
        .WithOpenApi();

        group.MapGet("/products/{id}", async (AppDbContext db, int id) =>
            await db.Producto.FindAsync(id) is Producto product
                ? Results.Json(product, options)
                : Results.NotFound())
            .WithOpenApi();

        group.MapPost("/products", async (AppDbContext db, Producto product) =>
        {
            //product.Guid = Guid.NewGuid();
            db.Producto.Add(product);
            await db.SaveChangesAsync();
            return Results.Created($"/products/{product.IdProducto}", product);
        }).WithOpenApi();

        group.MapPut("/products/{id}", async (AppDbContext db, int id, Producto updatedProduct) =>
        {
            var product = await db.Producto .FindAsync(id);
            if (product is null) return Results.NotFound();

            product.Nombre = updatedProduct.Nombre;
            product.IdCategoria = updatedProduct.IdCategoria;
            
            await db.SaveChangesAsync();
            return Results.NoContent();
        }).WithOpenApi();

        group.MapDelete("/products/{id}", async (AppDbContext db, int id) =>
        {
            var product = await db.Producto.FindAsync(id);
            if (product is null) return Results.NotFound();

            db.Producto.Remove(product);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }).WithOpenApi();

        return group;
    }
}
