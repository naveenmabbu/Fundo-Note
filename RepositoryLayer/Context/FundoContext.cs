﻿using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Context
{
    public class FundoContext : DbContext
    {
        public FundoContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<UserEntity> User { get; set; }
        public DbSet<NotesEntity> Notes { get; set; }
        public DbSet<CollabEntity> Collab { get; set; }

    }

}
