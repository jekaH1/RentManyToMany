using System.ComponentModel.DataAnnotations;

namespace HouseRent.Models
{
    public class AdminMessage
    {
        public int Id { get; set; }
        [StringLength(maximumLength:30)]
        public string FullName { get; set; }
        [StringLength(maximumLength: 200)]
        public string Message { get; set; }
        [StringLength(maximumLength: 200),DataType(DataType.EmailAddress)]
        public string eMail { get; set; }
        public DateTime MessageDate { get; set; }= DateTime.Now;

    }
}
