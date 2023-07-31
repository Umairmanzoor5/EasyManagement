using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.DataRepository.Projects
{
    public class CreateProject
    {
        [Required]
        public string Reference { get; set; } = null!;
        [Required]
        public string IdClient { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Attachments { get; set; } = null!;
    }
}
