using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NotasMoodle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LoginContext : IdentityDbContext
{
    public LoginContext(DbContextOptions<LoginContext> options)
        : base(options)
    {
    }

 
}
