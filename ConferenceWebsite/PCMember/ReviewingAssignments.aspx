<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReviewingAssignments.aspx.cs" Inherits="ConferenceWebsite.PCMember.ReviewingAssignments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfReviewedResult" Value="none" runat="server" />
    <asp:HiddenField ID="hfNotReviewedResult" Value="none" runat="server" />
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #800000" class="h4"><strong>Reviewing Assignments</strong></span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        <!-- Disply of submission reviewed -->
        <asp:Panel ID="pnlSubmissionsReviewed" runat="server" Visible="False">
            <hr />
            <h5><span style="text-decoration: underline; color: #800000" class="h5"><strong>Submissions Reviewed</strong></span></h5>
            <asp:Label ID="lblResultReviewedMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:GridView ID="gvAssignmentsReviewed" runat="server" CssClass="table-condensed"
                        OnRowDataBound="GvAssignmentsReviewed_RowDataBound" Font-Names="Arial" Font-Size="Small">
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="SUBMISSIONNO" DataNavigateUrlFormatString="DisplayReview.aspx?submissionNo={0}" NavigateUrl="~/PCMember/DisplayReview.aspx" HeaderText="VIEW" Text="View" />
                            <asp:HyperLinkField DataNavigateUrlFields="SUBMISSIONNO" DataNavigateUrlFormatString="DiscussReview.aspx?submissionNo={0}" NavigateUrl="~/PCMember/DiscussReview.aspx" HeaderText="DISCUSS" Text="Discuss" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>
        <!-- Display of submissions not reviewed -->
        <asp:Panel ID="pnlSubmissionsNotReviewed" runat="server" Visible="False">
            <hr />
            <h5><span style="text-decoration: underline; color: #800000" class="h5"><strong>Submissions Not Reviewed</strong></span></h5>
            <asp:Label ID="lblResulNotReviewedMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:GridView ID="gvAssignmentsNotReviewed" runat="server" CssClass="table-condensed"
                        OnRowDataBound="GvAssignmentsNotReviewed_RowDataBound" Font-Names="Arial" Font-Size="Small">
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="SUBMISSIONNO" DataNavigateUrlFormatString="CreateReview.aspx?submissionNo={0}" HeaderText="CREATE" NavigateUrl="~/PCMember/CreateReview.aspx" Text="Create" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>