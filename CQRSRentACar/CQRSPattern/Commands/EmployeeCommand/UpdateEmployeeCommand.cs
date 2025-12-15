namespace CQRSRentACar.CQRSPattern.Commands.EmployeeCommand
{
    public class UpdateEmployeeCommand
    {
        public int EmployeeId { get; set; }
        public string NameSurname { get; set; }
        public string Profession { get; set; }
        public string ImageUrl { get; set; }
    }
}
