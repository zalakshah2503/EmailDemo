using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailDemoApplication.Models
{
    public class MailViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string HostName { get; set; }
        public int PortNo { get; set; }
        public string To { get; set; }
        public string Content { get; set; }
        public bool enableSSl { get; set; }
    }

    public class MailConfig
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string HostName { get; set; }
        public int PortNo { get; set; }
        public bool enableSSl { get; set; }
    }
}