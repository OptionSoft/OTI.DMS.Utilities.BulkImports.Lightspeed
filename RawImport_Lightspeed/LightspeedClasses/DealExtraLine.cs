using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawImport_Lightspeed.LightspeedClasses
{
    public partial class DealExtraLine : BaseLightspeedClass
    {
        [JsonIgnore]
        public override bool HasSubtables => false;
        [JsonIgnore]
        public override string MatchingColumn => "Deal_ID";
        public override string SecondaryMatch => "ExtraLineNumber";
        [JsonIgnore]
        public override long? ItemNumber => ParentRowId;
        public override string ItemString => ExtraLineNumber;
        public override string TableName => "Deal_ExtraLine";
        [JsonProperty("DealerId", NullValueHandling = NullValueHandling.Ignore)]
        public string DealerId { get; set; }

        [JsonProperty("DealUnitId", NullValueHandling = NullValueHandling.Ignore)]
        public long? DealUnitId { get; set; }

        [JsonProperty("FinInvoiceId", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringIntegerConverter))]
        public int? FinInvoiceId { get; set; }

        [JsonProperty("LineName", NullValueHandling = NullValueHandling.Ignore)]
        public string LineName { get; set; }

        [JsonProperty("ExtraLineNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string ExtraLineNumber { get; set; }

        [JsonProperty("LineType", NullValueHandling = NullValueHandling.Ignore)]
        public string LineType { get; set; }

        [JsonProperty("Amount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Amount { get; set; }

        [JsonProperty("Cost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Cost { get; set; }

        [JsonProperty("Term", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringIntegerConverter))]
        public int? Term { get; set; }

        //[JsonProperty("arcustomer")]
        //public object Arcustomer { get; set; }

        //[JsonProperty("apvendor")]
        //public object Apvendor { get; set; }
    }
}
