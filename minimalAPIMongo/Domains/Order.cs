using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace minimalAPIMongo.Domains
{
    public class Order
    {
        [BsonId]

        [BsonElement("_id"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("status")]
        public string? Status { get; set; }


        // referencia para que eu consiga cadastrar um pedido com os produtos
        [BsonElement("productId")]
        [JsonIgnore]
        public List<string>? ProductId { get; set; }


        // referencia para que quando eu liste os pedidos, venham os dados de cada produto(lista)
        public List<Product>? Product { get; set; }


        //referencia ao cliente que esta fazendo o pedido

        // referencia para que eu consiga cadastrar um pedido com o cliente
        [BsonElement("clientId")]
        [JsonIgnore]
        public string ClientId { get; set; }

        //referencia para que quando eu liste os pedidos, venham os dados do cliente
        public Client? Client { get; set; }
       

    }
}
