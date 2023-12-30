
using BL;
using BL.Repos;
using Entities;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SharedModels;
using SpaceRouteService.Controllers;
using SpaceRouteService.Models;
using System.Text;
using System.Threading.Channels;

namespace SpaceRouteService.RabbitMQ
{
    public class RabbitMQListener : BackgroundService
    {
        private IConnection connection;
        private IModel channel;

        public RabbitMQListener()
        {
            var factory = new ConnectionFactory()
            {
                HostName = ServiceAddress.QueueServer,
                Port = ServiceAddress.QueuePort,
                UserName = ServiceAddress.QueueUser,
                Password = ServiceAddress.QueuePassword,
            };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare(queue: ServiceAddress.QueueName,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);
        }

        private ISpacePortRepo SpacePortRepo => RepoKeeper.Instance.SpacePortRepo;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var request = JsonConvert.DeserializeObject<SharedModels.BuildOrderRequest>(content);
                OrderModel model;
                var id2objkeepindex = MakeOrderController.id2objkeepindex;
                lock (ObjectKeeper.Instance)
                {
                    lock (id2objkeepindex)
                    {
                        model = new()
                        {
                            Cost = 0,
                            Driver = null,
                            Spaceship = null,
                            LoadingPort = null,
                            UnloadingPort = null,
                            ScheduleElements = null,
                            LoadingTime = request.LoadingTime,
                            UnloadingTime = request.UnloadingTime,
                            TotalTime = 0,
                            Volume = request.Volume,
                            Customer = request.Customer,
                            Ready = false,
                            Discarded = false,
                        };
                        int index = ObjectKeeper.Instance.Keep(model);
                        model.Index = index;
                        id2objkeepindex.Add(request.ID, index);
                    }
                }
                void Discard()
                {
                    model.Discarded = true;
                }
                var loadingPort = SpacePortRepo.FindByName(request.LoadingPortName);
                if (loadingPort is null) { Discard(); return; }
                var unloadingPort = SpacePortRepo.FindByName(request.UnloadingPortName);
                if (unloadingPort is null) { Discard(); return; }
                model.LoadingPort = loadingPort;
                model.UnloadingPort = unloadingPort;

                var (scheduleElements, cost, time, driver, spaceship) = PlanetGraph.GetBestWay(loadingPort, unloadingPort, request.LoadingTime, request.UnloadingTime, request.Volume);
                if (scheduleElements is null || driver is null || spaceship is null)
                { Discard(); return; }
                if (scheduleElements.Count == 0)
                { Discard(); return; }

                lock(ObjectKeeper.Instance)
                {
                    lock(id2objkeepindex)
                    {
                        model.Cost = cost;
                        model.Driver = driver;
                        model.Spaceship = spaceship;
                        model.ScheduleElements = scheduleElements;
                        model.TotalTime = time;
                        model.Ready = true;
                        model.Discarded = false;
                    }
                }

                //channel.BasicAck(ea.DeliveryTag, false);
            };

            channel.BasicConsume(ServiceAddress.QueueName, true, consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            channel.Close();
            connection.Close();
            base.Dispose();
        }
    }
}
