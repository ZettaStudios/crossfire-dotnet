using System;
using System.Net;
using System.Net.Sockets;
using Game.Model;
using Game.Network;
using Game.Session;
using Game.Task;
using Newtonsoft.Json;
using Shared;
using Shared.Enum;
using Shared.Exceptions;
using Shared.Util;

namespace Game {
    public class GameServer : Server
    {
        public bool LoadedSettings;
        public int ChannelsCount = 5;
        public GameServer(string[] args) : base(args)
        {
            if (args.Length > 0)
            {
                int id = int.Parse(args[0]);
                Internet.Get("http://localhost:3000/", $"server/unique/{id}", result =>
                {
                    ServerSettingsRequest settings = JsonConvert.DeserializeObject<ServerSettingsRequest>(result);
                    if (settings != null && settings.Status != (int) HttpStatusCode.NotFound)
                    {
                        name = settings.Server.Name;
                        maxConnections = (int) settings.Server.Limit;
                        port = settings.Server.Port;
                        type = ServerType.Game;
                        LoadedSettings = true;
                        network = new GameNetwork();
                        RegisterDefaultSchedulers();
                    }
                    else
                    {
                        throw new InstantiateServerException ("An error occurred while trying connect to API.") { Error = InstantiateError.NotFoundServerId };
                    }
                }, error => throw new InstantiateServerException ("An error occurred while trying connect to API.") { Error = InstantiateError.NotFoundServerId });
            }
            else
            {
                throw new InstantiateServerException ("Please, include an Target Server Id in program args.") { Error = InstantiateError.NotFoundServerId };
            }
        }

        public void RegisterDefaultSchedulers()
        {
            Scheduler.AddTask(new AnnouncementTask(scheduler), 1, true);
        }
        
        public override void OnRun(TcpClient client)
        {
            if (sessions.Count < maxConnections)
            {
                GameSession session = new GameSession(this, client);
                session.Start();
            }
            else
            {
                client.Client.Shutdown(SocketShutdown.Both);
            }
            base.OnRun(client);
        }
    }
}