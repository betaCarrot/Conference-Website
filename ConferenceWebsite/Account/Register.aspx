<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="ConferenceWebsite.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h4><span style="text-decoration: underline; color: #800000"><strong>Create An Author Account</strong></span></h4>
    <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small" Visible="false"></asp:Label>
    <hr />
    <div class="form-horizontal">
        <!-- Person information input controls -->
        <div class="form-group">
            <asp:Label runat="server" Text="Username:" CssClass="control-label col-xs-2" AssociatedControlID="txtUsername"
                Font-Names="Arial"></asp:Label>
            <div class="col-xs-3">
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control input-sm" MaxLength="10"
                    Wrap="False" Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ErrorMessage="A username is required." ControlToValidate="txtUsername"
                    CssClass="text-danger" Display="Dynamic" EnableClientScript="False" Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="cvUsername" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" 
                    Font-Names="Arial" Font-Size="Small" OnServerValidate="CvIsDuplicateUsername_ServerValidate" ControlToValidate="txtUsername"></asp:CustomValidator>
            </div>
            <asp:Label runat="server" Text="Title:" CssClass="control-label col-xs-2" AssociatedControlID="ddlTitle"
                Font-Names="Arial"></asp:Label>
            <div class="col-xs-3">
                <asp:DropDownList ID="ddlTitle" runat="server" CssClass="dropdown" Font-Names="Arial" Font-Size="Small"
                    ForeColor="Black">
                    <asp:ListItem>None</asp:ListItem>
                    <asp:ListItem>Mr</asp:ListItem>
                    <asp:ListItem>Ms</asp:ListItem>
                    <asp:ListItem>Miss</asp:ListItem>
                    <asp:ListItem>Dr</asp:ListItem>
                    <asp:ListItem>Prof</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" Text="Name:" CssClass="control-label col-xs-2" AssociatedControlID="txtName"
                Font-Names="Arial"></asp:Label>
            <div class="col-xs-3">
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control input-sm" MaxLength="50" Wrap="False"
                    Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ErrorMessage="A name is required." ControlToValidate="txtName"
                    CssClass="text-danger" Display="Dynamic" EnableClientScript="False" Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
            </div>
            <asp:Label runat="server" Text="Institution:" CssClass="control-label col-xs-2" AssociatedControlID="txtInstitution"
                Font-Names="Arial"></asp:Label>
            <div class="col-xs-3">
                <asp:TextBox ID="txtInstitution" runat="server" CssClass="form-control input-sm" MaxLength="100" Wrap="False"
                    Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ErrorMessage="An institution is required."
                    ControlToValidate="txtInstitution" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"
                    Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" Text="Country:" CssClass="control-label col-xs-2" AssociatedControlID="txtCountry"
                Font-Names="Arial"></asp:Label>
            <div class="col-xs-3">
                <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control input-sm" MaxLength="30" Wrap="False"
                    Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ErrorMessage="A country is required." ControlToValidate="txtCountry"
                    CssClass="text-danger" Display="Dynamic" EnableClientScript="False" Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
            </div>
            <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-xs-2 control-label" Font-Names="Arial">Email:</asp:Label>
            <div class="col-xs-3">
                <asp:TextBox runat="server" ID="Email" CssClass="form-control input-sm" TextMode="Email" Font-Names="Arial" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                    CssClass="text-danger" ErrorMessage="An email is required." Font-Names="Arial" Font-Size="Small" Display="Dynamic" EnableClientScript="False" ></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="cvEmail" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" 
                    Font-Names="Arial" Font-Size="Small" OnServerValidate="CvIsDuplicateEmail_ServerValidate" ></asp:CustomValidator>
            </div>
        </div>
                <div class="form-group">
            <div class="col-xs-offset-2 col-xs-10">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" CssClass="btn-sm" Font-Names="Arial" />
            </div>
        </div>
        <!-- The Password and ConfirmPassword fields are hidden in this application -->
        <div class="form-group">
            <div class="col-xs-10">
                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" Visible="false" >Conference1#</asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                    CssClass="text-danger" ErrorMessage="The password field is required." Display="Dynamic" EnableClientScript="False" />
            </div>
            <div class="col-xs-10">
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" Visible="false" >Conference1#</asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm password field is required." EnableClientScript="False" />
                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." EnableClientScript="False" />
            </div>
        </div>
    </div>
</asp:Content>
