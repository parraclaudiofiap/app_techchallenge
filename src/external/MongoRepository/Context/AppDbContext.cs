
using MongoRepository.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Gateway;
using DbGateway;
using Domain.Entities;
using AwsServices;

namespace MongoRepository.Context;

public class AppDbContext 
{
    private readonly MongoClient mongoClient;
    private readonly IMongoDatabase database;

    public AppDbContext(IOptions<MongoDbConfig> config)
    {
        var mongodbConnectionString = SecretManager.GetSecret("mongodb_connectionstring").GetAwaiter().GetResult();
        mongoClient = new MongoClient($"{mongodbConnectionString}/?retryWrites=true&w=majority&sslVerifyCertificate=false");
        database = mongoClient.GetDatabase("fiap");
    }
    
    public IMongoCollection<ClienteDAO> Clientes => database.GetCollection<ClienteDAO>("Clientes");
    public IMongoCollection<ProdutoDAO> Produtos => database.GetCollection<ProdutoDAO>("Produtos");
    public IMongoCollection<CarrinhoDeComprasDAO> CarrinhoDeCompras => database.GetCollection<CarrinhoDeComprasDAO>("CarrinhoDeCompras");
    public IMongoCollection<OrdemPagamentoDAO> OrdemPagamento => database.GetCollection<OrdemPagamentoDAO>("OrdensDePagamento");
  
    public IMongoCollection<PedidoDAO> Pedido => database.GetCollection<PedidoDAO>("Pedidos");
    
    public IMongoCollection<FilaPedidosDAO> FilaPedidos => database.GetCollection<FilaPedidosDAO>("FilaPedidos");

    public void Map()
    {
        BsonClassMap.RegisterClassMap<BaseEntity<ClienteDAO>>(cm => { cm.AutoMap(); });
                
        BsonClassMap.RegisterClassMap<BaseEntity<ProdutoDAO>>(cm => { cm.AutoMap(); });
        
        BsonClassMap.RegisterClassMap<BaseEntity<CarrinhoDeComprasDAO>>(cm => { cm.AutoMap(); });

        BsonClassMap.RegisterClassMap<BaseEntity<OrdemPagamentoDAO>>(cm => { cm.AutoMap(); });
        
        BsonClassMap.RegisterClassMap<BaseEntity<PedidoDAO>>(cm => {cm.AutoMap();});
        BsonClassMap.RegisterClassMap<BaseEntity<FilaPedidosDAO>>(cm => {cm.AutoMap();});
    }
    
}
