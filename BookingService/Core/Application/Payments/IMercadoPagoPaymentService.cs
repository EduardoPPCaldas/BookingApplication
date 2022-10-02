using Application.Payments.DTOs;
using Application.Payments.Responses;

namespace Application.Payments;

public interface IMercadoPagoPaymentService
{
    Task<PaymentResponse> PayWithCreditCard(string paymentIntention);
    Task<PaymentResponse> PayWithDebitCard(string paymentIntention);
    Task<PaymentResponse> PayWithTransfer(string paymentIntention);
}
