using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MyTask.WebAPI.Startup))]

namespace MyTask.WebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

        }
    }
}
