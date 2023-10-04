using CarBuilderAPI.Models;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;

List<PaintColor> paintColors = new List<PaintColor>
{
    new PaintColor()
    {
        Id = 1,
        Price = 500.00M,
        Color = "Silver"
    },
    new PaintColor()
    {
        Id = 2,
        Price = 650.00M,
        Color = "Midnight Blue"
    },
    new PaintColor()
    {
        Id = 3,
        Price = 750.00M,
        Color = "Firebrick Red"
    },
    new PaintColor()
    {
        Id = 4,
        Price = 800.00M, 
        Color = "Spring Green"
    }
};
List<Interior> interiors = new List<Interior>
{
    new Interior()
    {
        Id = 1,
        Price = 450.00M,
        Material = "Beige Fabric"
    },
     new Interior()
    {
        Id = 2,
        Price = 550.00M,
        Material = "Charcoal Fabric"
    },
     new Interior()
    {
        Id = 3,
        Price = 850.00M,
        Material = "White Leather"
    },
     new Interior()
    {
        Id = 4,
        Price = 750.00M,
        Material = "Black Leather"
    }
};
List<Wheels> wheels = new List<Wheels>
{
    new Wheels()
    {
        Id = 1,
        Price = 250.00M,
        Style = "17-inch Pair Radial"
    },
    new Wheels()
    {
        Id = 2,
        Price = 350.00M,
        Style = "17-inch Pair Radial Black"
    },
    new Wheels()
    {
        Id = 3,
        Price = 400.00M,
        Style = "18-inch Pair Spoke Silver"
    },
    new Wheels()
    {
        Id = 4,
        Price = 450.00M,
        Style = "18-inch Pair Spoke Black"
    }
};
List<Technology> technologies = new List<Technology>
{
    new Technology()
    {
        Id = 1,
        Price = 250.00M,
        Package = "Basic Package (basic sound system)"
    },
    new Technology()
    {
        Id = 2,
        Price = 350.00M,
        Package = "Navigation Package (includes integrated navigation controls)"
    },
    new Technology()
    {
        Id = 3,
        Price = 400.00M,
        Package = "Visibility Package (includes side and rear cameras)"
    },
    new Technology()
    {
        Id = 4,
        Price = 700.00M,
        Package = "Ultra Package (includes navigation and visibility packages)"
    }
};

List<Order> orders = new List<Order>
{
    new Order()
    {
        Id = 1,
        InteriorId = 3,
        WheelId = 1,
        TechnologyId = 4,
        PaintId = 2
    }
};
var builder = WebApplication.CreateBuilder(args);
// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(options =>
                {
                    options.AllowAnyOrigin();
                    options.AllowAnyMethod();
                    options.AllowAnyHeader();
                });
}

app.UseHttpsRedirection();


app.MapGet("/interiors", () =>
{
    return interiors;
});

app.MapGet("/paintcolors", () =>
{
    return paintColors;
});

app.MapGet("/technologies", () =>
{
    return technologies;
});

app.MapGet("/wheels", () =>
{
    return wheels;
});

app.MapGet("/orders", () =>
{
    // for (order of orders)
    // {
    // order.Interior = interiors.FirstOrDefault(i => i.Id == order.InteriorId);
    // order.PaintColor = paintColors.FirstOrDefault(pc => pc.Id == order.PaintId);
    // order.Technology = technologies.FirstOrDefault(t => t.Id == order.TechnologyId);
    // Wheels.wheels = wheels.FirstOrDefault(w => w.Id == order.WheelId);
    // order.wheels = wheels;
    // }
    return orders;
});

app.MapPut("/orders/{id}", (int id, Order order) =>
{
    Order orderToUpdate = orders.FirstOrDefault(or => or.Id == id);
    int orderIndex = orders.IndexOf(orderToUpdate);
    orders[orderIndex] = order;
    return Results.Ok();
});

app.MapPost("/orders", (Order order) =>
{
    order.Id = orders.Count > 0 ?orders.Max(o => o.Id) + 1 : 1;
    order.Timestamp = DateTime.Now;
    orders.Add(order);
    return order;
});

app.Run();