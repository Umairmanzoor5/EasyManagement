using EM.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using EM.DataRepository.Emails;
using Microsoft.EntityFrameworkCore;
using EM.DatabaseSQL.Contexts;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace EM.Business
{
    
    public class EmailService : IEmailService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public EmailService(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task SendEmailTask(SendEmail email)
        {
            await _context.Accounts.SingleAsync(x => x.Email == email.Email);
            var emailModel = new EmailModel
            {
                From = "pedromelo2000@outlook.pt",
                To = email.Email,
                Subject = "Teste",
                Body = "token: " + GenerateToken(email),
                Title = "Titulo da msg",
                ImageUrl0 = "",
                Head = "Head da msg",
                ImageUrl1 = "",
                BodyHead = "Head do corpo da msg",
                ButtonText = "texto de botao",
                ButtonLink = "",
                ImageUrl2 = ""
            };
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var sendGridMessage = new SendGridMessage();
            sendGridMessage.SetFrom(emailModel.From);
            sendGridMessage.AddTo(emailModel.To);
            sendGridMessage.SetSubject(emailModel.Subject);
            sendGridMessage.SetTemplateId("d-073811bf393b468ab4d5e0c31989caea");
            sendGridMessage.SetTemplateData(emailModel);
            //var displayRecipients = false; // set this to true if you want recipients to see each others mail id
            await client.SendEmailAsync(sendGridMessage);           
        }

        // To generate token
        private string GenerateToken(SendEmail email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,email.Email)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
