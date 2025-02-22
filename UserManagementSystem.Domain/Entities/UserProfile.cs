namespace UserManagementSystem.Domain.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string AvatarUrl { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
