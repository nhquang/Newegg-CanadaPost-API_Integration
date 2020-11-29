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
                <asp:TextBox runat="server" ID="orderNo" placeholder="Enter order number"></asp:TextBox>
                <asp:Button runat="server" ID="getorder" Text="Submit" OnClick="submit_Click"/>
                <asp:Label ID="error" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>
            <div>
                <h1>CanadaPost Get Rates</h1>
                <asp:Label ID="originLbl" runat="server" Text="Origin postal code"></asp:Label>
                <asp:TextBox runat="server" ID="origin" placeholder="Enter postal code"></asp:TextBox>
                <br />
                <asp:Label ID="destinLbl" runat="server" Text="Destination postal code"></asp:Label>
                <asp:TextBox runat="server" ID="des" placeholder="Enter postal code"></asp:TextBox>
                <br />
                <asp:Label ID="weightLbl" runat="server" Text="Weight(kilograms)"></asp:Label>
                <asp:TextBox runat="server" ID="weight" placeholder="1" TextMode="Number"></asp:TextBox>
                <br />
                <asp:Button ID="getrates" runat="server" Text="Get Rates" OnClick="getrates_Click"/>
                <asp:Label ID="error2" runat="server" Text="" ForeColor="Red"></asp:Label>
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
                    
                    <asp:TableHeaderCell HorizontalAlign="Left">Transit Days</asp:TableHeaderCell>
                    
                    <asp:TableHeaderCell HorizontalAlign="Left">Regular Price</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </div>
    </div>
</body>
</html>
