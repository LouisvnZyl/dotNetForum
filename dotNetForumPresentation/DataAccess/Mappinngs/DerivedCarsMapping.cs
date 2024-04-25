using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Cars;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappinngs
{
    internal class BakkieCarsMapping : IEntityTypeConfiguration<Bakkie>
    {
        public void Configure(EntityTypeBuilder<Bakkie> builder)
        {
            
        }
    }

    internal class SchoolBusMapping : IEntityTypeConfiguration<SchoolBus>
    {
        public void Configure(EntityTypeBuilder<SchoolBus> builder)
        {
            
        }
    }
}
