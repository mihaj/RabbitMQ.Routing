# MassTransit and RabbitMQ routing to tenant-specific consumers on .NET

There are 3 projects.
1. Core - shared classes.
2. Sender - A simple project which sends the message.
3. Consumer - A consumer project that accepts the message for a tenant defined in configuration.

## Try it

### 1. Setup your RabbitMQ with default settings
Default is set in the Sender and Consumer projects, but you can change them to your needs:
```csharp
configurator.Host("localhost",
    5672,
    "/", h =>
    {
       h.Username("guest");
       h.Password("guest");
    });
```

### 2. Configure and Run a consumer
Open the `appsettings.json` in the consumer and populate the tenantID you want to use for the consumer. For example:
```json
{
...
    "TenantId": "f49fc7a9-155a-4d84-86bd-8276431524e1",
...
}
```
Now you can run the consumer, which will generate the RabbitMQ infrastructure and start listening for the messages.


### 3. Run Sender and send a message
Run `Sender service` and go to the `/swagger` and execute the `sender` endpoint. The below action will be triggered. Use the tenantId you specified in the step 2 above.
```csharp
[HttpGet]
public async Task Send(string tenantId)
{
   await _sender.Publish(new MyMessage()
    {
        ID = "23423",
        Name = "My message",
        TenantId = tenantId
    });

   Ok();
}
```

Now you can try to run the second consumer with a different tenantID and try to send messages to both :).