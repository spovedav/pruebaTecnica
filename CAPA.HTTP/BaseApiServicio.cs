using Microsoft.Extensions.Configuration;
using System;

namespace CAPA.HTTP
{
    public class BaseApiServicio
    {
        private readonly IConfiguration configuration;

        public BaseApiServicio(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected string _metodo(string rutaConfig)
            => configuration[rutaConfig];
    }
}
