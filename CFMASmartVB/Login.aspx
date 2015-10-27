<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="CFMASmartVB.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

</head>
<body>
    <form id="form1" runat="server">
    <div id="LogoHeader">
    <img src="Sliki/Logo.jpg"/>
    </div>
     <div id="Content">
     <div id="NaslovMaska"><asp:Label ID="LblNaslovMaska" runat="server" Text="Label"></asp:Label></div>
     <div id="LoginDiv">
     <table>        
     <tr>   
            <td><asp:Label ID="lblKorisnikLogin" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
          <td><asp:TextBox ID="txtBoxKorisnik" runat="server"></asp:TextBox></td>
     </tr>
     <tr>
          <td><asp:Label ID="lblLozinkaLogin" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>   
          <td><asp:TextBox ID="txtBoxLozinka" TextMode="Password"  runat="server"></asp:TextBox></td>
     </tr>
     <tr>
           <td colspan="2"><asp:Label ID="lblPorakaLogin" runat="server" Text=""></asp:Label> </td>
     </tr>
     <tr>
           <td colspan="2"><asp:Button ID="btnLoginPotvrdi" runat="server" Text="Button" /></td>   
     </tr>
     </table>
     </div>

     </div>
    </form>
</body>
</html>
