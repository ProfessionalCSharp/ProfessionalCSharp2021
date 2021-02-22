using System;
using System.Collections.Generic;

#nullable disable

namespace ScaffoldSample
{
    public partial class Menu
    {
        public int MenuId { get; set; }
        public string Text { get; set; }
        public decimal? Price { get; set; }
        public int MenuCardId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime LastUpdated { get; set; }
        public Guid RestaurantId { get; set; }

        public virtual MenuCard MenuCard { get; set; }
    }
}
