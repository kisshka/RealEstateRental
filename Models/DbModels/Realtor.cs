using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Real_Estate_Rental_Practic.Models;
public partial class Realtor
{
    public int IdRealtor { get; set; }

    [Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public string Surname { get; set; } = null!;

    [Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public string Name { get; set; } = null!;

    public string? Patronymic { get; set; }
    
    [RegularExpression(@"^7\d{10}$", ErrorMessage = "*Неверный номер телефона.")]
    [Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public string PhoneNumber { get; set; } = null!;

    [EmailAddress(ErrorMessage = "*Введите валидный E-mail адрес.")]
    [Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public string EmailAddress { get; set; } = null!;

    public int IdEstateAgency { get; set; }

    public virtual ICollection<EstateRental> EstateRentals { get; set; } = new List<EstateRental>();

    public virtual EstateAgency? IdEstateAgencyNavigation { get; set; } = null!;
}
