using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Comport
{
    abstract class MESParam : ORM.IMapping
    {
        public string ResourceCode { get; set; }
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime LocalTime { get; set; }
        public string EquipmentCode { get; set; }
        public MESParam()
        {
            this.LocalTime = DateTime.Now;
        }
        public MESParam(string resourceCode, string equipmentCode)
        {
            this.LocalTime = DateTime.Now;
            this.ResourceCode = resourceCode;
            this.EquipmentCode = equipmentCode;
        }
    }

    class HeartbeatParam : MESParam
    {
        public bool IsOnline { get; set; }
    }

    class StateParam : MESParam
    {
        public StateCode StateCode { get; set; }
    }

    class AlarmParam : MESParam, ICloneable
    {
        public AlarmState Status { get; set; }
        public string AlarmMsg { get; set; }
        public string AlarmCode { get; set; }

        [JsonIgnore]
        public int Index { get; set; }
        [JsonIgnore]
        public string Guid { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    class DownReasonParam : MESParam
    {
        public DownReasonCode DownReasonCode { get; set; }
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime BeginTime { get; set; }
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime EndTime { get; set; }
    }

    class ProcessParam : MESParam
    {
        public List<Param> ParamList { get; set; }
        public ProcessParam()
        {
            ParamList = new List<Param>();
        }
    }

    class ProductParam : MESParam
    {
        public List<SFCParam> SFCParams { get; set; }
        public ProductParam()
        {
            SFCParams = new List<SFCParam>();
        }
    }

    sealed class Param
    {
        public string ParamCode { get; set; }
        public string ParamValue { get; set; }
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime Timestamp { get; set; }
        public Param()
        {
            this.Timestamp = DateTime.Now;
        }
    }

    sealed class SFCParam
    {
        public string SFC { get; set; }
        public List<Param> ParamList { get; set; }
        public SFCParam()
        {
            ParamList = new List<Param>();
        }
    }

    class InboundParam : MESParam
    {
        public string SFC { get; set; }
        public InboundParam() { }
        public InboundParam(string sfc, string resourceCode, string equipmentCode)
            : base(resourceCode, equipmentCode)
        {
            this.SFC = sfc;
        }
    }

    class InboundMoreParam : MESParam
    {
        public List<string> SFCs { get; set; }
    }

    class OutboundParam : MESParam
    {
        public string SFC { get; set; }
        public int Passed { get { return Result == "OK" ? 1 : 0; } }
        public List<Param> ParamList { get; set; }
        public List<string> BindFeedingCodes { get; set; }
        public List<NGItem> NG { get; private set; }
        public bool IsPassingStation { get; set; }
        [JsonIgnore]
        public string NGComment { get; set; }
        [JsonIgnore]
        public string NGCodes   //用于处理把NG保存到数据库和从数据库取出NG。
        {
            get { return NG == null ? null : string.Join(",", NG.Select(n => n.NGCode)); }
            set { NG = value == null ? null : value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(n => new NGItem() { NGCode = n }).ToList(); }
        }
        [JsonIgnore]
        public string Result { get; set; }
        [JsonIgnore]
        public string UploadResult { get; set; }
        [JsonIgnore]
        public string NGColumns { get; set; }
        public List<NgList> NgList { get; set; }
    }

    class OutboundMoreParam : MESParam
    {
        public List<Product> SFCs { get; set; }
        [JsonIgnore]
        public List<string> BindFeedingCodes
        {
            set
            {
                if (SFCs != null)
                    SFCs.ForEach(sfc => sfc.BindFeedingCodes = value);
            }
        }
        [JsonIgnore]
        public bool IsPassingStation
        {
            set
            {
                if (SFCs != null)
                    SFCs.ForEach(sfc => sfc.IsPassingStation = value);
            }
        }
        [JsonIgnore]
        public string UploadResult
        {
            set
            {
                if (SFCs != null)
                    SFCs.ForEach(sfc => sfc.UploadResult = value);
            }
        }

        public OutboundMoreParam()
        {
            SFCs = new List<Product>();
        }

        public class Product
        {
            public string SFC { get; set; }
            public int Passed { get { return Result == "OK" ? 1 : 0; } }
            public List<Param> ParamList { get; set; }
            public List<string> BindFeedingCodes { get; set; }
            public List<NGItem> NG { get; set; }
            public bool IsPassingStation { get; set; }
            [JsonIgnore]
            public string NGComment { get; set; }
            [JsonIgnore]
            public string NGCodes
            {
                get { return NG == null ? null : string.Join(",", NG.Select(n => n.NGCode)); }
                set { NG = value == null ? null : value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(n => new NGItem() { NGCode = n }).ToList(); }
            }
            [JsonIgnore]
            public string Result { get; set; }
            [JsonIgnore]
            public string UploadResult { get; set; }
            [JsonIgnore]
            public string NGColumns { get; set; }

            public Product()
            {
                ParamList = new List<Param>();
                BindFeedingCodes = new List<string>();
            }
        }
    }

    struct NGItem
    {
        public string NGCode { get; set; }
    }

    class FeedingParam : MESParam
    {
        public string SFC { get; set; }
        public decimal? Qty { get; set; }
        public bool IsFeedingPoint { get; set; }
    }

    class UnloadingParam : MESParam
    {
        public string SFC { get; set; }
        [JsonConverter(typeof(EnumJsonConverter), EnumJsonConverter.By.Value)]
        public UnloadingType Type { get; set; }
    }

    class BindSFCParam : MESParam
    {
        public string SFC { get; set; }
        public List<string> BindSFCs { get; set; }
    }

    class NgList
    {
        public string NGCode { get; set; }
    }
}