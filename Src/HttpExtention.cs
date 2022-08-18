using System;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using System.Net;

namespace JackFrame.HttpNS {

    public static class HttpExtention {

        // Request
        public static async Task<ArraySegment<byte>> ReadBufferAsync(this HttpListenerRequest req, byte[] buffer) {
            int count = await req.InputStream.ReadAsync(buffer, 0, buffer.Length);
            ArraySegment<byte> arr = new ArraySegment<byte>(buffer, 0, count);
            return arr;
        }

        public static ArraySegment<byte> ReadBuffer(this HttpListenerRequest req, byte[] buffer) {
            int count = req.InputStream.Read(buffer, 0, buffer.Length);
            ArraySegment<byte> arr = new ArraySegment<byte>(buffer, 0, count);
            return arr;
        }
        
        // Response
        public static void SendUTF8String(this HttpListenerResponse res, string content) {
            byte[] buffer = Encoding.UTF8.GetBytes(content);
            res.ContentLength64 = buffer.Length;
            Stream output = res.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }

        public static async Task SendUTF8StringAsync(this HttpListenerResponse res, string content) {
            byte[] buffer = Encoding.UTF8.GetBytes(content);
            res.ContentLength64 = buffer.Length;
            Stream output = res.OutputStream;
            await output.WriteAsync(buffer, 0, buffer.Length);
            output.Close();
        }

        public static void SendBuffer(this HttpListenerResponse res, byte[] buffer) {
            res.ContentLength64 = buffer.Length;
            Stream output = res.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }

        public static async Task SendBufferAsync(this HttpListenerResponse res, byte[] buffer) {
            res.ContentLength64 = buffer.Length;
            Stream output = res.OutputStream;
            await output.WriteAsync(buffer, 0, buffer.Length);
            output.Close();
        }

    }

}