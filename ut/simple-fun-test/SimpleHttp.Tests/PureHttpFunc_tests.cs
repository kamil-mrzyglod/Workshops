using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
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

        
        [Fact]
        public async Task should_return_OkObjectResult_if_name_is_present_in_query_string()
        {
            var name = "Tom";
            var expected = $"Hello, {name}";

            var context = new DefaultHttpContext();
            var request = new DefaultHttpRequest(context) {
                QueryString = new QueryString($"?name={name}")
            };

            var logger = NullLoggerFactory.Instance.CreateLogger("null logger");

            var response = await PureHttpFunc.Run(request, logger);

            Assert.NotNull(response);
            var obj = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(expected, obj.Value);
        }

        [Fact]
        public async Task should_return_OkObjectResult_if_name_is_present_in_body()
        {
            var name = "Tom";
            var expected = $"Hello, {name}";

            var request = CreateRequest(new { name = name });

            var logger = NullLoggerFactory.Instance.CreateLogger("null logger");

            var response = await PureHttpFunc.Run(request, logger);

            Assert.NotNull(response);
            var obj = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(expected, obj.Value);
        }


        private static DefaultHttpRequest CreateRequest(object body)
        {            
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);
        
            var json = JsonConvert.SerializeObject(body);
        
            sw.Write(json);
            sw.Flush();
        
            ms.Position = 0;
        
            var mockRequest = new DefaultHttpRequest(new DefaultHttpContext());
            mockRequest.Body = ms;
        
            return mockRequest;
        }
    }
}
