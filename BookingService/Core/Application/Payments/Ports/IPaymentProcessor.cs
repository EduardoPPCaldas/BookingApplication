using Application.Payments.DTOs;
using Application.Payments.Responses;

namespace Application.Payments.Ports;

public interface IPaymentProcessor
{
    Task<PaymentResponse> CapturePayment(string paymentIntention);
}
