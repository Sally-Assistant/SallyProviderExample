using Sally.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SallyProviderExample.DialogManagers
{
    public abstract class IDialogManager
    {

        //
        //Public Variables
        //


        //
        //Constructor
        //


        //
        //Public Functions
        //


        //
        //Private Functions
        //
        protected Dialog GetDialog(SallyProviderRequestContext RequestContext, String DialogLogicalName)
        {
            if (RequestContext.Request.Context.Conversation.Stack.Dialogs.Where(x => x.Settings.LogicalName == DialogLogicalName).Count() != 1)
            {
                throw new Exception($"No or more then one Dialog with LogicalName {DialogLogicalName} found!");
            }

            return RequestContext.Request.Context.Conversation.Stack.Dialogs.Where(x => x.Settings.LogicalName == DialogLogicalName).First();
        }



    }
}