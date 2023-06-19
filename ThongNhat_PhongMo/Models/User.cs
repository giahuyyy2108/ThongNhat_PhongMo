using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using ThongNhat_PhongMo.Models;

namespace ThongNhat_PhongMo.Models
{
    public class User : IdentityUser
    {


        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<ThongTinKhamBenh> benhnhan { get; set; }

    }
}
