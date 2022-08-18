using System;
using System.Text;
using JackFrame.HttpNS;

namespace JackFrame.SampleAPP {

    public class Sample {

        public static void Main(string[] args) {

            const int PORT = 5010;
            var server = new JackHttpServer(PORT);

            // Register
            server.GetListen("/", async (req, res) => {
                string content = "hello w";
                byte[] buffer = new byte[512];
                ArraySegment<byte> arr = await req.ReadBufferAsync(buffer);
                System.Console.WriteLine("Recv: " + Encoding.UTF8.GetString(arr.Array));
                await res.SendUTF8StringAsync(content);
            });

            server.PostListen("/add_package", async (req, res) => {
                byte[] buffer = new byte[1024];
                var count = await req.InputStream.ReadAsync(buffer, 0, buffer.Length);
                System.Console.WriteLine(count);
                await res.SendBufferAsync(new byte[] { 1 });
            });

            server.PutListen("/update_package", async (req, res) => {
                await res.SendBufferAsync(new byte[] { 2 });
            });

            server.DeleteListen("remove_package", async (req, res) => {
                await res.SendBufferAsync(new byte[] { 3 });
            });

            // Run
            server.Start();
            Console.WriteLine($"Listening:{PORT}");

            // Exit
            while (!Console.ReadLine().StartsWith("exit")) {

            }

            server.Stop();

        }

    }
}