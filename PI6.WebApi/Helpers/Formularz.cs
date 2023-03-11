using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;
using PI6.WebApi.Repositories;
using System.Xml.Serialization;

namespace PI6.WebApi.Helpers;

public static class Formularz
{
    public static string GenerateXml(FormularzDto form)
    {
        using var stream = new StringWriter();
        var serializer = new XmlSerializer(typeof(FormularzDto), new XmlRootAttribute("Formularz"));
        serializer.Serialize(stream, form);
        var xml = stream.ToString();

        return stream.ToString();
    }
}