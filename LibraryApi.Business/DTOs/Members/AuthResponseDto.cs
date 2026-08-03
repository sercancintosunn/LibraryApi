using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Business.DTOs.Members
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public MemberResponseDto Member { get; set; } = null!;
    }
}
