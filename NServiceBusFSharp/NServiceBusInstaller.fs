namespace NServiceBusFSharp
open NServiceBus

type NServiceBusInstaller() =
    interface Castle.MicroKernel.Registration.IWindsorInstaller with
        member x.Install(container, store) =

            match System.Configuration.ConfigurationManager.AppSettings.["publishToQueue"] with
            | null -> failwith("InputQueue not defined in settings.")
            | "" -> failwith("InputQueue empty in settings.")
            | endpoint -> 
                let assemblies = [System.Reflection.Assembly.GetExecutingAssembly()]
                NServiceBus.Configure
                    .With(assemblies) // Assemblies to register
                    .DefineEndpointName(endpoint) // Queue to use
                    .CastleWindsorBuilder(container) // Use castle to resolve
                    .DefiningEventsAs(fun t -> (t.Namespace <> null) && t.Namespace.EndsWith("Events", false, System.Globalization.CultureInfo.CurrentCulture)) // register all classes from .Events namespaces as NServiceBus events
                    .MsmqSubscriptionStorage()
                    .XmlSerializer() // Events are xml-serialized
                    .MsmqTransport()
                    .UnicastBus()
                    .LoadMessageHandlers()
                    .CreateBus()
                    .Start(fun _ -> Configure.Instance.ForInstallationOn<NServiceBus.Installation.Environments.Windows>().Install());
                    |> ignore

                printf "NServiceBus installed. We send messages to: %s" endpoint
