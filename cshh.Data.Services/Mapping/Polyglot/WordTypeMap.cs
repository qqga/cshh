﻿using cshh.Data.Polyglot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Services.Mapping.Polyglot
{
    class WordTypeMap :BaseEntityTypeConfiguration<WordType>
    {
        public WordTypeMap() : base()
        {
            ToTable("WordType", "Polyglot");
            Property(t => t.Name).IsRequired().HasMaxLength(100);

            HasMany(t => t.Words).WithOptional(w => w.Type).HasForeignKey(w=>w.Type_Id);
        }
    }
}
