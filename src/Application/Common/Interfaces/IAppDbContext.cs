using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Application.Common.Interfaces;
public interface IAppDbContext
{
    DbSet<Movie> Movies { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
