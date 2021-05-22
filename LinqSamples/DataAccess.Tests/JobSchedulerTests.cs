using System;
using System.Threading.Tasks;
using JobScheduler;
using Moq;
using Xunit;

namespace DataAccess.Tests
{
    public class JobSchedulerTests
    {
        [Fact]
        public async Task Start_ShouldRun_ExecutesJob()
        {
            // arrange
            var mockedJob = new Mock<IJob>();
            mockedJob.Setup(x => x.ShouldRun(It.IsAny<DateTime>())).Returns(true);

            var scheduler = new JobScheduler.JobScheduler(200);
            scheduler.RegisterJob(mockedJob.Object);

            // act
            scheduler.Start();
            await Task.Delay(250);

            // assert
            mockedJob.Verify(x => x.Execute(It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public async Task Start_ShouldNotRun_DoesNotExecuteJob()
        {
            // arrange
            var mockedJob = new Mock<IJob>();
            mockedJob.Setup(x => x.ShouldRun(It.IsAny<DateTime>())).Returns(false);

            var scheduler = new JobScheduler.JobScheduler(200);
            scheduler.RegisterJob(mockedJob.Object);

            // act
            scheduler.Start();
            await Task.Delay(250);

            // assert
            mockedJob.Verify(x => x.Execute(It.IsAny<DateTime>()), Times.Never);
        }

        [Fact]
        public async Task Start_ThrowsException_MarksJobAsFailed()
        {
            // arrange
            var mockedJob = new Mock<IJob>();
            mockedJob.Setup(x => x.ShouldRun(It.IsAny<DateTime>())).Returns(true);
            mockedJob.Setup(x => x.Execute(It.IsAny<DateTime>())).Throws<Exception>();

            var scheduler = new JobScheduler.JobScheduler(200);
            scheduler.RegisterJob(mockedJob.Object);

            // act
            scheduler.Start();
            await Task.Delay(300);

            // assert
            mockedJob.Verify(x => x.MarkAsFailed(), Times.AtLeast(1));
        }

        // 1. test stop
        // 2. test delayed job
        // 3. test job with our repositorys
    }
}
