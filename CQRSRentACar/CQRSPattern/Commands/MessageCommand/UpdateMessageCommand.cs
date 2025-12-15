namespace CQRSRentACar.CQRSPattern.Commands.MessageCommand
{
    public class UpdateMessageCommand
    {
        public int MessageId { get; set; }
        public int Name { get; set; }
        public string FullName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string MessageDetail { get; set; }
        public string Phone { get; set; }
    }
}
