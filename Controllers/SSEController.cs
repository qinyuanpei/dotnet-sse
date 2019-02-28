using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using DotNet_SSE.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Newtonsoft.Json;
using Remotion.Linq.Clauses;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotNet_SSE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SSEController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            //组织HTTP响应头
            Response.Headers.Add("Connection", "keep-alive");
            Response.Headers.Add("Cache-Control", "no-cache");
            Response.Headers.Add("Content-Type", "text/event-stream");

            //发送自定义事件
            var message = BuildSSE(new { Content = "SSE开始发送消息", Time = DateTime.Now }, "SSE_Start");
            Response.Body.Write(message, 0, message.Length);

            //每隔10秒钟向客户端发送一条消息
            while (true)
            {
                message = BuildSSE(new { Content = $"当前时间为{DateTime.Now}" });
                Response.Body.Write(message, 0, message.Length);
                Thread.Sleep(10000);
            }
        }

        /// <summary>
        /// 构造SSE消息
        /// </summary>
        /// <typeparam name="T">typeof(T)</typeparam>
        /// <param name="message">消息内容</param>
        /// <param name="eventName">事件名称，默认为空</param>
        /// <param name="retry">重连间隔，默认30秒</param>
        /// <returns></returns>
        private byte[] BuildSSE<TMessage>(TMessage message, string eventName = null, int retry = 30000)
        {
            var builder = new StringBuilder();
            builder.Append($"id:{Guid.NewGuid().ToString("N")}\n");
            if (!string.IsNullOrEmpty(eventName))
                builder.Append($"event:{eventName}\n");
            builder.Append($"retry:{retry}\n");
            builder.Append($"data:{JsonConvert.SerializeObject(message)}\n\n");
            return Encoding.UTF8.GetBytes(builder.ToString());
        } 
    }

    
}
