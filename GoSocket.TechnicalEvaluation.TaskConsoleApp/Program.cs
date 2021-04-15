using GoSocket.TechnicalEvaluation.Scheme;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSocket.TechnicalEvaluation.TaskConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Proxy proxy = new Proxy();

            var task1_Json = proxy.SendGet($"api/WorkXML/GetNumberAreas", null);
            var responseTask1 = JsonConvert.DeserializeObject<ResponseService>(task1_Json);
            if (responseTask1.IsSuccess)
                Console.WriteLine($"El número de áreas total son: {responseTask1.Data}");
            else
                Console.WriteLine($"Ocurrio un error: {responseTask1.Message}");

            var task2_Json = proxy.SendGet($"api/WorkXML/GetAreasPerEmployee?nodesPerEmployee=2", null);
            var responseTask2 = JsonConvert.DeserializeObject<ResponseService>(task2_Json);
            if (responseTask2.IsSuccess)
                Console.WriteLine($"El número de áreas total con mayor a 2 empelados es: {responseTask2.Data}");
            else
                Console.WriteLine($"Ocurrio un error: {responseTask2.Message}");

            var task3_Json = proxy.SendGet($"api/WorkXML/GetInformationSalary", null);
            var responseTask3 = JsonConvert.DeserializeObject<ResponseService>(task3_Json);
            if (responseTask3.IsSuccess)
            {
                Console.WriteLine($"El Salario total por área es:");
                foreach (var item in responseTask3.Data)
                {
                    Console.WriteLine($"{item.AreaName}|{item.TotalSalary}");
                }

            }

            else
                Console.WriteLine($"Ocurrio un error: {responseTask3.Message}");
        }
    }
}
