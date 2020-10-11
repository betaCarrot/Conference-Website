<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ConferenceWebsite.Account.Login" Async="true" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <style type="text/css">
        .radioButtonList {
            list-style: none;
            margin: 0;
            padding: 0;
        }

            .radioButtonList.horizontal li {
                margin-top: 20px;
                margin-bottom: 0px;
                display: inline;
            }

            .radioButtonList label {
                margin-top: 100px;
                margin-bottom: 0px;
                margin-left: 5px;
                margin-right: 10px;
                display: inline;
            }
    </style>
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #800000"><strong>Log in</strong></span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small" Visible="false"></asp:Label>
        <hr />
        <div class="form-group">
            <asp:Label runat="server" Text="Username:" AssociatedControlID="Username" CssClass="col-xs-2 control-label" Font-Names="Arial"></asp:Label>
            <div class="col-xs-3">
                <asp:TextBox runat="server" ID="Username" CssClass="form-control input-sm" MaxLength="10" Font-Names="Arial" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Username"
                    CssClass="text-danger" ErrorMessage="A username is required." Display="Dynamic" Font-Names="Arial" Font-Size="Small" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="lblRole" runat="server" Text="Login as:" AssociatedControlID="rblRole" CssClass="col-xs-2 control-label" Font-Names="Arial"></asp:Label>
            <div class="col-xs-10">
                <asp:RadioButtonList ID="rblRole" runat="server" CssClass="radioButtonList" Font-Names="Arial" Font-Size="Small" RepeatDirection="Horizontal" RepeatLayout="Table" CellSpacing="10">
                    <asp:ListItem Selected="True" Value="AuthorOf">Author</asp:ListItem>
                    <asp:ListItem Value="PCMember">PC Member</asp:ListItem>
                    <asp:ListItem Value="PCChair">PC Chair</asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
        <div class="form-group">
            <div class="col-xs-offset-2 col-xs-1">
                <asp:Button runat="server" OnClick="LogIn" Text="Log in" CssClass="btn-sm" Font-Names="Arial" />
            </div>
        </div>
        <%-- The Password textbox is disabled in this application --%>
        <div class="form-group">
            <div class="col-xs-10">
                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" Visible="False">Conference1#</asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="The password field is required." Display="Dynamic" Font-Names="Arial" Font-Size="Small" />
            </div>
        </div>
    </div>
</asp:Content>
