<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userProblems.aspx.cs" Inherits="BaixadorBom.userProblems" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PUXA WRONG_ANSWER</title>
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"/>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Olá, bem vindo. O hux fez uma sacanagem na reavaliação e todos perdemos muitos pontos por isso. Essa ferramenta visa facilitar a recuperação desses pontos. Insira o seu ID de usuário e te mostraremos os problemas que estão avaliados como incorretos.</h1>
        <br />
        <br />
        <h2>Você pode encontrar seu ID de usuário acessando o seu perfil e verificando o número que está na URL do browser. Dependendo da quantidade de problemas que você resolveu, essa operação pode levar vários segundos.</h2>
        <br />
        <asp:TextBox ID="usuID" placeholder="ID do Usuário" runat="server"></asp:TextBox> <asp:Button ID="Button1" OnClick="Button1_Click" runat="server" Text="Puxa" />
        <br />
        <hr />
        <br />
        <asp:Panel ID="Panel1" runat="server"></asp:Panel>
    </div>
    </form>
</body>
</html>
