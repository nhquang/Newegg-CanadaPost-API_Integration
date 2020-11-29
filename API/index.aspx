<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="API.index" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>API</title>
</head>
<body>
    <div>
        
        <form id="form1" runat="server">
            <div>
                <h1>Newegg Order Info</h1>
                <asp:TextBox runat="server" ID="orderNo" ></asp:TextBox>
                <asp:Button runat="server" ID="getorder" Text="Submit" OnClick="submit_Click"/>
                <asp:Label ID="error" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>
            <div>
                <h1>CanadaPost Get Rates</h1>
                <asp:Button ID="getrates" runat="server" Text="Get Rates" OnClick="getrates_Click" />
            </div>
        </form>
        <div>
            <asp:Table ID="table" 
                runat="server" 
                Font-Size="X-Large" 
                Width="800" 
                Font-Names="Arial"
            
                BorderColor="Black"
            
                ForeColor="Black"
                CellPadding="5"
                CellSpacing="5"
                >
                <asp:TableHeaderRow 
                    runat="server" 
                    ForeColor="Black"
               
                    Font-Bold="true"
                    >
                    <asp:TableHeaderCell HorizontalAlign="Left">OrderNo</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Left">Name</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Left">Address</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Left">ZIP Code</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Left">Country Code</asp:TableHeaderCell>

                </asp:TableHeaderRow>
            </asp:Table>
        </div>
        <div>
            <asp:Table ID="table2" 
                runat="server" 
                Font-Size="X-Large" 
                Width="800" 
                Font-Names="Arial"
            
                BorderColor="Black"
            
                ForeColor="Black"
                CellPadding="5"
                CellSpacing="5"
                >
                <asp:TableHeaderRow 
                    runat="server" 
                    ForeColor="Black"
               
                    Font-Bold="true"
                    >
                    <asp:TableHeaderCell HorizontalAlign="Left">Service</asp:TableHeaderCell>
                    
                    <asp:TableHeaderCell HorizontalAlign="Left">Transit Day</asp:TableHeaderCell>
                    
                    <asp:TableHeaderCell HorizontalAlign="Left">Regular Price</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </div>
    </div>
</body>
</html>
