namespace NServiceBusFSharp.Listener
open NServiceBusFSharp.Events
open NServiceBus

type MyEventHandler() =
    interface IMessageHandler<MyEvent> with
        member x.Handle(evt :MyEvent) =
            printf "\nMessage from queue found: \n %s" evt.Message