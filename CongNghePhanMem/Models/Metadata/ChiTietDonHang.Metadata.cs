﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CongNghePhanMem.Models
{
    [MetadataTypeAttribute(typeof(ChiTietDonHangMetadata))]
    public partial class ChiTietDonHang
    {
        internal sealed class ChiTietDonHangMetadata
        {
            [Display(Name = "Mã đơn hàng")]
            public int MaDH { get; set; }

            [Display(Name = "Sách")]
            public int MaSach { get; set; }
            [Display(Name = "Số lượng")]
            public Nullable<int> SoLuong { get; set; }

            [Display(Name = "Đơn giá")]
            public Nullable<decimal> DonGia { get; set; }
        }
    }
}