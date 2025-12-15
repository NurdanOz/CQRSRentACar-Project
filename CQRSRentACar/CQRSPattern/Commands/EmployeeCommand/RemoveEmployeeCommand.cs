namespace CQRSRentACar.CQRSPattern.Commands.EmployeeCommand
{
    public class RemoveEmployeeCommand
    {
        public int EmployeeId { get; set; }

        public RemoveEmployeeCommand(int employeeId)
        {
            EmployeeId = employeeId;
        }
    }
}
