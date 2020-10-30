using System.ComponentModel.DataAnnotations;

namespace Stp.TestingApi.Contracts
{
    public class MoveCategoryCommand
    {
        /// <summary>
        /// If null - moving within the root
        /// </summary>
        [Range(1, long.MaxValue)]
        public long? ParentCategoryId { get; set; }

        [Range(1, long.MaxValue)]
        public long CategoryId { get; set; }

        [Range(0, int.MaxValue)]
        public int Position { get; set; }
    }
}
