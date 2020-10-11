<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="ConferenceWebsite.TestPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #800000" class="h4"><strong>Display Referee Report/Discussionn — Add Discussion</strong></span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        <asp:Panel ID="pnlReview" runat="server">
            <hr />
            <div class="form-group">
                <asp:Label runat="server" Text="Title:" AssociatedControlID="txtTitle" CssClass="control-label col-md-2" Font-Names="Arial"></asp:Label>
                <div class="col-md-10">
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" BorderStyle="None" ReadOnly="True"
                        Font-Names="Arial" Font-Size="Small" BackColor="White" BorderColor="White"></asp:TextBox>
                </div>
            </div>

        </asp:Panel>
    </div>
</asp:Content>
