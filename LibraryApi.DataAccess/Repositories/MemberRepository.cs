using LibraryApi.Business.Interfaces.Repositories;
using LibraryApi.DataAccess.Data;
using LibraryApi.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.DataAccess.Repositories
{
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        public MemberRepository(LibraryDbContext context) : base(context)
        {
        }

        public Task<Member?> GetByEmailAsync(string email) =>
            _context.Members.AsNoTracking().FirstOrDefaultAsync(member => member.Email == email);

        public Task<bool> EmailExistsAsync(string email) =>
            _context.Members.AnyAsync(member => member.Email == email);
    }
}
