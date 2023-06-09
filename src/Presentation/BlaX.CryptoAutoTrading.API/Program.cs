using BlaX.CryptoAutoTrading.API.Extensions;
using BlaX.CryptoAutoTrading.API.Filters;
using BlaX.CryptoAutoTrading.API.Middlewares;
using BlaX.CryptoAutoTrading.Domain;
using BlaX.CryptoAutoTrading.Infrastructure;
using BlaX.CryptoAutoTrading.Persistence;
using Microsoft.AspNetCore.Mvc;
using NpgsqlTypes;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

#region Serilog Implementation
Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL"), "logs",
        needAutoCreateTable: true,
        columnOptions: new Dictionary<string, ColumnWriterBase>
        {
            {"message", new RenderedMessageColumnWriter(NpgsqlDbType.Text)},
            {"message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text)},
            {"level", new LevelColumnWriter(true , NpgsqlDbType.Varchar)},
            {"time_stamp", new TimestampColumnWriter(NpgsqlDbType.Timestamp)},
            {"exception", new ExceptionColumnWriter(NpgsqlDbType.Text)},
            {"log_event", new LogEventSerializedColumnWriter(NpgsqlDbType.Json)},
        })
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(log);
#endregion

#region Custom Service Configuration
builder.Services.AddInfrastructureService();
builder.Services.AddDataService(configuration);
builder.Services.AddAppSettingsServices(configuration);
#endregion

// OpenId Connect (Authentication) - (Kimlik Doðrulama)
builder.Services.AddAuthenticationService(configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors();
//builder.Services.AddMvc();
builder.Services.AddMvc().AddMvcOptions(p => { p.Filters.Insert(0, new AuthenticationFilter(configuration)); });


builder.Services.AddCustomizeSwagger();

#region Validation
//builder.Services.AddControllers()
//    .ConfigureApiBehaviorOptions(options =>
//    {
//        options.SuppressInferBindingSourcesForParameters = true;
//    });

builder.Services.Configure<ApiBehaviorOptions>(opt => { opt.SuppressModelStateInvalidFilter = false; }); // Microsoft'un, ASP.NET Core Web API'da sunmuþ olduðu default validation configuration'udur. (true = devre dýþý)
#endregion

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseCors(q => q.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CryptoAutoTrading.API v1"));
    //app.UseDeveloperExceptionPage();
}

#region Custom Exception Middlewares
app.ConfigureCustomExceptionMiddleware();
#endregion

app.UseStaticFiles();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseMiddleware<JwtBearerTokenMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

//DatabaseInitializer.AutoMigration.Initialize(app.Services);

//Seeder seeder = new();
//seeder.SeedAsync(app.Services).Wait();

//app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.MapControllers();

app.Run();
