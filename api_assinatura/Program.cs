using System.Text.Json;
using System.Text;
using System.Net.Http.Json;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using WSMap;

class Program
{
    public static async Task<int> Main(string[] args)
    {
        Initialize.Parametros(args);
        int exec = await ExecutaIntegra(Initialize.con, Initialize.rest, Initialize.tabela , Initialize.chave) == Initialize.SUCESSO 
                ? Initialize.SAIDA_NORMAL
                : Initialize.SAIDA_FALHA;

        return exec;
    }

    public static async Task<int> ExecutaIntegra(string connectionString, string restUri, string tabelaDestino, string initString)
    {
        string Sha256Token = ComputeSha256Hash(
            initString + DateTime.UtcNow.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("pt-br"))       
        );

        Initialize.client.Timeout = Initialize.TIMEOUT_CLIENT;
        Initialize.client.DefaultRequestHeaders.Add("user", "integracao");
        Initialize.client.DefaultRequestHeaders.Add("token", Sha256Token);

        Root? retorno = await BuscaDadosAPI(restUri);
        
        if(retorno != null) 
        { 
            using SqlConnection connection = new(connectionString); 
            await connection.OpenAsync();
            await InserirBulkAsync(connection, retorno.itens, tabelaDestino);
            return Initialize.SUCESSO;
        }
        else
        {
            return Initialize.FALHA;
        }
    }

    public static string ComputeSha256Hash(string rawData)
    {
        using SHA256 sha256Hash = SHA256.Create();
        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
        StringBuilder builder = new();
        for (int i = 0; i < bytes.Length; i++)
        {
            builder.Append(bytes[i].ToString("x2"));
        }
        return builder.ToString();
    }

    public static async Task<Root> BuscaDadosAPI(string restUri)
    {
        Root? jsonRecebido = new();

        var post = new PostItem()
        {
            pag = "ponto_assinatura_espelho",
            cmd = "get"
        };

        var body = new StringContent(JsonSerializer.Serialize(post), Encoding.UTF8, "application/json");
        Console.WriteLine(body.ReadAsStringAsync().Result);
        try
        {
            using var resposta = await Initialize.client.PostAsync(restUri, body);
            Console.WriteLine(resposta.StatusCode.ToString() + "\n" + resposta.Headers.ToString());
            jsonRecebido = await resposta.Content.ReadFromJsonAsync<Root>();
        }

        catch(Exception ex)
        {
            Console.WriteLine($"Erro ao solicitar dados a API : {ex}");
        }

        return jsonRecebido;
        
    }
    
    public static async Task InserirBulkAsync(SqlConnection con, List<Item> entries, string tabelaDestino)
    {
        DataTable dt = new DataTable();

        var STOU_INSERT = Dicionario.GetSTOU_INSERT(entries[0]);
        foreach (var key in STOU_INSERT.Keys)
        {
            dt.Columns.Add(key);
        }    

        foreach (var entry in entries)
        {
            STOU_INSERT = Dicionario.GetSTOU_INSERT(entry);
            DataRow row = dt.NewRow();
            for (int j = 0; j < STOU_INSERT.Count; j++)
            {
                var value = STOU_INSERT.Values.ElementAt(j);
                row[j] = value ?? DBNull.Value;
            }
            dt.Rows.Add(row);
        }
    

        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
        {
            for(int i = 0; i < STOU_INSERT.Count; i++)
            {
                bulkCopy.ColumnMappings.Add(i, i + 1);
            }
            bulkCopy.DestinationTableName = tabelaDestino;
            try
            {
                await bulkCopy.WriteToServerAsync(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir no banco de dados : {ex}");
            }
        }
    }
}