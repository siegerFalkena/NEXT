using System;
using System.Collections.Generic;

namespace NEXT.DB.Models
{
    public partial class ChanneProductPrice
    {
        public int ChannelID { get; set; }
        public int ProductID { get; set; }
        public int PriceTypeID { get; set; }
        public int VATID { get; set; }
        public int CurrencyID { get; set; }
        public DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IncludingVAT { get; set; }
        public DateTime? LastModified { get; set; }
        public int? LastModifiedBy { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }

        public virtual Currrency Currency { get; set; }
        public virtual PriceType PriceType { get; set; }
        public virtual VAT VAT { get; set; }
        public virtual ChannelProduct ChannelProduct { get; set; }
    }
}
