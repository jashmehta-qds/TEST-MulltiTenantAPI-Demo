using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using TEST_MulltiTenantAPI_Demo.Domain;
using TEST_MulltiTenantAPI_Demo.Entity.Context;
using TEST_MulltiTenantAPI_Demo.Domain.Service;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Serilog;
using TEST_MulltiTenantAPI_Demo.Entity;
using System.Reflection;
using Microsoft.AspNetCore.OData.Query;

namespace TEST_MulltiTenantAPI_Demo.Api.Controllers
{

    /// <summary>
    ///    
    /// A TaxAccountNumber controller
    ///
    /// MANUAL UPDATES REQUIRED!
    /// Update API version and uncomment route version declaration if required 
    ///       
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TaxAccountNumberAsyncController : ControllerBase
    {
        private readonly TaxAccountNumberServiceAsync<TaxAccountNumberViewModel, TaxAccountNumber> _taxaccountnumberServiceAsync;
        public TaxAccountNumberAsyncController(TaxAccountNumberServiceAsync<TaxAccountNumberViewModel, TaxAccountNumber> taxaccountnumberServiceAsync)
        {
            _taxaccountnumberServiceAsync = taxaccountnumberServiceAsync;
        }


        //get all

        [AllowAnonymous]
        [HttpGet]
        [EnableQuery]
        public IEnumerable<TaxAccountNumberViewModel> GetAll()
        {
            var items = _taxaccountnumberServiceAsync.GetAll();
            return (IEnumerable<TaxAccountNumberViewModel>)Ok(items);

        }
        //public async Task<IActionResult> GetAll()
        //{
        //    var items = await _taxaccountnumberServiceAsync.GetAll();
        //    return Ok(items);

        //}

        //get one
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _taxaccountnumberServiceAsync.GetOne(id);
            if (item == null)
            {
                Log.Error("GetById({ ID}) NOT FOUND", id);
                return NotFound();
            }

            return Ok(item);
        }

        //get by predicate example
        //get all active by username

        //add
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaxAccountNumberViewModel taxaccountnumber)
        {
            if (taxaccountnumber == null)
                return BadRequest();

            var id = await _taxaccountnumberServiceAsync.Add(taxaccountnumber);
            return Created($"api/TaxAccountNumber/{id}", id);  //HTTP201 Resource created
        }

        //update
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TaxAccountNumberViewModel taxaccountnumber)
        {
            if (taxaccountnumber == null || taxaccountnumber.Id != id)
                return BadRequest();

	    var retVal = await _taxaccountnumberServiceAsync.Update(taxaccountnumber);
            if (retVal == 0)
				return StatusCode(304);  //Not Modified
            else if (retVal == - 1)
                return StatusCode(412, "DbUpdateConcurrencyException");  //412 Precondition Failed  - concurrency
            else
                return Accepted(taxaccountnumber);
        }


        //delete
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
	    var retVal = await _taxaccountnumberServiceAsync.Remove(id);
	    if (retVal == 0)
                return NotFound();  //Not Found 404
            else if (retVal == -1)
                return StatusCode(412, "DbUpdateConcurrencyException");  //Precondition Failed  - concurrency
            else
                return NoContent();   	     //No Content 204
        }
    }
}