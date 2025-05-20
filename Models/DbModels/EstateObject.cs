using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Real_Estate_Rental_Practic.Models;
public partial class EstateObject
{
    public int IdEstateObject { get; set; }

[Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public string ObjectType { get; set; } = null!;

[Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public string Town { get; set; } = null!;

[Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public string Street { get; set; } = null!;

[Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public string Home { get; set; } = null!;

[Range(0, int.MaxValue, ErrorMessage = "*Неверное значение.")]
    public int? Flat { get; set; }

[Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public string CittyArea { get; set; } = null!;

[Range(0, int.MaxValue, ErrorMessage = "*Неверное значение.")]
[Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public int NumberOfRooms { get; set; }

[Range(0, double.MaxValue, ErrorMessage = "*Неверное значение.")]
[Required(ErrorMessage = "*Поле обязательно для заполнения.")]
[RegularExpression(@"\d+(\,\d+)?$", ErrorMessage = "*Неверное значение.")]
    public decimal Square { get; set; }

[Range(0, int.MaxValue, ErrorMessage = "*Неверное значение.")]
[Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public int RentalPeriod { get; set; }
    
[Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public int IdOwner { get; set; }

    public bool ChildrenAllowed { get; set; }

    public bool AnimalsAllowed { get; set; }

    public virtual ICollection<EstateRental> EstateRentals { get; set; } = new List<EstateRental>();

    public virtual Owner? IdOwnerNavigation { get; set; } = null!;
}
