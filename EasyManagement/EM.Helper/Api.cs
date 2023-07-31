using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace EM.Helper;

public static class Api
{
    // Server URL
    //internal const string Url = "https://localhost:44312/";
    internal const string Url = "https://localhost:7036/";
	//internal const string Url = "https://clinica.api.awsat.pt/api/";


	/// <summary>
	/// Post API JSON
	/// </summary>
	/// <param name="o">Object to serialize</param>
	/// <param name="route">API route</param>
	/// <returns>API Response</returns>
	public static async Task<HttpResponseMessage> PostApi(object o, string route)
    {
        var json = JsonConvert.SerializeObject(o);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        using var client = new HttpClient();

        var response = await client.PostAsync(Url + route, data);

        return response;
    }

    /// <summary>
    /// Post API JSON With Multipart Form Data
    /// </summary>
    /// <param name="o">Multipart Form data to send</param>
    /// <param name="route">API route</param>
    /// <returns>API Response</returns>
    public static async Task<HttpResponseMessage> PostApiMultipartForm(object o, string route)
    {
        var data = (MultipartFormDataContent)o;

        using var client = new HttpClient();

        var response = await client.PostAsync(Url + route, data);

        return response;
    }

    /// <summary>
    /// Put API JSON
    /// </summary>
    /// <param name="o">Object to serialize</param>
    /// <param name="route">API route</param>
    /// <returns>API Response</returns>
    public static async Task<HttpResponseMessage> PutApi(object o, string route)
    {
        var json = JsonConvert.SerializeObject(o);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        using var client = new HttpClient();

        var response = await client.PutAsync(Url + route, data);

        return response;
    }

    /// <summary>
    /// Delete API JSON
    /// </summary>
    /// <param name="route">API route</param>
    /// <returns>API Response</returns>
    public static async Task<HttpResponseMessage> DeleteApi(string route)
    {
        using var client = new HttpClient();

        var response = await client.DeleteAsync(Url + route);

        return response;
    }

    /// <summary>
    /// Get API JSON
    /// </summary>
    /// <param name="route">API route</param>
    /// <returns>API Response</returns>
    public static async Task<HttpResponseMessage> GetApi(string route)
    {
        using var client = new HttpClient();

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


        var response = await client.GetAsync(Url + route);

        return response;
    }

    /// <summary>
    /// Get API JSON - Authorized
    /// </summary>
    /// <param name="route">API route</param>
    /// <param name="token">User's Token</param>
    /// <returns>API Response</returns>
    public static async Task<HttpResponseMessage> GetApiAuthorized(string route, string token)
    {
        using var client = new HttpClient();

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await client.GetAsync(Url + route);

        return response;
    }

    /// <summary>
    /// Put API JSON - Authorized
    /// </summary>
    /// <param name="o">Object to serialize</param>
    /// <param name="route">API route</param>
    /// <param name="token">User's Token</param>
    /// <returns>API Response</returns>
    public static async Task<HttpResponseMessage> PutApiAuthorized(object o, string route, string token)
    {
        var json = JsonConvert.SerializeObject(o);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        using var client = new HttpClient();

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.PutAsync(Url + route, data);

        return response;
    }

    /// <summary>
    /// Post API JSON - Authorized
    /// </summary>
    /// <param name="o">Object to serialize</param>
    /// <param name="route">API route</param>
    /// <param name="token">User's Token</param>
    /// <returns>API Response</returns>
    public static async Task<HttpResponseMessage> PostApiAuthorized(object o, string route, string token)
    {
        var json = JsonConvert.SerializeObject(o);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        using var client = new HttpClient();

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.PostAsync(Url + route, data);

        return response;
    }

    /// <summary>
    /// Post API With Multipart Form Data Authenticated
    /// </summary>
    /// <param name="o">Multipart Form data to send</param>
    /// <param name="route">API route</param>
    /// <param name="token">User's Token</param>
    /// <returns>API Response</returns>
    public static async Task<HttpResponseMessage> PostApiMultipartFormAuthorized(object o, string route, string token)
    {
        var data = (MultipartFormDataContent)o;

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.PostAsync(Url + route, data);

        return response;
    }
}

