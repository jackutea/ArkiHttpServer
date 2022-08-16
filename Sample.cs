using JackFrame.HttpNS;

namespace JackFrame.SampleAPP {

    public class Sample {

        public static void Main(string[] args) {

            const int PORT = 5010;
            var server = new JackHttpServer(PORT);

            // Register
            server.GetListen("/", async (req, res) => {
                string content = "hello w";
                await res.SendUTF8String(content);
            });

            server.PostListen("/add_package", async (req, res) => {
                await res.SendBuffer(new byte[] { 1 });
            });

            server.PutListen("/update_package", async (req, res) => {
                await res.SendBuffer(new byte[] { 2 });
            });

            server.DeleteListen("remove_package", async (req, res) => {
                await res.SendBuffer(new byte[] { 3 });
            });

            // Run
            server.Start();
            Console.WriteLine($"Listening:{PORT}");

            // Exit
            while (!Console.ReadLine()!.StartsWith("exit")) {

            }

            server.Stop();

        }

    }
}