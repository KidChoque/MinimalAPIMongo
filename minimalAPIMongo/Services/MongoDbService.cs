using MongoDB.Driver;

namespace minimalAPIMongo.Services
{
    public class MongoDbService
    {
        /// <summary>
        /// Armazena a configuração da aplicação
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Armazena uma referência ao MongoDb
        /// </summary>
        private readonly IMongoDatabase _database;

        /// <summary>
        /// Recebe a configuração da aplicação como parâmetro
        /// </summary>
        /// <param name="configuration">objeto configuration</param>
        public MongoDbService(IConfiguration configuration)
        {
            // Atribui a configuração recebida em _configuration
            _configuration = configuration;

            // Obtém a string de conexão através do _configuration
            var connectionString = _configuration.GetConnectionString("DbConnection");

            // Cria um objeto MongoUrl que recebe como parâmetro a string de conexão
            var mongoUrl = MongoUrl.Create(connectionString);

            // Cria um cliente MongoClient para se conectar ao MongoDb
            var mongoClient = new MongoClient(mongoUrl);

            // Obtém a referência ao banco com o nome especificado na string de conexão
            _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        }

        /// <summary>
        /// Propriedade para acessar o banco de dados
        /// Retorna a referência ao bd
        /// </summary>
        public IMongoDatabase GetDatabase => _database;
    }
}
