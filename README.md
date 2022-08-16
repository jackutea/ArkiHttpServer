# JackHttpServer
（目前自用的）简易 dotnet / mono Http 服务框架

## Quick Start
```
using JackFrame.HttpNS;

var server = new JackHttpServer(5010); // port

// Register Get
server.GetListen("/", (req, res) => {
    res.SendUTF8String("Hello World!");
});

// Also Register Post
server.PostListen("/login", (req, res) => {
    res.SendBuffer(new byte[] { 1 });
});

// Also Register Put
// server.PutListen...

// Also Register Delete
// server.DeleteListen...

server.Start();
```