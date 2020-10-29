using System.ComponentModel.DataAnnotations;

namespace Stp.TestingApi.Contracts
{
    public class CreateCategoryCommand
    {
        /// <summary>
        /// If null - adding to the root
        /// </summary>
        [Range(1, long.MaxValue)]
        public long? ParentCategoryId { get; set; }

        [Required]
        [StringLength(512, MinimumLength = 1)]
        public string? Name { get; set; }
    }
}
