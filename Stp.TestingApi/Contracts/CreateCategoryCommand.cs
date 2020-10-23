using System.ComponentModel.DataAnnotations;

namespace Stp.TestingApi.Contracts
{
    public class CreateCategoryCommand
    {
        /// <summary>
        /// If null - adding to the root
        /// </summary>
        public long? ParentCategoryId { get; set; }
        
        public string Name { get; set; }
    }
}
