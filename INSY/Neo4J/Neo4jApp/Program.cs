using Neo4jApp.Components;
using Neo4j.Driver;
using Neo4jApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddSingleton<IDriver>(services =>
{
    var config = services.GetRequiredService<IConfiguration>();
    var password = config["Neo4j:Password"]
        ?? throw new InvalidOperationException("Bitte Neo4j:Password über User Secrets konfigurieren.");
    return GraphDatabase.Driver(
        config["Neo4j:Uri"]!,
        AuthTokens.Basic(config["Neo4j:Username"]!, password),
        options => options.WithConnectionTimeout(TimeSpan.FromSeconds(5))
            .WithMaxTransactionRetryTime(TimeSpan.FromSeconds(5)));
});
builder.Services.AddScoped<Neo4jService>();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.Run();
