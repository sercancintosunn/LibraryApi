using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Business.DTOs.Loans
{
    public class LoanResponseDto
    {
        public int Id { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public string MemberName { get; set; } = string.Empty;
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
