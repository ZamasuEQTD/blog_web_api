using Application.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Infraestructure
{
    public class IPV4Context(HttpContext context) : IIPV4Context
    {
        public string Ipv4 => context.Connection.RemoteIpAddress!.MapToIPv4().ToString();
    }
}