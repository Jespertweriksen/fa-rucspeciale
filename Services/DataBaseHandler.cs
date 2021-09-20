  
  using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace RUCSpeciale
{

    public class DataBaseService{

    }

    public class DataBaseHandler
    {
            public ReservationModel rObject(string email){
                 using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))  
                {  
                    connection.Open();
                    SqlCommand command;
                    SqlDataReader dataReader;
                    var Output = "";
                    var sql = "Select * from db.guest where email = '" + email + "'";
                    command = new SqlCommand(sql, connection);
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Output = Output + dataReader.GetValue(0)+" - "+dataReader.GetValue(1) +" - "+dataReader.GetValue(2)+ "\n";
                         ReservationModel r = new ReservationModel(){
                        Id = (int)dataReader.GetValue(0),
                        Email = (string)dataReader.GetValue(1),
                        First_Name = (string)dataReader.GetValue(2),
                        Last_Name = (string)dataReader.GetValue(3),
                        Phone = (int)dataReader.GetValue(4),
                        Postal = (int)dataReader.GetValue(5)

                    };
                      return r;
                    }       
                }
                return null;
        }
    }
}