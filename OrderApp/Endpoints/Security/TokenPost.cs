﻿using DotNetEnv;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OrderApp.Endpoints.Employees;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrderApp.Endpoints.Security;

public class TokenPost
{
    public static string Template => "/token";

    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };

    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static IResult Action(LoginRequest loginRequest, UserManager<IdentityUser> userManager)
    {
        var user = userManager.FindByEmailAsync(loginRequest.Email).Result;

        if (user is null)
        {
            return Results.NotFound("Unable to find user");
        }

        if (!userManager.CheckPasswordAsync(user, loginRequest.Password).Result)
        {
            return Results.BadRequest("Password is incorrect");
        }

        var key = Env.GetString("SECRET_KEY");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, loginRequest.Email),
            }),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Audience = Env.GetString("AUDIENCE"),
            Issuer = Env.GetString("ISSUER")
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return Results.Ok(new 
        { 
            token = tokenHandler.WriteToken(token) }
        );

    }
}
