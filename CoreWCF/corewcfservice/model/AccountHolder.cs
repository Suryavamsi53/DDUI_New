using System;
using System.Runtime.Serialization;

namespace corewcfservice.model
{
    [DataContract]
    public class AccountHolder
    {
        [DataMember]
        public int AccHID { get; set; }

        [DataMember]
        public string ?AccNUM { get; set; }

        [DataMember]
        public int AccTypeID { get; set; }

        [DataMember]
        public string ?Acc_holders_N { get; set; }

        [DataMember]
        public decimal AC_Balance { get; set; }

        [DataMember]
        public DateTime CD { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string ?CreatedBy { get; set; }

        [DataMember]
        public DateTime? UpdatedDate { get; set; }

        [DataMember]
        public string ?UpdatedBy { get; set; }
    }
}
