<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>


        <style>
        html { background: #000033}
        
        header
        {
            text-align: center;
            color: #e6e6e6;
        }
        
        div
        {
            font-family: sans-serif;
            display: none;
            width: 300px;
            margin: 0 auto;
            padding: 20px;
            background: white;
            border-radius: 10px;
        }

        .output
        {
            text-align: center;
            color: #e6e6e6;
        }
    </style>
    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    
    <script>
        $(document).ready(function() { 
            $("#btn1").click(function() { 
                $("#formTeam").show();
                $("#joinTeam").hide(); 
            });
            
            $("#btn2").click(function() { 
                $("#joinTeam").show();
                $("#formTeam").hide(); 
            }); 
        });
    </script>

</head>
<body>

    <header>
        <h1>Welcome to Registration</h1>
        <p><button id="btn1">Create New Team</button> &ensp; or &ensp; <button id="btn2">Join Existing One</button></p>

    </header>

    <form id="form1" runat="server">
        <div id="formTeam">
            <p>First Name: &ensp; <asp:TextBox ID="firstName1" runat="server"></asp:TextBox></p>
            <p>Last Name: &ensp; <asp:TextBox ID="lastName1" runat="server"></asp:TextBox></p>
            <p>Team Name: &ensp; <asp:TextBox ID="teamName1" runat="server"></asp:TextBox></p>
            <p>
                Flag Color: &ensp;
                <asp:DropDownList ID="teamColor" runat="server">
                    <asp:ListItem Value="Default" Selected="True">--Selected--</asp:ListItem>
                    <asp:ListItem Value="Red">Red</asp:ListItem>
                    <asp:ListItem Value="Green">Green</asp:ListItem>
                    <asp:ListItem Value="Blue">Blue</asp:ListItem>
                    <asp:ListItem Value="Orange">Orange</asp:ListItem>
                    <asp:ListItem Value="Yellow">Yellow</asp:ListItem>
                    <asp:ListItem Value="Purple">Purple</asp:ListItem>
                </asp:DropDownList>
            </p>

            <asp:Button ID="ftButton" runat="server" Text="Submit" OnClick="ftButton_Click" />
        </div>

        <div id="joinTeam">
            <p>First Name: &ensp; <asp:TextBox ID="firstName2" runat="server"></asp:TextBox></p>
            <p>Last Name: &ensp; <asp:TextBox ID="lastName2" runat="server"></asp:TextBox></p>
            <p>Team Name: &ensp; <asp:TextBox ID="teamName2" runat="server"></asp:TextBox></p>

            <asp:Button ID="jtButton" runat="server" Text="Submit" OnClick="jtButton_Click" />
        </div>

        <asp:Label ID="output" CssClass="output" runat="server" Text=""></asp:Label>
    </form>
</body>
</html>
