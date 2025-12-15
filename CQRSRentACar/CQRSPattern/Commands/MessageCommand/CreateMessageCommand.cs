namespace CQRSRentACar.CQRSPattern.Commands.MessageCommand
{
    public class CreateMessageCommand
    {
        public int Name { get; set; }
        public string FullName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string MessageDetail { get; set; }
        public string Phone { get; set; }
    }
}
