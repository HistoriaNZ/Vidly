﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class NewRentalDto
    {
        public List<int> movieIds { get; set; }
        public int custId { get; set; }
    }
}