using Application.Services.jobs;
using Application.Services.UserAccessor;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
    public class JobController : BaseApiController
    {
        private readonly IJobService _jobService;
        private readonly IConfiguration _configuration;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;

        public JobController(IJobService jobService,IConfiguration configuration,IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager)
        {
            this._jobService = jobService;
            this._configuration = configuration;
            this._backgroundJobClient = backgroundJobClient;
            this._recurringJobManager = recurringJobManager;
        }
        [HttpPost("fireandforget")]
        public IActionResult fireAndForget(string mail)
        {
            // Stores the job id into a variable and adding a background job for SendMail method.
            var fireAndForgetJob = _backgroundJobClient.Enqueue(() => SendMail(mail));

            // Return OK (Status 200) with a message that includes the job id from the scheduled job
            return Ok($"Great! The job {fireAndForgetJob} has been completed. The mail has been sent to the user.");
        }




        [HttpPost("delayed")]
        public IActionResult Delayed(string mail)
        {
            var delayed = _backgroundJobClient.Schedule(() => SendMail(mail), TimeSpan.FromSeconds(10));
            return Ok($"Great! The Delayed job with id: {delayed} has been added. The delayed mail has been scheduled to the user and will be sent within 1 minute.");
        }

        [HttpPost("recurring")]
        public IActionResult Recurring(string mail)
        {
            //id is task id!
            _recurringJobManager.AddOrUpdate("id", () => SendMail(mail), Cron.Weekly);
            return Ok($"The recurring job has been scheduled for user with mail: {mail}.");
        }

        ////////////////////////////////////////////////////////////////////////
        [HttpPost("continuation")]
        public IActionResult Continuation(string username, string mail)
        {
            var jobId = BackgroundJob.Enqueue(() => DeleteUserData(username));
            BackgroundJob.ContinueJobWith(jobId, () => SendConfirmationMailUponDataDeletion(mail));
            return Ok($"OK - Data for user with username: {username} has been deleted and a confirmation has been sent to: {mail}");
        }
        [NonAction]
        public void DeleteUserData(string username)
        {
            // Implement logic to delete data here for a specific user
            Console.WriteLine($"Deleted data for user {username}");
        }
        [NonAction]
        public void SendConfirmationMailUponDataDeletion(string mail)
        {
            Console.WriteLine($"Successfully sent deletion confirmation to mail: {mail}");
        }
        ////////////////////////////////////////////////////////////////////////
        [NonAction]
        public void SendMail(string mail)
        {
            // Implement any logic you want - not in the controller but in some repository.
            Console.WriteLine($"This is a test - Hello {mail}");
        }
        [NonAction]
        public void ExpireCodeAfterMinutes()
        {

             _jobService.ExpireCode();
        }

    }
}
