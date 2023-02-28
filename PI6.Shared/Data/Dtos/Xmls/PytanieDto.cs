﻿using System.Xml.Serialization;

namespace PI6.Shared.Data.Dtos.Xmls;

public class PytanieDto
{
    [XmlAttribute(AttributeName = "PytanieId")]
    public int PytanieId { get; set; }

    [XmlAttribute(AttributeName = "Nazwa")]
    public string PytanieNazwa { get; set; }
    [XmlAttribute(AttributeName = "Punkty")]
    public int PytaniePunkty { get; set; }
    [XmlAttribute(AttributeName = "CzyWieleOdp")]
    public bool PytanieCzyWieleOdp { get; set; }
    [XmlAttribute(AttributeName = "CzyWymagane")]
    public bool PytanieCzyWymagane { get; set; }
    [XmlAttribute(AttributeName = "ForId")]
    public int PytanieForId { get; set; }
    [XmlAttribute(AttributeName = "NumerPyt")]
    public int PytanieNumerPyt { get; set; }

    [XmlElement(ElementName = "OpcjaDto")]
    public List<OpcjaDto> Opcje { get; set; }
}