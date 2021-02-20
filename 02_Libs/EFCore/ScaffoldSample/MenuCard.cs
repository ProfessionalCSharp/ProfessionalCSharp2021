using System;
using System.Collections.Generic;

#nullable disable

namespace ScaffoldSample
{
    public partial class MenuCard
    {
        public MenuCard()
        {
            Menus = new HashSet<Menu>();
        }

        public int MenuCardId { get; set; }
        public string Title { get; set; }
        public Guid RestaurantId { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }
    }
}
