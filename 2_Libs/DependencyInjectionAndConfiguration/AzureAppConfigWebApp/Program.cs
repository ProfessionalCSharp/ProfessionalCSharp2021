var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddAzureAppConfiguration(options =>
{
    var endpoint = builder.Configuration["AppConfigEndpoint"] ?? throw new InvalidOperationException("AppConfigEndpoint not configured");
    DefaultAzureCredential credential = new();

    options.Connect(new Uri(endpoint), credential)
        .Select(KeyFilter.Any, LabelFilter.Null)
        .Select(KeyFilter.Any, builder.Environment.EnvironmentName)
        .ConfigureRefresh(refresh =>
        {
            refresh.Register("AppConfigurationSample:Settings:Sentinel",
                refreshAll: true)
            .SetCacheExpiration(TimeSpan.FromMinutes(5));
        })
        .ConfigureKeyVault(kv =>
        {
            kv.SetCredential(credential);
        });
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.Configure<IndexAppSettings>(builder.Configuration.GetSection("AppConfigurationSample:Settings"));
builder.Services.AddAzureAppConfiguration();
builder.Services.AddFeatureManagement().AddFeatureFilter<PercentageFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAzureAppConfiguration();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
