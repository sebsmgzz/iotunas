namespace IoTunas.Core.Collections;

using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Client.Transport.Mqtt;

/// <summary>
/// A list of client
/// </summary>
public class TransportSettingsList : List<ITransportSettings>
{

    /// <summary>
    /// Adds an MQTT transport setting.
    /// </summary>
    /// <param name="transportType">The transport type to be used by the module client. 
    /// Either <see cref="TransportType.Mqtt_WebSocket_Only"/> or <see cref="TransportType.Mqtt_Tcp_Only"/></param>
    /// <returns>The implemented settings.</returns>
    public MqttTransportSettings AddMqtt(TransportType transportType)
    {
        var settings = new MqttTransportSettings(transportType);
        Add(settings);
        return settings;
    }

    /// <summary>
    /// Adds an AMQP transport setting.
    /// </summary>
    /// <param name="transportType">The transport type to be used by the module client. 
    /// Either <see cref="TransportType.Amqp_WebSocket_Only"/> or <see cref="TransportType.Amqp_Tcp_Only"/></param>
    /// <returns>The implemented settings.</returns>
    public AmqpTransportSettings AddAmqp(TransportType transportType)
    {
        var settings = new AmqpTransportSettings(transportType);
        Add(settings);
        return settings;
    }

    /// <summary>
    /// Adds an HTTP transport setting.
    /// </summary>
    /// <returns>The implemented settings.</returns>
    public Http1TransportSettings AddHttp()
    {
        var settings = new Http1TransportSettings();
        Add(settings);
        return settings;
    }

    public static implicit operator ITransportSettings[](TransportSettingsList transportSettings)
    {
        return transportSettings.ToArray();
    }

}
