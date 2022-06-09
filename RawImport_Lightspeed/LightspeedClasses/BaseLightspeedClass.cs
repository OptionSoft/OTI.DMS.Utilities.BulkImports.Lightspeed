using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawImport_Lightspeed.LightspeedClasses
{
    public class BaseLightspeedClass
    {
        [JsonIgnore]
        public virtual string TableName { get; }
        [JsonIgnore]
        public virtual string MatchingColumn { get; }
        public virtual string SecondaryMatch { get; }
        [JsonIgnore]
        public long ID { get; set; }
        [JsonIgnore]
        public long? ParentRowId { get; set; }
        [JsonIgnore]
        public virtual bool HasSubtables { get; }
        [JsonIgnore]
        public virtual long? ItemNumber { get; }
        [JsonIgnore]
        public virtual string ItemString { get; }
        //public virtual List<Type> ChildTypes { get; }
        [JsonIgnore]
        public virtual IEnumerable<string> FieldsToIgnore => new[] { "TableName", "MatchingColumn", "SecondaryMatch", "HasSubtables", "ItemNumber", "ItemString", "ChildTypes", "FieldsToIgnore" };
        //public virtual IEnumerable<BaseLightspeedClass> GetSubClasses(Type t) => Enumerable.Empty<BaseLightspeedClass>();
    }
    public class PrimaryTable : BaseLightspeedClass
    {
        [JsonIgnore]
        public bool Processed => !HasSubtables;
        [JsonIgnore]
        public DateTime InsertDateTime { get; set; }
        [JsonIgnore]
        public DateTime? UpdateDateTime { get; set; }
        [JsonIgnore]
        public string RemoteDealershipID { get; set; }
    }
}
