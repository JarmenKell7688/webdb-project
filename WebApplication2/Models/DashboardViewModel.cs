namespace WebApplication2.Models
{
    public class DashboardViewModel
    {
        public int PendingReservations { get; set; }
        public int PendingGatePasses { get; set; }
        public int PendingLockerRequests { get; set; }

        public int ApprovedReservations { get; set; }
        public int ApprovedGatePasses { get; set; }
        public int ApprovedLockerRequests { get; set; }

        public int DeniedReservations { get; set; }
        public int DeniedGatePasses { get; set; }
        public int DeniedLockerRequests { get; set; }

        public List<Reservation> RecentReservations { get; set; } = new List<Reservation>();
        public List<GatePass> RecentGatePasses { get; set; } = new List<GatePass>();
        public List<LockerRequest> RecentLockerRequests { get; set; } = new List<LockerRequest>();
    }
}