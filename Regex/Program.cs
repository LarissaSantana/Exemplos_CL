using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace ExemploRegex
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ExemploRegex ex = new ExemploRegex();
            foreach (var x in ex.buscarData("https://andresecco.com.br/2017/01/azure-tech-nights/"))
            {
                Console.WriteLine(x);

            }

            string input = "Marivanda Gonçalves da Conceição";
            string pattern = @"(?i)[^0-9a-záéíóúàèìòùâêîôûãõç\s]";
            string replacement = "";
            Regex rgx = new Regex(pattern);
            string result = rgx.Replace(input, replacement);

            Console.WriteLine("String Original: {0}", input);
            Console.WriteLine("String tratada : {0}", result);

            string b = "b";
            string a = "a";

            Console.ReadKey();
        }

        public class ExemploRegex
        {


            #region buscar data indexada com Dia - blog do André Secco
            public Dictionary<int, DateTime> buscarDiaData(string url)
            {
                WebClient wc = new WebClient();
                var fonte = wc.DownloadString(url);

                // @"Dia (\d) \((\d{2}\/\d+\/\d+)\)" => {2} = qtd de vezes que o número vai aparecer
                // \d = um dígito
                // Uso do parênteses = porque é um dado que vai poder ser usado depois, ou seja, vai se tornar um grupo
                var regex = new Regex(@"Dia (\d) \((\d+\/\d+\/\d+)\)");


                /* Método Match => ele vai aplicar o padrão da variável regex dentro do texto(passado no parâmetro) e 
                 e o que ele encontrar vai ser retornado dentro da variável primeiroEncontrado 
                 Obs.: ele só traz a primeira correspondência */
                var primeiroEncontrado = regex.Match(fonte);

                /* Vai pegar todas as correspondências do padrão no texto e colocar dentro de uma Match Collection */
                var encontrados = regex.Matches(fonte);

                /* int => chave que vai receber o dígito do dia correspondente
                e a data vai ficar salva no valor do dicionário*/

                Dictionary<int, DateTime> dic = new Dictionary<int, DateTime>();

                /* Para cada item da variável encontrados, ele vai adicionar no dicionário */
                foreach (Match item in encontrados)
                {
                    /*  Grupo 1 => grupo do digito correspondente ao dia do evento
                        Grupo 2 => grupo dos digitos correspondentes à data */

                    //Converter o Grupo1 para um int e o Grupo2 para um DateTime
                    dic.Add(int.Parse(item.Groups[1].Value), DateTime.Parse(item.Groups[2].Value));
                }

                return dic;
            }
            #endregion

            #region buscarData
            public LinkedList<DateTime> buscarData(string url)
            {
                WebClient wc = new WebClient();
                var fonte = wc.DownloadString(url);

                // @"Dia (\d) \((\d{2}\/\d+\/\d+)\)" => {2} = qtd de vezes que o número vai aparecer
                // \d = um dígito
                // Uso do parênteses = porque é um dado que vai poder ser usado depois, ou seja, vai se tornar um grupo
                var regex = new Regex(@"[0-9]{2}[-|\/]{1}[0-9]{2}[-|\/]{1}[0-9]{4}");


                /* Método Match => ele vai aplicar o padrão da variável regex dentro do texto(passado no parâmetro) e 
                 e o que ele encontrar vai ser retornado dentro da variável primeiroEncontrado 
                 Obs.: ele só traz a primeira correspondência */
                var primeiroEncontrado = regex.Match(fonte);

                /* Vai pegar todas as correspondências do padrão no texto e colocar dentro de uma Match Collection */
                var encontrados = regex.Matches(fonte);

                /* int => chave que vai receber o dígito do dia correspondente
                e a data vai ficar salva no valor do dicionário*/

                LinkedList<DateTime> lk = new LinkedList<DateTime>();

                /* Para cada item da variável encontrados, ele vai adicionar no dicionário */
                foreach (Match item in encontrados)
                {
                    /*  Grupo 1 => grupo do digito correspondente ao dia do evento
                        Grupo 2 => grupo dos digitos correspondentes à data */

                    //Converter o Grupo1 para um int e o Grupo2 para um DateTime
                    lk.AddLast(DateTime.Parse(item.Value));
                }
                return lk;
            }
            #endregion
        }
    }
}
