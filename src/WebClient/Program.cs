using System;
using System.Threading.Tasks;
using WebClient.Http;
using WebClient.Managers;

namespace WebClient
{
    static class Program
    {
        /// <summary>
        /// URL адрес сервиса
        /// </summary>
        const string URL_WEB_API = @"http://localhost:5000";
        static async Task Main(string[] args)
        {
            await Run();
        }

        static async Task Run()
        {
            var restClient = new HttpClient(URL_WEB_API);
            var actionManager = new ActionsManager(restClient);

            await Console.Out.WriteLineAsync("Выберите желаемое действие:");

            while (!Console.KeyAvailable)
            {
                await PrintActions();
                await Task.Delay(100);

                try
                {
                    var input = Console.ReadKey(true);
                    switch (input.Key)
                    {
                        case ConsoleKey.D1:
                        {
                            await actionManager.GetCustomer();
                            continue;
                        }
                        case ConsoleKey.D2:
                        {
                            await actionManager.CreateCustomer(1);
                            continue;
                        }
                        case ConsoleKey.D3:
                        {
                            await actionManager.CreateCustomer(5);
                            continue;
                        }
                        case ConsoleKey.D4:
                        {
                            return;
                        }
                        default:
                        {
                            await Console.Out.WriteLineAsync("Доступны только клавишы 1,2,3,4 для выполнения действий...");
                            continue;
                        }
                    }

                }
                catch (Exception e)
                {
                    await Console.Out.WriteLineAsync(e.Message);
                }

                await Console.Out.WriteLineAsync("\nВыберите одно из доступных действий");
            }

        }

        static async Task PrintActions()
        {
            await Console.Out.WriteLineAsync("1: Получить информацию о клиенте по id");
            await Console.Out.WriteLineAsync("2: Сгененировать случайного пользователя");
            await Console.Out.WriteLineAsync("3: Сгененировать 5 случайных пользователей");
            await Console.Out.WriteLineAsync("4: Выход из приложения");
        }
    }
}