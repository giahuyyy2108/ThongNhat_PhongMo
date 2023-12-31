﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using ThongNhat_PhongMo.Models;

namespace ThongNhat_PhongMo.Models
{
    public class ThongTinKhamBenh
    {
        public string id { get; set; }

        [Column(TypeName ="nvarchar")]
        [StringLength(100)]
        [Required(ErrorMessage ="Phải nhập đầy đủ thông tin")]
        public string hoten { get; set; }
        public int gt { get; set; }
        [StringLength(7)]
        public string namsinh { get; set; }
        [Required(ErrorMessage = "Phải nhập đầy đủ thông tin")]

        public string mabn { get; set; }

        public int id_tinhtrang { get; set; }

        public string id_user { get; set; }
		
		public DateTime Thoigian { get; set; }
		public DateTime ThoigianDuKien { get; set; }

        public string Thoigianhoanthanh { get; set; }

        [AllowNull]
        public int id_phongban { get; set; }
        [ForeignKey("id_phongban")]
        public PhongBan phongban { get; set; }

        [ForeignKey("id_tinhtrang")]
        public TinhTrang tinhtrang { get; set; }

        [ForeignKey("id_user")]
        public User user { get; set; }
    }
}
