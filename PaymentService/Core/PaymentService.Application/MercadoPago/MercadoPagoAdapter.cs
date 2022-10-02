using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentService.Application.Exceptions;
using Application.Payments;
using Application.Payments.DTOs;
using Application.Payments.Enums;
using Application.Payments.Responses;
using Application;
using Application.Payments.Ports;

namespace PaymentService.Application.MercadoPago;

public class MercadoPagoAdapter : IPaymentProcessor
{
    public Task<PaymentResponse> CapturePayment(string paymentIntention)
    {
        try
        {
            if (string.IsNullOrEmpty(paymentIntention))
            {
                throw new InvalidPaymentIntentionException();
            }
            var dto = new PaymentStateDTO
            {
                CreatedDate = DateTime.Now,
                Message = $"Successfully paid: {paymentIntention}",
                Status = PaymentStatus.Success
            };
            var response = new PaymentResponse
            {
                Data = dto,
                Success = true
            };
            return Task.FromResult(response);
        }
        catch (InvalidPaymentIntentionException)
        {
            var response = new PaymentResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.PAYMENTS_INVALID_PAYMENT_INTENTION,
                Message = $"Invalid payment intention: {paymentIntention}"
            };
            return Task.FromResult(response);
        }
        catch(Exception)
        {
            var response = new PaymentResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.PAYMENTS_INVALID_PAYMENT_INTENTION,
                Message = "Could not process payment intention"
            };
            return Task.FromResult(response);
        }
    }
}
