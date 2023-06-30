using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using ThongNhat_PhongMo.Models;

namespace ThongNhat_PhongMo.Models
{
    public class PhongBan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string name { get; set; }


        public string sceensize { get; set; }

        public string time { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<ThongTinKhamBenh> thongTinKhamBenh { get; set; }
    }
}
