﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eshop.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
    }
}