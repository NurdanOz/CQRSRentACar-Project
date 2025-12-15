using System.Text.Json.Serialization;
using System.Collections.Generic;

public class GasPriceApiResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("result")]
    public ResultWrapper Result { get; set; }
}

public class ResultWrapper
{
    [JsonPropertyName("state")]
    public List<PriceDetails> State { get; set; }  

    [JsonPropertyName("cities")]
    public List<object> Cities { get; set; }
}

public class PriceDetails
{
    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("gasoline")]
    public string GasolinePrice { get; set; }

    [JsonPropertyName("midGrade")]  
    public string MidgradePrice { get; set; }

    [JsonPropertyName("premium")]
    public string PremiumPrice { get; set; }

    [JsonPropertyName("diesel")]
    public string DieselPrice { get; set; }
}
