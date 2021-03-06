﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zhiji.Common.Domain;

namespace Zhiji.Common.EntityFrameworkCore
{
    public static class DbContextExtensions
    {
        public static void EnsureEnumeration<T>(this DbContext context) where T : Enumeration
        {
            var enumerations = Enumeration.GetAll<T>();
            var existing = context.Set<T>().Select(e => e.Id);
            var missing = enumerations.Where(e => !existing.Contains(e.Id));
            context.Set<T>().AddRange(missing);
        }
    }
}
