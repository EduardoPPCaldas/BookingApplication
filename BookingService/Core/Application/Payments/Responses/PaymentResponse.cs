using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Payments.DTOs;

namespace Application.Payments.Responses;

public class PaymentResponse : Response
{
    public PaymentStateDTO? Data { get; set; }
}
