<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisplayPCMember.aspx.cs" Inherits="ConferenceWebsite.PCChair.DisplayPCMember" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #800000" class="h4"><strong>PC Member Information</strong></span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small" Visible="false"></asp:Label>
        <!-- Dsiplay of PC member information -->
        <asp:Panel ID="pnlPCMemberInfo" runat="server" Visible="false">
            <hr />
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:GridView ID="gvPCMember" runat="server" CssClass="table-condensed" Font-Names="Arial" Font-Size="Small">
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>