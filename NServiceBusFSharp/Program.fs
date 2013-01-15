namespace NServiceBusFSharp
open NServiceBusFSharp.Events
open NServiceBus

module Program =

    type EventPublisher(bus :IBus) =
        new(container :Castle.Windsor.IWindsorContainer) = 
            EventPublisher(container.Resolve<IBus>())

        member x.publish text =
            bus.Publish<MyEvent>(new MyEvent(text));

    [<EntryPoint>]
    let main argv = 

        let container = new Castle.Windsor.WindsorContainer()
        container.Install(new NServiceBusInstaller()) |> ignore

        let msg = "hello world!"
        printf "\nSending message: %s" msg
        EventPublisher(container).publish "hello world!"
        System.Console.ReadLine() |> ignore
        0