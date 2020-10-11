<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisplaySubmissionStatistics.aspx.cs" Inherits="ConferenceWebsite.PCChair.DisplaySubmissionStatistics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #800000" class="h4"><strong>Submission Statistics</strong></span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small" Visible="false"></asp:Label>
        <!-- Display of submission statistics -->
        <asp:Panel ID="pnlSubmissionStatistics" runat="server" Visible="false">
            <hr />
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:GridView ID="gvSubmissionStatistics" runat="server" CssClass="table-condensed" Font-Names="Arial" Font-Size="Small" 
                        OnRowDataBound="GvSubmissionStatistics_RowDataBound"></asp:GridView>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>