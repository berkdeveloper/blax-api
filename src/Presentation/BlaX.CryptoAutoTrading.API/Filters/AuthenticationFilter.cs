using BlaX.CryptoAutoTrading.Application.DTOs.CommonDTOs.BaseObjectDto;
using BlaX.CryptoAutoTrading.Application.DTOs.CommonDTOs.UserDto;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.ComplexTypes;
using BlaX.CryptoAutoTrading.Application.Utilities.Common.ResponseBases.Concrete;
using BlaX.CryptoAutoTrading.Application.Utilities.Extensions;
using BlaX.CryptoAutoTrading.Application.Utilities.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;

namespace BlaX.CryptoAutoTrading.API.Filters
{
    public class AuthenticationFilter : IAsyncActionFilter
    {
        readonly IConfiguration _configuration;

        public AuthenticationFilter(IConfiguration configuration) => _configuration = configuration;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controllerInfo = context.ActionDescriptor as ControllerActionDescriptor;

            //var allowAnonymousApiAuthorize = (AllowAnonymousAttribute)controllerInfo?.MethodInfo.GetCustomAttribute(typeof(AllowAnonymousAttribute), true);

            (AllowAnonymousAttribute allowAnonymous, AuthorizeAttribute authorizeAttribute) attributes = ((AllowAnonymousAttribute)controllerInfo?.MethodInfo.GetCustomAttribute(typeof(AllowAnonymousAttribute), true), (AuthorizeAttribute)controllerInfo?.MethodInfo.GetCustomAttribute(typeof(AuthorizeAttribute), true));

            //var authorizeAttribute = (AuthorizeAttribute)controllerInfo?.MethodInfo.GetCustomAttribute(typeof(AuthorizeAttribute), true);

            if (attributes.allowAnonymous is not null || attributes.authorizeAttribute is null)
            {
                await next.Invoke();
                return;
            }

            AuthorizedUserDto requestUser = null;

            #region Send Request
            try
            {

                var baseApiUrl = _configuration["AuthServerConfig:ApiUrl"];
                var authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var objectResponseBase = new ObjectResponseBase<BaseRequestDto>(new BaseRequestDto(new AuthorizedUserDto()));

                if (string.IsNullOrEmpty(authHeader) is false && authHeader.StartsWith("Bearer "))
                    using (HttpClient client = new())
                    {
                        string endpointUrl = $"{baseApiUrl}Auth/ValidateToken";
                        string requestToken = authHeader.Replace(JwtBearerDefaults.AuthenticationScheme, "").Trim();
                        #region Headers
                        client.DefaultRequestHeaders.Add("Access-Token", requestToken);
                        client.DefaultRequestHeaders.Add("Accept", "*/*");
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, requestToken);
                        #endregion
                        HttpResponseMessage response = await client.GetAsync(endpointUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            var settings = new JsonSerializerSettings { Converters = { new CamelCaseConverter<ObjectResponseBase<BaseRequestDto>>() } };
                            string responseBody = await response.Content.ReadAsStringAsync();

                            objectResponseBase = JsonConvert.DeserializeObject<ObjectResponseBase<BaseRequestDto>>(responseBody, settings);
                            requestUser = objectResponseBase.Data.AuthorizedUser;
                        }
                    }
            }
            catch { }
            #endregion

            var emptyGuidId = new Guid(Guid.Empty.ToString());

            if (requestUser is null || requestUser.UserId == emptyGuidId)
            {
                var errorResponse = new ResponseBase(HttpStatusCode.Unauthorized, "Giriş başarısız!");

                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                await context.HttpContext.Response.WriteAsync(errorResponse.ToJsonCamelCase());
                return;
            }

            try
            {
                BaseRequest request = null;
                request = context.ActionArguments.Select(req => req.Value.ToAuthorizedBase()).FirstOrDefault();

                if (request is null)
                {
                    var errorResponse = new ResponseBase(HttpStatusCode.NotFound, "Kullanıcı bulunamadı!");
                    context.HttpContext.Response.ContentType = "application/json";
                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.HttpContext.Response.WriteAsync(errorResponse.ToJsonCamelCase());
                    return;
                }

                request.AuthorizedUserObject = TypeConversion.Conversion<AuthorizedUserDto, AuthorizedUserObject>(requestUser);
            }
            catch
            {
                var errorResponse = new ResponseBase(HttpStatusCode.NotFound, "Kullanıcı bulunamadı!");
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.HttpContext.Response.WriteAsync(errorResponse.ToJsonCamelCase());
                return;
            }

            await next.Invoke();
        }
    }
}
