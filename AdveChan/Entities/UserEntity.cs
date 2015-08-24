namespace AdveChan.Entities
{
    using System;

    public class UserEntity : EntityBase
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? RegisterDate { get; set; }
        public UserRole UserRole { get; set; }
    }

    public enum UserRole
    {
        Moderator = 0,
        Administrator = 1
    }
}