using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ContactsApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContactsApi.Controllers
{
    [Route("[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactService _contactService;
        public ContactsController(ContactService contactService)
        {
            _contactService = contactService;
        }





    }
}