using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Payments.Ports;
using Application.Payments.Responses;

namespace PaymentService.Application;

public class NotImplementedPaymentProvider : IPaymentProcessor
{
    public Task<PaymentResponse> CapturePayment(string paymentIntention)
    {
        var paymentResponse = new PaymentResponse()
        {
            Success = false,
            ErrorCode = ErrorCodes.PAYMENTS_PAYMENT_PROVIDER_NOT_IMPLEMENTED,
            Message = "The selected payment provider is not available at the moment"
        };
        return Task.FromResult(paymentResponse);
    }
}
