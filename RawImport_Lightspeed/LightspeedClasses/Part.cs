using Newtonsoft.Json;

namespace RawImport_Lightspeed.LightspeedClasses
{
    public class Part : BaseLightspeedClass
    {
        [JsonIgnore]
        public override bool HasSubtables => false;
        [JsonIgnore]
        public override string MatchingColumn => "Deal_Unit_ID";
        [JsonIgnore]
        public override long? ItemNumber => ParentRowId;
        public override string ItemString => PartNumber;
        public override string TableName => "Deal_Unit_Part";
        [JsonProperty("DealerId", NullValueHandling = NullValueHandling.Ignore)]
        public string DealerId { get; set; }

        [JsonProperty("MajorUnitPartsId", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long MajorUnitPartsId { get; set; }

        [JsonProperty("PartNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string PartNumber { get; set; }

        [JsonProperty("SupplierCode", NullValueHandling = NullValueHandling.Ignore)]
        public string SupplierCode { get; set; }

        [JsonProperty("RequestedQty", NullValueHandling = NullValueHandling.Ignore)]
        public string RequestedQty { get; set; }

        [JsonProperty("SpecialOrderQty", NullValueHandling = NullValueHandling.Ignore)]
        public string SpecialOrderQty { get; set; }

        [JsonProperty("Qty", NullValueHandling = NullValueHandling.Ignore)]
        public string Qty { get; set; }

        [JsonProperty("Cost", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringDecimalConverter))]
        public decimal? Cost { get; set; }

        [JsonProperty("Price", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringDecimalConverter))]
        public decimal? Price { get; set; }

        [JsonProperty("Retail", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringDecimalConverter))]
        public decimal? Retail { get; set; }

        [JsonProperty("SetupInstall", NullValueHandling = NullValueHandling.Ignore)]
        public string SetupInstall { get; set; }

        [JsonProperty("Description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("dealstate", NullValueHandling = NullValueHandling.Ignore)]
        public string Dealstate { get; set; }

        [JsonProperty("internaltype", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringIntegerConverter))]
        public int? Internaltype { get; set; }
    }
}