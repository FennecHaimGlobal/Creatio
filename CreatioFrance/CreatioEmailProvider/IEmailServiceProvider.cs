using System.Threading.Tasks;

namespace CreatioEmailProvider
{
    public interface IEmailServiceProvider
    {
        /// <summary>
        /// Sends the subscription confirmation email asynchronous.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="token">The token.</param>
        /// <param name="destination">The destination.</param>
        /// <returns></returns>
        Task SendSubscriptionConfirmationEmailAsync(string subject, string token, string destination);
    }
}