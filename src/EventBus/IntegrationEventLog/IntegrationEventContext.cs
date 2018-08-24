﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace Zhiji.IntegrationEventLog
{
    public class IntegrationEventContext : DbContext
    {
        public DbSet<IntegrationEventEntry> IntegrationEvents { get; set; }

        public IntegrationEventContext(DbContextOptions<IntegrationEventContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var eventEntity = builder.Entity<IntegrationEventEntry>();
            eventEntity.HasKey(e => e.Id);
            eventEntity.Property(e => e.Id).ValueGeneratedNever();            
            eventEntity.Property(e => e.Arguments).IsRequired();
            eventEntity.Property(e => e.PublishTimes).IsRequired();
            eventEntity.Property(e => e.Type).IsRequired();
            eventEntity.Property(e => e.Status).IsRequired();

            eventEntity.Property(e => e.CreateTime)
                .IsRequired()
                .HasConversion(v => v.ToUnixTimeTicks(), v => Instant.FromUnixTimeTicks(v));
        }
    }
}