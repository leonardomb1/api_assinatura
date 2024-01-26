using System.Net;

namespace WSMap;

public class Initialize
{
    public static readonly int SAIDA_NORMAL = 0;
    public static readonly int SUCESSO = 1;
    public static readonly int FALHA = 0;
    public static readonly int SAIDA_FALHA = 1;
    public static readonly int TENTATIVAS_SQL = 5;
    public static readonly int PAUSA = 3000;
    public static readonly int TIMEOUT_SQL_MLS = 14400;
    public static readonly TimeSpan TIMEOUT_CLIENT = TimeSpan.FromSeconds(300);
    public static HttpClient client = new();
    public static string? con;
    public static string? rest;
    public static string? tabela;
    public static string? chave;
    public static void Parametros(string[] args)
    {
        con = args[0];
        rest = args[1];
        tabela = args[2];
        chave = args[3];

    }
}