using System;
using System.Collections.Generic;
namespace TEST_MulltiTenantAPI_Demo.Domain
{

    /// <summary>
    ///
    /// A TaxAccountNumber view model
    ///   
    ///       MANUAL UPDATES REQUIRED! 
    ///       NOTE use PM console to update database with new entity : 
    ///            PM>add-migration AddTaxAccountNumber
    ///            PM>update-database
    ///
    /// </summary>
    public class TaxAccountNumberViewModel : BaseDomain
    {

	  		public string TAN { get; set; } 
			public string DeductorType { get; set; } 
			public string GSTNumbers { get; set; } 
			public string ContactName { get; set; } 
			public string ContactPAN { get; set; } 
			public string Designation { get; set; } 
	} 
	     
}