using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models;
using System.Collections.Generic;
using WebApplication1.Models;
using Microsoft.Data.SqlClient;

public class ClaimController : Controller
{
    private string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=YourDatabaseName;Trusted_Connection=True;";

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult GetClaimData(int? claimID)
    {
        List<ClaimData> claimDataList = new List<ClaimData>();

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("usp_GetClaimData", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClaimID", (object)claimID ?? DBNull.Value);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                ClaimData claimData = new ClaimData
                {
                    ClaimID = (int)row["ClaimID"],
                    ClaimantName = row["ClaimantName"].ToString(),
                    ClaimDate = (DateTime)row["ClaimDate"],
                    ClaimStatus = row["ClaimStatus"].ToString(),
                    ClaimAmount = (decimal)row["ClaimAmount"],
                    LastModifiedBy = row["LastModifiedBy"].ToString(),
                    LastModifiedDate = (DateTime)row["LastModifiedDate"]
                };
                claimDataList.Add(claimData);
            }
        }

        return View("Index", claimDataList);
    }
}
