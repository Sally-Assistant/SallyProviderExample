using Autofac;
using Sally.Interfaces;
using SallyProviderExample.Connectors;
using SallyProviderExample.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SallyProviderExample.Transformators;

namespace SallyProviderExample.Stores
{

    // https://docs.microsoft.com/en-us/previous-versions/dynamicscrm-2016/developers-guide/mt779074(v=crm.8)
    // https://docs.microsoft.com/en-us/previous-versions/dynamicscrm-2016/developers-guide/mt770386(v=crm.8)
    // https://docs.microsoft.com/de-de/powerapps/developer/common-data-service/webapi/query-data-web-api
    public class AppointmentStore : IStore
    {

        //
        //Variables
        //


        //
        //Constructors
        //
        public AppointmentStore()
        {

        }


        //
        //Public Functions
        //
        public async Task<List<Appointment>> GetAppointmentsToday(SallyProviderRequestContext RequestContext)
        {
            //INIT
            UserConfiguredD365 D365ConnectionInfo = (UserConfiguredD365)RequestContext.Request.Context.User.Settings.DataServices.First(x => x.DataServiceID == Constants.Dynamics365DataServiceID);
            D365Connector Connector = new D365Connector(D365ConnectionInfo.OrganizationUri, D365ConnectionInfo.AccessToken);
            Transformator Transformator = RequestContext.ScopeContainer.Resolve<Transformator>();
            List<Appointment> ResultingEntities = new List<Appointment>();

            //Load the data
            List<ExpandoObject> Appointments = await Connector.GetByFetch("appointments",
                "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>" +
                  "<entity name='appointment'>" +
                    "<attribute name='subject' />" +
                    "<attribute name='statecode' />" +
                    "<attribute name='scheduledstart' />" +
                    "<attribute name='scheduledend' />" +
                    "<attribute name='createdby' />" +
                    "<attribute name='regardingobjectid' />" +
                    "<attribute name='activityid' />" +
                    "<order attribute='subject' descending='false' />" +
                    "<filter type='and'>" +
                      "<condition attribute='scheduledstart' operator='today' />" +
                    "</filter>" +
                  "</entity>" +
                "</fetch>");

            //Transform the data
            foreach (var Appointment in Appointments)
            {
                ResultingEntities.Add(Transformator.TransformToAppointment(RequestContext, Appointment));
            }

            //Return the data
            return ResultingEntities;

        }


        //
        //Private Functions
        //

    }
}