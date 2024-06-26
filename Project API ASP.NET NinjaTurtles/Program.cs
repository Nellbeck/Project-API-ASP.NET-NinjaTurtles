
using Microsoft.EntityFrameworkCore;
using Project_API_ASP.NET_NinjaTurtles.Data;
using Project_ASP.NET_NinjaTurtles.Models;
using System.Text.Json.Serialization;

namespace Project_API_ASP.NET_NinjaTurtles
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

            builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //Return all customers
            app.MapGet("/customers", async (ApplicationDbContext context) =>
            {
                var customer = await context.Customers.ToListAsync();
                if (customer == null || !customer.Any())
                {
                    return Results.NotFound("Didn't find any customer");
                }
                return Results.Ok(customer);
            });

            // Create a customer
            app.MapPost("/customers", async (Customer customer, ApplicationDbContext context) =>
            {
                context.Customers.Add(customer);
                await context.SaveChangesAsync();
                return Results.Created($"/customers/{customer.CustomerId}", customer);
            });

            //Get an customer by id
            app.MapGet("/customers/{id:Guid}", async (Guid id, ApplicationDbContext context) =>
            {
                var customer = await context.Customers.FindAsync(id);

                if (customer == null)
                {
                    return Results.NotFound("Did not find any customer with that Id.");
                }
                return Results.Ok(customer);
            });

            //Edit an customer
            app.MapPut("/customers/{id:Guid}", async (Guid id, Customer updatedCustomer, ApplicationDbContext context) =>
            {
                var customer = await context.Customers.FindAsync(id);

                if (customer == null)
                {
                    return Results.NotFound("Did not find any customer with that Id.");
                }
                else
                {
                    customer.CustomerName = updatedCustomer.CustomerName;
                    customer.CustomerEmail = updatedCustomer.CustomerEmail;
                    customer.CustomerPhone = updatedCustomer.CustomerPhone;
                    customer.CustomerAddress = updatedCustomer.CustomerAddress;
                    customer.CustomerBirthDate  = updatedCustomer.CustomerBirthDate;
                    customer.CustomerZipCode = updatedCustomer.CustomerZipCode;
                    await context.SaveChangesAsync();
                    return Results.Ok(customer);
                }
            });
            //Delete an customer
            app.MapDelete("/customers/{id:Guid}", async (Guid id, ApplicationDbContext context) =>
            {
                var customer = await context.Customers.FindAsync(id);

                if (customer == null)
                {
                    return Results.NotFound("Did not find any customer with that Id.");
                }
                else
                {
                    context.Customers.Remove(customer);
                    await context.SaveChangesAsync();
                    return Results.Ok($"Customer with ID: {id} deleted");
                }
            });

            /////////////////////////////////////
            //////////// Products ///////////////
            /////////////////////////////////////

            //Return all products
            app.MapGet("/products", async (ApplicationDbContext context) =>
            {
                var products = await context.Products.ToListAsync();
                if (products == null || !products.Any())
                {
                    return Results.NotFound("Didn't find any product");
                }
                return Results.Ok(products);
            });

            // Create a product
            app.MapPost("/products", async (Product product, ApplicationDbContext context) =>
            {
                context.Products.Add(product);
                await context.SaveChangesAsync();
                return Results.Created($"/products/{product.ProductId}", product);
            });

            //Get an product by id
            app.MapGet("/products/{id:Guid}", async (Guid id, ApplicationDbContext context) =>
            {
                var product = await context.Products.FindAsync(id);

                if (product == null)
                {
                    return Results.NotFound("Did not find any customer with that Id.");
                }
                return Results.Ok(product);
            });

            //Edit an product
            app.MapPut("/products/{id:Guid}", async (Guid id, Product updatedProduct, ApplicationDbContext context) =>
            {
                var product = await context.Products.FindAsync(id);

                if (product == null)
                {
                    return Results.NotFound("Did not find any customer with that Id.");
                }
                else
                {
                    product.ProductName = updatedProduct.ProductName;
                    product.ProductPrice = updatedProduct.ProductPrice;
                    product.ProductQuantity = updatedProduct.ProductQuantity;
                    product.ProductDescription = updatedProduct.ProductDescription;

                    await context.SaveChangesAsync();
                    return Results.Ok(product);
                }
            });
            //Delete an product
            app.MapDelete("/products/{id:Guid}", async (Guid id, ApplicationDbContext context) =>
            {
                var product = await context.Products.FindAsync(id);

                if (product == null)
                {
                    return Results.NotFound("Did not find any customer with that Id.");
                }
                else
                {
                    context.Products.Remove(product);
                    await context.SaveChangesAsync();
                    return Results.Ok($"Product with ID: {id} deleted");
                }
            });


            /////////////////////////////////////
            //////////// Orders ///////////////
            /////////////////////////////////////


            //Return all orders
            app.MapGet("/orders", async (ApplicationDbContext context) =>
            {
                var orders = await context.Orders.Include(c => c.Customer).Include(p => p.Product).ToListAsync();
                if (orders == null || !orders.Any())
                {
                    return Results.NotFound("Didn't find any order");
                }
                return Results.Ok(orders);
            });

            // Create a orders
            app.MapPost("/orders", async (Order order, ApplicationDbContext context) =>
            {

                context.Orders.Add(order);
                await context.SaveChangesAsync();
                return Results.Created($"/orders/{order.OrderId}", order);
            });

            //Get an order by id
            app.MapGet("/orders/{id:Guid}", async (Guid id, ApplicationDbContext context) =>
            {
                var order = await context.Orders.FindAsync(id);

                if (order == null)
                {
                    return Results.NotFound("Did not find any order with that Id.");
                }
                return Results.Ok(order);
            });

            //Edit an product
            app.MapPut("/orders/{id:Guid}", async (Guid id, Order updateOrder, ApplicationDbContext context) =>
            {
                var order = await context.Orders.FindAsync(id);

                if (order == null)
                {
                    return Results.NotFound("Did not find any order with that Id.");
                }
                else
                {


                    await context.SaveChangesAsync();
                    return Results.Ok(order);
                }
            });
            //Delete an product
            app.MapDelete("/orders/{id:Guid}", async (Guid id, ApplicationDbContext context) =>
            {
                var order = await context.Orders.FindAsync(id);

                if (order == null)
                {
                    return Results.NotFound("Did not find any order with that Id.");
                }
                else
                {
                    context.Orders.Remove(order);
                    await context.SaveChangesAsync();
                    return Results.Ok($"Order with ID: {id} deleted");
                }
            });

            /////////////////////////////////////
            //////////// Orders ///////////////
            /////////////////////////////////////



            app.Run();

        }
    }
}
