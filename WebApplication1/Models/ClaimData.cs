namespace WebApplication1.Models
{
    public class ClaimData
    {
        public int ClaimID { get; set; }
        public string ClaimantName { get; set; }
        public DateTime ClaimDate { get; set; }
        public string ClaimStatus { get; set; }
        public decimal ClaimAmount { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
