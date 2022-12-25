﻿namespace IoTunas.Extensions.Commands.Services.Factories;

using IoTunas.Extensions.Commands.Models;

public interface ICommandFactory
{

    bool TryGet(string methodName, out ICommand handler);

}