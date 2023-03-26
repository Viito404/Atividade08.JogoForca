using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace P07.JogoForca
{
     internal class Program
     {
          #region Variáveis Globais;

          static string[] palavras = { "ABACATE", "ABACAXI", "ACEROLA", "AÇAÍ", "ARAÇA", "AMORA", "BACABA", "BACURI", "BANANA", "CAJÁ", "CAJÚ", "CARAMBOLA", "CUPUAÇU", "GRAVIOLA", "GOIABA", "JABUTICABA", "JENIPAPO", "MAÇÃ", "MANGABA", "MANGA", "MARACUJÁ", "MURICI", "PEQUI", "PITANGA", "PITAYA", "SAPOTI", "TANGERINA", "UMBU", "UVA", "UVAIA" };
          static char chute;
          static char cabeca, bracoEsquerdo, bracoDireito, tronco, pernas;

          #endregion

          #region Método Principal;

          static void Main(string[] args)
          {
               do
               {
                    string opcao = GerarMenu();

                    if (opcao == "s" || opcao == "S")
                    {
                         Console.ForegroundColor = ConsoleColor.Red;
                         Console.WriteLine("\nSaindo...");
                         Console.ResetColor();
                         break;
                    }

                    int erros = 0;
                    int acertos = 0;
                    int indexLetrasDigitadas = 0;                    
                    string palavraAleatoria;
                    char[] letrasPalavraAleatoria, letrasForca, letrasDigitadas;
                    cabeca = ' '; 
                    bracoEsquerdo = ' '; 
                    bracoDireito = ' '; 
                    tronco = ' '; 
                    pernas = ' ';

                    GerarPalavraAleatoria(out palavraAleatoria, out letrasPalavraAleatoria, out letrasForca, out letrasDigitadas);

                    DefinirTracosPalavra(palavraAleatoria, letrasPalavraAleatoria, letrasForca);

                    do
                    {
                         GerarForca(palavraAleatoria, letrasForca);

                         PegarLetra();

                         VerificarLetra(ref erros, ref acertos, ref indexLetrasDigitadas, palavraAleatoria, letrasPalavraAleatoria, letrasForca, letrasDigitadas);

                         switch (erros)

                         {
                              case 1: cabeca = 'O'; break;
                              case 2: tronco = 'X'; break;
                              case 3: bracoEsquerdo = '/'; break;
                              case 4: bracoDireito = '\\'; break;
                              case 5: pernas = 'X'; break;
                         }

                    } while (erros != 6 && acertos < letrasPalavraAleatoria.Length);

                    GerarResultadoJogo(acertos, palavraAleatoria);

               } while (true);
          }

          #endregion

          #region Funções Gerais;

          private static void VerificarLetra(ref int erros, ref int acertos, ref int indexLetrasDigitadas, string palavraAleatoria, char[] letrasPalavraAleatoria, char[] letrasForca, char[] letrasDigitadas)
          {
               for (int i = 0; i < palavraAleatoria.Length; i++)
               {
                    if (chute == letrasForca[i] || letrasDigitadas.Contains(chute))
                    {
                         Console.WriteLine("Letra já digitada!");
                         Console.ReadLine();
                         break;
                    }

                    else if (letrasPalavraAleatoria.Contains(chute))
                    {
                         if (chute == letrasPalavraAleatoria[i])
                         {
                              letrasForca[i] = chute;
                              acertos++;
                         }
                    }

                    else
                    {
                         Console.WriteLine("\nLetra não existe!");
                         Console.ReadLine();
                         letrasDigitadas[indexLetrasDigitadas] = chute;
                         indexLetrasDigitadas++;
                         erros++;
                         break;
                    }
               }
          }

          private static void PegarLetra()
          {
               Console.WriteLine("\nQual o seu chute? ");
               chute = Convert.ToChar(Console.ReadLine().ToUpper());

               string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
               string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

               string chutet = Convert.ToString(chute);

               for (int i = 0; i < comAcentos.Length; i++)
               {
                    chutet = chutet.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
               }

               chute = Convert.ToChar(chutet);
          }

          private static void DefinirTracosPalavra(string palavraAleatoria, char[] letrasPalavraAleatoria, char[] letrasForca)
          {
               for (int i = 0; i < palavraAleatoria.Length; i++)
               {
                    if (letrasPalavraAleatoria[i] == ' ')
                    {
                         letrasForca[i] = ' ';
                    }
                    else
                    {
                         letrasForca[i] = '-';
                    }
               }
               Console.Clear();
          }

          private static void GerarResultadoJogo(int acertos, string palavraAleatoria)
          {
               Console.Clear();

               if (acertos == palavraAleatoria.Length)
               {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Você venceu, parabéns!");
                    Console.ReadLine();
                    Console.ResetColor();
               }

               else
               {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Você perdeu, mais sorte na próxima vez!");
                    Console.ReadLine();
                    Console.ResetColor();
               }
          }

          private static void GerarPalavraAleatoria(out string palavraAleatoria, out char[] letrasPalavraAleatoria, out char[] letrasForca, out char[] letrasDigitadas)
          {
               Random numeroAleatorio = new Random();
               int indexAleatorio = numeroAleatorio.Next(palavras.Length);
               palavraAleatoria = palavras[indexAleatorio];
               letrasPalavraAleatoria = palavraAleatoria.ToCharArray();
               letrasForca = palavraAleatoria.ToCharArray();
               letrasDigitadas = new char[30];
          }

          private static void GerarForca(string palavraAleatoria, char[] letrasForca)
          {
               Console.Clear();
               Console.WriteLine(" -----------");
               Console.WriteLine(" |/        |");
               Console.WriteLine($" |         {cabeca}");
               Console.WriteLine($" |        {bracoEsquerdo}{tronco}{bracoDireito}");
               Console.WriteLine($" |         {pernas}");
               Console.WriteLine(" |        ");
               Console.WriteLine(" |        ");
               Console.WriteLine(" |        ");
               Console.WriteLine(" |        ");
               Console.WriteLine("_|___        \n");

               for (int i = 0; i < palavraAleatoria.Length; i++)
               {
                    Console.Write(letrasForca[i] + " ");
               }
          }

          private static string GerarMenu()
          {
               Console.Clear();
               Console.WriteLine("=============================================");
               Console.WriteLine("\nJogo da Forca!\n");
               Console.WriteLine("=============================================");

               Console.Write("\nDigite S para sair, ou qualquer outro botão para continuar:\n> ");

               string opcao;
               opcao = Console.ReadLine();
               return opcao;
          }

          #endregion
     }
}