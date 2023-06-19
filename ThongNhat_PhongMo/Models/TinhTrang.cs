using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using ThongNhat_PhongMo.Models;

namespace ThongNhat_PhongMo.Models
{
    public class TinhTrang
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(30)]
        public string Name { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<ThongTinKhamBenh> benhnhan { get; set; }

    }
}
