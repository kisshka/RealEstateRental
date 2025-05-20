using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Real_Estate_Rental_Practic.Models;
public partial class Owner
{
    public int IdOwner { get; set; }

    [Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public string Surname { get; set; } = null!;

    [Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public string Name { get; set; } = null!;

    public string? Patronymic { get; set; }

    [Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    [RegularExpression(@"^7\d{10}$", ErrorMessage = "*Неверный номер телефона.")]
    public string PhoneNumber { get; set; } = null!;

    [Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public string Town { get; set; } = null!;

    [Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public string Street { get; set; } = null!;

    [Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public string Home { get; set; } = null!;

    [Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public int Flat { get; set; }

    [EmailAddress(ErrorMessage = "*Введите валидный E-mail адрес.")]
    [Required(ErrorMessage = "*Поле обязательно для заполнения.")]
    public string EmailAddress { get; set; } = null!;

    public virtual ICollection<EstateObject> EstateObjects { get; set; } = new List<EstateObject>();

    public virtual ICollection<OwnerEstateAgency> OwnerEstateAgencies { get; set; } = new List<OwnerEstateAgency>();
}
