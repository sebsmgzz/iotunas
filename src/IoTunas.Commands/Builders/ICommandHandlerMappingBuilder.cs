namespace IoTunas.Extensions.Commands.Builders;

using IoTunas.Extensions.Commands.Models;

public interface ICommandHandlerMappingBuilder
{
    
    int Count { get; }
    
    void AddHandler<T>() where T : ICommandHandler;
    
    void AddHandler<T>(string methodName) where T : ICommandHandler;
    
    void MapHandlers();

}