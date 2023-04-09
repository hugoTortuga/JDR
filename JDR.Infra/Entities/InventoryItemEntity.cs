﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Infra.Entities
{

    [Table("inventory_item")]
    public class InventoryItemEntity
    {

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public InventoryItemEntity()
        {
            
        }

    }
}