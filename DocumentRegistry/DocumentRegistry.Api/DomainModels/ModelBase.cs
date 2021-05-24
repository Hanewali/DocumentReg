using System;
using Dapper.Contrib.Extensions;
using DocumentRegistry.Api.Helpers;

namespace DocumentRegistry.Api.DomainModels
{
    
    public class ModelBase
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime ModifyDate { get; set; }
        public int ModifyUserId { get; set; }
        public bool IsActive { get; set; }
    }
}