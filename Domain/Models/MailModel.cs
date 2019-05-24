using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class MailModel : Model
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public static string UserNameToken = "[UserName]";
        public static string AppNameToken = "[AppName]";
        public static string CodeToken = "[Code]";
        public static string LinkToken = "[Link]";

        public static string NewUserPasswordlessLinkMessage = $"Hi {UserNameToken}, " +
                                                              "<br/>" +
                                                              $"You have been successfully created in {AppNameToken}. <br/>" +
                                                              "Kind click on the  following link  to login. <hr/>" +
                                                              $"Link : {LinkToken}";
        public static string NewUserPasswordMessage = $"Hi {UserNameToken}, " +
                                                             "<br/>" +
                                                             $"You have been successfully created in {AppNameToken}. <br/>" +
                                                             "Kind click on the  following link and use your email and password to login. <hr/>" +

                                                             $"Link : {LinkToken}";

    }



}
