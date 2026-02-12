using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models;

public partial class TblCustomer
{
    [Key]
    public int Cust_id { get; set; }

    public string Cust_name { get; set; } = null!;

    public string Cust_email { get; set; } = null!;

    public string Cust_password { get; set; } = null!;

    public string Cust_phone { get; set; } = null!;

    public string Cust_address { get; set; } = null!;

    public string Cust_country { get; set; } = null!;

    public string Cust_city { get; set; } = null!;

    public string Cust_gender { get; set; } = null!;

    public string Cust_image { get; set; } = null!;
}
