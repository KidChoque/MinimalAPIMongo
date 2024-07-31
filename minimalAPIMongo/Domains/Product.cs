using MongoDB.Bson.Serialization.Attributes;

namespace minimalAPIMongo.Domains
{
    public class Product
    {
        // Define que esta propriedade é o Id do objeto
        [BsonId]
        // Define o nome do campo no MongoDb como "_id" e o tipo como "ObjectId"
        [BsonElement("_id"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string? Name { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }

        // Adiciona um dicionário para atributos adicionais
        public Dictionary<string, string> AdditionalAttributes { get; set; }

        /// <summary>
        /// Ao ser instanciado um objeto da classe Product, o atributo AdditionalAttributes já virá com um novo dicionário
        /// e portanto habilitado para adicionar mais atributos
        /// </summary>
        public Product()
        {
            AdditionalAttributes = new Dictionary<string, string>();
        }
    }
}
