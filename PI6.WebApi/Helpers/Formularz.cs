using Microsoft.AspNetCore.Mvc;
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

    public static List<PytanieDto> GetFormQuestionsDto(List<formularz_pytanie> questions, List<formularz_pytanie_opcja> options)
    {
        var tempO =
            from option in options
            select new OpcjaDto
            {
                PytanieId = option.fpop_forp_id,
                OpcjaId = option.fpop_id,
                OpcjaNazwa = option.fpop_nazwa,
                OpcjaCzyPoprawna = option.fpop_czy_poprawna,
                OpcjaNumerOpc = option.forp_numer_opcji ?? 0
            };

        var tempQ =
            from question in questions
            select new PytanieDto
            {
                PytanieId = question.forp_id,

                PytanieNazwa = question.forp_nazwa,

                PytaniePunkty = question.forp_punkty,

                PytanieCzyWieleOdp = question.forp_czy_wiele_odp,

                PytanieCzyWymagane = question.forp_czy_wymagane,

                PytanieForId = question.forp_for_id,

                PytanieNumerPyt = question.forp_numer_pytania ?? 0,

                Opcje = tempO.Where(x => x.PytanieId == question.forp_id).ToList()
            };

        return tempQ.ToList();
    }

    public static FormularzDto GetFormularzDto(formularz form, List<PytanieDto> questionsDto)
    {
        var tempF = new FormularzDto
        {
            ForId = form.for_id,
            Nazwa = form.for_nazwa,
            DataStworzenia = form.for_data_stworzenia,
            DataOtwarcia = form.for_data_otwarcia,
            DataZamkniecia = form.for_data_zamkniecia,
            DozwolonePodejscia = form.for_dozwolone_podejscia,
            LimitCzasu = form.for_limit_czasu,
            ProgZal = form.for_prog_zal,
            FortId = form.for_fort_id,
            FortNazwa = string.Empty,
            Pytania = questionsDto
        };

        return tempF;
    }
}