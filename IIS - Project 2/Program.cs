using IIS___Project_2.Data;
using IIS___Project_2.Services;

var builder = WebApplication.CreateBuilder(args);

// Register your services here
builder.Services.AddScoped<IMarvelService, MarvelService>(); // Register the service and its implementation
builder.Services.AddScoped<MarvelCharacterRepository>(); // Register the repository
builder.Services.AddScoped<MovieRepository>(); // Register the movie repository

// Add controllers
builder.Services.AddControllers();

// Add Swagger for API documentation (optional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Include XML comments (assuming XML documentation is enabled in the project file)
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
