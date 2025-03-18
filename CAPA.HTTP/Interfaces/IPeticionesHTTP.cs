﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CAPA.HTTP.Interfaces
{
    public interface IPeticionesHTTP
    {   
        void SetUrlBase(string url);
        Task<TResponse> PostAsync<TRequest, TResponse>(string metodo, TRequest payload, string Token);
        TResponse Post<TRequest, TResponse>(string metodo, TRequest payload, string Token);


        Task<TResponse> GetAsync<TResponse>(string metodo, string Token, object queryParams = null);
        TResponse Get<TResponse>(string metodo, string Token, object queryParams = null);


        Task<TResponse> DeleteAsync<TResponse>(string metodo, string Token, object queryParams = null);
        TResponse Delete<TResponse>(string metodo, string Token, object queryParams = null);
    }
}
