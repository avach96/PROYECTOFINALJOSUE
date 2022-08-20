<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="InicioPF.aspx.cs" Inherits="PROYECTOFINAL.InicioPF" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
<link href="css/Inicio.css" rel="stylesheet" />
</html>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
        <div class="auto-style1">
            <strong>BIENVENIDO A EL PRESUPUESTO FAMILIAR DE LA FAMILIA JOSUE CHAVARRIA CASTILLO<br />
            <br />
            <br />
            <span class="auto-style2">Seleccione el tipo de tramite que desea realizar:<br />
            <br />
            </span>
            <asp:Button ID="Bregistrarp" runat="server" BackColor="#079A86" BorderColor="Black" ForeColor="White" Height="61px" OnClick="Bregistrarp_Click" Text="Registar" Width="155px" />
&nbsp;
            <asp:Button ID="Breporte" runat="server" BackColor="#429C90" BorderColor="Black" ForeColor="White" Height="61px" OnClick="Breporte_Click" Text="Reportes" Width="154px" />
            </strong>
        </div>

</asp:Content>
