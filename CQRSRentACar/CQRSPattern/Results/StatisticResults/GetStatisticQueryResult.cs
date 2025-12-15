namespace CQRSRentACar.CQRSPattern.Results.StatisticResults

{
    public class GetStatisticQueryResult
    {
        public int CarCount { get; set; }        
        public int EmployeeCount { get; set; }   
        public int ServiceCount { get; set; }    
        public int BookingCount { get; set; }  
    }
}