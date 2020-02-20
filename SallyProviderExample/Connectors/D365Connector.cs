using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Xrm.Tools.WebAPI;
using Xrm.Tools.WebAPI.Requests;
using Xrm.Tools.WebAPI.Results;

namespace SallyProviderExample.Connectors
{
    /// <summary>
    /// More information about using the WebAPI can be found here:
    /// https://docs.microsoft.com/en-us/previous-versions/dynamicscrm-2016/developers-guide/mt779074(v=crm.8)
    /// https://docs.microsoft.com/en-us/previous-versions/dynamicscrm-2016/developers-guide/mt770386(v=crm.8)
    /// https://docs.microsoft.com/de-de/powerapps/developer/common-data-service/webapi/query-data-web-api
    /// 
    /// We are using the package Xrm.Tools.CRMWebAPI here: https://github.com/davidyack/Xrm.Tools.CRMWebAPI/tree/master/dotnet
    /// 
    /// </summary>
    public class D365Connector
    {

        //
        //Public Variables
        //
        public HttpClient HttpClient { get; set; }


        //
        //Private Variables
        //
        private String OrganizationUri { get; set; }

        private String BearerToken { get; set; }


        //
        //Constructor
        //
        public D365Connector(String OrganizationUri, String BearerToken)
        {
            this.OrganizationUri = OrganizationUri;
            this.BearerToken = BearerToken;
        }


        //
        //Public Functions
        //
        //Todo remove the usage of the package Xrm.Tools.CRMWebAPI here

        //Create
        public async Task<Guid> Create(String EntityPluralLogicalName, ExpandoObject ObjectToCreate)
        {
            CRMWebAPI CRMWebAPI = GetCRMWebAPI();
            return await CRMWebAPI.Create(EntityPluralLogicalName, ObjectToCreate);
        }

        public async Task<ExpandoObject> GetByID(String EntityPluralLogicalName, Guid EntityID)
        {
            CRMWebAPI CRMWebAPI = GetCRMWebAPI();
            return await CRMWebAPI.Get(EntityPluralLogicalName, EntityID, new CRMGetListOptions() { FormattedValues = true });
        }

        public async Task<List<ExpandoObject>> GetByFetch(String EntityPluralLogicalName, String Fetch)
        {
            CRMWebAPI CRMWebAPI = GetCRMWebAPI();
            CRMGetListResult<ExpandoObject> Results = await CRMWebAPI.GetList(EntityPluralLogicalName, QueryOptions: new CRMGetListOptions() { FetchXml = Fetch, FormattedValues = true });

            return Results.List;
        }

        public async Task Update(String EntityPluralLogicalName, Guid EntityID, ExpandoObject ObjectToUpdate)
        {
            CRMWebAPI CRMWebAPI = GetCRMWebAPI();
            CRMUpdateResult Result = await CRMWebAPI.Update(EntityPluralLogicalName, EntityID, ObjectToUpdate);
        }

        public async Task Delete(String EntityPluralLogicalName, Guid EntityID)
        {
            CRMWebAPI CRMWebAPI = GetCRMWebAPI();
            await CRMWebAPI.Delete(EntityPluralLogicalName, EntityID);
        }


        //
        //Private Functions
        //
        private void BuildHttpClient()
        {
            HttpClient = new HttpClient();
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);
            HttpClient.BaseAddress = new Uri(OrganizationUri + "/api/data/v8.1/");
            HttpClient.Timeout = new TimeSpan(0, 2, 0);
            HttpClient.DefaultRequestHeaders.Add("OData-MaxVersion", "4.0");
            HttpClient.DefaultRequestHeaders.Add("OData-Version", "4.0");
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpClient.DefaultRequestHeaders.Add("Prefer", "odata.include-annotations=*");
        }

        private CRMWebAPI GetCRMWebAPI()
        {
            return new CRMWebAPI(OrganizationUri + "/api/data/v9.0/", BearerToken);
        }

    }
}