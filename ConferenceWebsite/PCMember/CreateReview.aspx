<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateReview.aspx.cs" Inherits="ConferenceWebsite.PCMember.CreateReview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #800000" class="h4"><strong>Create Review</strong></span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small" Visible="False"></asp:Label>
        <!-- Display of submission information -->
        <asp:Panel ID="pnlSubmission" runat="server" Visible="False">
            <hr />
            <div class="form-group">
                <asp:Label runat="server" Text="Title:" AssociatedControlID="txtTitle" CssClass="control-label col-xs-1" Font-Names="Arial"></asp:Label>
                <div class="col-xs-11">
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" BorderStyle="None" ReadOnly="True" 
                        BackColor="White" BorderColor="White" Font-Names="Arial" Font-Size="Small" Width="600px"></asp:TextBox>
                </div>
            </div>
        </asp:Panel>
        <!-- Display of submission authors -->
        <asp:Panel ID="pnlAuthors" runat="server" Visible="False">
            <div class="form-group">
                <asp:Label runat="server" Text="Author(s):" AssociatedControlID="txtAuthor" CssClass="control-label col-xs-1" Font-Names="Arial"></asp:Label>
                <div class="col-xs-11">
                    <asp:TextBox ID="txtAuthor" runat="server" CssClass="form-control" BorderStyle="None" ReadOnly="True" 
                        BackColor="White" BorderColor="White" Font-Names="Arial" Font-Size="Small" Width="600px"></asp:TextBox>
                </div>
            </div>
        </asp:Panel>
        <!-- Review input controls -->
        <asp:Panel ID="pnlReview" runat="server" Visible="False">
            <div class="form-group">
                <asp:Label runat="server" Text="Use (Y)es or(N)o and if absolutely necessary (M)aybe to answer the following<br/>(you must explain (N)o and (M)aybe answers in the comments section)." 
                    CssClass="col-xs-12" Font-Bold="True" Font-Names="Arial"></asp:Label>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="The&nbsp;paper&nbsp;is&nbsp;relevant&nbsp;to&nbsp;the&nbsp;conference:" 
                    AssociatedControlID="ddlRelevant" CssClass="control-label-left col-xs-4" Font-Names="Arial"></asp:Label>
                <div class="col-xs-1">
                    <asp:DropDownList ID="ddlRelevant" runat="server" CssClass="dropdown" Font-Names="Arial" Font-Size="Small">
                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                        <asp:ListItem Value="N">No</asp:ListItem>
                        <asp:ListItem Value="M">Maybe</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <asp:Label runat="server" Text="The&nbsp;paper&nbsp;is&nbsp;technically&nbsp;correct:" AssociatedControlID="ddlTechnicallyCorrect"
                    CssClass="control-label-left col-xs-3" Font-Names="Arial"></asp:Label>
                <div class="col-xs-1">
                    <asp:DropDownList ID="ddlTechnicallyCorrect" runat="server" CssClass="dropdown" Font-Names="Arial" Font-Size="Small">
                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                        <asp:ListItem Value="N">No</asp:ListItem>
                        <asp:ListItem Value="M">Maybe</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="The&nbsp;length&nbsp;and&nbsp;content&nbsp;of&nbsp;the&nbsp;paper&nbsp;are&nbsp;comparable&nbsp;to&nbsp;the&nbsp;expected&nbsp;final&nbsp;version:" 
                    AssociatedControlID="ddlLengthAndContent" CssClass="control-label-left col-xs-7" Font-Names="Arial"></asp:Label>
                <div class="col-xs-1">
                    <asp:DropDownList ID="ddlLengthAndContent" runat="server" CssClass="dropdown" Font-Names="Arial" Font-Size="Small">
                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                        <asp:ListItem Value="N">No</asp:ListItem>
                        <asp:ListItem Value="M">Maybe</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Reviewer&nbsp;Confidence&nbsp;(0.5-1):" AssociatedControlID="ddlConfidence" CssClass="control-label-left col-xs-3" Font-Names="Arial"></asp:Label>
                <div class="col-xs-1">
                    <asp:DropDownList ID="ddlConfidence" runat="server" CssClass="dropdown" Font-Size="Small">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>.9</asp:ListItem>
                        <asp:ListItem>.8</asp:ListItem>
                        <asp:ListItem>.7</asp:ListItem>
                        <asp:ListItem>.6</asp:ListItem>
                        <asp:ListItem>.5</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="For the following categories, please assign integer scores from 1 to 5." CssClass="col-xs-12" Font-Bold="True" Font-Names="Arial"></asp:Label>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Originality" AssociatedControlID="ddlOriginality" CssClass="control-label col-xs-2" Style="text-align: center" Font-Names="Arial"></asp:Label>
                <asp:Label runat="server" Text="Impact" AssociatedControlID="ddlImpact" CssClass="control-label col-xs-2" Style="text-align: center" Font-Names="Arial"></asp:Label>
                <asp:Label runat="server" Text="Presentation" AssociatedControlID="ddlPresentation" CssClass="control-label col-xs-2" Style="text-align: center" Font-Names="Arial"></asp:Label>
                <asp:Label runat="server" Text="Technical Depth" AssociatedControlID="ddlTechnicalDepth" CssClass="control-label col-xs-2" Style="text-align: center" Font-Names="Arial"></asp:Label>
                <asp:Label runat="server" Text="OVERALL RATING" AssociatedControlID="ddlOverallRating" CssClass="control-label col-xs-2" Style="text-align: center" Font-Names="Arial"></asp:Label>
            </div>
            <div class="form-group">
                <div class="col-xs-2">
                    <asp:DropDownList ID="ddlOriginality" runat="server" CssClass="dropdown" Font-Names="Arial" Font-Size="Small">
                        <asp:ListItem Value="1">1 - Reject</asp:ListItem>
                        <asp:ListItem Value="2">2 - Weak Reject</asp:ListItem>
                        <asp:ListItem Value="3">3 - Neutral</asp:ListItem>
                        <asp:ListItem Value="4">4 - Weak Accept</asp:ListItem>
                        <asp:ListItem Value="5">5 - Accept</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-2">
                    <asp:DropDownList ID="ddlImpact" runat="server" CssClass="dropdown" Font-Names="Arial" Font-Size="Small">
                        <asp:ListItem Value="1">1 - Reject</asp:ListItem>
                        <asp:ListItem Value="2">2 - Weak Reject</asp:ListItem>
                        <asp:ListItem Value="3">3 - Neutral</asp:ListItem>
                        <asp:ListItem Value="4">4 - Weak Accept</asp:ListItem>
                        <asp:ListItem Value="5">5 - Accept</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-2">
                    <asp:DropDownList ID="ddlPresentation" runat="server" CssClass="dropdown" Font-Names="Arial" Font-Size="Small">
                        <asp:ListItem Value="1">1 - Reject</asp:ListItem>
                        <asp:ListItem Value="2">2 - Weak Reject</asp:ListItem>
                        <asp:ListItem Value="3">3 - Neutral</asp:ListItem>
                        <asp:ListItem Value="4">4 - Weak Accept</asp:ListItem>
                        <asp:ListItem Value="5">5 - Accept</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-2">
                    <asp:DropDownList ID="ddlTechnicalDepth" runat="server" CssClass="dropdown" Font-Names="Arial" Font-Size="Small">
                        <asp:ListItem Value="1">1 - Reject</asp:ListItem>
                        <asp:ListItem Value="2">2 - Weak Reject</asp:ListItem>
                        <asp:ListItem Value="3">3 - Neutral</asp:ListItem>
                        <asp:ListItem Value="4">4 - Weak Accept</asp:ListItem>
                        <asp:ListItem Value="5">5 - Accept</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-2">
                    <asp:DropDownList ID="ddlOverallRating" runat="server" CssClass="dropdown" Font-Names="Arial" Font-Size="Small">
                        <asp:ListItem Value="1">1 - Reject</asp:ListItem>
                        <asp:ListItem Value="2">2 - Weak Reject</asp:ListItem>
                        <asp:ListItem Value="3">3 - Neutral</asp:ListItem>
                        <asp:ListItem Value="4">4 - Weak Accept</asp:ListItem>
                        <asp:ListItem Value="5">5 - Accept</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Main Contribution(s):" AssociatedControlID="txtMainContributions" 
                    CssClass="control-label-left col-xs-2" Font-Names="Arial"></asp:Label>
                <div class="col-xs-10">
                    <asp:TextBox ID="txtMainContributions" runat="server" Height="50px" MaxLength="300" TextMode="MultiLine" 
                        CssClass="form-control input-sm" Width="600px" Font-Names="Arial"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMainContributions" EnableClientScript="False" 
                        ErrorMessage="The main contributions is required." CssClass="text-danger" Display="Dynamic" Font-Names="Arial" 
                        Font-Size="Small"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Three strong points of the paper:" AssociatedControlID="txtStrongPoints" 
                    CssClass="control-label-left col-xs-2" Font-Names="Arial"></asp:Label>
                <div class="col-xs-10">
                    <asp:TextBox ID="txtStrongPoints" runat="server" Height="50px" MaxLength="300" TextMode="MultiLine" 
                        CssClass="form-control input-sm" Width="600px" Font-Names="Arial"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtStrongPoints" EnableClientScript="False" 
                        ErrorMessage="Three strong points is required." CssClass="text-danger" Display="Dynamic" Font-Names="Arial" 
                        Font-Size="Small"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Three weak points of the paper:" AssociatedControlID="txtWeakPoints" 
                    CssClass="control-label-left col-xs-2" Font-Names="Arial"></asp:Label>
                <div class="col-xs-10">
                    <asp:TextBox ID="txtWeakPoints" runat="server" Height="50px" MaxLength="300" TextMode="MultiLine" 
                        CssClass="form-control input-sm" Width="600px" Font-Names="Arial"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtWeakPoints" EnableClientScript="False" 
                        ErrorMessage="Three weak points is required." CssClass="text-danger" Display="Dynamic" Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Overall Summary:" AssociatedControlID="txtOverallSummary" 
                    CssClass="control-label-left col-xs-2" Font-Names="Arial"></asp:Label>
                <div class="col-xs-10">
                    <asp:TextBox ID="txtOverallSummary" runat="server" Height="50px" MaxLength="300" TextMode="MultiLine" 
                        CssClass="form-control input-sm" Width="600px" Font-Names="Arial"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtOverallSummary" EnableClientScript="False" 
                        ErrorMessage="The overall summary is required." CssClass="text-danger" Display="Dynamic" Font-Names="Arial" 
                        Font-Size="Small"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Detailed Comments:" AssociatedControlID="txtDetailedComments" 
                    CssClass="control-label-left col-xs-2" Font-Names="Arial"></asp:Label>
                <div class="col-xs-10">
                    <asp:TextBox ID="txtDetailedComments" runat="server" Height="50px" MaxLength="1000" TextMode="MultiLine" 
                        CssClass="form-control input-sm" Width="600px" Font-Names="Arial"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Confidential comments:" AssociatedControlID="txtConfidentialComments" 
                    CssClass="control-label-left col-xs-2" Font-Names="Arial"></asp:Label>
                <div class="col-xs-10">
                    <asp:TextBox ID="txtConfidentialComments" runat="server" Height="50px" MaxLength="300" TextMode="MultiLine" 
                        CssClass="form-control input-sm" Width="600px" Font-Names="Arial"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <div class="col-xs-2">
                    <asp:Button ID="btnCreatetReview" runat="server" OnClick="BtnCreateReview_Click" Text="Create Review" 
                        CssClass="btn-sm" Font-Names="Arial"></asp:Button>
                </div>
                <div class="col-xs-10">
                    <asp:Label ID="lblCreateResultMessage" runat="server" Text="Label" Visible="false"></asp:Label>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>