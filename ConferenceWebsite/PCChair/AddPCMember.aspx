<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddPCMember.aspx.cs" Inherits="ConferenceWebsite.PCChair.CreatePCMember" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfIsNewPerson" runat="server" />
    <asp:HiddenField ID="hfIsUsernameSet" runat="server" />
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #800000" class="h4"><strong>Add PC Member</strong></span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small" Visible="False"></asp:Label>
        <asp:Panel ID="pnlCreatePCMember" runat="server">
            <hr />
            <!-- Search for person input controls -->
            <div class="form-group">
                <asp:Label runat="server" Text="Email:" CssClass="control-label col-xs-1" AssociatedControlID="txtEmail"
                    Font-Names="Arial"></asp:Label>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control input-sm" MaxLength="50" TextMode="Email"
                        Wrap="False" Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="An email is required." ControlToValidate="txtEmail"
                        CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ValidationGroup="EmailValidation"
                        Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvIsDuplicatePCMember" runat="server"
                        CssClass="text-danger" Display="Dynamic" EnableClientScript="False" Font-Names="Arial" Font-Size="Small"
                        OnServerValidate="CvIsDuplicatePCMember_ServerValidate" ValidationGroup="EmailValidation"></asp:CustomValidator>
                    <asp:CustomValidator ID="cvIsEmailUnique" runat="server"
                        CssClass="text-danger" Display="Dynamic" EnableClientScript="False" Font-Names="Arial" Font-Size="Small"
                        OnServerValidate="CvIsEmailUnique_ServerValidate" ValidationGroup="PCMemberValidation"></asp:CustomValidator>
                </div>
                <div class="col-xs-1">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn-sm" Font-Names="Arial"
                        OnClick="BtnSearchForPerson_Click" ValidationGroup="EmailValidation" />
                </div>
                <div class="col-xs-7">
                    <asp:Label ID="lblSearchResult" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow"
                        Font-Size="Small" ForeColor="Blue"></asp:Label>
                </div>
            </div>
            <!-- PC member information input controls -->
            <asp:Panel ID="pnlPCMemberInput" runat="server" Visible="False">
                <div class="form-group">
                    <asp:Label runat="server" Text="Username:" CssClass="control-label col-xs-1" AssociatedControlID="txtUsername"
                        Font-Names="Arial"></asp:Label>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control input-sm" MaxLength="10"
                            Wrap="False" Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="A username is required." ControlToValidate="txtUsername"
                            CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ValidationGroup="PCMemberValidation"
                            Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="CvIsDuplicateUsername" runat="server" CssClass="text-danger" Display="Dynamic" 
                            EnableClientScript="False" Font-Names="Arial" Font-Size="Small"  ValidationGroup="PCMemberValidation"
                            OnServerValidate="CvIsDuplicateUsername_ServerValidate" ControlToValidate="txtUsername" ></asp:CustomValidator>
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
                            CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ValidationGroup="PCMemberValidation"
                            Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
                    </div>
                    <asp:Label runat="server" Text="Institution:" CssClass="control-label col-xs-2" AssociatedControlID="txtInstitution"
                        Font-Names="Arial"></asp:Label>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtInstitution" runat="server" CssClass="form-control input-sm" MaxLength="100" Wrap="False"
                            Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="An institution is required."
                            ControlToValidate="txtInstitution" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"
                            ValidationGroup="PCMemberValidation" Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" Text="Country:" CssClass="control-label col-xs-1" AssociatedControlID="txtCountry"
                        Font-Names="Arial"></asp:Label>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control input-sm" MaxLength="30" Wrap="False"
                            Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="A country is required." ControlToValidate="txtCountry"
                            CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ValidationGroup="PCMemberValidation"
                            Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-offset-1 col-xs-2">
                        <asp:Button ID="btnAddPCMember" runat="server" OnClick="BtnAddPCMember_Click" Text="Add PC Member" CssClass="btn-sm"
                            ValidationGroup="PCMemberValidation" Font-Names="Arial" />
                    </div>
                </div>
            </asp:Panel>
        </asp:Panel>
    </div>
</asp:Content>