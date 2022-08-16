using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace JackFrame.HttpNS {

    public class JackHttpServer {

        HttpListener listener;

        // Thread
        Task task;
        CancellationTokenSource cancellationTokenSource;

        // Events
        public delegate void OnRequestDelegate(HttpListenerRequest req, HttpListenerResponse res);
        SortedDictionary<int, OnRequestDelegate> getListenDic; // key: raw uri without prefix like "/index"
        SortedDictionary<int, OnRequestDelegate> postListenDic;
        SortedDictionary<int, OnRequestDelegate> putListenDic;
        SortedDictionary<int, OnRequestDelegate> deleteListenDic;

        public JackHttpServer(int port) {

            this.listener = new HttpListener();
            this.listener.Prefixes.Add($"http://localhost:{port}/");

            this.getListenDic = new SortedDictionary<int, OnRequestDelegate>();
            this.postListenDic = new SortedDictionary<int, OnRequestDelegate>();
            this.putListenDic = new SortedDictionary<int, OnRequestDelegate>();
            this.deleteListenDic = new SortedDictionary<int, OnRequestDelegate>();

            this.cancellationTokenSource = new CancellationTokenSource();
            this.task = new Task(async () => {

                try {

                    while (!cancellationTokenSource.IsCancellationRequested) {

                        var ctx = await listener.GetContextAsync();
                        var req = ctx.Request;
                        var res = ctx.Response;
                        string uri = req.RawUrl;
                        Trigger(req.HttpMethod, uri.Split('?')[0], req, res);

                    }

                } catch {

                    throw;

                } finally {

                    listener.Stop();

                }

            });

        }

        // ==== GET ====
        public void GetListen(string uri, OnRequestDelegate callback) {
            getListenDic.Add(uri.GetHashCode(), callback);
        }

        public void PostListen(string uri, OnRequestDelegate callback) {
            postListenDic.Add(uri.GetHashCode(), callback);
        }

        public void PutListen(string uri, OnRequestDelegate callback) {
            putListenDic.Add(uri.GetHashCode(), callback);
        }

        public void DeleteListen(string uri, OnRequestDelegate callback) {
            deleteListenDic.Add(uri.GetHashCode(), callback);
        }

        void Trigger(string method, string uri, HttpListenerRequest req, HttpListenerResponse res) {

            SortedDictionary<int, OnRequestDelegate> dic;
            if (method == "GET") {
                dic = getListenDic;
            } else if (method == "POST") {
                dic = postListenDic;
            } else if (method == "PUT") {
                dic = putListenDic;
            } else if (method == "DELETE") {
                dic = deleteListenDic;
            } else {
                throw new Exception($"What's this method: {method}?");
            }

            dic.TryGetValue(uri.GetHashCode(), out var callback);
            if (callback == null) {
                Console.Error.WriteLine($"No uri: {uri}");
                return;
            }
            callback.Invoke(req, res);

        }

        public void Start() {
            listener.Start();
            task.Start();
        }

        public void Stop() {
            cancellationTokenSource.Cancel();
        }

    }
}