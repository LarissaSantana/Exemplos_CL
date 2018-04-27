using System;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    public class TratamentoEmail
    {
        public static void Main(string[] args)
        {
            Teste teste = new Teste();
            Console.WriteLine("Extrair email: ");
            Console.WriteLine(teste.ExtrairEmail("larissa.santana@connectlead.com.br"));
            Console.WriteLine(teste.ExtrairEmail("larissa.santana@l.com"));

            Console.WriteLine("Clear email base: ");
            Console.WriteLine(teste.plainTextToHtml("teste"));
            Console.WriteLine(teste.ExtrairIdentificador("larissa"));

            Console.WriteLine();


            Console.ReadKey();

        }


        public class Teste
        {
            #region Retorna o e-mail se ele for válido, se for inválido retorna uma string vazia
            /*  Retorna o e-mail se ele for válido, se for inválido retorna uma string vazia */
            public string ExtrairEmail(string value)
            {
                const string MatchEmailPattern =
               @"(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
               + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                 + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
               + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})";
                Regex re = new Regex(MatchEmailPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

                var identifier = re.Match(value);
                if (!string.IsNullOrWhiteSpace(identifier.Value))
                    return identifier.Value;

                return string.Empty;
            }
            #endregion

            public string ClearEmailBase(string body)
            {

                var startIndex = body.IndexOf("<base href=");
                int endIndex;
                while (startIndex > -1)
                {
                    endIndex = body.IndexOf(">", startIndex) - startIndex;
                    body = body.Remove(startIndex, endIndex + 1);
                    startIndex = body.IndexOf("<base href=");
                }
                return body;
            }

            public string ClearEmailScript(string body)
            {
                var startIndex = body.IndexOf("<script");
                int endIndex;
                while (startIndex > -1)
                {
                    endIndex = body.IndexOf("/script>", startIndex) - startIndex;
                    body = body.Remove(startIndex, endIndex + 8);
                    startIndex = body.IndexOf("<script");
                }
                return body;
            }

            public string ClearEmailStyle(string body)
            {
                var startIndex = body.IndexOf("<style");
                while (startIndex > -1)
                {
                    startIndex = body.IndexOf(">", startIndex) + 1;
                    var endIndex = body.IndexOf("</style", startIndex) - 1;
                    string estilos = body.Substring(startIndex, endIndex + 1 - startIndex);
                    string estilosAlterados = estilos + "";
                    estilosAlterados = estilosAlterados.Insert(0, "#emailBody ");
                    estilosAlterados = estilosAlterados.Replace("}", "} #emailBody ");
                    estilosAlterados = estilosAlterados.Replace(" * ", "#emailBody");

                    body = body.Replace(estilos, estilosAlterados);

                    startIndex = body.IndexOf("<style", endIndex);
                }
                return body;
            }
            public String plainTextToHtml(String s)
            {
                StringBuilder builder = new StringBuilder();
                Boolean previousWasASpace = false;
                foreach (char c in s)
                {
                    if (c == ' ')
                    {
                        if (previousWasASpace)
                        {
                            builder.Append("&nbsp;");
                            previousWasASpace = false;
                            continue;
                        }
                        previousWasASpace = true;
                    }
                    else
                    {
                        previousWasASpace = false;
                    }
                    switch (c)
                    {
                        case '<': builder.Append("&lt;"); break;
                        case '>': builder.Append("&gt;"); break;
                        case '&': builder.Append("&amp;"); break;
                        case '"': builder.Append("&quot;"); break;
                        case '\n': builder.Append("<br>"); break;
                        case '\t': builder.Append("&nbsp; &nbsp; &nbsp;"); break;
                        default:
                            {
                                if (c < 128)
                                { builder.Append(c); break; }
                                else
                                { builder.Append("&#").Append((int)c).Append(";"); break; }
                            }
                    }
                }
                //Create LINKS
                String str = "(?i)\\b((?:https?://|www\\d{0,3}[.]|[a-z0-9.\\-]+[.][a-z]{2,4}/)(?:[^\\s()<>]+|\\(([^\\s()<>]+|(\\([^\\s()<>]+\\)))*\\))+(?:\\(([^\\s()<>]+|(\\([^\\s()<>]+\\)))*\\)|[^\\s`!()\\[\\]{};:\'\".,<>?«»“”‘’]))";
                Regex patt = new Regex(str);
                Match matcher = patt.Match(builder.ToString());
                if (!String.IsNullOrEmpty(matcher.Value))
                {
                    String plain = builder.ToString();
                    while (!String.IsNullOrEmpty(matcher.Value))
                    {
                        plain = plain.Replace(matcher.Value, "<a href=\"" + matcher.Value + "\">" + matcher.Value + "</a>");
                        matcher = matcher.NextMatch();
                    }
                    return plain;
                }
                return builder.ToString();
            }

            public string ExtrairIdentificador(string value)
            {
                Regex re = new Regex(@"[A-Za-z0-9-]+@identificadoremail.com");
                var identifier = re.Match(value);
                if (!string.IsNullOrWhiteSpace(identifier.Value))
                    return identifier.Value.Substring(0, identifier.Value.IndexOf("@identificadoremail.com"));

                return null;
            }
        }
    }
}

