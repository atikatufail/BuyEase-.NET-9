using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models;

public partial class TblCategory
{
    [Key]
    public int Category_id { get; set; }

    public string Category_name { get; set; } = null!;
    public List<TblProduct> Product {get; set; }  //1 category k manu product (ref banaya h)
}
