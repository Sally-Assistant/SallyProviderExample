using Sally.Interfaces;
using SallyProviderExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SallyProviderExample.Transformators
{

    public class Transformator : ITransformator
    {
        //
        //Variables
        //

        //
        //Constructor
        //
        public Transformator()
        {

        }


        //
        //Public Functions
        //
        public Appointment TransformToAppointment(SallyProviderRequestContext RequestContext, dynamic D365Object)
        {
            return new Appointment()
            {
                ID = D365Object.activityid,
                Name = D365Object.subject
            };
        }


        //
        //Private Functions
        //

    }
}