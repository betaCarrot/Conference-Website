﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateSubmission.aspx.cs" Inherits="ConferenceWebsite.Author.CreateSubmission" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfIsNewPerson" runat="server" Value="false" />
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #800000"><strong>Create Submission</strong></span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small" Visible="false"></asp:Label>
        <asp:Panel ID="pnlSubmissionInfo" runat="server">
            <hr />
            <!-- Submission input controls -->
            <div class="form-group">
                <asp:Label runat="server" Text="Title:" CssClass="control-label col-xs-1" AssociatedControlID="txtTitle"
                    Font-Names="Arial"></asp:Label>
                <div class="col-xs-7">
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control input-sm" MaxLength="100" Font-Names="Arial"
                        Font-Size="Small" Width="600px"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="A title is required." ControlToValidate="txtTitle"
                        CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ValidationGroup="SubmissionValidation"
                        Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
                </div>
                <asp:Label runat="server" Text="Type:" CssClass="control-label col-xs-1" AssociatedControlID="ddlSubmissionType"
                    Font-Names="Arial"></asp:Label>
                <div class="col-xs-1">
                    <asp:DropDownList ID="ddlSubmissionType" runat="server" CssClass="dropdown" Font-Names="Arial" Font-Size="Small"
                        ForeColor="Black">
                        <asp:ListItem Value="research">Research</asp:ListItem>
                        <asp:ListItem Value="demo">Demo</asp:ListItem>
                        <asp:ListItem Value="industrial">Industrial</asp:ListItem>
                        <asp:ListItem Value="vision">Vision</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Abstract:" CssClass="control-label col-xs-1" AssociatedControlID="txtAbstract"
                    Font-Names="Arial"></asp:Label>
                <div class="col-xs-7">
                    <asp:TextBox ID="txtAbstract" runat="server" CssClass="form-control input-sm" MaxLength="300" TextMode="MultiLine"
                        Height="100" Font-Names="Arial" Font-Size="Small" Width="540px"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="An abstract is required." ControlToValidate="txtAbstract"
                        CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ValidationGroup="SubmissionValidation"
                        Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <div class="col-xs-offset-1 col-xs-11">
                    <asp:Button ID="btnSubmit" runat="server" OnClick="BtnSubmit_Click" Text="Submit" CssClass="btn-sm"
                        Font-Names="Arial" />
                </div>
            </div>
            <!-- Contact author display -->
            <hr />
            <h4><span style="text-decoration: underline; color: #800000"><strong>Authors</strong></span></h4>
            <div class="form-group">
                <asp:Label runat="server" Text="Contact author:" CssClass="control-label col-xs-1" AssociatedControlID="gvContactAuthor" Font-Names="Arial"></asp:Label>
                <div class="col-xs-11">
                    <asp:GridView ID="gvContactAuthor" runat="server" ShowHeaderWhenEmpty="True" OnRowDataBound="GvContactAuthor_RowDataBound"
                        CssClass="table-condensed" BorderStyle="Solid" CellPadding="0" Font-Names="Arial" Font-Size="Small">
                    </asp:GridView>
                </div>
            </div>
            <!-- Coauthor display -->
            <asp:Panel ID="pnlCoauthors" runat="server" Visible="False">
                <div class="form-group">
                    <asp:Label runat="server" Text="Coauthors:" CssClass="control-label col-xs-1" AssociatedControlID="gvCoauthors" Font-Names="Arial"></asp:Label>
                    <div class="col-xs-11">
                        <asp:GridView ID="gvCoauthors" runat="server" ShowHeaderWhenEmpty="True" OnRowDataBound="GvCoauthors_RowDataBound"
                            CssClass="table-condensed" BorderStyle="Solid" CellPadding="0" Font-Names="Arial" Font-Size="Small" 
                            AutoGenerateDeleteButton="True" OnRowDeleting="GvCoauthors_RowDeleting">
                        </asp:GridView>
                    </div>
                </div>
            </asp:Panel>
            <br />
            <!-- Author input controls -->
            <h5><span style="text-decoration: underline; color: #800000" class="h5"><strong>Add Author</strong></span></h5>
            <div class="form-group">
                <asp:Label runat="server" Text="Email:" CssClass="control-label col-xs-1" AssociatedControlID="txtEmail"
                    Font-Names="Arial"></asp:Label>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control input-sm" MaxLength="50" TextMode="Email"
                        Wrap="False" Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="An email is required." ControlToValidate="txtEmail"
                        CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ValidationGroup="EmailValidation"
                        Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvIsDuplicateAuthor" runat="server"
                        CssClass="text-danger" Display="Dynamic" EnableClientScript="False" Font-Names="Arial" Font-Size="Small"
                        OnServerValidate="CvIsDuplicateAuthor_ServerValidate" ValidationGroup="EmailValidation"></asp:CustomValidator>
                    <asp:CustomValidator ID="cvIsEmailUnique" runat="server"
                        CssClass="text-danger" Display="Dynamic" EnableClientScript="False" Font-Names="Arial" Font-Size="Small"
                        OnServerValidate="CvIsEmailUnique_ServerValidate" ValidationGroup="AuthorValidation"></asp:CustomValidator>
                </div>
                <div class="col-xs-1">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn-sm" Font-Names="Arial"
                        OnClick="BtnSearchForAuthor_Click" ValidationGroup="EmailValidation" />
                </div>
                <div class="col-xs-7">
                    <asp:Label ID="lblSearchResult" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow"
                        Font-Size="Small" ForeColor="Blue"></asp:Label>
                </div>
            </div>
            <asp:Panel ID="pnlAuthorInput" runat="server" Visible="False">
                <div class="form-group">
                    <asp:Label runat="server" Text="Title:" CssClass="control-label col-xs-1" AssociatedControlID="ddlTitle"
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
                    <asp:Label runat="server" Text="Name:" CssClass="control-label col-xs-1" AssociatedControlID="txtAuthorName"
                        Font-Names="Arial"></asp:Label>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtAuthorName" runat="server" CssClass="form-control input-sm" MaxLength="50" Wrap="False"
                            Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="A name is required." ControlToValidate="txtAuthorName"
                            CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ValidationGroup="AuthorValidation"
                            Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
                    </div>
                    <asp:Label runat="server" Text="Institution:" CssClass="control-label col-xs-2" AssociatedControlID="txtInstitution"
                        Font-Names="Arial"></asp:Label>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtInstitution" runat="server" CssClass="form-control input-sm" MaxLength="100" Wrap="False"
                            Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="An institution is required."
                            ControlToValidate="txtInstitution" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"
                            ValidationGroup="AuthorValidation" Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" Text="Country:" CssClass="control-label col-xs-1" AssociatedControlID="txtCountry"
                        Font-Names="Arial"></asp:Label>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control input-sm" MaxLength="30" Wrap="False"
                            Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="A country is required." ControlToValidate="txtCountry"
                            CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ValidationGroup="AuthorValidation"
                            Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-offset-1 col-xs-2">
                        <asp:Button ID="btnAddAuthor" runat="server" OnClick="BtnAddAuthor_Click" Text="Add Author" CssClass="btn-sm"
                            ValidationGroup="AuthorValidation" Font-Names="Arial" />
                    </div>
                </div>
            </asp:Panel>
        </asp:Panel>
    </div>
</asp:Content>