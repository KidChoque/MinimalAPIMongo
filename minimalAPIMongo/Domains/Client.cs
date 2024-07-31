using MongoDB.Bson.Serialization.Attributes;

namespace minimalAPIMongo.Domains
{
    public class Client
    {
        [BsonId]

        [BsonElement("_id"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }


        [BsonElement("_userId"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string UserId { get; set; }  


        [BsonElement("cpf")]
        public  string  CPF { get; set; }

        [BsonElement("phoneNumber")]    
        public string PhoneNumber { get; set; }

        [BsonElement("adress")]
        public string Adress { get; set; }

        public Dictionary<string, string> AdditionalAtributtes { get; set; }
        public Client()
        {
            AdditionalAtributtes = new Dictionary<string, string>();
        }

    }
}
