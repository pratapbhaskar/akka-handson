﻿using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace console_actors
{
    class Program
    {
        static void Main(string[] args)
        {
            ActorSystem actorSystem = ActorSystem.Create("ConsoleActorSystem");

            
            var consoleWriterProps = Props.Create(() =>
                new ConsoleWriterActor());
            var consoleWriterActor = actorSystem.ActorOf(consoleWriterProps,"consoleWriterActor");

            var validationActorProps = Props.Create(() =>
                new ValidationActor(consoleWriterActor));
            var validationActor = actorSystem.ActorOf(validationActorProps, "validationActor");

            Props consoleReaderProps = Props.Create<ConsoleReaderActor>(validationActor);
            var consoleReaderActor = actorSystem.ActorOf(consoleReaderProps, "consoleReaderActor");

            consoleReaderActor.Tell(ConsoleReaderActor.StartCommand);

            actorSystem.WhenTerminated.Wait();
        }
    }
}
