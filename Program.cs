using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Email_Auto
{
    class Program
    {
        static void Main(string[] args)
        {
            string cs = @"Data Source=RONIN;Initial Catalog=NORTHWND;Integrated Security=True";
            string sql = "Select * From Orders Where OrderDate >= DATEADD(DAY,-10000,CONVERT(date,SYSDATETIME()))";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, cs);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            string mailBody = ""; //şimdilik içini doldurmakdık
            foreach (DataRow item in dt.Rows) //DataRow tipindeki dt'nin data Rowlarını item'a tanımla
            {
                //mailbody'i dolduracağız. 
                mailBody += item["OrderDate"] + " " + item["CustomerID"] + "\n";
            }
            SendMail(mailBody);

        }

        private static void SendMail(string mailBody)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("testronintest@gmail.com");
            mailMessage.To.Add("alierendasdemir@gmail.com");
            mailMessage.Subject = "Orders";
            mailMessage.Body = mailBody;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Credentials = new System.Net.NetworkCredential("testronintest@gmail.com", "password");
            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);
        }
    }
}
