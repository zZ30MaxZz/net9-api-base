namespace Dokypets.Api.Injection
{
    public static class CorsAllowedServices
    {
        public static IServiceCollection AddInjectionCors(this IServiceCollection services, IConfiguration configuration, string corsName)
        {
            var corsUrls = configuration.GetValue<string>("CorsAllowedUrl")?.Split(',')
                          ?? Array.Empty<string>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: corsName,
                                  policy =>
                                  {
                                      policy.WithOrigins(corsUrls)
                                      .AllowAnyMethod()
                                      .AllowAnyHeader()
                                      .AllowCredentials()
                                      .WithExposedHeaders("*");
                                  });
            });


            return services;
        }
    }
}
