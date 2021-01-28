﻿


// —————————————— 
// <auto-generated> 
//	This code was auto-generated 01/02/2021 21:21:56
//	T4 template generates test code 
//	NOTE:T4 generated code may need additional updates/addjustments by developer in order to compile a solution.
// <auto-generated> 
// —————————————–
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TEST_MulltiTenantAPI_Demo.Api;
using TEST_MulltiTenantAPI_Demo.Domain;
using IdentityModel.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using static JWT.Controllers.TokenController;

namespace TEST_MulltiTenantAPI_Demo.Test
{
	#region unit tests
	#region TaxAccountNumber tests

    /// <summary>
    ///
    /// TaxAccountNumber API Integration tests
    ///
    /// MANUAL UPDATES REQUIRED!
    ///
    /// NOTE: In order to run an pass these scaffolded tests they have to be manually adjusted 
    ///       according to new entity class properties - search for MANUAL UPDATES REQUIRED!
    ///
    /// </summary>
    [Collection("HttpClient collection")]
    public class TaxAccountNumberTest: BaseTest
    {
        public HttpClientFixture fixture;
        public TaxAccountNumberTest(HttpClientFixture fixture)
        {
            this.fixture = fixture;
            var client = fixture.Client;
        }

        public static string LastAddedTaxAccountNumber { get; set; }

        #region TaxAccountNumber tests

        [Fact]
        public async Task taxaccountnumber_getall()
        {
            var httpclient = fixture.Client;
            if (String.IsNullOrEmpty(TokenTest.TokenValue)) await TokenTest.token_get(httpclient);
            //
            var util = new UtilityExt();
                        //MANUAL UPDATES REQUIRED!
			//todo - add if any parent of the entity
			//add entity
            var taxaccountnumberid = await util.addTaxAccountNumber(httpclient);
            //
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenTest.TokenValue);
            var response = await httpclient.GetAsync("/api/taxaccountnumber");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var jsonString = await response.Content.ReadAsStringAsync();
            var vmenititys = (ICollection<UserViewModel>)JsonConvert.DeserializeObject<IEnumerable<UserViewModel>>(jsonString);
            Assert.True(vmenititys.Count > 0);
            // lazy-loading test if entity has children
            response = await httpclient.GetAsync("/api/taxaccountnumber/" + taxaccountnumberid.ToString());
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonString = await response.Content.ReadAsStringAsync();
            var vmenitity = JsonConvert.DeserializeObject<TaxAccountNumberViewModel>(jsonString);
            //Assert.True(vmenitity.Kids.Count == 1);
            //clean
            await util.removeTaxAccountNumber(httpclient, taxaccountnumberid);
			//remove if any parent entity added 
        }


        [Fact]
        public async Task taxaccountnumber_add_update_delete()
        {
            var httpclient = fixture.Client;;
            if (String.IsNullOrEmpty(TokenTest.TokenValue)) await TokenTest.token_get(httpclient);
            //
            TaxAccountNumberViewModel taxaccountnumber = new TaxAccountNumberViewModel
            {
			//MANUAL UPDATES REQUIRED!
			TestText = "tt updated"
            };

            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenTest.TokenValue);
            var response = await httpclient.PostAsync("/api/taxaccountnumber", new StringContent(
                                                               JsonConvert.SerializeObject(taxaccountnumber), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var lastAddedId = await response.Content.ReadAsStringAsync();
            Assert.True(int.Parse(lastAddedId) > 1);
            int id = 0; int.TryParse(lastAddedId, out id);

            //get inserted
            var util = new UtilityExt();
            var vmentity = await util.GetTaxAccountNumber(httpclient, id);

            //update test
            vmentity.TestText = "tt updated";
            response = await httpclient.PutAsync("/api/taxaccountnumber/" + id.ToString(), new StringContent(JsonConvert.SerializeObject(vmentity), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);

            //confirm update
            response = await httpclient.GetAsync("/api/taxaccountnumber/" + id.ToString());
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            var oj = JObject.Parse(jsonString);
            var tt = oj["testText"].ToString();
            Assert.Equal(tt, vmentity.TestText);

            //another update with same account - concurrency
            vmentity.TestText = "tt updated 2";
            response = await httpclient.PutAsync("/api/taxaccountnumber/" + id.ToString(), new StringContent(JsonConvert.SerializeObject(vmentity), Encoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.PreconditionFailed, response.StatusCode);

            //delete test 
            response = await httpclient.DeleteAsync("/api/taxaccountnumber/" + id.ToString());
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task taxaccountnumber_getbyid()
        {
			var httpclient = fixture.Client;
            if (String.IsNullOrEmpty(TokenTest.TokenValue)) await TokenTest.token_get(httpclient);
            //
            var util = new UtilityExt();
	                //MANUAL UPDATES REQUIRED!
			//todo - add parent of the entity if exist
			//add entity
            var taxaccountnumberid = await util.addTaxAccountNumber(httpclient);
            //
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenTest.TokenValue);
            var response = await httpclient.GetAsync("/api/taxaccountnumber/" + taxaccountnumberid.ToString());
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var jsonString = await response.Content.ReadAsStringAsync();
            var vmenitity = JsonConvert.DeserializeObject<TaxAccountNumberViewModel>(jsonString);
            Assert.True(vmenitity.TestText == "tt updated");
			
            //clean
            await util.removeTaxAccountNumber(httpclient, taxaccountnumberid);
	    //remove if any parent entity added 
        }

        #endregion

        #region TaxAccountNumber async tests

        [Fact]
        public async Task taxaccountnumber_getallasync()
        {
            var httpclient = fixture.Client;
            if (String.IsNullOrEmpty(TokenTest.TokenValue)) await TokenTest.token_get(httpclient);
            //
            var util = new UtilityExt();
			//MANUAL UPDATES REQUIRED!
			//todo - add parent of the entity if exist
			//add entity
            var taxaccountnumberid = await util.addTaxAccountNumber(httpclient);
            //
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenTest.TokenValue);
            var response = await httpclient.GetAsync("/api/taxaccountnumberasync");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var jsonString = await response.Content.ReadAsStringAsync();
            var vmenititys = (ICollection<UserViewModel>)JsonConvert.DeserializeObject<IEnumerable<UserViewModel>>(jsonString);
            Assert.True(vmenititys.Count > 0);
            // lazy-loading test if entity has children
            response = await httpclient.GetAsync("/api/taxaccountnumberasync/" + taxaccountnumberid.ToString());
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonString = await response.Content.ReadAsStringAsync();
            var vmenitity = JsonConvert.DeserializeObject<TaxAccountNumberViewModel>(jsonString);
            //Assert.True(vmenitity.Kids.Count == 1);
            //clean
            await util.removeTaxAccountNumber(httpclient, taxaccountnumberid);
			//remove if any parent entity added 
        }


        [Fact]
        public async Task taxaccountnumber_add_update_delete_async()
        {
            var httpclient = fixture.Client;;
            if (String.IsNullOrEmpty(TokenTest.TokenValue)) await TokenTest.token_get(httpclient);
            //
            TaxAccountNumberViewModel taxaccountnumber = new TaxAccountNumberViewModel
            {
			//MANUAL UPDATES REQUIRED!
			//initiate viewmodel object
			TestText = "tt updated"
            };

            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenTest.TokenValue);
            var response = await httpclient.PostAsync("/api/taxaccountnumberasync", new StringContent(
                                                               JsonConvert.SerializeObject(taxaccountnumber), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var lastAddedId = await response.Content.ReadAsStringAsync();
            Assert.True(int.Parse(lastAddedId) > 1);
            int id = 0; int.TryParse(lastAddedId, out id);

            //get inserted
            var util = new UtilityExt();
            var vmentity = await util.GetTaxAccountNumber(httpclient, id);

            //update test
            vmentity.TestText = "tt updated";
            response = await httpclient.PutAsync("/api/taxaccountnumberasync/" + id.ToString(), new StringContent(JsonConvert.SerializeObject(vmentity), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);

            //confirm update
            response = await httpclient.GetAsync("/api/taxaccountnumberasync/" + id.ToString());
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            var oj = JObject.Parse(jsonString);
            var tt = oj["testText"].ToString();
            Assert.Equal(tt, vmentity.TestText);

            //another update with same account - concurrency
            vmentity.TestText = "tt updated 2";
            response = await httpclient.PutAsync("/api/taxaccountnumberasync/" + id.ToString(), new StringContent(JsonConvert.SerializeObject(vmentity), Encoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.PreconditionFailed, response.StatusCode);

            //delete test 
            response = await httpclient.DeleteAsync("/api/taxaccountnumberasync/" + id.ToString());
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        }

        [Fact]
        public async Task taxaccountnumber_getbyidasync()
        {

			var httpclient = fixture.Client;
            if (String.IsNullOrEmpty(TokenTest.TokenValue)) await TokenTest.token_get(httpclient);
            //
            var util = new UtilityExt();
			//MANUAL UPDATES REQUIRED!
			//todo - add if any parent of the entity
			//add entity
            var taxaccountnumberid = await util.addTaxAccountNumber(httpclient);
            //
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenTest.TokenValue);
            var response = await httpclient.GetAsync("/api/taxaccountnumberasync/" + taxaccountnumberid.ToString());
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var jsonString = await response.Content.ReadAsStringAsync();
            var vmenitity = JsonConvert.DeserializeObject<TaxAccountNumberViewModel>(jsonString);
            Assert.True(vmenitity.TestText == "tt updated");
			
            //clean
            await util.removeTaxAccountNumber(httpclient, taxaccountnumberid);
	    //remove if any parent entity added 
        }

        #endregion
	}
        #endregion

    #endregion

    #region Shared test

    public class UtilityExt
    {

        public async Task<int> addTaxAccountNumber(HttpClient client)
        {
		    
            TaxAccountNumberViewModel vmentity = new TaxAccountNumberViewModel
            {
			//MANUAL UPDATES REQUIRED!
			//initiate viewmodel object
			TestText = "tt updated"
            };

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenTest.TokenValue);
            var response = await client.PostAsync("/api/taxaccountnumber", new StringContent(
                                                               JsonConvert.SerializeObject(vmentity), Encoding.UTF8, "application/json"));
            var jsonString = await response.Content.ReadAsStringAsync();
            int lastAdded = 0;
            int.TryParse(jsonString, out lastAdded);
            return lastAdded;
        }
        public async Task<TaxAccountNumberViewModel> GetTaxAccountNumber(HttpClient client, int id)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenTest.TokenValue);
            var response = await client.GetAsync("/api/taxaccountnumberasync/" + id.ToString());
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            var vmentity = JsonConvert.DeserializeObject<TaxAccountNumberViewModel>(jsonString);
            return vmentity;
        }
        public async Task removeTaxAccountNumber(HttpClient client, int id)
        {
            await client.DeleteAsync("/api/taxaccountnumber/" + id.ToString());
        }

	}
	 #endregion
}

