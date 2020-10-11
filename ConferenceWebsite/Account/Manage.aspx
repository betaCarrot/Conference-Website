<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="ConferenceWebsite.Account.Manage" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #800000" class="h4"><strong>Manage Account</strong></span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small" Visible="False"></asp:Label>
        <asp:Panel ID="pnlCreatePerson" runat="server" Visible="false">
            <hr />
            <!-- Person information input controls -->
            <div class="form-group">
                <asp:Label runat="server" Text="Username:" CssClass="control-label col-xs-1" AssociatedControlID="txtUsername"
                    Font-Names="Arial"></asp:Label>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control input-sm" MaxLength="10"
                        Wrap="False" Font-Names="Arial" Font-Size="Small" BackColor="White" BorderColor="White" BorderStyle="None" ReadOnly="True"></asp:TextBox>
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
                <asp:Label runat="server" Text="Name:" CssClass="control-label col-xs-1" AssociatedControlID="txtName"
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
                <asp:Label runat="server" Text="Country:" CssClass="control-label col-xs-1" AssociatedControlID="txtCountry"
                    Font-Names="Arial"></asp:Label>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control input-sm" MaxLength="30" Wrap="False"
                        Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="A country is required." ControlToValidate="txtCountry"
                        CssClass="text-danger" Display="Dynamic" EnableClientScript="False"
                        Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
                </div>
                <asp:Label runat="server" Text="Email:" CssClass="control-label col-xs-2" AssociatedControlID="txtEmail"
                    Font-Names="Arial"></asp:Label>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control input-sm" MaxLength="50" TextMode="Email"
                        Wrap="False" Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="An email is required." ControlToValidate="txtEmail"
                        CssClass="text-danger" Display="Dynamic" EnableClientScript="False" Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvIsEmailUnique" runat="server"
                        CssClass="text-danger" Display="Dynamic" EnableClientScript="False" Font-Names="Arial" Font-Size="Small"
                        OnServerValidate="CvIsEmailUnique_ServerValidate"></asp:CustomValidator>
                </div>
            </div>
            <div class="form-group">
                <div class="col-xs-offset-1 col-xs-1">
                    <asp:Button ID="btnUpdate" runat="server" OnClick="BtnUpdate_Click" Text="Update" CssClass="btn-sm" Font-Names="Arial" />
                </div>
                <div class="col-xs-10">
                    <asp:Label ID="lblUpdateMessage" runat="server" CssClass="label" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>