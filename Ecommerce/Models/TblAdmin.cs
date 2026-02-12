using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models;

public partial class TblAdmin
{
    [Key]
    public int Admin_id { get; set; }

    public string Admin_name { get; set; } = null!;

    public string Admin_email { get; set; } = null!;

    public string Admin_password { get; set; } = null!;

    public string Admin_image { get; set; } = null!;
}
