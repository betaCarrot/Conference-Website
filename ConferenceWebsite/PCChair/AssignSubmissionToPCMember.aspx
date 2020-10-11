<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AssignSubmissionToPCMember.aspx.cs" Inherits="ConferenceWebsite.PCChair.AssignSubmissionToPCMember" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #800000" class="h4"><strong>Assign Submission To PC Member</strong></span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small" Visible="False"></asp:Label>
        <!-- Select submission input -->
        <asp:Panel ID="pnlSelectSubmission" runat="server">
            <hr />
            <div class="form-group">
                <asp:Label runat="server" Text="Submission:" CssClass="control-label col-xs-2" AssociatedControlID="ddlSubmissionNumbers" Font-Names="Arial"></asp:Label>
                <div class="col-xs-1">
                    <asp:DropDownList ID="ddlSubmissionNumbers" runat="server" OnSelectedIndexChanged="DdlSubmission_SelectedIndexChanged"
                        AutoPostBack="True" Font-Names="Arial" Font-Size="Small" CausesValidation="True">
                    </asp:DropDownList>
                </div>
                <div class="col-xs-9">
                    <asp:Label ID="lblTitle" runat="server" Visible="False" Font-Names="Arial" Font-Size="Small"></asp:Label>
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="Please select a submission." ControlToValidate="ddlSubmissionNumbers" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" InitialValue="none selected" ForeColor="Blue" Font-Names="Arial Narrow" Font-Size="Small" Font-Bold="True"></asp:RequiredFieldValidator>
                </div>
                <div class="col-xs-offset-2 col-xs-10">
                </div>
            </div>
        </asp:Panel>
        <!-- PC Members currently assigned display -->
        <asp:Panel ID="pnlCurrentlyAssigned" runat="server">
            <hr />
            <h5><span style="text-decoration: underline; color: #800000" class="h5"><strong>PC Members Assigned To This Submission:</strong></span></h5>
            <asp:Label ID="lblCurrentlyAssignedResult" runat="server" CssClass="label" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small" Visible="False"></asp:Label>
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:GridView ID="gvCurrentlyAssigned" runat="server" CssClass="table-condensed" BorderStyle="Solid" CellPadding="0"
                        OnRowDataBound="GvCurrentlyAssigned_RowDataBound" Font-Names="Arial" Font-Size="Small">
                        <RowStyle Wrap="False" />
                    </asp:GridView>
                </div>
            </div>
            <!-- PC Members available for assignment display -->
            <hr />
            <h5><span style="text-decoration: underline; color: #800000" class="h5"><strong>PC Members Available To Be Assigned To This Submission:</strong></span></h5>
            <div class="form-group">
                <asp:Label runat="server" Text="Select minimum preference:" CssClass="control-label col-xs-3"
                    AssociatedControlID="ddlMinimumPreference" Font-Names="Arial"></asp:Label>
                <div class="col-xs-1">
                    <asp:DropDownList ID="ddlMinimumPreference" runat="server"
                        OnSelectedIndexChanged="DdlMinimumPreference_SelectedIndexChanged" AutoPostBack="True" CssClass="dropdown" Font-Names="Arial" Font-Size="Small" 
                        CausesValidation="True">
                        <asp:ListItem Value="none selected">Select</asp:ListItem>
                        <asp:ListItem Value="None">None</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-8">
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="Please select a minimum preference."
                        ControlToValidate="ddlMinimumPreference" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"
                        InitialValue="none selected" ID="rfvDdlMinumumPreference" ForeColor="Blue" Font-Names="Arial Narrow" Font-Size="Small" Font-Bold="True"></asp:RequiredFieldValidator>
                </div>
            </div>
            <asp:Label ID="lblAvailableForAssignmentResult" runat="server" CssClass="label" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small" Visible="False"></asp:Label>
        </asp:Panel>
        <asp:Panel ID="pnlAvailableForAssignment" runat="server">
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:GridView ID="gvAvailableForAssignment" runat="server" CssClass="table-condensed" BorderStyle="Solid" CellPadding="0"
                        OnRowDataBound="GvAvailableForAssignment_RowDataBound" Font-Names="Arial" Font-Size="Small">
                        <Columns>
                            <asp:TemplateField HeaderText="SELECT">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chkSelected" runat="server" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelected" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle Wrap="False" />
                    </asp:GridView>
                </div>
            </div>
            <div class="form-group">
                <div class="col-xs-3">
                    <asp:Button ID="btnAssignPCMember" runat="server" Text="Assign Selected PC Members" CssClass="btn-sm"
                        OnClick="BtnAssignPCMember_Click" Font-Names="Arial" />
                </div>
                <div class="col-xs-9">
                    <asp:Label ID="lblAssignmentResult" runat="server" CssClass="label" Font-Bold="True" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>