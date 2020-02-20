using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SallyProviderExample
{
    public static class Constants
    {

        public static Guid Dynamics365DataServiceID { get; set; } = new Guid("20E29A23-24C2-466D-B505-8EECC0029637");

        public static Guid MicrosoftGraphDataServiceID { get; set; } = new Guid("2F2B0F5B-0DDE-488E-804B-2F3595161E3E");
    }
}