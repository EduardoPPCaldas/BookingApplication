using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Payments;
using Application.Payments.Enums;
using Application.Payments.Ports;
using PaymentService.Application.Exceptions;
using PaymentService.Application.MercadoPago;

namespace PaymentService.Application;

public class PaymentProcessorFactory : IPaymentProcessorFactory
{
    public IPaymentProcessor GetPaymentProcessor(SuportedPaymentProviders selectedPaymentProvider)
    {
        switch(selectedPaymentProvider)
        {
            case SuportedPaymentProviders.MercadoPago:
                return new MercadoPagoAdapter();
            
            default:
                return new NotImplementedPaymentProvider();
        }
    }
}
