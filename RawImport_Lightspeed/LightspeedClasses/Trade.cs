namespace RawImport_Lightspeed.LightspeedClasses
{
    using Newtonsoft.Json;

    public partial class Trade : BaseLightspeedClass
    {
        [JsonIgnore]
        public override bool HasSubtables => false;
        [JsonIgnore]
        public override string TableName => "Deal_Trade";
        [JsonIgnore]
        public override string MatchingColumn => "Deal_ID";
        [JsonIgnore]
        public override long? ItemNumber => ParentRowId;
        [JsonProperty("DealerId", NullValueHandling = NullValueHandling.Ignore)]
        public string DealerId { get; set; }

        [JsonProperty("DealTradeId", NullValueHandling = NullValueHandling.Ignore)]
        public long DealTradeId { get; set; }

        [JsonProperty("VIN", NullValueHandling = NullValueHandling.Ignore)]
        public string Vin { get; set; }

        [JsonProperty("StdMake", NullValueHandling = NullValueHandling.Ignore)]
        public string StdMake { get; set; }

        [JsonProperty("StdModel", NullValueHandling = NullValueHandling.Ignore)]
        public string StdModel { get; set; }

        [JsonProperty("StdYear", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? StdYear { get; set; }

        [JsonProperty("StdCategory", NullValueHandling = NullValueHandling.Ignore)]
        public string StdCategory { get; set; }

        [JsonProperty("allowance", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Allowance { get; set; }

        [JsonProperty("acv", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Acv { get; set; }

        [JsonProperty("payoff", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Payoff { get; set; }

        [JsonProperty("lienholder", NullValueHandling = NullValueHandling.Ignore)]
        public string Lienholder { get; set; }

        [JsonProperty("modelname", NullValueHandling = NullValueHandling.Ignore)]
        public string Modelname { get; set; }

        [JsonProperty("unitclass", NullValueHandling = NullValueHandling.Ignore)]
        public string Unitclass { get; set; }

        [JsonProperty("color", NullValueHandling = NullValueHandling.Ignore)]
        public string Color { get; set; }

        [JsonProperty("odometer", NullValueHandling = NullValueHandling.Ignore)]
        public long? Odometer { get; set; }

        [JsonProperty("manufacturer", NullValueHandling = NullValueHandling.Ignore)]
        public string Manufacturer { get; set; }

        [JsonProperty("note", NullValueHandling = NullValueHandling.Ignore)]
        public string Note { get; set; }

        [JsonProperty("unittype", NullValueHandling = NullValueHandling.Ignore)]
        public string Unittype { get; set; }
    }
}
