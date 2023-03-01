using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;
using System.Xml.Serialization;

namespace PI6.WebApi.Helpers;

public static class Formularz
{
    public static string GenerateXml(List<formularz_pytanie> questions, List<formularz_pytanie_opcja> options)
    {
        var optionsList =
            from option in options
            select new OpcjaDto
            {
                PytanieId = option.fpop_forp_id,
                OpcjaId = option.fpop_id,
                OpcjaNazwa = option.fpop_nazwa,
                OpcjaCzyPoprawna = option.fpop_czy_poprawna,
                OpcjaNumerOpc = (int)option.forp_numer_opcji
            };

        var data =
        from question in questions
        select new PytanieDto
        {
            PytanieId = question.forp_id,
            PytanieNazwa = question.forp_nazwa,
            PytaniePunkty = question.forp_punkty,
            PytanieCzyWieleOdp = question.forp_czy_wiele_odp,
            PytanieCzyWymagane = question.forp_czy_wymagane,
            PytanieForId = question.forp_for_id,
            PytanieNumerPyt = (int)question.forp_numer_pytania,

            Opcje = optionsList.Where(x => x.PytanieId == question.forp_id).ToList()
        };

        var dataList = data.ToList();

        using var stream = new StringWriter();
        var serializer = new XmlSerializer(typeof(List<PytanieDto>), new XmlRootAttribute("Pytania"));
        serializer.Serialize(stream, dataList);
        var xml = stream.ToString().Replace(" encoding=\"utf-16\"", "").Replace(@"\r\n", "").Replace(@"\", "");
        var path = @"D:\Programowanie\visual\csharp\console\ConsoleApp\ConsoleApp\formularz.xml";
        //File.WriteAllText(path, xml);

        return xml;
    }

    public static string GenerateXml1(FormularzDto form)
    {
        var optionsList =
            from option in options
            select new OpcjaDto
            {
                PytanieId = option.fpop_forp_id,
                OpcjaId = option.fpop_id,
                OpcjaNazwa = option.fpop_nazwa,
                OpcjaCzyPoprawna = option.fpop_czy_poprawna,
                OpcjaNumerOpc = (int)option.forp_numer_opcji
            };

        var data =
        from question in questions
        select new PytanieDto
        {
            PytanieId = question.forp_id,
            PytanieNazwa = question.forp_nazwa,
            PytaniePunkty = question.forp_punkty,
            PytanieCzyWieleOdp = question.forp_czy_wiele_odp,
            PytanieCzyWymagane = question.forp_czy_wymagane,
            PytanieForId = question.forp_for_id,
            PytanieNumerPyt = (int)question.forp_numer_pytania,

            Opcje = optionsList.Where(x => x.PytanieId == question.forp_id).ToList()
        };

        var dataList = data.ToList();

        using var stream = new StringWriter();
        var serializer = new XmlSerializer(typeof(List<PytanieDto>), new XmlRootAttribute("Pytania"));
        serializer.Serialize(stream, dataList);
        var xml = stream.ToString().Replace(" encoding=\"utf-16\"", "").Replace(@"\r\n", "").Replace(@"\", "");
        var path = @"D:\Programowanie\visual\csharp\console\ConsoleApp\ConsoleApp\formularz.xml";
        //File.WriteAllText(path, xml);

        return xml;
    }
}