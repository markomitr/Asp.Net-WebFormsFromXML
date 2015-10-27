<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="DatumIzbor.ascx.vb" Inherits="CFMASmartVB.DatumIzbor" %>


<div id="DatumPicker">
<table width="95%" cellpadding="0" cellspacing="0" border="0">
<tr>
    <td><asp:Label ID="LabelDen" runat="server" Text=""></asp:Label></td>
    <td><asp:Label ID="LabelMesec" runat="server" Text=""></asp:Label></td>
    <td><asp:Label ID="LabelGodina" runat="server" Text=""></asp:Label></td>
</tr>
<tr>
    <td><asp:TextBox nasID="Den" ID="Den" runat="server" AutoComplete="off" CssClass="datum" onkeyup="proverkaDatum(event,this,'')"></asp:TextBox></td>
    <td><asp:TextBox nasID="Mesec" ID="Mesec" runat="server" AutoComplete="off"  CssClass="datum" onkeyup="proverkaDatum(event,this,'')"></asp:TextBox></td>
    <td><asp:TextBox nasID="Godina" ID="Godina" runat="server" AutoComplete="off"  CssClass="datum" onkeyup="proverkaDatum(event,this,'')"></asp:TextBox></td>

</tr>

</table>

</div>