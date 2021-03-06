﻿using Ninject;
using Ninject.Modules;
using Raven.Client;
using Raven.Client.Document;

namespace AwesomeSPAReboot.App_Start.IoC
{

    public class RavenDBNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDocumentStore>()
           .ToMethod(context => new DocumentStore
                                    {
                                        //Url = "https://1.ravenhq.com/databases/magnusg-stuff",
                                        //ApiKey = "b4b7973f-50be-49a5-9f0f-62c2b43e491e"
                                        ConnectionStringName = "RavenDb"
                   
                                    }.Initialize()).InSingletonScope();

            //Bind<IDocumentSession>().ToMethod(context => context.Kernel.Get<IDocumentStore>().OpenSession()).InTransientScope();
        }
    }
}