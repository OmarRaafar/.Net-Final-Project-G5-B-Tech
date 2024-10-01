using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsMAFP.Authentication_and_Authorization
{
    public class TokenModelMAFP
    {
        [Key]
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
