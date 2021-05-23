using System;

namespace DocumentRegistry.DomainModels
{
    public interface IModel
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public User CreateUser { get; set; }
        public DateTime ModifyDate { get; set; }
        public User ModifyUser { get; set; }
    }
}