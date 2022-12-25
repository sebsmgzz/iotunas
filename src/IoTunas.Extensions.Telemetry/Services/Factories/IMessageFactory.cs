﻿namespace IoTunas.Extensions.Telemetry.Factories;

using Microsoft.Azure.Devices.Client;

public interface IMessageFactory
{

    Message Create(byte[] bytesMessage);

    Message Create(Stream streamMessage);

    Message Create(string stringMessage);

    Message Create(object objectMessage);

}
