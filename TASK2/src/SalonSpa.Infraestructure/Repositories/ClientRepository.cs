using SalonSpa.Domain.Entities;
using SalonSpa.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonSpa.Infrastructure.Repositories
{
    public class ClientRepository : GenericRepository<Client>
    {
        private readonly SalonSpaContext _context;

        public ClientRepository(SalonSpaContext context) : base(context)
        {
            _context = context;
        }

        public Client? GetByPhone(string phone)
        {
            return _context.Clients
                .FirstOrDefault(c => c.Phone == phone);
        }

        public Client? GetByEmail(string email)
        {
            return _context.Clients
                .FirstOrDefault(c => c.Email != null &&
                                     c.Email.ToLower() == email.ToLower());
        }

        public List<Client> GetActiveClients()
        {
            return _context.Clients
                .Where(c => c.IsActive)
                .OrderBy(c => c.FullName)
                .ToList();
        }
    }
}





