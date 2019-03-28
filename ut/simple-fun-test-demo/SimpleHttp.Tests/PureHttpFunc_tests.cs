using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace SimpleHttp.Tests
{
    public class PureHttpFunc_tests
    {
        [Fact]
        public async Task should_return_BadRequestObjectResult_if_no_name_is_present()
        {
            var request = new DefaultHttpRequest(new DefaultHttpContext());
            var logger = NullLoggerFactory.Instance.CreateLogger("null logger");

            var response = await PureHttpFunc.Run(request, logger);

            Assert.NotNull(response);
            Assert.IsType<BadRequestObjectResult>(response);
        }
    }
}
