﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using OptimalPayments;
using Authorization = OptimalPayments.CardPayments.Authorization;
using OptimalPayments.CustomerVault;

namespace SampleApp
{
    public partial class customervault_w_EFTAccount : System.Web.UI.Page
    {
        protected String response = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo ui = System.Globalization.CultureInfo.CurrentUICulture;

            btnSubmit.Click += new System.EventHandler(this.submit);
        }

        protected void submit(object sender, System.EventArgs e)
        {
            string apiKey = System.Configuration.ConfigurationManager.AppSettings["ApiKey"];
            string apiSecret = System.Configuration.ConfigurationManager.AppSettings["ApiSecret"];
            string accountNumber = System.Configuration.ConfigurationManager.AppSettings["accountNumber"];

            OptimalApiClient client = new OptimalApiClient(apiKey, apiSecret, OptimalPayments.Environment.TEST, accountNumber);
            try
            {
                Profile profile = client.customerVaultService().create(Profile.Builder()
                    .merchantCustomerId(System.Guid.NewGuid().ToString())
                    .locale("en_US")
                    .firstName("John")
                    .lastName("Smith")
                    .email("john.smith@somedomain.com")
                    .phone("713-444-5555")
                    .Build());

                Address address = client.customerVaultService().create(Address.Builder()
                    .nickName("home")
                    .street("100 Queen Street West")
                    .street2("Unit 201")
                    .city("Toronto")
                    .country("CA")
                    .state("ON")
                    .zip("M5H 2N2")
                    .recipientName("Jane Doe")
                    .phone("647-788-3901")
                    .profileId(profile.id())
                    .Build());

                EFTBankAccounts account = client.customerVaultService().create(EFTBankAccounts.Builder()
                    .accountNumber(Request.Form["account_number"])
                    .transitNumber(Request.Form["transit_number"])
                    .institutionId(Request.Form["institution_id"])
                    .accountHolderName(Request.Form["account_holder_name"])
                    .nickName(Request.Form["nick_name"])
                    .billingAddressId(address.id())
                    .profileId(profile.id())
                    .Build());
                this.response = account.ToString();
            }
            catch (Exception ex)
            {
                Response.Write("<font style=\"color: #FF0000;\">Error Message is : " + ex.Message + "</font>\n");
            }
        }
    }
}