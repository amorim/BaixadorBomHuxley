<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BaixadorBom.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Bem-vindo ao super master baixador de casos de teste do thehuxley.com</h1>
        <h2>Esse site não é oficialmente endorsado por ninguém, muito menos pelo tio Nelson, mais conhecido como o senhor que abre a ufal</h2>
        <h2>Se esse site não funcionar, culpe o thehuxley. Ele tá caindo diariamente se vocês não perceberam</h2>
        <h2>Eu, como folgado que sou, desenvolvi essa merda aqui em 15 minutos. Então, não há multi-threading, não há progresso, não há nada. Clicou no botão, espera um tempo. Se não vier, não veio.</h2>
        <br />
        <hr />
        <br />
        <h3>Então eu vou precisar de algumas coisas: o número do problema e suas credenciais do huxley. Não se preocupe, novamente, eu não sei se você percebeu, mas ali em cima tem um cadeado e está escrito HTTPS, então a conexão aqui é segura. Insira aqui em baixo e eu te devolvo os casos de teste:</h3>
        <asp:TextBox ID="TextBox1" placeholder="Usuário/email do hux" runat="server"></asp:TextBox><br /><br />
        <asp:TextBox ID="TextBox2" placeholder="Senha do hux"  runat="server" TextMode="Password"></asp:TextBox><br /><br />
        <asp:TextBox ID="TextBox3" placeholder="Número do problema" runat="server"></asp:TextBox><br /><br />
        <asp:Button ID="Button1" runat="server" Text="Puxa os casos" OnClick="Button1_Click" /><br /><br /><hr />
        <asp:Panel ID="Panel1" runat="server"></asp:Panel>    
    </div>
    </form>
</body>
</html>
