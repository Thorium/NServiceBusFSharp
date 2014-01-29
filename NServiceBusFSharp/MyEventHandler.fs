namespace NServiceBusFSharp.Listener
open NServiceBusFSharp.Events
open NServiceBus

type MyEventHandler() =
    interface IHandleMessages<MyEvent> with
        member x.Handle(evt :MyEvent) =
            printf "\nMessage from queue found: \n %s" evt.Message