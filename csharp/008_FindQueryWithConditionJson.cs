﻿using System;
using MapRDB.Driver;
using MapRDB.Driver.Ojai;

public class FindQueryWithConditionJson
{
	public async void FindQueryWithConditionJson()
	{
        // Create a connection to data access server
        var connectionStr = $"localhost:5678?auth=basic;" +
            $"user=mapr;" +
            $"password=mapr;" +
            $"ssl=true;" +
            $"sslCA=/opt/mapr/conf/ssl_truststore.pem;" +
            $"sslTargetNameOverride=node1.mapr.com";
        var connection = ConnectionFactory.CreateConnection(connectionStr);

        // Get a store and assign it as a DocumentStore object
        var store = connection.GetStore("/demo_table");

        // Create an OJAI query
        var query =
            @"{" +
                @"""$where"":" +
                    @"{" +
                        @"""$eq"":{""address.zipCode"":{""$numberLong"":""95196""}}" +
                    @"}" +
            @"}";

        // Fetch OJAI Documents by query
        var queryResult = store.FindQuery(query);

        var documentStream = await queryResult.GetAllDocuments();
        // Print OJAI Documents from document stream
        foreach (var document in documentStream)
        {
            Console.WriteLine(document.ToDictionary());
        }

        // Close the OJAI connection
        connection.Close();
    }
}
