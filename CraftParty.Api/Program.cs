using CraftParty.Application;
using CraftParty.Infrastructure;
using CraftParty.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration)
        .AddAuthentication(builder.Configuration);
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.MapControllers();

    app.UseAuthentication();
    app.UseAuthorization();

    app.Run();
}
