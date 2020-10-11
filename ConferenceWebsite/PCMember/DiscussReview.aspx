<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DiscussReview.aspx.cs" Inherits="ConferenceWebsite.PCMember.DiscussReview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #800000" class="h4"><strong>Review Discussion</strong></span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small" Visible="False"></asp:Label>
        <!-- Display of submission information -->
        <asp:Panel ID="pnlSubmission" runat="server" Visible="False">
            <hr />
            <div class="form-group">
                <asp:Label runat="server" Text="Submission:" AssociatedControlID="txtSubmissionNO" CssClass="control-label col-xs-1" Font-Names="Arial"></asp:Label>
                <div class="col-xs-2">
                    <asp:TextBox ID="txtSubmissionNo" runat="server" CssClass="form-control" BorderStyle="None" ReadOnly="True"
                        Font-Names="Arial" Font-Size="Small" BackColor="White" BorderColor="White"></asp:TextBox>
                </div>
                <asp:Label runat="server" Text="Title:" AssociatedControlID="txtTitle" CssClass="control-label col-xs-1" Font-Names="Arial"></asp:Label>
                <div class="col-xs-8">
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" BorderStyle="None" ReadOnly="True"
                        Font-Names="Arial" Font-Size="Small" BackColor="White" BorderColor="White"></asp:TextBox>
                </div>
            </div>
        </asp:Panel>
        <!-- Display of submission authors -->
        <asp:Panel ID="pnlAuthors" runat="server" Visible="False">
            <div class="form-group">
                <asp:Label runat="server" Text="Status:" AssociatedControlID="txtStatus" CssClass="control-label col-xs-1" Font-Names="Arial"></asp:Label>
                <div class="col-xs-2">
                    <asp:TextBox ID="txtStatus" runat="server" CssClass="form-control" BorderStyle="None" ReadOnly="True"
                        Font-Names="Arial" Font-Size="Small" BackColor="White" BorderColor="White" BorderWidth="0px"></asp:TextBox>
                </div>
                <asp:Label runat="server" Text="Author(s):" AssociatedControlID="txtAuthor" CssClass="control-label col-xs-1" Font-Names="Arial"></asp:Label>
                <div class="col-xs-8">
                    <asp:TextBox ID="txtAuthor" runat="server" CssClass="form-control" BorderStyle="None" ReadOnly="True"
                        Font-Names="Arial" Font-Size="Small" BackColor="White" BorderColor="White" BorderWidth="0px"></asp:TextBox>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlOverallSummary" runat="server" Visible="False">
            <!-- Display of overall summary of each reviewer -->
            <br />
            <h5><span style="text-decoration: underline; color: #800000" class="h5"><strong>Overall Summaries</strong></span></h5>
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:GridView ID="gvOverallSummary" runat="server" BorderStyle="None" OnRowDataBound="GvOverallSummary_RowDataBound" BorderColor="White" CssClass="table-condensed" Font-Names="Arial" Font-Size="Small"></asp:GridView>
                </div>
            </div>
            <!-- Display of review summary of each reviewer -->
            <br />
            <h5><span style="text-decoration: underline; color: #800000" class="h5"><strong>Summary of Reviews</strong></span></h5>
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:GridView ID="gvReviewSummary" runat="server" OnRowDataBound="GvReviewSummary_RowDataBound" CssClass="table-condensed" Font-Names="Arial" Font-Size="Small"></asp:GridView>
                </div>
            </div>
            <div class="form-group">
                <div>
                    <asp:Label runat="server" Text="Spread: " CssClass="control-label col-xs-1" Font-Names="Arial" 
                        AssociatedControlID="txtSpread"></asp:Label>
                    <div class="col-xs-1">
                        <asp:TextBox ID="txtSpread" runat="server" CssClass="form-control" BorderStyle="None" ReadOnly="True"
                        Font-Names="Arial" Font-Size="Small" BackColor="White" BorderColor="White" BorderWidth="0px" Font-Bold="True"></asp:TextBox>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <!-- Display of discussion -->
        <asp:Panel ID="pnlDiscussion" runat="server" Visible="False">
            <hr />
            <h5><span style="text-decoration: underline; color: #800000" class="h5"><strong>Discussion For This Submission</strong></span></h5>
            <asp:Label ID="lblDiscussionResultMessage" runat="server" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small" Width="600px"></asp:Label>
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:GridView ID="gvDiscussion" runat="server" CssClass="table-condensed" Font-Names="Arial" Font-Size="Small" OnRowDataBound="gvDiscussion_RowDataBound" Width="710px"></asp:GridView>
                </div>
            </div>
        </asp:Panel>
        <!-- Discussion input controls -->
        <asp:Panel ID="pnlAddNewDiscussion" runat="server" Visible="False">
            <hr />
            <h5><span style="text-decoration: underline; color: #800000" class="h5"><strong>Add Comments To This Discussion</strong></span></h5>
            <div class="form-group">
                <div class="col-xs-1">
                    <asp:Button ID="btnAddToDiscussion" runat="server" OnClick="BtnAddToDiscussion_Click" Text="Add" CssClass="btn-sm"
                        Font-Names="Arial" />
                </div>
                <div class="col-xs-11">
                    <asp:TextBox ID="txtNewDiscussion" runat="server" Height="50px" MaxLength="200" TextMode="MultiLine"
                        Font-Names="Arial" Font-Size="Small" Width="630px"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="&lt;br/&gt;Please enter some discussion comments." ControlToValidate="txtNewDiscussion"
                        CssClass="text-danger" Display="Dynamic" EnableClientScript="False" Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
