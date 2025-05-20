using System;
using System.Collections.Generic;

namespace Real_Estate_Rental_Practic.Models;
public partial class OwnerEstateAgency
{
    public int IdOwnerEstateAgency { get; set; }

    public int IdOwner { get; set; }

    public int? IdEstateAgency { get; set; }

    public virtual EstateAgency? IdEstateAgencyNavigation { get; set; }

    public virtual Owner IdOwnerNavigation { get; set; } = null!;
}
