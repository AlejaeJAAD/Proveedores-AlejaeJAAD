using System.Net;
using System.Net.Mail;
using System.IO;
using System.Text;
using System;
using System.Security.Cryptography;
namespace grud_backend.Models
{
    public class Global
    {
        public Global(){}
        public byte[] Secret = Encoding.ASCII.GetBytes("1234567890AAAAAA");
        public byte[] SSL = Encoding.ASCII.GetBytes("AnitaLava.LaTina");
        public string Encrypt(string Pw){
            byte[] inputBytes = Encoding.ASCII.GetBytes(Pw);
            byte[] encrypted;
            RijndaelManaged crypt = new RijndaelManaged();
            using(MemoryStream ms = new MemoryStream(inputBytes.Length))
            {
                using(CryptoStream cryptoStream = 
                new CryptoStream(ms, crypt.CreateEncryptor(Secret, SSL),CryptoStreamMode.Write))
                {
                    cryptoStream.Write(inputBytes,0,inputBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cryptoStream.Close();
                }
                encrypted = ms.ToArray();
            }
            return Convert.ToBase64String(encrypted);
        }
        public string Decrypt(string encrypted)
            {
                byte[] inputBytes = Convert.FromBase64String(encrypted);
                byte[] resultBytes = new byte[inputBytes.Length];
                string pw = String.Empty;
                RijndaelManaged crypt = new RijndaelManaged();
                using(MemoryStream ms = new MemoryStream(inputBytes))
                {
                    using(CryptoStream cryptoStream = new CryptoStream(ms, crypt.CreateDecryptor(Secret,SSL), CryptoStreamMode.Read))
                    {
                        using(StreamReader sr = new StreamReader(cryptoStream, true))
                        {
                            pw = sr.ReadToEnd();
                        }
                    }
                }
                return pw;
            }
    
        public void Email()
        {
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Credentials = new System.Net.NetworkCredential("pruebagrud1@gmail.com","camarada123");
            smtp.EnableSsl = true;
            smtp.Port=587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("pruebagrud1@gmail.com","camarada123");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            MailMessage mail = new MailMessage("pruebagrud@gmail.com","alejandroanayadom@gmail.com","Hola", "HolaBody");
            mail.From = new MailAddress("pruebagrud@gmail.com");
            mail.To.Add(new MailAddress("alejandroanayadom@gmail.com"));
            smtp.Send(mail);
            



        }
    }


}