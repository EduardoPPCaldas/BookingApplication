using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Payments.Enums;

namespace Application.Payments.Ports;

public interface IPaymentProcessorFactory
{
    IPaymentProcessor GetPaymentProcessor(SuportedPaymentProviders selectedPaymentProvider);
}
