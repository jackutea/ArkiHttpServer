using System.Text;
using System.Net;
using System.Net.Http;

namespace JackFrame.HttpNS {

    public static class HttpExtention {

        public static async Task SendUTF8String(this HttpListenerResponse res, string content) {
            byte[] buffer = Encoding.UTF8.GetBytes(content);
            res.ContentLength64 = buffer.Length;
            Stream output = res.OutputStream;
            await output.WriteAsync(buffer, 0, buffer.Length);
            output.Close();
        }

        public static async Task SendBuffer(this HttpListenerResponse res, byte[] buffer) {
            res.ContentLength64 = buffer.Length;
            Stream output = res.OutputStream;
            await output.WriteAsync(buffer, 0, buffer.Length);
            output.Close();
        }

    }

}