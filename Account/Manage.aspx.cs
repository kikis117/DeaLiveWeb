using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Microsoft.AspNet.Membership.OpenAuth;

public partial class Account_Manage : System.Web.UI.Page
{
    protected string SuccessMessage
    {
        get;
        private set;
    }

    protected bool CanRemoveExternalLogins
    {
        get;
        private set;
    }

    protected void Page_Load()
    {

        //Check if the user has already a registered store

        SqlConnection memberConnection = new SqlConnection("user id=G30;server=(LocalDb)\\v11.0;" +
                                                        "Trusted_Connection=yes;" +
                                                        "database=aspnet-WebSite4-20130524105041;" +
                                                        "connection timeout=30");

        SqlDataReader myReader = null;
       
        try
        {
            memberConnection.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

         SqlCommand myCommand = new SqlCommand("Select Id_S From dbo.Users WHERE UserName = '"+User.Identity.Name+"'", memberConnection);
         myReader = myCommand.ExecuteReader();




        myReader.Read();

         if (myReader[0].ToString() != "")
         {
             M1.Visible = true;
             M2.Visible = false;
         }
         else
         {
             M1.Visible = false;
             M2.Visible = true;
         }


        
        if (!IsPostBack)
        {
            // Determine the sections to render
            var hasLocalPassword = OpenAuth.HasLocalPassword(User.Identity.Name);
            setPassword.Visible = !hasLocalPassword;
            changePassword.Visible = hasLocalPassword;

            CanRemoveExternalLogins = hasLocalPassword;

            // Render success message
            var message = Request.QueryString["m"];
            if (message != null)
            {
                // Strip the query string from action
                Form.Action = ResolveUrl("~/Account/Manage.aspx");

                SuccessMessage =
                    message == "ChangePwdSuccess" ? "Your password has been changed."
                    : message == "SetPwdSuccess" ? "Your password has been set."
                    : message == "RemoveLoginSuccess" ? "The external login was removed."
                    : String.Empty;
                successMessage.Visible = !String.IsNullOrEmpty(SuccessMessage);
            }
        }
        
    }

    protected void setPassword_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            var result = OpenAuth.AddLocalPassword(User.Identity.Name, password.Text);
            if (result.IsSuccessful)
            {
                Response.Redirect("~/Account/Manage.aspx?m=SetPwdSuccess");
            }
            else
            {
                
                ModelState.AddModelError("NewPassword", result.ErrorMessage);
                
            }
        }
    }

    
    public IEnumerable<OpenAuthAccountData> GetExternalLogins()
    {
        var accounts = OpenAuth.GetAccountsForUser(User.Identity.Name);
        CanRemoveExternalLogins = CanRemoveExternalLogins || accounts.Count() > 1;
        return accounts;
    }

    public void RemoveExternalLogin(string providerName, string providerUserId)
    {
        var m = OpenAuth.DeleteAccount(User.Identity.Name, providerName, providerUserId)
            ? "?m=RemoveLoginSuccess"
            : String.Empty;
        Response.Redirect("~/Account/Manage.aspx" + m);
    }
    

    protected static string ConvertToDisplayDateTime(DateTime? utcDateTime)
    {
        // You can change this method to convert the UTC date time into the desired display
        // offset and format. Here we're converting it to the server timezone and formatting
        // as a short date and a long time string, using the current thread culture.
        return utcDateTime.HasValue ? utcDateTime.Value.ToLocalTime().ToString("G") : "[never]";
    }

}