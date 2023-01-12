﻿using DomainLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }
        public string Capacity { get; set; }
        public DateTime CreateDate { get; set; }
        public Teacher Teacher { get; set; }
    }
}
