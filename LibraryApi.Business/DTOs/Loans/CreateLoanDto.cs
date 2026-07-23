using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Business.DTOs.Loans
{
    public class CreateLoanDto
    {
        public int BookId { get; set; }
        public int MemberId { get; set; }
    }
}
