using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TokenService.WcfService
{
    [DataContract]
    public class TokenObject
    {
        [DataMember]
        [Key]
        public int UserId { get; set; }
        [DataMember]
        public string Token { get; set; }
        [DataMember]
        public string ValidityDate { get; set; }
    }
}