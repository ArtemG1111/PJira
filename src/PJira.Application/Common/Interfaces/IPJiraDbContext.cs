﻿

using Microsoft.EntityFrameworkCore;
using PJira.Core.Models;


namespace PJira.Application.Common.Interfaces
{
    public interface IPJiraDbContext
    {
        DbSet<Assignment> Assignments { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}