using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Payments.Enums;

namespace Application.Payments.Requests;
public class CreatePaymentRequest
{
    public int BookingId { get; set; }
    public string? PaymentIntention { get; set; }
    public SuportedPaymentProviders SelectedPaymentProvider { get; set; }
    public SuportedPaymentMethods SelectedPaymentMethod { get; set; }

}
