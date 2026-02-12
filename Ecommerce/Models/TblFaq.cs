using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models;

public partial class TblFaq
{
    [Key]
    public int Faq_id { get; set; }

    public string Fa_ques { get; set; } = null!;

    public string Faq_ans { get; set; } = null!;
}
