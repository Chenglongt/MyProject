﻿
using MyProject.Shared.DataTransferObjects;
using MyProject.Api.Services;

namespace MyProject.Api.EndPoints
{
    public static class EndPoints
    {
        public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/signup",
             async (SginupRequestDataTransferObjects dataTransferObjects, AuthenticationService authenticationService) =>
                 TypedResults.Ok(await authenticationService.SignupAsync(dataTransferObjects)));           
            
            app.MapPost("/api/signin",
             async (SgininRequestDataTransferObjects dataTransferObjects, AuthenticationService authenticationService) =>
                 TypedResults.Ok(await authenticationService.SigninAsync(dataTransferObjects)));



            return app;


        }
    }
}
