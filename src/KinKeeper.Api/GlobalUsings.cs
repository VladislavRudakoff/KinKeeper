global using System.Reflection;
global using System.Text.Json.Serialization;
global using FluentValidation;
global using FluentValidation.Results;
global using KinKeeper.Api.Extensions;
global using KinKeeper.Api.Filters;
global using KinKeeper.Api.Middlewares;
global using KinKeeper.Api.Validators.Options;
global using KinKeeper.Core.Models;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.Extensions.Options;
global using Microsoft.Extensions.Primitives;
global using Microsoft.OpenApi.Models;
global using Serilog;
global using Serilog.Context;
global using Swashbuckle.AspNetCore.SwaggerGen;
