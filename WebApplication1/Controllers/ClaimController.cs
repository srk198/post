using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models;
using System.Collections.Generic;
using WebApplication1.Models;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

public class ClaimController : Controller
{
    private string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=Concepts;Trusted_Connection=True;";
    private readonly ILogger<ClaimController> _logger;

    public ClaimController(ILogger<ClaimController> logger)
    {
        _logger = logger;
    }


    public IActionResult Index()

    {

        return View();
    }
    [HttpPost]
    public IActionResult GetClaimData(int? claimID)
    {
        List<ClaimData> claimDataList = new List<ClaimData>();

        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open(); // Ensure the connection is opened
                SqlCommand cmd = new SqlCommand("usp_GetClaimData", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClaimID", (object)claimID ?? DBNull.Value);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Check if any rows are returned
                if (dataTable.Rows.Count > 0)
                {
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
                else
                {
                    // Log or debug output to indicate no data was returned
                    Debug.WriteLine("No data returned from the database.");
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error fetching claim data: {ex.Message}");
            // Log or handle exception as needed
        }

        // Check if claimDataList has data
        if (claimDataList == null)
        {
            Debug.WriteLine("claimDataList is null.");
        }
        else if (claimDataList.Count == 0)
        {
            Debug.WriteLine("claimDataList is empty.");
        }
        else
        {
            Debug.WriteLine($"claimDataList has {claimDataList.Count} items.");
        }

        return View("Index", claimDataList);
    }



}
