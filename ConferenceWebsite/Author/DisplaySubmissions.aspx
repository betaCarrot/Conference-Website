<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisplaySubmissions.aspx.cs" Inherits="ConferenceWebsite.Author.DisplaySubmission" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #800000" class="h4"><strong>Your Submissions</strong></span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small" Visible="false"></asp:Label>
        <!-- Display of submissions -->
        <asp:Panel ID="pnlSearchResult" runat="server" Visible="False">
            <hr />
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:GridView ID="gvSubmission" runat="server" CssClass="table-condensed" BorderStyle="Solid" CellPadding="0"
                        OnRowDataBound="GvSubmission_RowDataBound" Font-Names="Arial" Font-Size="Small" OnSelectedIndexChanged="GvSubmission_SelectedIndexChanged">
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="SUBMISSIONNO" DataNavigateUrlFormatString="EditSubmission.aspx?submissionNo={0}" NavigateUrl="~/Author/EditSubmission.aspx" Text="Edit" HeaderText="EDIT" />
                            <asp:ButtonField CommandName="Select" Text="Withdraw" HeaderText="WITHDRAW" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <br />
            <asp:Label runat="server" Text="You can only edit or withdraw submissions for which you are the contact author." CssClass="label" Font-Names="Arial Narrow" Font-Size="Small" ForeColor="Blue"></asp:Label>
        </asp:Panel>
    </div>
</asp:Content>