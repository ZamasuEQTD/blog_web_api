using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Infraestructure
{
    public abstract class ApiController : Controller
    {
        public ISender sender;

        protected ApiController(ISender sender)
        {
            this.sender = sender;
        }
    }
}