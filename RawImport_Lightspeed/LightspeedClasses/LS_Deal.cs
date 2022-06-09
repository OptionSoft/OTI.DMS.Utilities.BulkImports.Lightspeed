namespace RawImport_Lightspeed.LightspeedClasses
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class LS_Deal : PrimaryTable
    {
        [JsonIgnore]
        public override bool HasSubtables => true;
        [JsonIgnore]
        public override string TableName => "Deal";
        [JsonIgnore]
        public override string MatchingColumn => "DealNo";
        [JsonIgnore]
        public override long? ItemNumber => DealNo;
        public override IEnumerable<string> FieldsToIgnore => base.FieldsToIgnore.Union(new [] { "Units", "Trade", "DealExtraLines", "DealProspect" });
        //public override List<Type> ChildTypes => new List<Type> { typeof(Unit), typeof(Trade), typeof(DealProspect), typeof(DealExtraLine) };
        //public override IEnumerable<BaseLightspeedClass> GetSubClasses(Type t) 
        //{
        //    switch (t.Name)
        //    {
        //        case "Unit": return Units.Select(x=> (x as BaseLightspeedClass));
        //    }
        //    return base.GetSubClasses(t);
        //}

        [JsonProperty("Cmf", NullValueHandling = NullValueHandling.Ignore)]
        public string Cmf { get; set; }

        [JsonProperty("DealerId", NullValueHandling = NullValueHandling.Ignore)]
        public string DealerId { get; set; }

        [JsonProperty("DealNo", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long DealNo { get; set; }

        [JsonProperty("FinInvoiceId", NullValueHandling = NullValueHandling.Ignore)]
        public int? FinInvoiceId { get; set; }

        [JsonProperty("CommonInvoiceID", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? CommonInvoiceId { get; set; }

        [JsonProperty("FinanceDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? FinanceDate { get; set; }

        [JsonProperty("OriginatingDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? OriginatingDate { get; set; }

        [JsonProperty("DeliveryDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? DeliveryDate { get; set; }

        [JsonProperty("lastmodifieddate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Lastmodifieddate { get; set; }

        [JsonProperty("salesmanid", NullValueHandling = NullValueHandling.Ignore)]
        public string Salesmanid { get; set; }

        [JsonProperty("clpremium", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Clpremium { get; set; }

        [JsonProperty("clcost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Clcost { get; set; }

        [JsonProperty("sourcetype", NullValueHandling = NullValueHandling.Ignore)]
        public string Sourcetype { get; set; }

        [JsonProperty("ahpremium", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Ahpremium { get; set; }

        [JsonProperty("ahcost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Ahcost { get; set; }

        [JsonProperty("CustID", NullValueHandling = NullValueHandling.Ignore)]
        public string CustId { get; set; }

        [JsonProperty("lienholder", NullValueHandling = NullValueHandling.Ignore)]
        public string Lienholder { get; set; }

        [JsonProperty("AmtFinanced", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? AmtFinanced { get; set; }

        [JsonProperty("Term", NullValueHandling = NullValueHandling.Ignore)]
        public int? Term { get; set; }

        [JsonProperty("Rate", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Rate { get; set; }

        [JsonProperty("Payment", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Payment { get; set; }

        [JsonProperty("Downpayment", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Downpayment { get; set; }

        [JsonProperty("totaldownpayment", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Totaldownpayment { get; set; }

        [JsonProperty("fincharge", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Fincharge { get; set; }

        [JsonProperty("fincost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Fincost { get; set; }

        [JsonProperty("DaysToFirst", NullValueHandling = NullValueHandling.Ignore)]
        public int? DaysToFirst { get; set; }

        [JsonProperty("SalesmanName", NullValueHandling = NullValueHandling.Ignore)]
        public string SalesmanName { get; set; }

        [JsonProperty("lienaddressfirstline", NullValueHandling = NullValueHandling.Ignore)]
        public string Lienaddressfirstline { get; set; }

        [JsonProperty("lienaddresssecondline", NullValueHandling = NullValueHandling.Ignore)]
        public string Lienaddresssecondline { get; set; }

        [JsonProperty("liencity", NullValueHandling = NullValueHandling.Ignore)]
        public string Liencity { get; set; }

        [JsonProperty("lienzip", NullValueHandling = NullValueHandling.Ignore)]
        public string Lienzip { get; set; }

        [JsonProperty("lienstate", NullValueHandling = NullValueHandling.Ignore)]
        public string Lienstate { get; set; }

        [JsonProperty("cobuyercustid", NullValueHandling = NullValueHandling.Ignore)]
        public string Cobuyercustid { get; set; }

        [JsonProperty("cobuyername", NullValueHandling = NullValueHandling.Ignore)]
        public string Cobuyername { get; set; }

        [JsonProperty("cobuyeraddr", NullValueHandling = NullValueHandling.Ignore)]
        public string Cobuyeraddr { get; set; }

        [JsonProperty("cobuyeraddr2", NullValueHandling = NullValueHandling.Ignore)]
        public string Cobuyeraddr2 { get; set; }

        [JsonProperty("cobuyercity", NullValueHandling = NullValueHandling.Ignore)]
        public string Cobuyercity { get; set; }

        [JsonProperty("cobuyerstate", NullValueHandling = NullValueHandling.Ignore)]
        public string Cobuyerstate { get; set; }

        [JsonProperty("cobuyerzip", NullValueHandling = NullValueHandling.Ignore)]
        public string Cobuyerzip { get; set; }

        [JsonProperty("cobuyercounty", NullValueHandling = NullValueHandling.Ignore)]
        public string Cobuyercounty { get; set; }

        [JsonProperty("cobuyerhomephone", NullValueHandling = NullValueHandling.Ignore)]
        public string Cobuyerhomephone { get; set; }

        [JsonProperty("cobuyerworkphone", NullValueHandling = NullValueHandling.Ignore)]
        public string Cobuyerworkphone { get; set; }

        [JsonProperty("cobuyercellphone", NullValueHandling = NullValueHandling.Ignore)]
        public string Cobuyercellphone { get; set; }

        [JsonProperty("cobuyeremail", NullValueHandling = NullValueHandling.Ignore)]
        public string Cobuyeremail { get; set; }

        [JsonProperty("stagename", NullValueHandling = NullValueHandling.Ignore)]
        public string Stagename { get; set; }

        [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
        public string Source { get; set; }

        [JsonProperty("cobuyerbirthdate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Cobuyerbirthdate { get; set; }

        [JsonProperty("Salesman2name", NullValueHandling = NullValueHandling.Ignore)]
        public string Salesman2Name { get; set; }

        [JsonProperty("Salesman2id", NullValueHandling = NullValueHandling.Ignore)]
        public string Salesman2Id { get; set; }

        [JsonProperty("Salesmanfi1id", NullValueHandling = NullValueHandling.Ignore)]
        public string Salesmanfi1Id { get; set; }

        [JsonProperty("Salesmanfi1name", NullValueHandling = NullValueHandling.Ignore)]
        public string Salesmanfi1Name { get; set; }

        [JsonProperty("Salesmanfi2id", NullValueHandling = NullValueHandling.Ignore)]
        public string Salesmanfi2Id { get; set; }

        [JsonProperty("Salesmanfi2name", NullValueHandling = NullValueHandling.Ignore)]
        public string Salesmanfi2Name { get; set; }

        [JsonProperty("Fincostoverride", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Fincostoverride { get; set; }

        [JsonProperty("Salesman1split", NullValueHandling = NullValueHandling.Ignore)]
        public int? Salesman1Split { get; set; }

        [JsonProperty("Salesman2split", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Salesman2Split { get; set; }

        [JsonProperty("Salestaxtotal", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Salestaxtotal { get; set; }

        [JsonProperty("Vehicletaxtotal", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Vehicletaxtotal { get; set; }

        [JsonProperty("Insurancetaxtotal", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Insurancetaxtotal { get; set; }

        [JsonProperty("totalcashprice", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Totalcashprice { get; set; }

        [JsonProperty("totalprevpymt", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Totalprevpymt { get; set; }

        [JsonProperty("additionalpymttoday", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Additionalpymttoday { get; set; }

        [JsonProperty("deferredamt", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Deferredamt { get; set; }

        [JsonProperty("extra1amt", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Extra1Amt { get; set; }

        [JsonProperty("balancetofinance", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Balancetofinance { get; set; }

        [JsonProperty("totalofpayments", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Totalofpayments { get; set; }

        [JsonProperty("addonrate", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Addonrate { get; set; }

        [JsonProperty("aprbuyrate", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Aprbuyrate { get; set; }

        [JsonProperty("addonbuyrate", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Addonbuyrate { get; set; }

        [JsonProperty("balloonterm", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Balloonterm { get; set; }

        [JsonProperty("balloonpayment", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Balloonpayment { get; set; }

        [JsonProperty("ahprice", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Ahprice { get; set; }

        [JsonProperty("clprice", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Clprice { get; set; }

        [JsonProperty("ins1amt", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Ins1Amt { get; set; }

        [JsonProperty("ins1cost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Ins1Cost { get; set; }

        [JsonProperty("ins2amt", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Ins2Amt { get; set; }

        [JsonProperty("ins2cost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Ins2Cost { get; set; }

        [JsonProperty("ins3amt", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Ins3Amt { get; set; }

        [JsonProperty("ins3cost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Ins3Cost { get; set; }

        [JsonProperty("ins4amt", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Ins4Amt { get; set; }

        [JsonProperty("ins4cost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Ins4Cost { get; set; }

        [JsonProperty("ins5amt", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Ins5Amt { get; set; }

        [JsonProperty("ins5cost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Ins5Cost { get; set; }

        [JsonProperty("ins6amt", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Ins6Amt { get; set; }

        [JsonProperty("ins6cost", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Ins6Cost { get; set; }

        [JsonProperty("insurancetaxtotal", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? DealInsurancetaxtotal { get; set; }

        [JsonProperty("femargin", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Femargin { get; set; }

        [JsonProperty("bemargin", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Bemargin { get; set; }

        [JsonProperty("createdate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Createdate { get; set; }

        [JsonProperty("customerextraline", NullValueHandling = NullValueHandling.Ignore)]
        public string Customerextraline { get; set; }

        [JsonProperty("customernotes")]
        public string Customernotes { get; set; }

        [JsonProperty("salesmanusername", NullValueHandling = NullValueHandling.Ignore)]
        public string Salesmanusername { get; set; }

        [JsonProperty("fincostoverride", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? DealFincostoverride { get; set; }

        [JsonProperty("isfincostoverride", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Isfincostoverride { get; set; }

        [JsonProperty("firstlienaddressfirstline", NullValueHandling = NullValueHandling.Ignore)]
        public string Firstlienaddressfirstline { get; set; }

        [JsonProperty("firstlienaddresssecondline", NullValueHandling = NullValueHandling.Ignore)]
        public string Firstlienaddresssecondline { get; set; }

        [JsonProperty("firstliencity", NullValueHandling = NullValueHandling.Ignore)]
        public string Firstliencity { get; set; }

        [JsonProperty("firstlienzip", NullValueHandling = NullValueHandling.Ignore)]
        public string Firstlienzip { get; set; }

        [JsonProperty("firstlienstate", NullValueHandling = NullValueHandling.Ignore)]
        public string Firstlienstate { get; set; }

        [JsonProperty("salestaxtotal", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? DealSalestaxtotal { get; set; }

        [JsonProperty("vehicletaxtotal", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? DealVehicletaxtotal { get; set; }

        [JsonProperty("salesagent1", NullValueHandling = NullValueHandling.Ignore)]
        public string Salesagent1 { get; set; }

        [JsonProperty("salesagent2", NullValueHandling = NullValueHandling.Ignore)]
        public string Salesagent2 { get; set; }

        [JsonProperty("salesagent3", NullValueHandling = NullValueHandling.Ignore)]
        public string Salesagent3 { get; set; }

        [JsonProperty("salesmanager", NullValueHandling = NullValueHandling.Ignore)]
        public string Salesmanager { get; set; }

        [JsonProperty("dealdescription", NullValueHandling = NullValueHandling.Ignore)]
        public string Dealdescription { get; set; }

        [JsonProperty("Units", NullValueHandling = NullValueHandling.Ignore)]
        public List<Unit> Units { get; set; }

        [JsonProperty("Trade", NullValueHandling = NullValueHandling.Ignore)]
        public List<Trade> Trade { get; set; }

        [JsonProperty("DealExtraLines", NullValueHandling = NullValueHandling.Ignore)]
        public List<DealExtraLine> DealExtraLines { get; set; }

        [JsonProperty("DealProspect", NullValueHandling = NullValueHandling.Ignore)]
        public List<DealProspect> DealProspect { get; set; }
    }

    public partial class Deals
    {
        public static List<LS_Deal> FromJson(string json) => JsonConvert.DeserializeObject<List<LS_Deal>>(json, LightspeedClasses.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson<T>(this List<T> self) => JsonConvert.SerializeObject(self, LightspeedClasses.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (Int64.TryParse(value, out long l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class ParseStringIntegerConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(int) || t == typeof(int?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (Int32.TryParse(value, out int i))
            {
                return i;
            }
            throw new Exception("Cannot unmarshal type int");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class ParseStringDecimalConverter : JsonConverter
    {
        public override bool CanConvert(Type t) =>t == typeof(decimal) || t == typeof(decimal?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (Decimal.TryParse(value, out decimal d))
            {
                return d;
            }
            throw new Exception("Cannot unmarshal type decimal");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
    internal class DateTimeValidatorConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(DateTime?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<DateTime>(reader);
            if (value < DateTime.Now.AddYears(-200)) return null;
            else return value;
            throw new Exception("Cannot unmarshal type decimal");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
