namespace WSMap;

public static class Dicionario
{
    public static Dictionary<string, Object> GetSTOU_INSERT(Item entry)
    {
        return new Dictionary<string, Object>
        {
            { "[centro_custo]" , entry.centro_custo}
            ,{"[cod_ponto_fechamento]", entry.cod_ponto_fechamento}
            ,{"[dtcadastro]", entry.dtcadastro}
            ,{"[login_cadastro]", entry.login_cadastro}
            ,{"[dtinicio]", entry.dtinicio}
            ,{"[dtfim]"   , entry.dtfim}
            ,{"[descricao]"   , entry.descricao}
            ,{"[periodo]", entry.periodo}
            ,{"[cod_pessoa]", entry.cod_pessoa}
            ,{"[pessoa]", entry.pessoa}
            ,{"[matricula]"   , entry.matricula}
            ,{"[cargo]"   , entry.cargo}
            ,{"[unidade]", entry.unidade}
            ,{"[depto_cc]", entry.depto_cc}
            ,{"[empresa]", entry.empresa}
            ,{"[gestor]", entry.gestor}
            ,{"[codigo]", entry.codigo}
            ,{"[cod_fechamento_automatico]"   , entry.cod_fechamento_automatico}
            ,{"[dtassinatura]", entry.dtassinatura}
            ,{"[login_assinatura]", entry.login_assinatura}
            ,{"[cod_pessoa_assinatura]"   , entry.cod_pessoa_assinatura}
            ,{"[dtassinatura_gestor]", entry.dtassinatura_gestor}
            ,{"[login_assinatura_gestor]", entry.login_assinatura_gestor}
            ,{"[cod_pessoa_assinatura_gestor]", entry.cod_pessoa_assinatura_gestor}
            ,{"[dtvisualizou]", entry.dtvisualizou}
            ,{"[font_assinatura]", entry.font_assinatura}
            ,{"[font_assinatura_gestor]", entry.font_assinatura_gestor}
            ,{"[dtvisualizou_gestor]", entry.dtvisualizou_gestor}
            ,{"[dtdownload]", entry.dtdownload}
            ,{"[dtconfirmacao_download]", entry.dtconfirmacao_download}
            ,{"[j_assinatura]", entry.j_assinatura}
            ,{"[j_assinatura_gestor]", entry.j_assinatura_gestor}
            ,{"[dtapagou]", entry.dtapagou}
            ,{"[login_apagou]", entry.login_apagou}
            ,{"[data_inicio]", entry.data_inicio}
            ,{"[data_fim]", entry.data_fim}
            ,{"[dt_auto]", entry.dt_auto}
        };
    }
}