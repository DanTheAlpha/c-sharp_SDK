﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using OptimalPayments;
using Purchases = OptimalPayments.DirectDebit.Purchases;
namespace SampleApp
{
    public partial class direct_debit_purchase_EFTAccount : System.Web.UI.Page
    {
        protected string response = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo ui = System.Globalization.CultureInfo.CurrentUICulture;
            btnSubmit.Click += new System.EventHandler(this.submit);
        }
        protected void submit(object sender, System.EventArgs e)
        {
            string apiKey = System.Configuration.ConfigurationManager.AppSettings["ApiKey"];
            string apiSecret = System.Configuration.ConfigurationManager.AppSettings["ApiSecret"];
            string accountNumber = System.Configuration.ConfigurationManager.AppSettings["AccountNumber_EFT"];

            OptimalApiClient client = new OptimalApiClient(apiKey, apiSecret, OptimalPayments.Environment.TEST, accountNumber);
            try
            {
                Purchases purchase = Purchases.Builder()
                     .merchantRefNum(Request.Form["merchant_customer_id"])
                     .amount(Convert.ToInt32(Double.Parse(Request.Form["amount"])))
                     .eft()
                          .accountHolderName(Request.Form["account_holder_name"])
                          .accountNumber(Request.Form["account_number"])
                          .transitNumber(Request.Form["transit_number"])
                          .institutionId(Request.Form["institution_id"])
                          .Done()
                      .customerIp(Request.Form["customer_ip"])
                     .profile()
                            .firstName(Request.Form["first_name"])
                            .lastName(Request.Form["last_name"])
                            .email(Request.Form["email"])
                            .Done()
                      .billingDetails()
                             .street(Request.Form["street"])
                             .city(Request.Form["city"])
                             .state(Request.Form["state"])
                             .country(Request.Form["country"])
                             .zip(Request.Form["zip"])
                             .phone(Request.Form["phone"])
                             .Done()
                      .Build();
                Purchases response = client.directDebitService().submit(purchase);
                this.response = response.ToString();
            }
            catch (Exception ex)
            {
                Response.Write("<font style=\"color: #FF0000;\">Error Message is : " + ex.Message + "</font>\n");
            }
        }
    }
}