namespace IoTunas.Commands.Builders;

using IoTunas.Commands.Collections;
using System;

public interface ICommandServicesBuilder
{
    
    void AddHandlers(Action<ICommandHandlerMapping> configure);
    
    void AddInvokers();

}