﻿namespace API.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IApplicationBuilder UseSwaggerDocumention(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
