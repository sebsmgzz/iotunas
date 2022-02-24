namespace IoTunas.Twin.Models;

using Microsoft.Azure.Devices.Shared;
using System;

public static class TwinCollectionExtensions
{

    public static bool TryGet(
        this TwinCollection twinCollection,
        string propertyName,
        out dynamic? propertyValue)
    {
        if (twinCollection.Contains(propertyName))
        {
            propertyValue = twinCollection[propertyName];
            return true;
        }
        propertyValue = null;
        return false;
    }

    public static bool TryAction(
        this TwinCollection twinCollection,
        string propertyName,
        Action<dynamic> propertyAction)
    {
        if (twinCollection.TryGet(propertyName, out var propertyValue))
        {
            propertyAction.Invoke(propertyValue);
            return true;
        }
        return false;
    }

}