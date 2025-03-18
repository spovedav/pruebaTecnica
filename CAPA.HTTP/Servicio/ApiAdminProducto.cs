using CAPA.DOMAIN.DTOs;
using CAPA.DOMAIN.DTOs.Producto;
using CAPA.DOMAIN.Static;
using CAPA.HTTP.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CAPA.HTTP.Servicio
{
    public class ApiAdminProducto : BaseApiServicio, IApiAdminProducto
    {
        private readonly IPeticionesHTTP _http;
        public ApiAdminProducto(IPeticionesHTTP _http, IConfiguration configuration) : base(configuration) {
            this._http = _http;
            this._http.SetUrlBase(ParametrosConfi.UrlBaseAdmin);
        }

        public ResponseDto<List<ProductoDto>> GetAll(string Token, object queryParameto = null)
            => _http.Get<ResponseDto<List<ProductoDto>>>(_metodo("Apis:Admin:Producto:get-all"), Token, queryParameto);

        public ResponseDto<ProductoDto> GetProducto(int Id, string Token, object queryParameto = null)
            => _http.Get<ResponseDto<ProductoDto>>(_metodo("Apis:Admin:Producto:get"), Token, queryParameto);

        public ResponseDto<bool> Mantenimiento(int Id, ProductoDto model, string Token)
            => _http.Post<ProductoDto, ResponseDto<bool>>(_metodo("Apis:Admin:Producto:Mantenimiento") + "?Id="+ Id, model,Token);
    }
}
