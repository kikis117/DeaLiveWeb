using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AddressError.Text = "";
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        AddressError.Text = "";
        //If Address && Name are not empty
        if(Name.Text.Trim().Length>0)
            if (Address.Text.Trim().Length > 0)
            {
                //Create a Reader and two connections for the DBases
                SqlDataReader myReader = null;
                SqlConnection myConnection = new SqlConnection("user id=G30;" +
                                       "server=G30-PC\\SQLEXPRESS;" +
                                       "Trusted_Connection=yes;" +
                                       "database=DealBase; " +
                                       "connection timeout=30");

               
                SqlConnection memberConnection = new SqlConnection("user id=G30;server=(LocalDb)\\v11.0;"+
                                                                        "Trusted_Connection=yes;"+
                                                                        "database=aspnet-WebSite4-20130524105041;"+
                                                                        "connection timeout=30");

                //Try to open the connections
                try
                {
                    myConnection.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                try
                {
                    memberConnection.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                //Create two command queries
                SqlCommand myCommand2 = new SqlCommand("SELECT Id FROM dbo.Store WHERE Name='"+Name.Text+"'", myConnection);
                SqlCommand myCommand = new SqlCommand("INSERT INTO Store (Name,Address,Phone,Description) Values ('"+Name.Text+"','"+Address.Text +"','"+Phone.Text +"','"+Description.Text+"')", myConnection);

                //Try to execute them
                try
                {
                    myCommand.ExecuteNonQuery();
                }
                    //Handle the exception of the database(Address already registered)
                catch (Exception ex) 
                {
                    if ((uint)ex.HResult == 0x80131904)
                    {
                        AddressError.Text = "A store with this address is already registered.";
                        myReader = myCommand2.ExecuteReader();
                        
                    }
                    
                }

               //try to read the id of the store
                try 
                {
                    myReader = myCommand2.ExecuteReader();
                
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.ToString());
                
                
                }
                 
               
                //assign the id to the current user so we can access it later, when the deals are going to be registered
              myReader.Read();
               myCommand = new SqlCommand("UPDATE dbo.Users SET Id_S ="+ myReader[0]+"WHERE UserName='"+ User.Identity.Name+"'" ,memberConnection);
                myCommand.ExecuteNonQuery();
                   
                
                

                //try to close the connections
                try
                {
                    myConnection.Close();
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine(ex.ToString());
                }

                try
                {
                    memberConnection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                Response.Redirect("~/Deals/Success.aspx");
        }
    }
}