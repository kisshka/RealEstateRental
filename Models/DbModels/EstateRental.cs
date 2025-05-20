using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Real_Estate_Rental_Practic.Models;
public partial class EstateRental
{
    public int IdEstateRental { get; set; }

[Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public DateOnly RentBeginning { get; set; }

    public DateOnly? RentEnding { get; set; }

[Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public decimal CostPerMonth { get; set; }

[Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public decimal Deposit { get; set; }

    public int IdEstateObject { get; set; }

    public int? IdRealtor { get; set; }

    public int IdUser { get; set; }

    public virtual EstateObject? IdEstateObjectNavigation { get; set; } = null!;

    public virtual Realtor? IdRealtorNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; } = null!;
}
