﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Zhiji.IntegrationEventLog;

namespace Zhiji.IntegrationEventLog.Migrations
{
    [DbContext(typeof(IntegrationEventContext))]
    [Migration("20180829060458_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Zhiji.IntegrationEventLog.IntegrationEventEntry", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("Arguments")
                        .IsRequired();

                    b.Property<long>("CreateTime");

                    b.Property<int>("PublishTimes");

                    b.Property<int>("Status");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("IntegrationEvents");
                });
#pragma warning restore 612, 618
        }
    }
}
