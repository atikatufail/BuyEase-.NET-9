using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models;

public partial class TblFeedback
{
    [Key]
    public int Feed_id { get; set; }

    public string User_name { get; set; } = null!;

    public string User_msg { get; set; } = null!;
}
