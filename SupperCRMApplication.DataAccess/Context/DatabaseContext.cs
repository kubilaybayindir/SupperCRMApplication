﻿using Microsoft.EntityFrameworkCore;
using SupperCRMApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupperCRMApplication.DataAccess.Context
{
    public class DatabaseContext:DbContext
    {

        public DatabaseContext(DbContextOptions options ):base(options)
        {

        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Issue> Issues { get; set; }

    }
}
