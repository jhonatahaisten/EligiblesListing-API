using EligiblesListingAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MyProject.Domain.Entities;
using System.Collections.Generic;

namespace EligiblesListingAPI.Infrastructure.Data
{
    public class ClienteContext : Context
    {
        public ClienteContext(DbContextOptions<ClienteContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
    }
}
