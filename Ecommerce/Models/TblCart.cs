using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models;

public partial class TblCart
{
    [Key]
    public int Cart_id { get; set; }

    public int Prod_id { get; set; }

    public int Prod_quantity { get; set; }

    public int Cust_id { get; set; }

    public int Status { get; set; }

    [ForeignKey("Prod_id")]
    public virtual TblProduct? Product { get; set; }

    [ForeignKey("Cust_id")]
    public virtual TblCustomer? Customer { get; set; }
}
