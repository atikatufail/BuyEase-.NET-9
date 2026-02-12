using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models;

public partial class TblProduct
{
    [Key]
    public int Prod_id { get; set; }

    public string Prod_name { get; set; }

    public int Prod_price { get; set; } 

    public string Prod_description { get; set; } 

    public string Prod_image { get; set; } 

    public int Cat_id { get; set; }

    [ForeignKey("Cat_id")]
    public TblCategory Category {get; set; } 
}

