using System.Diagnostics;
using System.Formats.Tar;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;

namespace TestProject1;

[TestClass]
public class UnitTest1
{
    private HttpClient? httpClient;

    [TestInitialize]
    public void Setup()
    {
        httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://www.boredapi.com/api");
    }

    [DataRow("social", 2)]
    [DataRow("social", 2)]
    [DataRow("social", 2)]
    [DataTestMethod]
    public async Task TestMethod1(string type, int participants)
    {
        // Call the api
        var uri = $"{httpClient.BaseAddress}/activity?tpye={type}&participants={participants}";
        var response = await httpClient.GetAsync(uri);

        if (response.IsSuccessStatusCode)
        {
            // Read the response content as a string
            string responseData = await response.Content.ReadAsStringAsync();
            
            // Format the response
            JsonNode? rootNode = JsonNode.Parse(responseData);
                
            JsonNode activityNode = rootNode["activity"];
            Console.WriteLine("Activity: " + activityNode.GetValue<string>());

            // Check the requested parameters are met
            Assert.AreEqual(type, rootNode["type"].GetValue<string>());
            Assert.AreEqual(2, rootNode["participants"].GetValue<int>());
        }
        else
        {
            Assert.Fail("Response: " + response.StatusCode);
        }
    }

}