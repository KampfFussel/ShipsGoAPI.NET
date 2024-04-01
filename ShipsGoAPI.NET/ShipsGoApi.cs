using System.Xml.Linq;
using ShipsGoAPI.NET.Properties;
using ShipsGoAPI.NET.Enums;
using Newtonsoft.Json;

namespace ShipsGoAPI.NET
{
    // WARNING: This just gives direct access to the shipsgo api. There is no wrapper or nothing for this yet.

    // TODO: Add comments, summarys and explanations..

    // Based on documentation of the ShipsGoAPI v1.2

    // API Documentation: https://shipsgo.com/pdfs/api-documentation-v1.2.pdf
    // API Mapping Documentation: https://drive.google.com/file/d/1EgOEGQ7Rt4p9qRrq3Bu5DhX7OmO-K5fs/view
    // API FAQ: https://shipsgo.com/api-documentation

    // No idea why it's not within one document.. or one location.

    public class ShipsGoApi
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _authCode;

        public ShipsGoApi(string baseUrl, string authCode)
        {
            _baseUrl = baseUrl;
            _authCode = authCode;
            _httpClient = new HttpClient();
        }

        public async Task<TrackingRequestResponse> CreateTrackingRequestWithBlNumberCustomForm(
            string containerNumber, string shippingLine, string emailAddress, string referenceNo, string blContainersRef, string[] tags)
        {
            var url = $"{_baseUrl}/api/v1.2/ContainerService/PostCustomContainerFormWithBl";
            var data = new Dictionary<string, string>()
        {
            { "authCode", _authCode },
            { "containerNumber", containerNumber },
            { "shippingLine", shippingLine },
            { "emailAddress", emailAddress },
            { "referenceNo", referenceNo },
            { "blContainersRef", blContainersRef },
            { "tags", string.Join(",", tags ?? new string[0]) },
        };

            var content = new FormUrlEncodedContent(data);
            var response = await _httpClient.PostAsync(url, content);
            return await HandleResponse<TrackingRequestResponse>(response);
        }
        public async Task<TrackingRequestResponse> PostContainerInfoWithBl(string containerNumber, string? shippingLine = null, string? blContainersRef = null)
        {
            var url = $"{_baseUrl}/api/v1.2/ContainerService/PostContainerInfoWithBl";
            var data = new Dictionary<string, string>()
            {
                { "authCode", _authCode },
                { "containerNumber", containerNumber }, // Use containerNumber
                { "shippingLine", shippingLine ?? string.Empty }, // Set empty string if shippingLine is null
                { "blContainersRef", blContainersRef ?? string.Empty }, // Set empty string if blContainersRef is null
            };

            var content = new FormUrlEncodedContent(data);
            var response = await _httpClient.PostAsync(url, content);
            return await HandleResponse<TrackingRequestResponse>(response);
        }

        public async Task<TrackingRequestResponse> PostContainerInfo(string containerNumber, string? shippingLine = null)
        {
            var url = $"{_baseUrl}/api/v1.2/ContainerService/PostContainerInfo";
            var data = new Dictionary<string, string>()
            {
                { "authCode", _authCode },
                { "containerNumber", containerNumber },
                { "shippingLine", shippingLine ?? string.Empty }, // Set empty string if shippingLine is null
            };

            var content = new FormUrlEncodedContent(data);
            var response = await _httpClient.PostAsync(url, content);
            return await HandleResponse<TrackingRequestResponse>(response);
        }

        public async Task<TrackingRequestResponse> PostCustomContainerForm(string containerNumber, string? shippingLine = null, string? emailAddress = null, string? referenceNo = null, string[]? tags = null)
        {
            var url = $"{_baseUrl}/api/v1.2/ContainerService/PostCustomContainerForm";
            var data = new Dictionary<string, string>()
            {
                { "authCode", _authCode },
                { "containerNumber", containerNumber },
                { "shippingLine", shippingLine ?? string.Empty }, // Set empty string if shippingLine is null
                { "emailAddress", emailAddress ?? string.Empty },  // Set empty string if emailAddress is null
                { "referenceNo", referenceNo ?? string.Empty },  // Set empty string if referenceNo is null
                { "tags", string.Join(",", tags ?? []) },  // Join tags into comma-separated string
            };

            var content = new FormUrlEncodedContent(data);
            var response = await _httpClient.PostAsync(url, content);
            return await HandleResponse<TrackingRequestResponse>(response);
        }

        public async Task<VoyageDataResponse> GetVoyageData(string requestId)
        {
            var url = $"{_baseUrl}/api/v1.2/ContainerService/GetContainerInfo/?authCode={_authCode}&requestId={requestId}";
            var response = await _httpClient.GetAsync(url);
            return await HandleResponse<VoyageDataResponse>(response);
        }

        public async Task<List<ShippingLine>> GetShippingLineList()
        {
            var url = $"{_baseUrl}/api/v1.2/ContainerService/GetShippingLineList";
            var response = await _httpClient.GetAsync(url);

            // Handle potential parsing errors (i should probably handle this more gracefully..)
            try
            {
                var content = await response.Content.ReadAsStringAsync();
                var xmlDoc = XDocument.Parse(content);
                var elements = xmlDoc.Descendants("string");
                return elements.Select(element => new ShippingLine { Name = element.Value }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error parsing shipping line list: {ex.Message}"); // Should i leave this or should i not? Should probably use an error logger..
            }

            return await HandleResponse<List<ShippingLine>>(response); // Fallback if parsing fails.. in case i remove the exception throwing..
        }

        private async Task<T> HandleResponse<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content); // I could probably handle this better.. but idc atm
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
            }
        }
    }
}
