namespace RUCSpeciale
{
    /// <summary>
    /// ServiceBus Topic Definitions. 
    /// Generated from T4 template ServiceBusConfigurations.tt  
    /// </summary>
    public static class ServiceBusTopics {
     /// <summary>
    /// Generated from T4 template ServiceBusConfigurations.tt  
    /// </summary>
    public const string BookingCreated = "sbt-bookingcreated";
    }
    /// <summary>
    /// ServiceBus Queue Definitions.
    /// Generated from T4 template ServiceBusConfigurations.tt  
    /// </summary>
    public static class ServiceBusQueues {
    /// <summary>
    /// Generated from T4 template ServiceBusConfigurations.tt  
    /// </summary>
      public const string demoQueue = "rucspeciale-queue";
      public const string BookingCreatedQueue = "sbq-bookingcreated";
      public const string UserHandlerQueue = "sbq-userhandler";
    }
    /// <summary>
    /// ServiceBus Subscription Definitions.
    /// Generated from T4 template ServiceBusConfigurations.tt  
    /// </summary>
    public static class ServiceBusTopicsSubscriptions {
    /// <summary>
    /// Generated from T4 template ServiceBusConfigurations.tt  
    /// </summary>
      public const string BookingConfirmation = "sbts-bookingconfirmation";
    }
}

