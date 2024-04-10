using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MvcCoreMicrosoftAD.Data
{
    public class ApplicationContext : IdentityDbContext
    {
        public ApplicationContext
            (DbContextOptions<ApplicationContext> options)
            : base(options)
        { }
    }
}
