using Autofac;
using Sally.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SallyProviderExample.Stores;
using SallyProviderExample.Models;

namespace SallyProviderExample.DialogManagers
{
    public class TalkUser_HowAreYou_DialogManager : IDialogManager
    {

        //
        //Public Variables
        //

        //
        //Private Variables
        //


        //
        //Constructor
        //
        public TalkUser_HowAreYou_DialogManager()
        {

        }


        //
        //Public Functions
        //
        public async Task<ExternalFunctionExecutableResponse> PostDialog(SallyProviderRequestContext RequestContext)
        {
            //INIT
            AppointmentStore AppointmentStore = RequestContext.ScopeContainer.Resolve<AppointmentStore>();

            //Load Appointments [Only ShowCase]
            List<Appointment> Appointments = await AppointmentStore.GetAppointmentsToday(RequestContext);

            //Build Resulting Message
            MessageEvent NewMessageEvent = new MessageEvent()
            {
                Message = new BotMessage()
                {
                    Channel = RequestContext.Request.Context.Conversation.Message.Channel,
                    Wording = new BotMessageWording()
                    {
                        Text = "I am fine! How are you? By the way you have " + Appointments.Count + " Appointments today!"
                    }
                }
            };

            return new ExternalFunctionExecutableResponse()
            {
                Events = new List<IEvent>()
                {
                    NewMessageEvent
                }
            };
        }


        //
        //Private Functions
        //

    }
}