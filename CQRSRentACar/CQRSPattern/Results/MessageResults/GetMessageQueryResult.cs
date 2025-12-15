namespace CQRSRentACar.CQRSPattern.Results.MessageResults
{
    public class GetMessageQueryResult
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
