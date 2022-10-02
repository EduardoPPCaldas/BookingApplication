using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Payments.Enums;

namespace Application.Payments.DTOs;
public class PaymentStateDTO
{
    public PaymentStatus Status { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public string? Message { get; set; }
}
