using Oblong_Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*
builder.Services.AddEntityFrameworkMySql()
    .AddDbContextPool<PersonalDbContext>((serviceProvider, optionsBuilder) =>
    {
        optionsBuilder.UseInternalServiceProvider(serviceProvider);
    });
*/


builder.Services.AddDbContext<PersonalDbContext>();


var DevCors = "_devCors";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: DevCors,
                      policy =>
                      {
                          policy.AllowAnyOrigin();

                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                      });
});

var ProdCors = "_prodCors";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: ProdCors,
                      policy =>
                      {
                          /*
                          policy.WithOrigins("http://oblonggato.com",
                                                "http://oblonggato.com/",
                                                "https://oblonggato.com/",
                                                "https://oblonggato.com", 
                                                "http://www.oblonggato.com",
                                                "http://www.oblonggato.com/",
                                                "https://www.oblonggato.com",
                                                "https://www.oblonggato.com/"
                                        );

                          */
                          policy.AllowAnyOrigin();
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                      });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(DevCors);

}
else
{
    app.UseCors(ProdCors);
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
