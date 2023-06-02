using System;
using System.Threading.Tasks;
using WebClient.Http;

namespace WebClient.Managers
{
    sealed class ActionsManager
    {
        private IHttpClient HttpClient { get; }

        private ClientGenerator ClientGenerator { get; }

        private ActionsManager() { }

        public ActionsManager(IHttpClient httpClient)
        {
            this.HttpClient = httpClient;
            ClientGenerator = new ClientGenerator();
        }

        internal async Task GetCustomer()
        {
            await Console.Out.WriteLineAsync("Введите id клиента:");
            var read = await Console.In.ReadLineAsync();
            if (!long.TryParse(read, out var id))
            {
                throw new Exception("Не удалось преобразовать строку в идентификатор");
            }

            var client = await HttpClient.GetCustomerAsync(id);
            await Console.Out.WriteLineAsync($"{client.Id}: {client.Firstname}, {client.Lastname}");
        }

        internal async Task CreateCustomer(int count)
        {
            await Console.Out.WriteLineAsync("Генерируем данные по клиентам...");
            for (int i = 0; i < count; i++)
            {
                try
                {
                    await Console.Out.WriteLineAsync();
                    var clientFirstName = ClientGenerator.GenerateFirstName();
                    var clientLastName = ClientGenerator.GenerateLastName();
                    await Console.Out.WriteLineAsync($"{clientFirstName} {clientLastName} летит на сервер...");
                    var request = new CustomerCreateRequest(clientFirstName, clientLastName);
                    var id = await HttpClient.CreateCustomerAsync(request);
                    Console.WriteLine($"Клиента {clientFirstName} {clientLastName} успешно создан с id: {id}");
                }
                catch (Exception e)
                {
                    await Console.Out.WriteLineAsync(e.Message);
                }
                
            }
        }
    }
}
