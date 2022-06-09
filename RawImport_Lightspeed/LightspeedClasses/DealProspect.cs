namespace RawImport_Lightspeed.LightspeedClasses
{
    using Newtonsoft.Json;

    public partial class DealProspect : BaseLightspeedClass
    {
        [JsonIgnore]
        public override bool HasSubtables => false;
        [JsonIgnore]
        public override string TableName => "Deal_Prospect";
        [JsonIgnore]
        public override string MatchingColumn => "Deal_ID";
        [JsonIgnore]
        public override long? ItemNumber => ParentRowId;
        [JsonProperty("DealerId", NullValueHandling = NullValueHandling.Ignore)]
        public string DealerId { get; set; }

        [JsonProperty("dealid", NullValueHandling = NullValueHandling.Ignore)]
        public string Dealid { get; set; }

        [JsonProperty("sourceprospectid", NullValueHandling = NullValueHandling.Ignore)]
        public string Sourceprospectid { get; set; }

        [JsonProperty("prospectsourceid", NullValueHandling = NullValueHandling.Ignore)]
        public string Prospectsourceid { get; set; }

        [JsonProperty("prospectstate", NullValueHandling = NullValueHandling.Ignore)]
        public string Prospectstate { get; set; }
    }
}
