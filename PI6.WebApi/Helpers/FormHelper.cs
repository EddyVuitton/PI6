using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;
using System.Xml.Serialization;

namespace PI6.WebApi.Helpers;

public static class FormHelper
{
    public static string GenerateXml(FormularzDto form)
    {
        using var stream = new StringWriter();
        var serializer = new XmlSerializer(typeof(FormularzDto), new XmlRootAttribute("Formularz"));
        serializer.Serialize(stream, form);
        var xml = stream.ToString();

        stream.Close();

        return stream.ToString();
    }

    public static string GenerateXmlGeneric<T>(T objectToGenerateXml, string rootAttribute)
    {
        using var stream = new StringWriter();
        var serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(rootAttribute));
        serializer.Serialize(stream, objectToGenerateXml);
        var xml = stream.ToString();

        stream.Close();

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

    public static List<formularz_odpowiedz> GetFormAnswer(List<formularz_pytanie_opcja> options, int formId)
    {
        var answers = new List<formularz_odpowiedz>();

        foreach (var o in options)
        {
            answers.Add(new formularz_odpowiedz()
            {
                fodp_id = -1,
                fodp_for_id = formId,
                fodp_forp_id = o.fpop_forp_id,
                fodp_wybrana_odp = o.fpop_id
            });
        }

        return answers;
    }

    public static List<GroupAssignedFormCheckDto> GetGroupAssignedFormCheckDto(List<student_group> studentGroups, List<group_assigned_forms> groupAssignedForms, int usId, int forId)
    {
        var dtos = new List<GroupAssignedFormCheckDto>();
        if (studentGroups == null || studentGroups.Count == 0)
            return dtos;

        foreach (var g in studentGroups)
        {
            if (g == null)
                break;
            else
            {
                var check = groupAssignedForms.FirstOrDefault(x => x.gaf_sgr_id == g.sgr_id);
                var temp = new GroupAssignedFormCheckDto()
                {
                    UsId = usId,
                    GrpId = g.sgr_id,
                    GrpName = g.sgr_name,
                    ForId = forId,
                    Check = check != null
                };
                dtos.Add(temp);
            }
        }

        return dtos;
    }
}