<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SubmissionPreferences.aspx.cs" Inherits="ConferenceWebsite.PCMember.SubmissionPreferences" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #800000" class="h4"><strong>Submission Preferences</strong></span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small" Visible="false"></asp:Label>
        <!-- Display of submission for which preferences have been specified -->
        <asp:Panel ID="pnlPreferencesSpecified" runat="server">
            <hr />
            <h5><span style="text-decoration: underline; color: #800000" class="h5"><strong>Submissions For Which You Have Specified A Preference:</strong></span></h5>
            <asp:Label ID="lblResultWithPreferenceMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
            <div><br /></div>
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:GridView ID="gvPreferenceSpecified" runat="server" CssClass="table-condensed"
                        OnRowDataBound="GvPreferenceSpecified_RowDataBound" Font-Names="Arial" Font-Size="Small">
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>
        <!-- Display of submission for which preferences have not been specified -->
        <asp:Panel ID="pnlPreferencesNotSpecified" runat="server">
            <hr />
            <h5><span style="text-decoration: underline; color: #800000" class="h5"><strong>Submissions For Which You Have Not Specified A Preference:</strong></span></h5>
            <asp:Label ID="lblResultWithNoPreferenceMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
            <asp:Panel ID="pnlNoPreferenceSpecifiedResult" runat="server">
                <div class="form-group">
                    <div class="col-xs-12">
                        <asp:GridView ID="gvNoPreferenceSpecified" runat="server" CssClass="table-condensed"
                            OnRowDataBound="GvNoPreferenceSpecified_RowDataBound" Font-Names="Arial" Font-Size="Small">
                            <Columns>
                                <asp:TemplateField HeaderText="PREFERENCE">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlPreference" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-2">
                        <asp:Button ID="btnUpdatePreferences" runat="server" Text="Update Preferences" OnClick="BtnUpdatePreferences_Click" CssClass="btn-sm" Font-Names="Arial" />
                    </div>
                    <div>
                        <asp:Label ID="lblUpdateMessage" runat="server" Font-Bold="True" CssClass="label col-xs-4" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
                    </div>
                </div>
            </asp:Panel>
        </asp:Panel>
    </div>
</asp:Content>