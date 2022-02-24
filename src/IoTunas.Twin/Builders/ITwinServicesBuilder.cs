namespace IoTunas.Twin.Builders;

using IoTunas.Twin.Collections;
using IoTunas.Twin.Models;
using System;

public interface ITwinServicesBuilder
{

    void AddDesiredProperties(
        Action<ITwinPropertyMapping<DesiredProperty>> configure);
    
    void AddReportedProperties(
        Action<ITwinPropertyMapping<ReportedProperty>> configure);

}