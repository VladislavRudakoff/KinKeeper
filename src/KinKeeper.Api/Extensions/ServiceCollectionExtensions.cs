namespace KinKeeper.Api.Extensions;

/// <summary>
/// Методы расширения для <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрирует все слои приложения
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddAllLayers(this IServiceCollection services) =>
        services
            .AddOptions()
            .AddEndpointsLayer()
            .AddMiddlewares();

    private static IServiceCollection AddEndpointsLayer(this IServiceCollection services)
    {
        services
            .AddControllers(options => options.Filters.Add<AutoValidationEndpointFilter>())
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        services.AddEndpointsApiExplorer();

        services.AddResponseCaching();

        services.AddSwaggerGen(options =>
        {
            options.SupportNonNullableReferenceTypes();

            options.AddSecurityDefinition("Bearer", new()
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            options.AddSecurityRequirement(new()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });

            options.SwaggerDoc("v1", new()
            {
                Version = "v1",
                Title = $"{nameof(KinKeeper)} API",
                Description = "Family tree",
                Contact = new()
                {
                    Name = "Author",
                    Url = new("https://github.com/VladislavRudakoff"),
                    Email = "vladislav.rudakoff@gmail.com"
                },
                License = new()
                {
                    Name = "License",
                    Url = new("https://mit-license.org/")
                }
            });

            options.IncludeXmlCommentaries();
        });

        services.AddHealthChecks();

        return services;
    }

    private static void IncludeXmlCommentaries(this SwaggerGenOptions options)
    {
        string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        options.IncludeXmlComments(xmlPath, true);
    }

    private static IServiceCollection AddMiddlewares(this IServiceCollection services) =>
        services
            .AddScoped<CorrelationIdMiddleware>();

    private static IServiceCollection AddOptions(this IServiceCollection services) => services;
}
