﻿namespace IoTunas.Extensions.Methods.Models;

using Microsoft.Azure.Devices.Client;
using System.Threading.Tasks;

public interface ICommand
{

    Task<MethodResponse> HandleAsync(
        MethodRequest methodRequest, object userContext);

}