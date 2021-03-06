﻿using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ShopClient.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("Price")]
        public double Price { get; set; }

        [BsonElement("Photo")]
        public string Photo { get; set; }

        [BsonElement("ProductType")]
        public ProductType Type;

        public enum ProductType { SeaSalt, Freesia, WildBlueBell, Peony }

    }

   
}
