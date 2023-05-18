using BLL;
using DAL;
using Entity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "policy",
                      policy =>
                      {
                          policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                      });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




builder.Services.AddScoped(typeof(UserIDAL), typeof(UserDAL));
builder.Services.AddScoped(typeof(UserIBLL), typeof(UserBLL));

builder.Services.AddScoped(typeof(SkirtIDAL), typeof(SkirtDAL));
builder.Services.AddScoped(typeof(SkirtIBLL), typeof(SkirtBLL));
builder.Services.AddDbContext<SuitYouDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("policy");
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
