using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public partial class CoreCostCenter
    {
        public CoreCostCenter()
        {
            IfmsVoucherDetails = new HashSet<IfmsVoucherDetail>();
            InverseParent = new HashSet<CoreCostCenter>();
            IfmsVoucherHeader = new HashSet<IfmsVoucherHeader>();
            IfmsVoucherDetailHistorys = new HashSet<IfmsVoucherDetailHistory>();
            IfmsVoucherTypeSettings = new HashSet<IfmsVoucherTypeSetting>();
            IfmsSettings = new HashSet<IfmsSetting>();
            IfmsSettings_2 = new HashSet<IfmsSetting>();
            IfmsFixedAssetSettings = new HashSet<IfmsFixedAssetSetting>();
        }

        public Guid Id{ get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid ParentId{ get; set; }
     //  public byte[] IsDeleted { get; set; }
     //  public DateTime? LastUpdated { get; set; }
     //  public DateTime? CreatedAt { get; set; }
     //  public DateTime? UpdatedAt { get; set; }
       
        public virtual CoreCostCenter Parent { get; set; }
        public virtual ICollection<CoreCostCenter> InverseParent { get; set; }
        public virtual ICollection<IfmsVoucherDetail> IfmsVoucherDetails { get; set; }
        public virtual ICollection<IfmsVoucherDetailHistory> IfmsVoucherDetailHistorys { get; set; }
        public virtual ICollection<IfmsVoucherHeader> IfmsVoucherHeader { get; set; }
        public virtual ICollection<IfmsVoucherTypeSetting> IfmsVoucherTypeSettings { get; set; }

        public virtual ICollection<IfmsSetting> IfmsSettings { get; set; }
        public virtual ICollection<IfmsSetting> IfmsSettings_2 { get; set; }
        public virtual ICollection<IfmsFixedAssetSetting> IfmsFixedAssetSettings { get; set; }


    }
}
