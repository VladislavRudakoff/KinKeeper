namespace KinKeeper.Api.Validators.Options;

public sealed class OptionsValidator<TOptions>(string? name, IServiceProvider serviceProvider) : IValidateOptions<TOptions>
    where TOptions : class
{
    private readonly IServiceProvider serviceProvider = serviceProvider;
    private readonly string? sectionName = name;

    /// <inheritdoc />
    public ValidateOptionsResult Validate(string? name, TOptions options)
    {
        if (sectionName != null && sectionName != name)
        {
            return ValidateOptionsResult.Skip;
        }

        ArgumentNullException.ThrowIfNull(options);

        using IServiceScope scope = serviceProvider.CreateScope();

        IValidator<TOptions> validator = scope.ServiceProvider.GetRequiredService<IValidator<TOptions>>();

        ValidationResult results = validator.Validate(options);

        if (results.IsValid)
        {
            return ValidateOptionsResult.Success;
        }

        string typeName = options.GetType().Name;

        List<string> errors = [];

        foreach (ValidationFailure result in results.Errors)
        {
            errors.Add($"Validation failed for '{typeName}.{result.PropertyName}' with the error: '{result.ErrorMessage}'.");
        }

        return ValidateOptionsResult.Fail(errors);
    }
}
