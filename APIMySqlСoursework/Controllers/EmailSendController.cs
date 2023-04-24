using APIMySqlСoursework.Attributes;
using APIMySqlСoursework.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace APIMySqlСoursework.Controllers
{
    [ApiKey]
    [Route("api/emailSend")]
    public class EmailSendController
    {
        [HttpGet("{userEmail}")]
        public async Task<IActionResult> SendCodeEmailAdress(string userEmail)
        {
            try
            {
                MailAddress from = new MailAddress("cofFfieTin@yandex.ru", "Фитнес-клуб - 'GLY UP'");
                MailAddress to = new MailAddress(userEmail);
                MailMessage m = new MailMessage(from, to);
                m.Subject = "Завершение регистрации в Фитнес-клубе - 'GLY UP'";
                Random rnd = new Random();
                string numberCode = rnd.Next(10000, 99999).ToString();
                m.Body = "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n\r\n    <meta charset=\"utf-8\">\r\n    <meta http-equiv=\"x-ua-compatible\" content=\"ie=edge\">\r\n    <title>Email Confirmation</title>\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">\r\n    <style type=\"text/css\">\r\n\r\n             @media screen {\r\n                 @font-face {\r\n                     font-family: 'Source Sans Pro';\r\n                     font-style: normal;\r\n                     font-weight: 400;\r\n                     src: local('Source Sans Pro Regular'), local('SourceSansPro-Regular'), url(https://fonts.gstatic.com/s/sourcesanspro/v10/ODelI1aHBYDBqgeIAH2zlBM0YzuT7MdOe03otPbuUS0.woff) format('woff');\r\n                 }\r\n\r\n                 @font-face {\r\n                     font-family: 'Source Sans Pro';\r\n                     font-style: normal;\r\n                     font-weight: 700;\r\n                     src: local('Source Sans Pro Bold'), local('SourceSansPro-Bold'), url(https://fonts.gstatic.com/s/sourcesanspro/v10/toadOcfmlt9b38dHJxOBGFkQc6VGVFSmCnC_l7QZG60.woff) format('woff');\r\n                 }\r\n             }\r\n\r\n             body,\r\n             table,\r\n             td,\r\n             a {\r\n                 -ms-text-size-adjust: 100%; /* 1 */\r\n                 -webkit-text-size-adjust: 100%; /* 2 */\r\n             }\r\n\r\n             table,\r\n             td {\r\n                 mso-table-rspace: 0pt;\r\n                 mso-table-lspace: 0pt;\r\n             }\r\n\r\n        img {\r\n            -ms-interpolation-mode: bicubic;\r\n            display: block;\r\n            height: 400px;\r\n            width: 400px;\r\n            margin-bottom: -80px;\r\n            margin-top: -80px;\r\n        }\r\n\r\n             a[x-apple-data-detectors] {\r\n                 font-family: inherit !important;\r\n                 font-size: inherit !important;\r\n                 font-weight: inherit !important;\r\n                 line-height: inherit !important;\r\n                 color: inherit !important;\r\n                 text-decoration: none !important;\r\n             }\r\n\r\n             div[style*=\"margin: 16px 0;\"] {\r\n                 margin: 0 !important;\r\n             }\r\n\r\n        body {\r\n            color: #95b2da !important;\r\n            background-color: #e9ecef;\r\n            width: 100% !important;\r\n            height: 100% !important;\r\n            padding: 0 !important;\r\n            margin: 0 !important;\r\n        }\r\n\r\n             table {\r\n                 border-collapse: collapse !important;\r\n             }\r\n\r\n             a {\r\n                 color: #1a82e2;\r\n             }\r\n\r\n             img {\r\n                 height: auto;\r\n                 line-height: 100%;\r\n                 text-decoration: none;\r\n                 border: 0;\r\n                 outline: none;\r\n             }\r\n\r\n        input {\r\n            height: 50px;\r\n            width: 49px;\r\n            margin: 0px 10px 10px 10px;\r\n            font-size: 40px;\r\n            border-radius: 10px;\r\n            text-align: center;\r\n            background-color: #1c375c;\r\n            color: #267af0;\r\n        }\r\n    </style>\r\n\r\n</head>\r\n<body style=\"background-color: #e9ecef;\">\r\n\r\n\r\n    <div class=\"preheader\" style=\"display: none; max-width: 0; max-height: 0; overflow: hidden; font-size: 1px; line-height: 1px; color: #fff; opacity: 0;\">\r\n        A preheader is the short summary text that follows the subject line when an email is viewed in the inbox.\r\n    </div>\r\n\r\n    <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\r\n        <tr>\r\n            <td align=\"center\" bgcolor=\"#1c375c\">\r\n\r\n                <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\">\r\n                    <tr>\r\n                        <td align=\"center\" valign=\"top\" style=\"padding: 36px 24px;\">\r\n                                <img  alt=\"Logo\" border=\"0\" style=\"display: block; margin-top: -15%; height:400px; width: 400px;\" src=\"http://217.66.25.160:5001/Images/logofitnes.png\">\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td align=\"center\" bgcolor=\"#1c375c\">\r\n                <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px; margin-top: -10%;\">\r\n                    <tr>\r\n                        <td align=\"left\" bgcolor=\"#4d4d4d\" style=\"padding: 36px 24px 0; color : #95b2da; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; border-radius: 10px 10px 0 0;\">\r\n                            <h1 style=\"margin: 0; font-size: 32px; font-weight: 700; letter-spacing: -1px; line-height: 48px;\">Подтвержедение почтового адреса</h1>\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td align=\"center\" bgcolor=\"#e9ecef\">\r\n                <![endif]-->\r\n                <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;     margin-top: -10%;\">\r\n                    <tr>\r\n                        <td align=\"left\" bgcolor=\"#4d4d4d\" style=\"padding: 24px; color : #95b2da; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;\">\r\n                            <p style=\"margin: 0;\">Введите код подтвержедния в мобильном приложении для проверки указанного почтового адреса.</p>\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td align=\"left\" bgcolor=\"#ffffff\">\r\n                            <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\r\n                                <tr>\r\n                                    <td align=\"center\" bgcolor=\"#4d4d4d\" style=\"padding: 12px;\">\r\n                                        <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">\r\n                                            <tr>\r\n                                                <td align=\"center\" bgcolor=\"#4d4d4d\" style=\"border-radius: 6px;\">\r\n" + $"<input type=\"text\" value=\"{numberCode[0]}\" readonly>\r\n                                                    <input type=\"text\" value=\"{numberCode[1]}\" readonly>\r\n                                                    <input type=\"text\" value=\"{numberCode[2]}\" readonly>\r\n                                                    <input type=\"text\" value=\"{numberCode[3]}\" readonly>\r\n                                                    <input type=\"text\" value=\"{numberCode[4]}\" readonly>\r\n                                                </td>\r\n                                            </tr>\r\n                                        </table>\r\n                                    </td>\r\n                                </tr>\r\n                            </table>\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td align=\"left\" bgcolor=\"#4d4d4d\" style=\"padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px; border-radius: 0px 0px 10px 10px;\">\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td align=\"center\" bgcolor=\"#e9ecef\" style=\"padding: 24px; \">\r\n            </td>\r\n        </tr>\r\n    </table>\r\n</body>\r\n</html>";
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587);
                smtp.Credentials = new NetworkCredential("cofFfieTin@yandex.ru", "NVoPkRBFoGVKWM8AmNd5");
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(m);
                return new OkObjectResult(new { numberCode = numberCode });
            }
            catch (Exception) { return new NotFoundResult(); }
        }

        public async Task<IActionResult> SendRecoveryPasswordEmail(string userEmail)
        {
            try
            {
                MailAddress from = new MailAddress("cofFfieTin@yandex.ru", "Фитнес-клуб - 'GLY UP'");
                MailAddress to = new MailAddress(userEmail);
                MailMessage m = new MailMessage(from, to);
                m.Subject = "Завершение регистрации в Фитнес-клубе - 'GLY UP'";
                m.Body = "";
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587);
                smtp.Credentials = new NetworkCredential("cofFfieTin@yandex.ru", "NVoPkRBFoGVKWM8AmNd5");
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(m);
                return new OkObjectResult(new { isSend = true });
            }
            catch {return new NotFoundResult(); }
        }
    }
    
}
