using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BaixadorBom
{
    public partial class userProblems : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int id = 0, offset = 0;
            if (!int.TryParse(usuID.Text, out id))
                return;
            List<Problem> list = new List<Problem>();
            while (true)
            {
                string url = "https://www.thehuxley.com/api/v1/users/" + id + "/problems?filter=attempted&max=100&offset=@offset&order=asc".Replace("@offset", offset.ToString());
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url.ToString());
                request.Method = "GET";
                HttpWebResponse webResponsee = (HttpWebResponse)request.GetResponse();
                Stream responseStreame = webResponsee.GetResponseStream();
                StreamReader responseStreamReadere = new StreamReader(responseStreame);
                string jsonhux = responseStreamReadere.ReadToEnd();
                dynamic tudo = JArray.Parse(jsonhux);
                if (tudo.Count == 0)
                    break;
                for (int i = 0; i < tudo.Count; i++)
                    if (tudo[i].currentUser.status != "CORRECT")
                        list.Add(Problem.GetProblem(int.Parse(tudo[i].id.ToString()), tudo[i].currentUser.status.ToString()));
                offset += 100;
            }
            string injeção = "";
            injeção += @"<table class=""table""> <thead> <tr> <th>ID do Problema</th> <th>Situação</th> </tr> </thead> <tbody>";
            foreach (Problem p in list)
            {
                injeção += @"<tr><th scope=""row""><a href=""https://www.thehuxley.com/problem/"+ p.id + @""">" + p.id + "</th>";
                injeção += "<td>" + p.eval + "</td></tr>";
            }
            injeção += "</tbody></table>";
            Panel1.Controls.Add(new LiteralControl(injeção));
        }
    }
}