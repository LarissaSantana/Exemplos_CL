using System;

namespace ExemploSubstringIndexOf
{
    class Program
    {
        static void Main(string[] args)
        {

            string tel = @"(11) 97226 - 4481, (11) 955555581, (79) 99996-8444, (79) 994455-8444 </p>";

            if (tel.IndexOf(",") > 0)
            {
                int indexIni1 = tel.IndexOf(tel);
                int indexFim1 = tel.IndexOf(",");
                string tel1 = tel.Substring(indexIni1, (indexFim1 - indexIni1)).Trim();
                // Console.WriteLine(tel1);
                int indexFim2 = tel.IndexOf("</p>");
                string tel2 = tel.Substring((indexFim1 + 1), (indexFim2 - (indexFim1 + 1))).Trim();
                // Console.WriteLine(tel2);
            }
            string TratarTelefone(string tell)
            {
                tell = tell.Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "").Trim();
                if (tell.Length > 14) //Telefone inválido
                    tell = "";
                return tell;
            }
            string TratarMultiplosTelefones(string numero)
            {
                int pos = numero.IndexOf(",");
                int index = numero.IndexOf(numero);
                string telAux = "";
                for (int i = 0; i <= numero.Length; i++)
                {
                    if (pos > 0)
                    {
                        string aux = TratarTelefone(numero.Substring(index, (pos - index)));
                        telAux = telAux + aux;
                    }
                    else { break; }
                    index = pos;
                    pos = numero.IndexOf(",", (index + 1));
                    if (pos == -1)
                    {
                        pos = numero.IndexOf("<", (index));
                    }
                }
                return telAux;
            }

            Console.WriteLine(TratarMultiplosTelefones(tel));

            /* Console.WriteLine(@"margin:0" + ";" + "\">");
             Console.WriteLine(@";" + "\">");
             Console.WriteLine(@";" + "\">");
             Exemplo e = new Exemplo();
             string nome = "       Larissa Santana       ";
             Console.WriteLine("Primeiro nome: " + e.RetornarPrimeiroNome(nome));
             Console.WriteLine("Ultimo nome: " + e.RetornarUltimoNome(nome));


             string nome2 = @"<strong>";
             string strong = @"mensagem do cliente:
                            </p>
                            <strong>";

             string indice = @"luisaq<br>";
             string br = "<br>";
             Console.WriteLine("Indice: " + e.RetornaIndice(br, indice));

             string url = @"URL:					<a href=" + "https://avonale.slack.com/" + " style=" + "white-space: nowrap; color: #0576b9;" + "\">";


             Console.WriteLine(e.RetornaIndice(@";" + "\">", url));
             //Console.Write("\"");
             Console.Write("\">");*/
            Console.ReadKey();
        }
    }
    public class Exemplo
    {
        public string RetornarPrimeiroNome(string nome)
        {
            nome = nome.Trim();
            string primeiroNome = nome.Substring(0, nome.IndexOf(' '));
            /* Trim remove espaços em branco */
            return primeiroNome;
        }

        public string RetornarUltimoNome(string nome)
        {
            /* +1 remove o espaço em branco */
            nome = nome.Trim();
            string ultimoNome = nome.Substring(nome.IndexOf(' ') + 1);
            return ultimoNome;
        }

        public int RetornaIndice(string nome)
        {
            nome = nome.Trim();
            /* a partir de qual indice começa o nome */
            int indice = nome.IndexOf(nome);
            return indice;
        }
        public int RetornaIndice(string nome, string nome2)
        {
            nome = nome.Trim();
            /* a partir de qual indice começa o nome */
            int indice = nome.IndexOf(nome);
            /* a partir do indice de nome, qual é o indice de nome2? */
            int ind = nome2.IndexOf(nome, indice);
            return ind;
        }
    }
}
