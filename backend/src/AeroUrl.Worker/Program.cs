using System;
using System.Collections.Generic;
using System.Text;

namespace AeroUrl.Worker;


public class Program
{

    public static void Main(string[] args){

        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddHostedService<Worker>();

        var host = builder.Build();

        host.Run();
    }

}
