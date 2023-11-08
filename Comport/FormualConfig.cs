using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HymsonAutomation.CCS
{
    [DataContract]
    public class FormualConfig
    {
        private const string FileName = "formual.xml";
        private static readonly string FilePath = Environment.CurrentDirectory + "//Configure//";
        private static FormualConfig s_self;

        [DisplayName("电芯类型数组"), Description("电芯设置数组"), DataMember]
        public List<BatteriesType> Batteries { get; set; }

        public static void Init()
        {
            try
            {
                s_self = FileHelper.Load<FormualConfig>(FilePath, FileName);
            }
            catch (Exception e)
            {
                s_self = new FormualConfig();
                //var log = LogManager.GetLogger(typeof(FormualConfig));
                //log.Error(e.Message);
            }
        }

        public static void Save()
        {
            try
            {
                FileHelper.Save(s_self, FilePath, FileName);
            }
            catch (Exception e)
            {
                //var log = LogManager.GetLogger(typeof(FormualConfig));
                //log.Error(e.Message);
            }
        }
        public static FormualConfig GetInstance()
        {
            return s_self ?? (s_self = new FormualConfig());
        }
    }
    public class BatteriesType
    {
        [DisplayName("电芯类型"), Description("电池个数"), DataMember]
        public string BatteriesTypeNumber { get; set; } = "52个电芯";
        [DisplayName("CCS类型1"), Description("CCS安装顺序1"), DataMember]
        public string CCSType1 { get; set; } = "CCS1";

        [DisplayName("CCS类型2"), Description("CCS安装顺序2"), DataMember]
        public string CCSType2 { get; set; } = "CCS2";

        [DisplayName("CCS类型3"), Description("CCS安装顺序3"), DataMember]
        public string CCSType3 { get; set; } = "CCS3";

        [DisplayName("CCS类型4"), Description("CCS安装顺序4"), DataMember]
        public string CCSType4 { get; set; } = "CCS4";
        [DisplayName("CCS类型1对应位置"), Description("CCS类型顺序1对应位置"), DataMember]
        public string CCSTypeLocation1 { get; set; } = "A";

        [DisplayName("CCS类型2对应位置"), Description("CCS类型顺序2对应位置"), DataMember]
        public string CCSTypeLocation2 { get; set; } = "B";

        [DisplayName("CCS类型3对应位置"), Description("CCS类型顺序3对应位置"), DataMember]
        public string CCSTypeLocation3 { get; set; } = "C";

        [DisplayName("CCS类型4对应位置"), Description("CCS类型顺序4对应位置"), DataMember]
        public string CCSTypeLocation4 { get; set; } = "D";
    }
}
