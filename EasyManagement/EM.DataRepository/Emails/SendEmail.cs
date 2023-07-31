using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EM.DataRepository.Emails
{
    public class SendEmail
    {
        [Required]
        public string Email { get; set; } = null!;
    }
}
