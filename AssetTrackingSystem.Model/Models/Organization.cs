﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace AssetTrackingSystem.Models
{
    public class Organization
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        [Required]
        [DisplayName("Short Name")]
        public string ShortName { get; set; }


        [Required]
        [DisplayName("Code")]
        [StringLength(3, ErrorMessage = "The Field Short Name must be three character long")]
        public string Code { get; set; }
        public string Location { get; set; }
        public bool IsDeleted { get; set; }



    }
}