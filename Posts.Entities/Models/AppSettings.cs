using System;

namespace Posts.Entities.Models
{
    public class AppSettings
    {
        public string Secret { get; set; }  
        public int TimeExpirationMinutes { get; set; }
    }
}
