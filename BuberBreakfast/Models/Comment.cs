namespace BuberBreakfast.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public AppUser User { get; set; }
    }
}
