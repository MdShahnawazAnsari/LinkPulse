using FluentValidation;
using Link.Application.Services;
using Link.Application.Validators;

namespace Link.API.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        // 1. Add Bussiness Logic Service
        services.AddScoped<LinkService>();

        // 2. Add Validators (Automatically finds all validators in the assembly)
        // services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CreateLinkRequestValidator>();

        return services;
    }
}