using System.ComponentModel.DataAnnotations;

namespace KheyaShop.Models
{
    public class UserText
    {
        [Key]
        public int Id { get; set; }

        public string ReviewText { get; set; }

        public int Pid { get; set; }

        public int UserId { get; set; }
    }
}
