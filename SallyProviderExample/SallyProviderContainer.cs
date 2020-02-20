using Autofac;
using Sally.Provider;
using SallyProviderExample.DialogManagers;
using SallyProviderExample.Stores;
using SallyProviderExample.Transformators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SallyProviderExample
{
    public static class SallyProviderContainer
    {

        //
        //Variables
        //
        public static Autofac.IContainer ApplicationContainer { get; set; }


        //
        //Functions
        //

        public static void BuildApplicationContainer()
        {
            var builder = new ContainerBuilder();

            

            //
            //Add All DialogManager
            //
            var DialogManager_Type = typeof(IDialogManager);
            var DialogManager_Types = typeof(SallyProviderContainer).Assembly.GetTypes()
                                    .Where(p => DialogManager_Type.IsAssignableFrom(p)).ToList();


            foreach (var item in DialogManager_Types)
            {
                if (item != typeof(IDialogManager))
                {
                    dynamic DialogManager_Instance = Activator.CreateInstance(item);
                    builder.Register(c => DialogManager_Instance)
                            .As(item)
                            .SingleInstance();
                }
            }

            //
            //Add All Stores
            //
            var Store_Type = typeof(IStore);
            var Store_Types = typeof(SallyProviderContainer).Assembly.GetTypes()
                                    .Where(p => Store_Type.IsAssignableFrom(p)).ToList();


            foreach (var item in Store_Types)
            {
                if (item != typeof(IStore))
                {
                    dynamic Store_Instance = Activator.CreateInstance(item);
                    builder.Register(c => Store_Instance)
                            .As(item)
                            .SingleInstance();
                }
            }

            //
            //Add All Transformators
            //
            var Transformator_Type = typeof(ITransformator);
            var Transformator_Types = typeof(SallyProviderContainer).Assembly.GetTypes()
                                    .Where(p => Transformator_Type.IsAssignableFrom(p)).ToList();


            foreach (var item in Transformator_Types)
            {
                if (item != typeof(ITransformator))
                {
                    dynamic Transformator_Instance = Activator.CreateInstance(item);
                    builder.Register(c => Transformator_Instance)
                            .As(item)
                            .SingleInstance();
                }
            }


            //TextAnalysisManager
            builder.RegisterType<TextAnalysisManager>().SingleInstance();


            ApplicationContainer = builder.Build();
        }

    }
}