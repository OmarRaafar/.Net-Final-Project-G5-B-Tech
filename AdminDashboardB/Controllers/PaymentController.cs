using ApplicationB.Services_B.Order;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json.Nodes;

namespace DTOsB.Controllers
{
    public class PaymentController : Controller
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        //[HttpPost]
        //public async Task<IActionResult> PayWithPayPal(decimal amount, string currency)
        //{
        //    var approvalUrl = await _paymentService.CreatePayment(amount, currency);

        //    if (!string.IsNullOrEmpty(approvalUrl))
        //    {
        //        return Redirect(approvalUrl);  // Redirect user to PayPal to complete payment
        //    }

        //    return View("Error");  // Handle payment creation failure
        //}

        // Success and Cancel methods:
        public IActionResult Success()
        {
            // Handle success logic, such as capturing the payment and confirming the order
            return View("Success");
        }

        public IActionResult Cancel()
        {
            // Handle payment cancellation
            return View("Cancel");
        }
        public async Task<IActionResult> Success(string token)
        {
            await _paymentService.CapturePayment(token);
            // Handle successful order completion
            return View("Success");
        }
    }



    //public class PaymentController : Controller
    //{
    //    private string PaypalClientId { get; set; } = "";
    //    private string PaypalSecret { get; set; } = "";
    //    private string PaypalUrl { get; set; } = "";

    //    public PaymentController(IConfiguration configuration)
    //    {
    //        PaypalClientId = configuration["paypal:ClientId"];
    //        PaypalSecret = configuration["paypal:Secret"];
    //        PaypalUrl = configuration["paypal:Url"];
    //    }

    //    public IActionResult Index()
    //    {
    //        ViewBag.PaypalClientId = PaypalClientId;
    //        return View();
    //    }
    //    public async Task<string> test()
    //    {
    //        return await GetPaypalAccessToken();
    //    }

    //    private async Task<string> GetPaypalAccessToken()
    //    {
    //        string token = "";
    //        string url = PaypalUrl + "/v1/oauth2/token";

    //        using (var httpClient = new HttpClient())
    //        {
    //            string credentilas = Convert.ToBase64String(Encoding.UTF8.GetBytes(PaypalClientId + ":" + PaypalSecret));
    //            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + credentilas);
    //            var request = new HttpRequestMessage(HttpMethod.Post, url);
    //            request.Content = new StringContent("grant_type=client_credentals", null, "application/x-www-form-urlencoded");
    //            var httpResponse = await httpClient.SendAsync(request);


    //            if (httpResponse.IsSuccessStatusCode)
    //            {
    //                var strResponse = await httpResponse.Content.ReadAsStringAsync();
    //                var jsonResponse = JsonNode.Parse(strResponse);
    //                if (jsonResponse != null)
    //                {
    //                    token = jsonResponse["access_token"]?.ToString() ?? "";
    //                }
    //            }
    //        }
    //        return token;
    //    }
    //}
}
