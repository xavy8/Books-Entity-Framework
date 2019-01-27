<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Entity.aspx.cs" Inherits="EntityPractice.Entity" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" integrity="sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO" crossorigin="anonymous">
    <title></title>
    <script src="js/jquery-3.1.0.min.js" ></script>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.10.1/jquery.min.js"></script>
    <style>
        #gvEntity
        {
            table-layout: fixed;
        }
        #gvEntity tr:not(:first-child)
        {
            display: block;            
        }
        #gvEntity tr:first-child
        {
            width: 600px;
            position: absolute;            
        }
        #gvEntity tr:nth-child(2)
        {
            margin-top: 40px;
        }
        .mayus
        {
            text-transform:uppercase;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" >
        <h2 style="color:#00562f;text-align:center"></h2>
        <hr />
        <div>
            <center>
            <table style="text-align:center">
                <tr>
                    <td>
                        <asp:Label ID="lblTitulo" runat="server" Text="Título" ></asp:Label><br />
                        <asp:TextBox ID="txtTitulo" runat="server" PlaceHolder="Título" CssClass="form-control mayus" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblAutor" runat="server" Text="Autor"></asp:Label><br />
                        <asp:TextBox ID="txtAutor" runat="server" PlaceHolder="Autor" CssClass="form-control mayus"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblGenero" runat="server" Text="Genero"></asp:Label><br />
                        <asp:TextBox ID="txtGenero" runat="server" PlaceHolder="Genero" CssClass="form-control mayus"></asp:TextBox>
                    </td>
                </tr>
            </table>
            </center>
            <hr />
        </div>
        <div style="text-align:center">
            <span style="margin:2%"><asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-outline-success" OnClick="btnAgregar_Click" /></span>
            <span style="margin:2%"><asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="btn btn-outline-info" OnClick="btnNuevo_Click" /></span>
            <span style="margin:2%"><asp:Button ID="btnEliminar" runat="server" Text="Eliminar" Enabled="false" CssClass="btn btn-outline-danger" OnClick="btnEliminar_Click" /></span>
            <span style="margin:2%"><asp:DropDownList ID="cmbFiltro" runat="server" AutoPostBack="true" CssClass="btn btn-outline-secondary" OnSelectedIndexChanged="cmbFiltro_SelectedIndexChanged"></asp:DropDownList></span>
            <hr />
        </div>
        <div style="overflow:auto;height:400px;text-align:center">
            <center>
            <asp:GridView ID="gvEntity" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="gvEntity_SelectedIndexChanged" OnRowDataBound="gvEntity_RowDataBound" >
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" HeaderStyle-Width="50px" HeaderStyle-Height="40px" ItemStyle-Width="50px" />
                    <asp:BoundField DataField="Titulo" HeaderText="Título" HeaderStyle-Width="250px" HeaderStyle-Height="40px" ItemStyle-Width="250px" />
                    <asp:BoundField DataField="Autor" HeaderText="Autor" HeaderStyle-Width="150px" HeaderStyle-Height="40px" ItemStyle-Width="150px" />
                    <asp:BoundField DataField="Genero" HeaderText="Genero" HeaderStyle-Width="150px" HeaderStyle-Height="40px" ItemStyle-Width="150px" />
                </Columns>
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                <SortedAscendingHeaderStyle BackColor="#246B61" />
                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                <SortedDescendingHeaderStyle BackColor="#15524A" />
            </asp:GridView>
            </center>
        </div>
    </form>
</body>
    <script>
        $('#cmbFiltro').change(function () {
            
            var filtro = $('#cmbFiltro').val();
            
            $.ajax({
                type: "POST",
                url: "Entity.aspx/Asignar",
                data: '{filtro:"' + filtro + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                },
                failure: function (response) {
                }
            });
        });        
    </script>
</html>
