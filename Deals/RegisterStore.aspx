<%@ Page Title="Register Your Store!" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="RegisterStore.aspx.cs" Inherits="_Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %></h1>
                
            </hgroup>
           
        </div>
    </section>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <p>
        &nbsp;</p>
    <p>
        <asp:Label ID="Label1" runat="server" AssociatedControlID="Name">Name</asp:Label>
        <asp:TextBox ID="Name" runat="server" ></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="Name"
                                    CssClass="field-validation-error" ErrorMessage="The Name field is required." />
    </p>
    <p>
        <asp:Label ID="Label2" runat="server" AssociatedControlID="Address">Address</asp:Label>
        <asp:TextBox ID="Address" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="Address"
                                    CssClass="field-validation-error" ErrorMessage="The  Address field is required." />
        <asp:Label ID="AddressError" runat="server" Text="" CssClass="field-validation-error"></asp:Label>
    </p>
    <p>
        <asp:Label ID="Label3" runat="server" AssociatedControlID="Phone">Phone</asp:Label>
        <asp:TextBox ID="Phone" runat="server"></asp:TextBox>

    </p>
    <p>
        <asp:Label ID="Label4" runat="server" AssociatedControlID="Description">Description</asp:Label>
        <asp:TextBox ID="Description" runat="server"></asp:TextBox>
    </p>

    <asp:Button ID="Button1" runat="server" Text="Register" OnClick="Button1_Click" />
</asp:Content>