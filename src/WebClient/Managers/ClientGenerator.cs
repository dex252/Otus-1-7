using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClient.Managers
{
    internal class ClientGenerator
    {
        const string LAST_NAMES = "Альпака,Бадьян,Воробей,Горшко,Ежик,Муха,Комар,Колбаса,Макаронина,Чухчух,Чыхпых,Пугач,Валериков,Мячик,Хлеб,Изюм,Виноград,Конфета,Банан";
        const string FIRST_NAME = "Аня,Гена,Саша,Валера,Борис,Миша,Геннадий,Ольга,Света,Слава,Петр,Мария,Марина,Юля,Гриша,Кристина,Рафик,Дима,Лёша";
        private Random Random { get; }
        public ClientGenerator()
        {
            Random = new Random();
        }
        internal string GenerateFirstName()
        {
            return GetRandomValue(FIRST_NAME);
        }

        internal string GenerateLastName()
        {
            return GetRandomValue(LAST_NAMES);
        }

        private string GetRandomValue(string bundle)
        {
            var lastNames = bundle.Split(',');
            var next = Random.Next(0, lastNames.Length);
            return lastNames[next];
        }
    }
}
