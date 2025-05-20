using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Real_Estate_Rental_Practic.Models;

public partial class EstateAgency
{ 
    public int IdEstateAgency { get; set; }

[Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public string AgencyName { get; set; } = null!;

[RegularExpression(@"^7\d{10}$", ErrorMessage = "*Неверный номер телефона.")]
[Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public string PhoneNumber { get; set; } = null!;

[Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public string DirectorSurname { get; set; } = null!;

[Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public string DirectorName { get; set; } = null!;

    public string? DirectorPatronymic { get; set; }

[Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public string Town { get; set; } = null!;

[Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public string Street { get; set; } = null!;

[Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public string Home { get; set; } = null!;

[Range(0, int.MaxValue, ErrorMessage = "*Неверное значение.")]
[Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public int Flat { get; set; }

[Range(0, double.MaxValue, ErrorMessage = "*Неверное значение.")]
[Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public decimal Comission { get; set; }

    public virtual ICollection<OwnerEstateAgency> OwnerEstateAgencies { get; set; } = new List<OwnerEstateAgency>();

    public virtual ICollection<Realtor> Realtors { get; set; } = new List<Realtor>();
}
