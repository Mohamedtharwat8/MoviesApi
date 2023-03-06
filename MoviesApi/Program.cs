using Microsoft.OpenApi.Models; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(
    options=>options.UseSqlServer(connectionString)
    );

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IGenresService, GenresService>();
builder.Services.AddTransient<IMoviesService, MoviesService>();

//builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors();
builder.Services.AddSwaggerGen(options=>
{
    options.SwaggerDoc(name: "v1", info: new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "MoviesApi",
        Description= "Description  of site",
        TermsOfService = new Uri(uriString: "http://www.google.com"),
        Contact = new OpenApiContact
        {
            Name= "Name",
            Email="",
            Url=new Uri(uriString:"http://www.google.com")
        },
        License = new OpenApiLicense
        {
            Name = "Name",
            Url = new Uri(uriString: "http://www.google.com")
        },



    });
    options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
    {
        Name="Authorizatio",
        Type=SecuritySchemeType.ApiKey,
        Scheme="Bearer",
        BearerFormat="JWT",
        In =ParameterLocation.Header,
        Description= "JWT Authorization header using the Bearer scheme."
    });

    options.AddSecurityRequirement(securityRequirement:new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Name="Bearer",
                In =ParameterLocation.Header,

            },
            new List<string>()
        }
    } );


}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(c=>c.AllowAnyHeader().AllowAnyMethod().WithOrigins());

app.UseAuthorization();

app.MapControllers();

app.Run();
