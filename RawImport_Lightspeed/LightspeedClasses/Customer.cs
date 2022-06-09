namespace RawImport_Lightspeed.LightspeedClasses
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Customer : PrimaryTable
    {
        [JsonIgnore]
        public override string TableName => "Customer";
        [JsonIgnore]
        public override bool HasSubtables => false;
        [JsonIgnore]
        public override long? ItemNumber => CustomerId;
        public override string MatchingColumn => "CustomerId"; 

        [JsonProperty("Cmf", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Cmf { get; set; }

        [JsonProperty("DealerId", NullValueHandling = NullValueHandling.Ignore)]
        public string DealerId { get; set; }

        [JsonProperty("CustomerId", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? CustomerId { get; set; }

        [JsonProperty("storename", NullValueHandling = NullValueHandling.Ignore)]
        public string Storename { get; set; }

        [JsonProperty("DateGathered", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime DateGathered { get; set; }

        [JsonProperty("CustFullName", NullValueHandling = NullValueHandling.Ignore)]
        public string CustFullName { get; set; }

        [JsonProperty("FirstName", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }

        [JsonProperty("MIddleName", NullValueHandling = NullValueHandling.Ignore)]
        public string MIddleName { get; set; }

        [JsonProperty("LastName", NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }

        [JsonProperty("attention", NullValueHandling = NullValueHandling.Ignore)]
        public string Attention { get; set; }

        [JsonProperty("Companyname", NullValueHandling = NullValueHandling.Ignore)]
        public string Companyname { get; set; }

        [JsonProperty("Address1", NullValueHandling = NullValueHandling.Ignore)]
        public string Address1 { get; set; }

        [JsonProperty("Address2", NullValueHandling = NullValueHandling.Ignore)]
        public string Address2 { get; set; }

        [JsonProperty("City", NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }

        [JsonProperty("State", NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }

        [JsonProperty("Zip", NullValueHandling = NullValueHandling.Ignore)]
        public string Zip { get; set; }

        [JsonProperty("County", NullValueHandling = NullValueHandling.Ignore)]
        public string County { get; set; }

        [JsonProperty("Country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }

        [JsonProperty("HomePhone", NullValueHandling = NullValueHandling.Ignore)]
        public string HomePhone { get; set; }

        [JsonProperty("WorkPhone", NullValueHandling = NullValueHandling.Ignore)]
        public string WorkPhone { get; set; }

        [JsonProperty("CellPhone", NullValueHandling = NullValueHandling.Ignore)]
        public string CellPhone { get; set; }

        [JsonProperty("EMail", NullValueHandling = NullValueHandling.Ignore)]
        public string EMail { get; set; }

        [JsonProperty("Birthdate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Birthdate { get; set; }

        [JsonProperty("HasDriversLicenseNumber", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? HasDriversLicenseNumber { get; set; }

        [JsonProperty("LoyaltyCustomer", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? LoyaltyCustomer { get; set; }

        [JsonProperty("CustomerType", NullValueHandling = NullValueHandling.Ignore)]
        public string CustomerType { get; set; }

        [JsonProperty("optoutmarketing", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Optoutmarketing { get; set; }

        [JsonProperty("optoutsharedata", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Optoutsharedata { get; set; }

        [JsonProperty("optoutselldata", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Optoutselldata { get; set; }

        [JsonProperty("removepersonalinformation", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Removepersonalinformation { get; set; }

        [JsonProperty("comments", NullValueHandling = NullValueHandling.Ignore)]
        public string Comments { get; set; }
    }

    public partial class Customers
    {
        public static List<Customer> FromJson(string json) => JsonConvert.DeserializeObject<List<Customer>>(json, LightspeedClasses.Converter.Settings);
    }
}
