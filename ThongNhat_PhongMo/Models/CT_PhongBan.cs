using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThongNhat_PhongMo.Models
{
    public class CT_PhongBan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string Id_User { get; set; }

        [ForeignKey("Id_User")]
        public User user { get; set; }

        public int id_phongban { get; set; }

        [ForeignKey("id_phongban")]
        public PhongBan phong { get; set; }
    }
}
