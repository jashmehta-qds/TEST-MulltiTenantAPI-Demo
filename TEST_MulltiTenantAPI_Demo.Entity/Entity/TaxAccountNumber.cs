using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace TEST_MulltiTenantAPI_Demo.Entity
{
    public class TaxAccountNumber : BaseEntity, ISlaveEntity
    {


        [Required]
        [StringLength(30)]
        public string TAN { get; set; }

        [Required]
        [StringLength(30)]
        public string DeductorType { get; set; }

        [Required]
        public string GSTNumbers { get; set; }

        [Required]
        public string ContactName { get; set; }

        [Required]
        [StringLength(30)]
        public string ContactPAN { get; set; }

        [Required]
        [StringLength(30)]
        public string Designation { get; set; }
    }
}
