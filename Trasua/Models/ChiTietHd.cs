using System;
using System.Collections.Generic;

namespace Trasua.Models;

public partial class ChiTietHd
{
    public int MaCt { get; set; }

    public int? MaSp { get; set; }

    public decimal? Price { get; set; }

    public int? SoLuong { get; set; }

    public int? MaHd { get; set; }

    public virtual HoaDon? MaHdNavigation { get; set; }

    public virtual SanPham? MaSpNavigation { get; set; }
    public SanPham SanPham { get; internal set; }
}
