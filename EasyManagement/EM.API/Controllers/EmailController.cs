using Microsoft.AspNetCore.Mvc;
using EM.DataRepository.Emails;
using EM.Services;
using EM.Business;
using EM.DatabaseSQL.Tables;

namespace EM.API.Controllers;
    
[Route("[controller]")]
[ApiController]       
public sealed class EmailController : ControllerBase
{
    private readonly IEmailService _emailService;

    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SendEmail email)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        try
        {
            await _emailService.SendEmailTask(email);

            return Created(string.Empty, email);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict("Unable to send email!");
        }
    }
}
