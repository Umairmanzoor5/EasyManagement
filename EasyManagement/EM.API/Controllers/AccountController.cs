using EM.DataRepository.Accounts;
using EM.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EM.API.Controllers;

[Route("[controller]")]
[ApiController]
public sealed class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(await _accountService.ListAccountTask());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }

    // GET api/<AccountController>/john_doe@gmail.com
    [HttpGet("{email}")]
    public async Task<IActionResult> Get(string email)
    {
        try
        {
            return Ok(await _accountService.InfoAccountTask(email));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound(email);
        }
    }

    // POST api/<EntitiesController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RegisterAccount account)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        try
        {
            await _accountService.RegisterAccountTask(account);

            return Created(string.Empty, account);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict("The email " + account.Email + " already exists!");
        }

    }

    // POST api/<AccountController>/Login
    [HttpPost("Login")]
    public async Task<IActionResult> PostLogin([FromBody] LoginAccount account)
    {        
        try
        {            
            var token = await _accountService.LoginAccountTask(account);
            if (token != null)
            {                
                return Ok(token);
            }
                        
            return Conflict("Incorrect password.");            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict("The email " + account.Email + " isn't registered!");
        }
    }

    [HttpGet("UserEndPoint")]
    public async Task<IActionResult> GetUserEndPoint()
    {
        var currentUser = GetCurrentUser();
        return Ok($"Hi you are user with email: {currentUser.Email}");
    }

    private LoginAccount GetCurrentUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity != null)
        {
            var userClaims = identity.Claims;
            return new LoginAccount
            {
                Email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value
            };
        }
        return null;
    }

    /// PUT api/<AccountController>/5
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] EditAccount account)
    {
        try
        {
            await _accountService.EditAccountTask(account);
            return Ok(account);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound(account.Email);
        }
    }

    

    // DELETE api/<EntitiesController>/5
    //[HttpDelete("{nif}")]
    //public async Task<IActionResult> Delete(string nif)
    //{
    //    try
    //    {
    //        await _clientService.DisableClientTask(nif);

    //        return Ok();
    //    }
    //    catch (Exception e)
    //    {
    //        Console.WriteLine(e);
    //        return NotFound(nif);
    //    }
    //}
}