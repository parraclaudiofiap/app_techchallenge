namespace DbGateway;

public interface IProdutoRepository
{
  Task<bool> Inserir(ProdutoDAO produto);

  Task EditarProduto(ProdutoDAO produto);

  Task RemoverProduto(string nome);

  Task<ProdutoDAO> PesquisarProdutoPorNome(string nomeProduto);

  Task<IList<ProdutoDAO>> PesquisarProdutosPorCategoria(string categoriaProdutoEnum);
}
