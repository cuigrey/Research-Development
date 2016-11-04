using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPP.App_Code
{
    public class WebUnityInitializer
    {
        public IUnityContainer UnityInitializer()
        {
            var container = new UnityContainer();
            container.AddNewExtension<Interception>();

            InjectionMember[] injectionMemebersForParser = new InjectionMember[] {
                new InterceptionBehavior<PolicyInjectionBehavior>(),
                new InjectionConstructor(),
                new Interceptor<InterfaceInterceptor>()
            };

            return container;
        }
    }
}