using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class IndexModel : PageModel
{
    [BindProperty]
    public string? Name { get; set; }
    [BindProperty]
    public string? Mail { get; set; }
    [BindProperty]
    public string? SSN { get; set; }

    public string? ResponseMessage { get; set; }

    private const string ServerIp = "service_system";
    private const int ServerPort = 20200;       

    public void OnGet()
    {
        // Display the page with a form.
    }

    public async Task<IActionResult> OnPost()
    {
        try
        {
            // Validate input fields
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Mail) || string.IsNullOrEmpty(SSN))
            {
                ResponseMessage = "Please fill in all fields.";
                return Page();
            }

            // Create a TCP client to send data
            using (TcpClient client = new TcpClient(ServerIp, ServerPort))
            using (NetworkStream stream = client.GetStream())
            {
                // Prepare the message
                string message = $"createpatient,{Name},{Mail},{SSN}";
                byte[] messageBytes = Encoding.ASCII.GetBytes(message);

                // Send the message to the server
                await stream.WriteAsync(messageBytes, 0, messageBytes.Length);
                await stream.FlushAsync();

                ResponseMessage = "Data successfully sent to the server via TCP.";
            }
        }
        catch (Exception ex)
        {
            // Handle any errors related to network communication
            ResponseMessage = "Error sending data to the server: " + ex.Message;
        }

        // Return the page with the updated response message
        return Page();
    }
}
