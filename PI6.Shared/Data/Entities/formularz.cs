﻿namespace PI6.Shared.Data.Entities;

public class formularz
{
    public int for_id { get; set; }
    public string for_nazwa { get; set; }
    public int for_fort_id { get; set; }
    public DateTime for_data_stworzenia { get; set; }
    public DateTime for_data_otwarcia { get; set; }
    public DateTime? for_data_zamkniecia { get; set; }
    public int? for_dozwolone_podejscia { get; set; }
    public int? for_limit_czasu { get; set; }
    public int? for_prog_zal { get; set; }
}