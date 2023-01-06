# ArkiHttpServer
（目前自用的）简易 dotnet / mono Http 服务框架

## Quick Start
``` C#
using JackFrame.HttpNS;

var server = new JackHttpServer(5010); // port

// Register Get
server.GetListen("/", async (req, res) => {
    await res.SendUTF8StringAsync("Hello World!");
});

// Also Register Post
server.PostListen("/login", async (req, res) => {
    await res.SendBufferAsync(new byte[] { 1 });
});

// Also Register Put
// server.PutListen...

// Also Register Delete
// server.DeleteListen...

server.Start();
```

## Todo （主要自己实现，也欢迎PR）
1. 静态资源请求
2. 线程安全处理
3. 常用数据序列化接口(Json / Protobuf / MessagePack)
4. 常用Header接口
5. 测试用例
6. Nuget 发布
6. UnityPackageManager 发布
