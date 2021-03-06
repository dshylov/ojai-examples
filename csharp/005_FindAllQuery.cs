﻿using System;
using MapRDB.Driver;
using MapRDB.Driver.Ojai;

public class FindAllQuery
{
	public async void FindAllQuery()
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

        // Build an OJAI query
        var query = connection.NewQuery().Build();

        // Options for find request
        var options = new QueryOptions()
        {
            IncludeQueryPlan = true,
            Timeout = 1000
        };

        // Fetch all OJAI Documents from table
        var queryResult = store.Find(query, options);

        // Get query plan
        Console.WriteLine(queryResult.GetQueryPlan());

        var documentStream = await queryResult.GetDocumentAsyncStream().GetAllDocuments();
        // Print OJAI Documents from document stream
        foreach (var document in documentStream)
        {
            Console.WriteLine(document.ToDictionary());
        }

        // Close the OJAI connection
        connection.Close();
    }
}
