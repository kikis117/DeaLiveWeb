<%@ Page Title="Register a new Deal!" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="RegisterDeal.aspx.cs" Inherits="_Default" %>

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

    
       

        <asp:RadioButtonList ID="DType" runat="server" TextAlign="Left" >
            <asp:ListItem Selected="True" Value="1" >Coffee</asp:ListItem>            
            <asp:ListItem Value="2" >Food</asp:ListItem>
            <asp:ListItem Value="3">Drink</asp:ListItem>
        </asp:RadioButtonList>


        
        <asp:Label ID="Error" runat="server" CssClass="field-validation-error"></asp:Label>


        
    <p>
        <asp:Label ID="Label1" runat="server" AssociatedControlID="Name">Name</asp:Label>
        <asp:TextBox ID="Name" runat="server" ></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="Name"
                                    CssClass="field-validation-error" ErrorMessage="The Name field is required." />
        &nbsp;</p>
        
      
        
    <p>
        <asp:Label ID="Label4" runat="server" AssociatedControlID="Price">Price</asp:Label>
        <asp:TextBox ID="Price" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Price"
                                    CssClass="field-validation-error" ErrorMessage="The Price field is required." />
        <asp:RegularExpressionValidator ID="vldNumber" ControlToValidate="Price"
        Display="Dynamic" ErrorMessage="Must Be A Number" ValidationExpression="(^([0-9]*\d*\d{1}?\d*)$)" Runat="server" CssClass="field-validation-error"/>
    </p>
    <p>
        <asp:Label ID="Label5" runat="server" AssociatedControlID="Place">Place</asp:Label>
        <asp:TextBox ID="Place" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Place"
                                    CssClass="field-validation-error" ErrorMessage="The Place field is required." />
    </p>

    <asp:Button ID="Button1" runat="server" Text="Register" OnClick="Button1_Click" />
</asp:Content>