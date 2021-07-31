using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public partial class PeriodEntity
    {
        public Guid Id { get; set; }
        public Guid FiscalYearId { get; set; }
        public int? OrderNumber { get; set; }
        public int? QuarterNumber { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
    //    public byte[] LastUpdated { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public PeriodEntity()
        {
        }

        public PeriodEntity(CorePeriod corePeriod)
        {
            this.Id = corePeriod.Id;
            this.FiscalYearId = corePeriod.FiscalYearId;
            this.OrderNumber = corePeriod.OrderNumber;
            this.QuarterNumber = corePeriod.QuarterNumber;
            this.StartDate = corePeriod.StartDate;
            this.EndDate = corePeriod.EndDate;
            this.IsActive = corePeriod.IsActive;
      //      this.LastUpdated = corePeriod.LastUpdated;
            this.IsDeleted = corePeriod.IsDeleted;
            
        }
    }
}
