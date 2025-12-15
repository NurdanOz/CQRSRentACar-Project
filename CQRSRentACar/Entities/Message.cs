namespace CQRSRentACar.Entities
{
    public class Message
    {
        public int MessageId { get; set; }
        public int Name { get; set; }
        public string FullName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string MessageDetail { get; set; }
        public string Phone { get; set; }

        public bool EmailSent { get; set; } = false;
        public DateTime? EmailSentDate { get; set; }
        public string AIResponse { get; set; }
    }
}
