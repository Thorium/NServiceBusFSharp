namespace NServiceBusFSharp.Events

[<System.Serializable>]
type MyEvent(m :string) =
    member val Message = m with get, set