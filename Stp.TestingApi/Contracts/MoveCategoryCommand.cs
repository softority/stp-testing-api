namespace Stp.TestingApi.Contracts
{
    public class MoveCategoryCommand
    {
        /// <summary>
        /// If null - moving within the root
        /// </summary>
        public long? ParentCategoryId { get; set; }
        public long CategoryId { get; set; }
        public int Position { get; set; }
    }
}
