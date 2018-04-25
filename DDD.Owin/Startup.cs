using Newtonsoft.Json;
using Owin;

namespace DDD.Owin
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            app.Run(context =>
            {
                var result = ServiceExecuter.Execute(context.Request.ToServiceInfo());
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsync(JsonConvert.SerializeObject(result.Result));
            });
        }
    }
}