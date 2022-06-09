namespace RawImport_Lightspeed.LightspeedClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    public partial class Unit : BaseLightspeedClass
    {
        [JsonIgnore]
        public override bool HasSubtables => true;
        [JsonIgnore]
        public override string TableName => "Deal_Unit";
        [JsonIgnore]
        public override string MatchingColumn => "Deal_ID";
        [JsonIgnore]
        public override long? ItemNumber => ParentRowId;
        public override IEnumerable<string> FieldsToIgnore => base.FieldsToIgnore.Union(new[] { "Parts" });
        //public override List<Type> ChildTypes => new List<Type> { typeof(Part) };
        [JsonProperty("DealerId", NullValueHandling = NullValueHandling.Ignore)]
        public string DealerId { get; set; }

        [JsonProperty("DealUnitId", NullValueHandling = NullValueHandling.Ignore)]
        public long? DealUnitId { get; set; }

        [JsonProperty("Newused", NullValueHandling = NullValueHandling.Ignore)]
        public string Newused { get; set; }

        [JsonProperty("Year", NullValueHandling = NullValueHandling.Ignore)]
        public string Year { get; set; }

        [JsonProperty("Make", NullValueHandling = NullValueHandling.Ignore)]
        public string Make { get; set; }

        [JsonProperty("Model", NullValueHandling = NullValueHandling.Ignore)]
        public string Model { get; set; }

        [JsonProperty("VIN", NullValueHandling = NullValueHandling.Ignore)]
        public string Vin { get; set; }

        [JsonProperty("Class", NullValueHandling = NullValueHandling.Ignore)]
        public string Class { get; set; }

        [JsonProperty("Unitprice", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Unitprice { get; set; }

        [JsonProperty("mucost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Mucost { get; set; }

        [JsonProperty("Freight", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Freight { get; set; }

        [JsonProperty("Freightcost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Freightcost { get; set; }

        [JsonProperty("Handling", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Handling { get; set; }

        [JsonProperty("Handlingcost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Handlingcost { get; set; }

        [JsonProperty("LicFees", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? LicFees { get; set; }

        [JsonProperty("LicFeesCost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? LicFeesCost { get; set; }

        [JsonProperty("Totaccy", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Totaccy { get; set; }

        [JsonProperty("Totinstall", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Totinstall { get; set; }

        [JsonProperty("Tradeall", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Tradeall { get; set; }

        [JsonProperty("Tradeacv", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Tradeacv { get; set; }

        [JsonProperty("Accycost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Accycost { get; set; }

        [JsonProperty("Installcost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Installcost { get; set; }

        [JsonProperty("docfees", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Docfees { get; set; }

        [JsonProperty("actualcost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Actualcost { get; set; }

        [JsonProperty("DateReceived", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(DateTimeValidatorConverter))]
        public DateTime? DateReceived { get; set; }

        [JsonProperty("servcont", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Servcont { get; set; }

        [JsonProperty("sccost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Sccost { get; set; }

        [JsonProperty("propliab", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Propliab { get; set; }

        [JsonProperty("plcost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Plcost { get; set; }

        [JsonProperty("PackAmt", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PackAmt { get; set; }

        [JsonProperty("HoldbackAmt", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? HoldbackAmt { get; set; }

        [JsonProperty("unittype", NullValueHandling = NullValueHandling.Ignore)]
        public string Unittype { get; set; }

        [JsonProperty("SalesType", NullValueHandling = NullValueHandling.Ignore)]
        public string SalesType { get; set; }

        [JsonProperty("UnitSoldPrice", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitSoldPrice { get; set; }

        [JsonProperty("UnitSoldCost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitSoldCost { get; set; }

        [JsonProperty("TotalPartsAmount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TotalPartsAmount { get; set; }

        [JsonProperty("DaysInStore", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? DaysInStore { get; set; }

        [JsonProperty("UnitLine1", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine1 { get; set; }

        [JsonProperty("UnitLine2", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine2 { get; set; }

        [JsonProperty("UnitLine3", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine3 { get; set; }

        [JsonProperty("UnitLine4", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine4 { get; set; }

        [JsonProperty("UnitLine5", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine5 { get; set; }

        [JsonProperty("UnitLine6", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine6 { get; set; }

        [JsonProperty("UnitLine7", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine7 { get; set; }

        [JsonProperty("UnitLine8", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine8 { get; set; }

        [JsonProperty("UnitLine9", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine9 { get; set; }

        [JsonProperty("UnitLine10", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine10 { get; set; }

        [JsonProperty("UnitLine11", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine11 { get; set; }

        [JsonProperty("UnitLine12", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine12 { get; set; }

        [JsonProperty("UnitLine13", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine13 { get; set; }

        [JsonProperty("UnitLine14", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine14 { get; set; }

        [JsonProperty("UnitLine15", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine15 { get; set; }

        [JsonProperty("UnitLine16", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine16 { get; set; }

        [JsonProperty("UnitLine17", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine17 { get; set; }

        [JsonProperty("UnitLine18", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine18 { get; set; }

        [JsonProperty("UnitLine19", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine19 { get; set; }

        [JsonProperty("UnitLine20", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine20 { get; set; }

        [JsonProperty("UnitLine1cost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine1Cost { get; set; }

        [JsonProperty("UnitLine2cost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine2Cost { get; set; }

        [JsonProperty("UnitLine3cost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine3Cost { get; set; }

        [JsonProperty("UnitLine4cost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine4Cost { get; set; }

        [JsonProperty("UnitLine5cost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine5Cost { get; set; }

        [JsonProperty("UnitLine6cost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine6Cost { get; set; }

        [JsonProperty("UnitLine7cost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine7Cost { get; set; }

        [JsonProperty("UnitLine8cost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine8Cost { get; set; }

        [JsonProperty("UnitLine9cost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine9Cost { get; set; }

        [JsonProperty("UnitLine10cost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine10Cost { get; set; }

        [JsonProperty("UnitLine11cost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine11Cost { get; set; }

        [JsonProperty("UnitLine12cost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine12Cost { get; set; }

        [JsonProperty("UnitLine13cost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine13Cost { get; set; }

        [JsonProperty("UnitLine14cost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine14Cost { get; set; }

        [JsonProperty("UnitLine15cost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine15Cost { get; set; }

        [JsonProperty("UnitLine16cost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine16Cost { get; set; }

        [JsonProperty("UnitLine17cost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine17Cost { get; set; }

        [JsonProperty("UnitLine18cost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine18Cost { get; set; }

        [JsonProperty("UnitLine19cost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine19Cost { get; set; }

        [JsonProperty("UnitLine20cost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitLine20Cost { get; set; }

        [JsonProperty("Color", NullValueHandling = NullValueHandling.Ignore)]
        public string Color { get; set; }

        [JsonProperty("Odometer", NullValueHandling = NullValueHandling.Ignore)]
        public string Odometer { get; set; }

        [JsonProperty("stocknumber", NullValueHandling = NullValueHandling.Ignore)]
        public string Stocknumber { get; set; }

        [JsonProperty("deposit", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Deposit { get; set; }

        [JsonProperty("bodytype", NullValueHandling = NullValueHandling.Ignore)]
        public string Bodytype { get; set; }

        [JsonProperty("enginenumber", NullValueHandling = NullValueHandling.Ignore)]
        public string Enginenumber { get; set; }

        [JsonProperty("cylinders", NullValueHandling = NullValueHandling.Ignore)]
        public string Cylinders { get; set; }

        [JsonProperty("gvwr", NullValueHandling = NullValueHandling.Ignore)]
        public string Gvwr { get; set; }

        [JsonProperty("fueltype", NullValueHandling = NullValueHandling.Ignore)]
        public string Fueltype { get; set; }

        [JsonProperty("majorunitsalescategory", NullValueHandling = NullValueHandling.Ignore)]
        public string Majorunitsalescategory { get; set; }

        [JsonProperty("Vehicletaxtotal", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Vehicletaxtotal { get; set; }

        [JsonProperty("Taxableamounttotal", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Taxableamounttotal { get; set; }

        [JsonProperty("Totaltax", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Totaltax { get; set; }

        [JsonProperty("taxpercent", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Taxpercent { get; set; }

        [JsonProperty("salestaxpercent", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Salestaxpercent { get; set; }

        [JsonProperty("vehicletaxpercent", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Vehicletaxpercent { get; set; }

        [JsonProperty("servcontterm", NullValueHandling = NullValueHandling.Ignore)]
        public int? Servcontterm { get; set; }

        [JsonProperty("unitline4term", NullValueHandling = NullValueHandling.Ignore)]
        public string Unitline4Term { get; set; }

        [JsonProperty("unitline5term", NullValueHandling = NullValueHandling.Ignore)]
        public string Unitline5Term { get; set; }

        [JsonProperty("unitline6term", NullValueHandling = NullValueHandling.Ignore)]
        public string Unitline6Term { get; set; }

        [JsonProperty("unitline10term", NullValueHandling = NullValueHandling.Ignore)]
        public string Unitline10Term { get; set; }

        [JsonProperty("unitline11term", NullValueHandling = NullValueHandling.Ignore)]
        public string Unitline11Term { get; set; }

        [JsonProperty("unitline12term", NullValueHandling = NullValueHandling.Ignore)]
        public string Unitline12Term { get; set; }

        [JsonProperty("plateno", NullValueHandling = NullValueHandling.Ignore)]
        public string Plateno { get; set; }

        [JsonProperty("customerunitid", NullValueHandling = NullValueHandling.Ignore)]
        public int? Customerunitid { get; set; }

        [JsonProperty("packageid", NullValueHandling = NullValueHandling.Ignore)]
        public long? Packageid { get; set; }

        [JsonProperty("unitid", NullValueHandling = NullValueHandling.Ignore)]
        public string Unitid { get; set; }

        [JsonProperty("modelname", NullValueHandling = NullValueHandling.Ignore)]
        public string Modelname { get; set; }

        [JsonProperty("majorunitid", NullValueHandling = NullValueHandling.Ignore)]
        public int? Majorunitid { get; set; }

        [JsonProperty("manufacturer", NullValueHandling = NullValueHandling.Ignore)]
        public string Manufacturer { get; set; }

        [JsonProperty("msrp", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Msrp { get; set; }

        [JsonProperty("partscustomeraddtaxablecost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Partscustomeraddtaxablecost { get; set; }

        [JsonProperty("partscustomeraddnontaxablecost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Partscustomeraddnontaxablecost { get; set; }

        [JsonProperty("partsdealeraddtaxablecost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Partsdealeraddtaxablecost { get; set; }

        [JsonProperty("partsdealeraddnontaxablecost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Partsdealeraddnontaxablecost { get; set; }

        [JsonProperty("laborcustomeraddtaxablecost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Laborcustomeraddtaxablecost { get; set; }

        [JsonProperty("laborcustomeraddnontaxablecost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Laborcustomeraddnontaxablecost { get; set; }

        [JsonProperty("labordealeraddtaxablecost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Labordealeraddtaxablecost { get; set; }

        [JsonProperty("labordealeraddnontaxablecost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Labordealeraddnontaxablecost { get; set; }

        [JsonProperty("Parts", NullValueHandling = NullValueHandling.Ignore)]
        public List<Part> Parts { get; set; }
    }
}
