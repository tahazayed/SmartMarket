﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessEntities
{
    public class Product : CommonBaseBusinessEntity
    {
        [Required]
        [StringLength(500, MinimumLength = 2)]
        [Index]

        public string ProductName { get; set; }

        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Index]
        public Guid SubCategoryId { get; set; }


        [ForeignKey(nameof(SubCategoryId))]
        public virtual SubCategory SubCategory { get; set; }

        [Index]
        public Guid CompanyId { get; set; }


        [ForeignKey(nameof(CompanyId))]
        public virtual Company Company { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public long AvailableStock { get; set; } = 0;

        [Index]
        public double Rate { get; set; } = 0.0;

        [Required]
        [StringLength(500)]
        [Index]

        public string ImageURL { get; set; }
    }
}
