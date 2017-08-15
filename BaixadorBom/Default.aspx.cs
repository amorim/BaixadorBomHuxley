using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BaixadorBom
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string username = TextBox1.Text;
            string pass = TextBox2.Text;
            string prob = TextBox3.Text;
            string token = DoLogin(username, pass);
            if (token == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Usuario ou senha invalidos/hux caiu');", true);
                return;
            }
           // try
            //{
                string URL = "https://www.thehuxley.com/api/v1/user/problems/" + prob + "/submissions";
                string formBoundary = "--heheheh";
                string payload = "";
                payload += formBoundary + Environment.NewLine;
                payload += @"Content-Disposition: form-data; name=""language""";
                payload += Environment.NewLine + Environment.NewLine;
                payload += "1" + Environment.NewLine + formBoundary + Environment.NewLine;
                payload += @"Content-Disposition: form-data; name=""file""; filename=""testezao.c""";
                payload += Environment.NewLine;
                payload += "Content-Type: text/plain";
                payload += Environment.NewLine + Environment.NewLine;
                payload += "#include <stdio.h>\r\n" +
                                "main() {\r\n" +
                                "    char str[10000];\r\n" +
                                "    int tam = 0;\r\n" +
                                "    char vem;\r\n" +
                                "    while (scanf(\"%c\", &vem) != EOF) {\r\n" +
                                "        if (vem == '\\n')\r\n" +
                                "        vem = '`';\r\n" +
                                "        if (tam > 9998)\r\n" +
                                "        break;\r\n" +
                                "        str[tam++] = vem;\r\n" +
                                "    }\r\n" +
                                "    str[tam] = '\\0';\r\n" +
                                "    printf(\"%s\", str);\r\n" +
                                "} ";
                payload += Environment.NewLine;
                payload += formBoundary + "--";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL.ToString());
                request.Method = "POST";
                request.ContentType = "multipart/form-data; boundary=heheheh";
                request.Headers.Add("Authorization", "Bearer " + token);
                request.Accept = "application/json, text/plain, */*";
                request.Headers.Add("Accept-Language", "pt-BR,pt;q=0.8,en-US;q=0.5,en;q=0.3");
                request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                UTF8Encoding utfenc = new UTF8Encoding();
                byte[] bytes = utfenc.GetBytes(payload);
                Stream os = null;
                request.ContentLength = bytes.Length;
                os = request.GetRequestStream();
                os.Write(bytes, 0, bytes.Length);
                HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();
                Stream responseStream = webResponse.GetResponseStream();
                StreamReader responseStreamReader = new StreamReader(responseStream);
                string json = responseStreamReader.ReadToEnd();
                int tries = 0;
                dynamic tudo;
                while (true)
                {
                    string url = "https://www.thehuxley.com/api/v1/submissions?max=1&order=desc&problem=" + prob + "&sort=submissionDate";
                    HttpWebRequest requestt = (HttpWebRequest)WebRequest.Create(url.ToString());
                    requestt.Method = "GET";
                    requestt.Headers.Add("Authorization", "Bearer " + token);
                    if (tries > 10)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Hux está com o módulo avaliador offline provavelmente');", true);
                        return;
                    }
                    HttpWebResponse webResponsee = (HttpWebResponse)requestt.GetResponse();
                    Stream responseStreame = webResponsee.GetResponseStream();
                    StreamReader responseStreamReadere = new StreamReader(responseStreame);
                    string jsonhux = responseStreamReadere.ReadToEnd();
                    tudo = JArray.Parse(jsonhux);
                    if (tudo[0].evaluation != "WAITING")
                        break;
                    tries++;

                    System.Threading.Thread.Sleep(3000);
                }
                dynamic testes = tudo[0].testCaseEvaluations;
                List<TestCase> list = new List<TestCase>();
                for (int i = 0; i < testes.Count; i++)
                {
                    TestCase tc = new TestCase();
                    string difff = testes[i].diff;
                    if (difff.Equals("null"))
                        continue;
                    dynamic diff = JObject.Parse(difff);
                    dynamic linhas = diff.lines;
                    tc.input = TestCase.deserializeInput(Convert.ToString(linhas[0].actual));
                    string possibleOutput = "";
                    for (int j = 0; j < linhas.Count; j++)
                        possibleOutput += linhas[j].expected + "<br />";
                    tc.output = possibleOutput;
                    tc.id = testes[i].testCaseId;
                    list.Add(tc);
                }
                string liter = "";
                foreach (TestCase t in list)
                {
                    liter += "<b>CASO DE TESTE Nº " + t.id + "</b><br><br>";
                    liter += "<b>ENTRADA</b><br><br>";
                    liter += t.input;
                    liter += "<br><br>";
                    liter += "<b>SAÍDA</b><br><br>";
                    liter += t.output;
                    liter += "<br><hr>";
                }
                Panel1.Controls.Clear();
                Panel1.Controls.Add(new LiteralControl(liter));

           // }
            //catch
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Erro. Se voce inseriu as informacoes corretas, aguarde em torno de 30 segundos e tente novamente');", true);
           // }

        }

        private string DoLogin(string username, string password)
        {
            string url = "https://www.thehuxley.com/api/login";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url.ToString());
            request.Method = "POST";
            request.ContentType = "application/json";
            UTF8Encoding utfenc = new UTF8Encoding();
            byte[] bytes = utfenc.GetBytes(new JavaScriptSerializer().Serialize(new Credential()
            {
                username = username,
                password = password
            }));
            Stream os = null;
            request.ContentLength = bytes.Length;
            os = request.GetRequestStream();
            os.Write(bytes, 0, bytes.Length);
            try
            {
                HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();
                Stream responseStream = webResponse.GetResponseStream();
                StreamReader responseStreamReader = new StreamReader(responseStream);
                string jsonhux = responseStreamReader.ReadToEnd();
                var chaves = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(jsonhux);
                return chaves["access_token"].ToString();
            }
            catch
            {
                return "";
            }
        }
    }
}