using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Post : AuditableBaseEntity
    {
        public string Text { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int? UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string ImageURL {get;set;}
    }
}
