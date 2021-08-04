using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Carly_Challenge_xUnit_Test
{
    public class BookingAPIUnitTest
    {
        [Fact]
        public async Task TestGetAllBookings()
        {
            var client = new TestClientProvider().Client;

            var response = await client.GetAsync("/api/bookings");

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
