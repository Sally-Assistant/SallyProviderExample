using Autofac;
using Sally.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SallyProviderExample
{
    public class SallyProviderRequestContext
    {

        //
        //Public Variables
        //
        public ILifetimeScope ScopeContainer { get; set; }

        public ExternalFunctionExecutableRequest Request { get; set; }

        //
        //Constructors
        //

        //
        //Static Functions
        //
        public static SallyProviderRequestContext GetContext(ExternalFunctionExecutableRequest Request)
        {
            return new SallyProviderRequestContext()
            {
                Request = Request,
                ScopeContainer = SallyProviderContainer.ApplicationContainer.BeginLifetimeScope()
            };
        }

    }
}