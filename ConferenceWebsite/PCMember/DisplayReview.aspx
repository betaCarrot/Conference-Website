<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisplayReview.aspx.cs" Inherits="ConferenceWebsite.PCMember.DisplayReview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #800000" class="h4"><strong>Display Review</strong></span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small" Visible="False"></asp:Label>
        <!-- Display of submission information -->
        <asp:Panel ID="pnlSubmission" runat="server" Visible="False">
            <hr />
            <div class="form-group">
                <asp:Label runat="server" Text="Title:" AssociatedControlID="txtTitle" CssClass="control-label col-xs-1" Font-Names="Arial"></asp:Label>
                <div class="col-xs-11">
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" BorderStyle="None" ReadOnly="True"
                        Font-Names="Arial" Font-Size="Small" BackColor="White" BorderColor="White"></asp:TextBox>
                </div>
            </div>
        </asp:Panel>
        <!-- Display of submission authors -->
        <asp:Panel ID="pnlAuthors" runat="server" Visible="False">
            <div class="form-group">
                <asp:Label runat="server" Text="Author(s):" AssociatedControlID="txtAuthor" CssClass="control-label col-xs-1" Font-Names="Arial"></asp:Label>
                <div class="col-xs-10">
                    <asp:TextBox ID="txtAuthor" runat="server" CssClass="form-control" BorderStyle="None" ReadOnly="True"
                        Font-Names="Arial" Font-Size="Small" BackColor="White" BorderColor="White" BorderWidth="0px"></asp:TextBox>
                </div>
            </div>
        </asp:Panel>
        <!-- Display of review -->
        <asp:Panel ID="pnlReview" runat="server" Visible="False">
            <div class="form-group">
                <asp:Label runat="server" Text="The&nbsp;paper&nbsp;is&nbsp;relevant&nbsp;to&nbsp;the&nbsp;conference:" AssociatedControlID="txtRelevant"
                    CssClass="control-label-left col-xs-4" Font-Names="Arial"></asp:Label>
                <div class="col-xs-1">
                    <asp:TextBox ID="txtRelevant" runat="server" CssClass="form-control" BorderStyle="None" ReadOnly="True"
                        Font-Names="Arial" Font-Size="Small" BackColor="White" BorderColor="White" BorderWidth="0px" Wrap="False"></asp:TextBox>
                </div>
                <asp:Label runat="server" Text="The&nbsp;paper&nbsp;is&nbsp;technically&nbsp;correct:" AssociatedControlID="txtTechnicallyCorrect"
                    CssClass="control-label-left col-xs-3" Font-Names="Arial"></asp:Label>
                <div class="col-xs-1">
                    <asp:TextBox ID="txtTechnicallyCorrect" runat="server" CssClass="form-control" BorderStyle="None" ReadOnly="True"
                        Font-Names="Arial" Font-Size="Small" BackColor="White" BorderColor="White" BorderWidth="0px" Wrap="False"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="The&nbsp;length&nbsp;and&nbsp;content&nbsp;of&nbsp;the&nbsp;paper&nbsp;are&nbsp;comparable&nbsp;to&nbsp;the&nbsp;expected&nbsp;final&nbsp;version:"
                    AssociatedControlID="txtLengthAndContent" CssClass="control-label-left col-xs-7" Font-Names="Arial"></asp:Label>
                <div class="col-xs-1">
                    <asp:TextBox ID="txtLengthAndContent" runat="server" CssClass="form-control" BorderStyle="None" ReadOnly="True"
                        Font-Names="Arial" Font-Size="Small" BackColor="White" BorderColor="White" BorderWidth="0px" Wrap="False"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Reviewer&nbsp;Confidence:" AssociatedControlID="txtConfidence" CssClass="control-label-left col-xs-2"
                    Font-Names="Arial"></asp:Label>
                <div class="col-xs-1">
                    <asp:TextBox ID="txtConfidence" runat="server" CssClass="form-control" BorderStyle="None" ReadOnly="True"
                        Font-Names="Arial" Font-Size="Small" BackColor="White" BorderColor="White" BorderWidth="0px" Wrap="False"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Originality" AssociatedControlID="txtOriginality" CssClass="control-label col-xs-2"
                    Style="text-align: center" Font-Names="Arial"></asp:Label>
                <asp:Label runat="server" Text="Impact" AssociatedControlID="txtImpact" CssClass="control-label col-xs-2"
                    Style="text-align: center; right: 101px;" Font-Names="Arial"></asp:Label>
                <asp:Label runat="server" Text="Presentation" AssociatedControlID="txtPresentation" CssClass="control-label col-xs-2"
                    Style="text-align: center" Font-Names="Arial"></asp:Label>
                <asp:Label runat="server" Text="Technical&nbsp;Depth" AssociatedControlID="txtTechnicalDepth" CssClass="control-label col-xs-2"
                    Style="text-align: center" Font-Names="Arial"></asp:Label>
                <asp:Label runat="server" Text="OVERALL&nbsp;RATING" AssociatedControlID="txtOverallRating" CssClass="control-label col-xs-2"
                    Style="text-align: center" Font-Names="Arial"></asp:Label>
            </div>
            <div class="form-group">
                <div class="col-xs-2">
                    <asp:TextBox ID="txtOriginality" runat="server" CssClass="form-control" BorderStyle="None" ReadOnly="True"
                        Style="text-align: center" Font-Names="Arial" Font-Size="Small" BackColor="White" BorderColor="White" BorderWidth="0px"></asp:TextBox>
                </div>
                <div class="col-xs-2">
                    <asp:TextBox ID="txtImpact" runat="server" CssClass="form-control" BorderStyle="None" ReadOnly="True"
                        Style="text-align: center" Font-Names="Arial" Font-Size="Small" BackColor="White" BorderColor="White" BorderWidth="0px"></asp:TextBox>
                </div>
                <div class="col-xs-2">
                    <asp:TextBox ID="txtPresentation" runat="server" CssClass="form-control" BorderStyle="None" ReadOnly="True"
                        Style="text-align: center" Font-Names="Arial" Font-Size="Small" BackColor="White" BorderColor="White" BorderWidth="0px"></asp:TextBox>
                </div>
                <div class="col-xs-2">
                    <asp:TextBox ID="txtTechnicalDepth" runat="server" CssClass="form-control" BorderStyle="None" ReadOnly="True"
                        Style="text-align: center" Font-Names="Arial" Font-Size="Small" BackColor="White" BorderColor="White" BorderWidth="0px"></asp:TextBox>
                </div>
                <div class="col-xs-2">
                    <asp:TextBox ID="txtOverallRating" runat="server" CssClass="form-control" BorderStyle="None" ReadOnly="True"
                        Style="text-align: center" Font-Names="Arial" Font-Size="Small" BackColor="White" BorderColor="White" BorderWidth="0px"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Main Contribution(s):" AssociatedControlID="txtMainContributions"
                    CssClass="control-label-left col-xs-2" Font-Names="Arial"></asp:Label>
                <div class="col-xs-10">
                    <asp:TextBox ID="txtMainContributions" runat="server" Height="60px"
                        CssClass="form-control" BorderStyle="None" ReadOnly="True" Font-Names="Arial" Font-Size="Small" BackColor="White" BorderColor="White" BorderWidth="0px" Width="700px" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Three strong points<br/>of the paper:" AssociatedControlID="txtStrongPoints"
                    CssClass="control-label-left col-xs-2" Font-Names="Arial"></asp:Label>
                <div class="col-xs-10">
                    <asp:TextBox ID="txtStrongPoints" runat="server" Height="60px"
                        CssClass="form-control" BorderStyle="None" ReadOnly="True" Font-Names="Arial" Font-Size="Small" BackColor="White" BorderColor="White" BorderWidth="0px" Width="700px" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Three weak points<br/>of the paper:" AssociatedControlID="txtWeakPoints"
                    CssClass="control-labelleft col-xs-2" Font-Names="Arial"></asp:Label>
                <div class="col-xs-10">
                    <asp:TextBox ID="txtWeakPoints" runat="server" Height="60px" CssClass="form-control"
                        BorderStyle="None" ReadOnly="True" Font-Names="Arial" Font-Size="Small" BackColor="White" BorderColor="White" BorderWidth="0px" Width="700px" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Overall Summary:" AssociatedControlID="txtOverallSummary" CssClass="control-label-left col-xs-2"
                    Font-Names="Arial"></asp:Label>
                <div class="col-xs-10">
                    <asp:TextBox ID="txtOverallSummary" runat="server" Height="60px" CssClass="form-control"
                        BorderStyle="None" ReadOnly="True" Font-Names="Arial" Font-Size="Small" BackColor="White" BorderColor="White" BorderWidth="0px" Width="700px" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Detailed Comments:" AssociatedControlID="txtDetailedComments" CssClass="control-label-left col-xs-2"
                    Font-Names="Arial"></asp:Label>
                <div class="col-xs-10">
                    <asp:TextBox ID="txtDetailedComments" runat="server" Height="60px"
                        CssClass="form-control" BorderStyle="None" ReadOnly="True" Font-Names="Arial" Font-Size="Small" BackColor="White" BorderColor="White" BorderWidth="0px" Width="700px" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Confidential comments to the PC, if any:" AssociatedControlID="txtConfidentialComments"
                    CssClass="control-label-left col-xs-2" Font-Names="Arial"></asp:Label>
                <div class="col-xs-10">
                    <asp:TextBox ID="txtConfidentialComments" runat="server" Height="60px"
                        CssClass="form-control" BorderStyle="None" ReadOnly="True" Font-Names="Arial" Font-Size="Small" BackColor="White" BorderColor="White" BorderWidth="0px" Width="700px" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>