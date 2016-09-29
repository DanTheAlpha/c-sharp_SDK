﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="customervault-w-SEPAAccount.aspx.cs" Inherits="SampleApp.customervault_w_SEPAAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml"> 
<head runat="server">
    <title>Optimal SDK - CardPayment With Customer Vault</title>
</head>
<body>
    <% if (response != null)
       { %>
           <h1>Payment Successful ID:</h1><br/> <% Response.Write(response); %>
    <% }
       else
       { %>
    <form runat="server">
        <fieldset>
            <legend>SEPA Bank Account Details</legend>
            <div>
                <label>
                    Account Number: 
						<input type="text" name="account_number" value="<%Response.Write(Request.Form["account_number"]); %>" />
                </label>
            </div>
             <div>
                <label>
                    IBAN Number: 
						<input type="text" name="iban" value="<%Response.Write(Request.Form["iban"]); %>" />
                </label>
            </div>
             <div>
                <label>
                    BIC: 
						<input type="text" name="bic" value="<%Response.Write(Request.Form["bic"]); %>" />
                </label>
            </div>
             <div>
                <label>
                    Nick Name: 
						<input type="text" name="nick_name" value="<%Response.Write(Request.Form["nick_name"]); %>" />
                </label>
            </div>
            <div>
                <label>
                     Account Holder Name: 
						<input type="text" name="account_holder_name" value="<% Response.Write(Request.Form["account_holder_name"]); %>" />
                </label>
            </div>                    
           </fieldset>
        <asp:Button ID="btnSubmit" Text="Submit" runat="server" />
    </form>
    <% } %>
</body>
</html>

