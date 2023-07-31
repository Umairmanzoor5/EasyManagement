using EM.DatabaseSQL.Contexts;
using EM.DatabaseSQL.Tables;
using EM.DataRepository.Accounts;
using EM.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EM.Business
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public AccountService(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        
        public async Task<List<ListAccounts>> ListAccountTask()
        {                       
            var listAccount = await _context.Accounts.Select(account => new ListAccounts
            {
                Email = account.Email,
                Password = account.Password,
                Name = account.Name,
                Salary = account.Salary
            })
            .ToListAsync();

            return listAccount;            
        }
        public async Task<InfoAccount> InfoAccountTask(string email)
        {
            var account = await _context.Accounts.SingleAsync(x => x.Email == email);

            var infoAccount = new InfoAccount
            {
                Email = account.Email,
                Password = account.Password,
                Name = account.Name,
                Salary = account.Salary
            };

            return infoAccount;
        }

        public async Task<string> LoginAccountTask(LoginAccount account)
        {                       
            var dbAccount = await _context.Accounts.SingleAsync(x => x.Email == account.Email);
            var StringCipher = new StringCipherService();
            string decryptedPassword = StringCipher.Decrypt(dbAccount.Password, account.Password);

            if (decryptedPassword == account.Password)
            {
                var token = await GenerateToken(account);
                return token;
            }

            return null;
        }

        // To generate token
        private async Task<string> GenerateToken(LoginAccount account)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, account.Email)               
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }        
        
        public async Task RegisterAccountTask(RegisterAccount account)
        {
            var StringCipher = new StringCipherService();
            string encryptedPassword = StringCipher.Encrypt(account.Password, account.Password);
            var newAccount = new Account
            {
                Email = account.Email,
                Password = encryptedPassword,                
                Name = account.Name,
                Salary = account.Salary
            };
                       
            await _context.Accounts.AddAsync(newAccount);
            await _context.SaveChangesAsync();            
        }
        public async Task EditAccountTask(EditAccount account)
        {
            var result = await _context.Accounts.SingleAsync(x => x.Email == account.Email);
            result.Email = account.Email;
            result.Password = account.Password;
            result.Name = account.Name;
            result.Salary = account.Salary;

            await _context.SaveChangesAsync();
        }

       /* public async Task DisableAccountTask(string email)
        {
            var result = await _context.Accounts.SingleAsync(x => x.Email == email);

            result.Active = false;

            await _context.SaveChangesAsync();
        }
       */
    }
}
