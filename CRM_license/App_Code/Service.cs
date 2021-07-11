using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]

public class Service : System.Web.Services.WebService
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ToString());
    public Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public int ManageCompany(int type, string guid, string name, string code, string lictype, int licdays, int licno, DateTime startdate, string details, string address, string country, string state, string city, string zip, string email, string phone, string altphone, string owner, string contactperson, string cpersonphone, string cpersonaltphone, string cpersonemail)
    {

        int res = 0;
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
      
        try
        {

            SqlCommand cmd = new SqlCommand("proc_ManageCompanyLicense", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@type", SqlDbType.Int).Value =type;
            cmd.Parameters.Add("@guid", SqlDbType.NVarChar, 150).Value = guid;
            cmd.Parameters.Add("@name", SqlDbType.NVarChar, 150).Value = name;
            cmd.Parameters.Add("@code", SqlDbType.NVarChar, 20).Value = code;
            cmd.Parameters.Add("@lictype", SqlDbType.NVarChar, 50).Value = lictype;
            cmd.Parameters.Add("@licdays", SqlDbType.Int).Value = licdays;
            cmd.Parameters.Add("@licno", SqlDbType.Int).Value = licno;
            cmd.Parameters.Add("@startdate", SqlDbType.DateTime).Value = startdate;
            cmd.Parameters.Add("@details", SqlDbType.NVarChar,500).Value = details;

            cmd.Parameters.Add("@address", SqlDbType.NVarChar, 500).Value = address;
            cmd.Parameters.Add("@country", SqlDbType.NVarChar, 250).Value = country;
            cmd.Parameters.Add("@state", SqlDbType.NVarChar, 250).Value = state;
            cmd.Parameters.Add("@city", SqlDbType.NVarChar,250).Value = city;
            cmd.Parameters.Add("@zip", SqlDbType.NVarChar,10).Value = zip;
            cmd.Parameters.Add("@email", SqlDbType.NVarChar,250).Value = email;
            cmd.Parameters.Add("@phone", SqlDbType.NVarChar, 20).Value = phone;
            cmd.Parameters.Add("@altphone", SqlDbType.NVarChar, 20).Value = altphone;
            cmd.Parameters.Add("@owner", SqlDbType.NVarChar, 150).Value = owner;
            cmd.Parameters.Add("@contactperson", SqlDbType.NVarChar, 150).Value = contactperson;
            cmd.Parameters.Add("@cpersonphone", SqlDbType.NVarChar, 20).Value = cpersonphone;
            cmd.Parameters.Add("@cpersonaltphone", SqlDbType.NVarChar, 20).Value = cpersonaltphone;
            cmd.Parameters.Add("@cpersonemail", SqlDbType.NVarChar, 250).Value = cpersonemail;
            cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
            res = Convert.ToInt32(cmd.ExecuteScalar().ToString());
           
        }
        catch
        {

           
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        return res;
    }
    [WebMethod]
    public int ManageCompanyUsers(int cid, string uname, string mac, string cpuid, string pcname)
    {

        int res = 0;
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        try
        {
            string ip = getIP();

            SqlCommand cmd = new SqlCommand("proc_ManageCompanyUser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@cid", SqlDbType.Int).Value = cid;
            cmd.Parameters.Add("@uname", SqlDbType.NVarChar, 50).Value = uname;
            cmd.Parameters.Add("@mac", SqlDbType.NVarChar, 50).Value = mac;
            cmd.Parameters.Add("@cpuid", SqlDbType.NVarChar, 50).Value = cpuid;
            cmd.Parameters.Add("@ip", SqlDbType.NVarChar, 50).Value = ip;        
            cmd.Parameters.Add("@pcname", SqlDbType.NVarChar, 150).Value = pcname;
            cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
            res = Convert.ToInt32(cmd.ExecuteScalar().ToString());

        }
        catch
        {


        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        return res;
    }
    [WebMethod]
    public int checkIsOnline(int cid, string uname)
    {
        int res = 0;

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        try
        {
            string ip = getIP();

            SqlCommand cmd = new SqlCommand("proc_checkIsOnline", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@cid", SqlDbType.Int).Value = cid;
            cmd.Parameters.Add("@uname", SqlDbType.NVarChar, 50).Value = uname;
           
            res = Convert.ToInt32(cmd.ExecuteScalar().ToString());

        }
        catch
        {


        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        return res;
    }

    [WebMethod]
    public int logoutUser(int cid, string uname)
    {
        int res = 0;

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        try
        {
            string ip = getIP();

            SqlCommand cmd = new SqlCommand("proc_logoutUsers", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@cid", SqlDbType.Int).Value = cid;
            cmd.Parameters.Add("@uname", SqlDbType.NVarChar, 50).Value = uname;

            res = Convert.ToInt32(cmd.ExecuteScalar().ToString());

        }
        catch
        {


        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        return res;
    }

    [WebMethod]
    public DataTable logoutOnlineUser(int cid)
    {
        DataTable dt = new DataTable();
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        try
        {
            string ip = getIP();

            SqlCommand cmd = new SqlCommand("proc_getAllUsers", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@cid", SqlDbType.Int).Value = cid;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
        }
        catch
        {


        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        return dt;
    }
    private string getIP()
    {
        string ip = "";       
        foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
        {
            if (IPA.AddressFamily.ToString() == "InterNetwork")
            {
               ip = IPA.ToString();
                break;
            }

        }
        if(ip=="")
        {
             ip = HttpContext.Current.Request.UserHostAddress;
        }
        return ip;
    }
    [WebMethod]
    public int getNoOfUser(int cid)
    {
        int res = 0;
		DataSet ds1 = null;        
        ds1 = new DataSet();
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        try
        {
			string StrSql = "select nooflicense from companylicense where id='"+cid+"'";  
            SqlDataAdapter ObjAdap = new SqlDataAdapter(StrSql, con);
            SqlCommandBuilder com = new SqlCommandBuilder(ObjAdap);
            ObjAdap.Fill(ds1, "companylicense");

            if (ds1.Tables[0].Rows.Count > 0)
            {
				res = Convert.ToInt32(ds1.Tables[0].Rows[0][0].ToString());
			}
			
        }
        catch
        {


        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        return res;
    }

    //=====================Add HNS lead records=====================================
    [WebMethod]
    public int ADDRec(string rec)
    {
        int res = 0;

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        try
        {

            string FirstName ="";
            string LastName  ="";
            string Phone ="";
            string AltPhone ="";
            string Email ="";
            string Country ="";
            string state ="";
            string city  ="";
            string zip  ="";
            string Address  ="";
            string Amount = "0";


            if (rec.Contains("~"))
            {
                var leadrecord = rec.Split('~');
                if (leadrecord.Length > 0)
                {
                    FirstName = leadrecord[0];
                    LastName = leadrecord[1];
                    Phone = leadrecord[2];
                    AltPhone = leadrecord[3];
                    Email = leadrecord[4];
                    Country = leadrecord[5];
                    state = leadrecord[6];
                    city = leadrecord[7];
                    zip = leadrecord[8];
                    Address = leadrecord[9];
                    Amount = leadrecord[10];
                }
            }

            SqlCommand cmd = new SqlCommand("proc_leadHNS", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 150).Value = FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 150).Value = LastName;
            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = Phone;
            cmd.Parameters.Add("@AltPhone", SqlDbType.NVarChar, 20).Value = AltPhone;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 120).Value = Email;
            cmd.Parameters.Add("@Country", SqlDbType.NVarChar, 150).Value = Country;
            cmd.Parameters.Add("@state", SqlDbType.NVarChar, 150).Value = state;
            cmd.Parameters.Add("@city", SqlDbType.NVarChar, 150).Value = city;
            cmd.Parameters.Add("@zip", SqlDbType.NVarChar, 10).Value = zip;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 250).Value = Address;
            cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Amount;
            res = Convert.ToInt32(cmd.ExecuteScalar().ToString());

        }
        catch
        {


        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        return res;
    }
    //===============================================================
	
	
	//=====================Add safeway lead records=====================================
    [WebMethod]
    public int ADDRecsafeway(string rec)
    {
        int res = 0;

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        try
        {

            string FirstName = "";
            string LastName = "";
            string Phone = "";
            string AltPhone = "";
            string Email = "";
            string Country = "";
            string state = "";
            string city = "";
            string zip = "";
            string Address = "";
            string Amount = "0";


            if (rec.Contains("~"))
            {
                var leadrecord = rec.Split('~');
                if (leadrecord.Length > 0)
                {
                    FirstName = leadrecord[0];
                    LastName = leadrecord[1];
                    Phone = leadrecord[2];
                    AltPhone = leadrecord[3];
                    Email = leadrecord[4];
                    Country = leadrecord[5];
                    state = leadrecord[6];
                    city = leadrecord[7];
                    zip = leadrecord[8];
                    Address = leadrecord[9];
                    Amount = leadrecord[10];
                }
            }

            SqlCommand cmd = new SqlCommand("proc_leadsafeway", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 150).Value = FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 150).Value = LastName;
            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = Phone;
            cmd.Parameters.Add("@AltPhone", SqlDbType.NVarChar, 20).Value = AltPhone;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 120).Value = Email;
            cmd.Parameters.Add("@Country", SqlDbType.NVarChar, 150).Value = Country;
            cmd.Parameters.Add("@state", SqlDbType.NVarChar, 150).Value = state;
            cmd.Parameters.Add("@city", SqlDbType.NVarChar, 150).Value = city;
            cmd.Parameters.Add("@zip", SqlDbType.NVarChar, 10).Value = zip;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 250).Value = Address;
            cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Amount;
            res = Convert.ToInt32(cmd.ExecuteScalar().ToString());

        }
        catch
        {


        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        return res;
    }
    //===============================================================
	
	
	//=====================Add CTIC lead records=====================================
    [WebMethod]
    public int ADDRecCTIC(string rec)
    {
        int res = 0;

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        try
        {

            string FirstName = "";
            string LastName = "";
            string Phone = "";
            string AltPhone = "";
            string Email = "";
            string Country = "";
            string state = "";
            string city = "";
            string zip = "";
            string Address = "";
            string Amount = "0";


            if (rec.Contains("~"))
            {
                var leadrecord = rec.Split('~');
                if (leadrecord.Length > 0)
                {
                    FirstName = leadrecord[0];
                    LastName = leadrecord[1];
                    Phone = leadrecord[2];
                    AltPhone = leadrecord[3];
                    Email = leadrecord[4];
                    Country = leadrecord[5];
                    state = leadrecord[6];
                    city = leadrecord[7];
                    zip = leadrecord[8];
                    Address = leadrecord[9];
                    Amount = leadrecord[10];
                }
            }

            SqlCommand cmd = new SqlCommand("proc_leadCTIC", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 150).Value = FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 150).Value = LastName;
            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = Phone;
            cmd.Parameters.Add("@AltPhone", SqlDbType.NVarChar, 20).Value = AltPhone;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 120).Value = Email;
            cmd.Parameters.Add("@Country", SqlDbType.NVarChar, 150).Value = Country;
            cmd.Parameters.Add("@state", SqlDbType.NVarChar, 150).Value = state;
            cmd.Parameters.Add("@city", SqlDbType.NVarChar, 150).Value = city;
            cmd.Parameters.Add("@zip", SqlDbType.NVarChar, 10).Value = zip;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 250).Value = Address;
            cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Amount;
            res = Convert.ToInt32(cmd.ExecuteScalar().ToString());

        }
        catch
        {


        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        return res;
    }
    //===============================================================
	
	
	//=====================Add SAPPHIRE MAKKAR lead records=====================================
    [WebMethod]
    public int ADDRecSapphire(string rec)
    {
        int res = 0;

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        try
        {

            string FirstName = "";
            string LastName = "";
            string Phone = "";
            string AltPhone = "";
            string Email = "";
            string Country = "";
            string state = "";
            string city = "";
            string zip = "";
            string Address = "";
            string Amount = "0";


            if (rec.Contains("~"))
            {
                var leadrecord = rec.Split('~');
                if (leadrecord.Length > 0)
                {
                    FirstName = leadrecord[0];
                    LastName = leadrecord[1];
                    Phone = leadrecord[2];
                    AltPhone = leadrecord[3];
                    Email = leadrecord[4];
                    Country = leadrecord[5];
                    state = leadrecord[6];
                    city = leadrecord[7];
                    zip = leadrecord[8];
                    Address = leadrecord[9];
                    Amount = leadrecord[10];
                }
            }

            SqlCommand cmd = new SqlCommand("proc_Lead_sapphire_makkar", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 150).Value = FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 150).Value = LastName;
            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = Phone;
            cmd.Parameters.Add("@AltPhone", SqlDbType.NVarChar, 20).Value = AltPhone;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 120).Value = Email;
            cmd.Parameters.Add("@Country", SqlDbType.NVarChar, 150).Value = Country;
            cmd.Parameters.Add("@state", SqlDbType.NVarChar, 150).Value = state;
            cmd.Parameters.Add("@city", SqlDbType.NVarChar, 150).Value = city;
            cmd.Parameters.Add("@zip", SqlDbType.NVarChar, 10).Value = zip;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 250).Value = Address;
            cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Amount;
            res = Convert.ToInt32(cmd.ExecuteScalar().ToString());

        }
        catch
        {


        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        return res;
    }
    //===============================================================


    //=====================Add CTIC_Nitin lead records=====================================
    [WebMethod]
    public int ADDRecCticNitin(string rec)
    {
        int res = 0;

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        try
        {

            string FirstName = "";
            string LastName = "";
            string Phone = "";
            string AltPhone = "";
            string Email = "";
            string Country = "";
            string state = "";
            string city = "";
            string zip = "";
            string Address = "";
            string Amount = "0";


            if (rec.Contains("~"))
            {
                var leadrecord = rec.Split('~');
                if (leadrecord.Length > 0)
                {
                    FirstName = leadrecord[0];
                    LastName = leadrecord[1];
                    Phone = leadrecord[2];
                    AltPhone = leadrecord[3];
                    Email = leadrecord[4];
                    Country = leadrecord[5];
                    state = leadrecord[6];
                    city = leadrecord[7];
                    zip = leadrecord[8];
                    Address = leadrecord[9];
                    Amount = leadrecord[10];
                }
            }

            SqlCommand cmd = new SqlCommand("proc_Lead_CTIC_Nitin", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 150).Value = FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 150).Value = LastName;
            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = Phone;
            cmd.Parameters.Add("@AltPhone", SqlDbType.NVarChar, 20).Value = AltPhone;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 120).Value = Email;
            cmd.Parameters.Add("@Country", SqlDbType.NVarChar, 150).Value = Country;
            cmd.Parameters.Add("@state", SqlDbType.NVarChar, 150).Value = state;
            cmd.Parameters.Add("@city", SqlDbType.NVarChar, 150).Value = city;
            cmd.Parameters.Add("@zip", SqlDbType.NVarChar, 10).Value = zip;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 250).Value = Address;
            cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Amount;
            res = Convert.ToInt32(cmd.ExecuteScalar().ToString());

        }
        catch
        {


        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        return res;
    }
    //===============================================================


    //=====================Add OzzieeTech lead records=====================================
    [WebMethod]
    public int ADDRecOzzieeTech(string rec)
    {
        int res = 0;

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        try
        {

            string FirstName = "";
            string LastName = "";
            string Phone = "";
            string AltPhone = "";
            string Email = "";
            string Country = "";
            string state = "";
            string city = "";
            string zip = "";
            string Address = "";
            string Amount = "0";


            if (rec.Contains("~"))
            {
                var leadrecord = rec.Split('~');
                if (leadrecord.Length > 0)
                {
                    FirstName = leadrecord[0];
                    LastName = leadrecord[1];
                    Phone = leadrecord[2];
                    AltPhone = leadrecord[3];
                    Email = leadrecord[4];
                    Country = leadrecord[5];
                    state = leadrecord[6];
                    city = leadrecord[7];
                    zip = leadrecord[8];
                    Address = leadrecord[9];
                    Amount = leadrecord[10];
                }
            }

            SqlCommand cmd = new SqlCommand("proc_Lead_OzzieeTech", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 150).Value = FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 150).Value = LastName;
            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = Phone;
            cmd.Parameters.Add("@AltPhone", SqlDbType.NVarChar, 20).Value = AltPhone;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 120).Value = Email;
            cmd.Parameters.Add("@Country", SqlDbType.NVarChar, 150).Value = Country;
            cmd.Parameters.Add("@state", SqlDbType.NVarChar, 150).Value = state;
            cmd.Parameters.Add("@city", SqlDbType.NVarChar, 150).Value = city;
            cmd.Parameters.Add("@zip", SqlDbType.NVarChar, 10).Value = zip;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 250).Value = Address;
            cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Amount;
            res = Convert.ToInt32(cmd.ExecuteScalar().ToString());

        }
        catch
        {


        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        return res;
    }
    //===============================================================
	

    //=====================Add Engrave Demo Lead records=====================================
    [WebMethod]
    public int ADDRecEngraveDemo(string rec)
    {
        int res = 0;

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        try
        {

            string FirstName = "";
            string LastName = "";
            string Phone = "";
            string AltPhone = "";
            string Email = "";
            string Country = "";
            string state = "";
            string city = "";
            string zip = "";
            string Address = "";
            string Amount = "0";


            if (rec.Contains("~"))
            {
                var leadrecord = rec.Split('~');
                if (leadrecord.Length > 0)
                {
                    FirstName = leadrecord[0];
                    LastName = leadrecord[1];
                    Phone = leadrecord[2];
                    AltPhone = leadrecord[3];
                    Email = leadrecord[4];
                    Country = leadrecord[5];
                    state = leadrecord[6];
                    city = leadrecord[7];
                    zip = leadrecord[8];
                    Address = leadrecord[9];
                    Amount = leadrecord[10];
                }
            }

            SqlCommand cmd = new SqlCommand("proc_Lead_EngraveDemo", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 150).Value = FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 150).Value = LastName;
            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = Phone;
            cmd.Parameters.Add("@AltPhone", SqlDbType.NVarChar, 20).Value = AltPhone;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 120).Value = Email;
            cmd.Parameters.Add("@Country", SqlDbType.NVarChar, 150).Value = Country;
            cmd.Parameters.Add("@state", SqlDbType.NVarChar, 150).Value = state;
            cmd.Parameters.Add("@city", SqlDbType.NVarChar, 150).Value = city;
            cmd.Parameters.Add("@zip", SqlDbType.NVarChar, 10).Value = zip;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 250).Value = Address;
            cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Amount;
            res = Convert.ToInt32(cmd.ExecuteScalar().ToString());

        }
        catch
        {


        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        return res;
    }
    //===============================================================
	
	 //=====================Foxtraut [Amit pal]=====================================
    [WebMethod]
    public int ADDRecFoxtraut(string rec)
    {
        int res = 0;

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        try
        {

            string FirstName = "";
            string LastName = "";
            string Phone = "";
            string AltPhone = "";
            string Email = "";
            string Country = "";
            string state = "";
            string city = "";
            string zip = "";
            string Address = "";
            string Amount = "0";


            if (rec.Contains("~"))
            {
                var leadrecord = rec.Split('~');
                if (leadrecord.Length > 0)
                {
                    FirstName = leadrecord[0];
                    LastName = leadrecord[1];
                    Phone = leadrecord[2];
                    AltPhone = leadrecord[3];
                    Email = leadrecord[4];
                    Country = leadrecord[5];
                    state = leadrecord[6];
                    city = leadrecord[7];
                    zip = leadrecord[8];
                    Address = leadrecord[9];
                    Amount = leadrecord[10];
                }
            }

            SqlCommand cmd = new SqlCommand("proc_Lead_foxtraut", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 150).Value = FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 150).Value = LastName;
            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = Phone;
            cmd.Parameters.Add("@AltPhone", SqlDbType.NVarChar, 20).Value = AltPhone;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 120).Value = Email;
            cmd.Parameters.Add("@Country", SqlDbType.NVarChar, 150).Value = Country;
            cmd.Parameters.Add("@state", SqlDbType.NVarChar, 150).Value = state;
            cmd.Parameters.Add("@city", SqlDbType.NVarChar, 150).Value = city;
            cmd.Parameters.Add("@zip", SqlDbType.NVarChar, 10).Value = zip;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 250).Value = Address;
            cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Amount;
            res = Convert.ToInt32(cmd.ExecuteScalar().ToString());

        }
        catch
        {


        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        return res;
    }
    //===============================================================

    //=====================Add Engrave Demo Lead records=====================================
    [WebMethod]
    public int ADDRecQUADRANT(string rec)
    {
        int res = 0;

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        try
        {

            string FirstName = "";
            string LastName = "";
            string Phone = "";
            string AltPhone = "";
            string Email = "";
            string Country = "";
            string state = "";
            string city = "";
            string zip = "";
            string Address = "";
            string Amount = "0";


            if (rec.Contains("~"))
            {
                var leadrecord = rec.Split('~');
                if (leadrecord.Length > 0)
                {
                    FirstName = leadrecord[0];
                    LastName = leadrecord[1];
                    Phone = leadrecord[2];
                    AltPhone = leadrecord[3];
                    Email = leadrecord[4];
                    Country = leadrecord[5];
                    state = leadrecord[6];
                    city = leadrecord[7];
                    zip = leadrecord[8];
                    Address = leadrecord[9];
                    Amount = leadrecord[10];
                }
            }

            SqlCommand cmd = new SqlCommand("proc_Lead_QUADRANT", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 150).Value = FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 150).Value = LastName;
            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = Phone;
            cmd.Parameters.Add("@AltPhone", SqlDbType.NVarChar, 20).Value = AltPhone;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 120).Value = Email;
            cmd.Parameters.Add("@Country", SqlDbType.NVarChar, 150).Value = Country;
            cmd.Parameters.Add("@state", SqlDbType.NVarChar, 150).Value = state;
            cmd.Parameters.Add("@city", SqlDbType.NVarChar, 150).Value = city;
            cmd.Parameters.Add("@zip", SqlDbType.NVarChar, 10).Value = zip;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 250).Value = Address;
            cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Amount;
            res = Convert.ToInt32(cmd.ExecuteScalar().ToString());

        }
        catch
        {


        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        return res;
    }
    //===============================================================
 #region HNS BSOD

    [WebMethod(Description = " HNS BSOD-  Save Track")]
    public string  HNS_BSOD_TraceRecord(string type, string program, string pcname)
    {
        string status = "";
        string strFinal = "";
        string Api = "";
        String ipAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
        string country = "", city = "", zip = "";
        try
        {
            DataSet ds_loc = new DataSet();
            Api = "http://ip-api.com/xml/" + ipAddress;
            if (ds_loc.Tables.Count > 0)
                ds_loc.Tables[0].Rows.Clear();
            ds_loc.ReadXml(Api);
            country = ds_loc.Tables[0].Rows[0]["country"].ToString();
            city = ds_loc.Tables[0].Rows[0]["city"].ToString();
            zip = ds_loc.Tables[0].Rows[0]["zip"].ToString();
        }
        catch
        {
            country = "";
            city = "";
            zip = "";
        }
        try
        {

            if (con.State == ConnectionState.Closed)
			{
				con.Open();
			}
            string k = pcname;// System.Environment.MachineName;
            SqlCommand cmd = new SqlCommand("insert into HNS_BSOD_Trace(Ip_Address,Date,Mac_Address,Pc_Name,Type,Program,countryName,city,Zip,status) values('" + ipAddress + "','" + System.DateTime.Now + "','','" + k + "','" + type + "','" + program + "','" + country + "','" + city + "','" + zip + "','1')", con);


            cmd.ExecuteNonQuery();
            con.Close();
            status = "success";
            return status;

        }
        catch (Exception ex)
        {
            strFinal = ex.Message.ToString();
            status = "Fail: " + strFinal;
            return status;
        }
    }
    #endregion
	 //===============================================================
 #region Sultan BSOD

    [WebMethod(Description = " MS BSOD-  Save Track")]
    public string  MS_BSOD_TraceRecord(string type, string program, string pcname)
    {
        string status = "";
        string strFinal = "";
        string Api = "";
        String ipAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
        string country = "", city = "", zip = "";
        try
        {
            DataSet ds_loc = new DataSet();
            Api = "http://ip-api.com/xml/" + ipAddress;
            if (ds_loc.Tables.Count > 0)
                ds_loc.Tables[0].Rows.Clear();
            ds_loc.ReadXml(Api);
            country = ds_loc.Tables[0].Rows[0]["country"].ToString();
            city = ds_loc.Tables[0].Rows[0]["city"].ToString();
            zip = ds_loc.Tables[0].Rows[0]["zip"].ToString();
        }
        catch
        {
            country = "";
            city = "";
            zip = "";
        }
        try
        {

            if (con.State == ConnectionState.Closed)
			{
				con.Open();
			}
            string k = pcname;// System.Environment.MachineName;
            SqlCommand cmd = new SqlCommand("insert into MS_BSOD_Trace(Ip_Address,Date,Mac_Address,Pc_Name,Type,Program,countryName,city,Zip,status) values('" + ipAddress + "','" + System.DateTime.Now + "','','" + k + "','" + type + "','" + program + "','" + country + "','" + city + "','" + zip + "','1')", con);


            cmd.ExecuteNonQuery();
            con.Close();
            status = "success";
            return status;

        }
        catch (Exception ex)
        {
            strFinal = ex.Message.ToString();
            status = "Fail: " + strFinal;
            return status;
        }
    }
    #endregion
}