using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Error.Text = "";
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        bool redirect = true;
        if(Name.Text.Trim().Length>0)
            if (Price.Text.Trim().Length > 0)
                if (Place.Text.Trim().Length > 0)
                        {
                            Error.Text = "";
                            SqlDataReader myReader = null;
                            //Create the connection to the DataBase
                            SqlConnection myConnection = new SqlConnection("user id=G30;" +
                                                   "server=G30-PC\\SQLEXPRESS;" +
                                                   "Trusted_Connection=yes;" +
                                                   "database=DealBase; " +
                                                   "connection timeout=30");


                            SqlConnection memberConnection = new SqlConnection("user id=G30;server=(LocalDb)\\v11.0;" +
                                                                       "Trusted_Connection=yes;" +
                                                                       "database=aspnet-WebSite4-20130524105041;" +
                                                                       "connection timeout=30");

                            //try to open the connection
                            try
                            {
                                myConnection.Open();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                                redirect = false;
                            }

                            try
                            {
                                memberConnection.Open();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                                redirect = false;
                            }




                            //Create a command to find the user's store id
                            SqlCommand idCommand = new SqlCommand("Select Id_S From dbo.Users WHERE UserName='"+User.Identity.Name+"'",memberConnection);
                            myReader = idCommand.ExecuteReader();
                            redirect=myReader.Read();

                            //create the command to insert the deal
                            SqlCommand myCommand = new SqlCommand("INSERT INTO Deals (Name,Type,Time,Price,Place,Id_S) Values ('" + Name.Text + "'," + DType.SelectedValue + ",'" + System.DateTime.Now + "'," + Convert.ToDecimal(Price.Text) + ", '" + Place.Text + "', " + myReader[0] + ")", myConnection);
               
                            //try to execute the command
                            try
                            {
                                myCommand.ExecuteNonQuery();
                            }

                                 //Handle the exception of the database(the store already has an offer of this type registered)
                                // TODO: OFFER THE USER THE OPTION TO OVERWRITE THE OFFER
                            catch (Exception ex) 
                            {
                                if ((uint)ex.HResult == 0x80131904)
                                    Error.Text = "You already have an offer of this type";
                                redirect = false;
                            }
                
                            //try to close the connection
                            try
                            {
                                myConnection.Close();
                            }
                            catch (Exception ex)
                            {
                    
                                Console.WriteLine(ex.ToString());
                                redirect = false;
                            }

                            if (redirect == true) 
                            Response.Redirect("~/Deals/Success.aspx");
                    }
        

    }
    
}