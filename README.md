# NSB behavior: Investigation

TL;DR

Behavior for outgoing messages may be instantiated more than once, if registered using 

```csharp
config.Pipeline.Register(typeof(MyBehavior), "Sample behavior");
```

In this case, an instance of `MyBehavior` is created for each pipeline, i.e.

- `NServiceBus.PipelineComponent.CreatePipeline<NServiceBus.Pipeline.IOutgoingSendContext>`
- `NServiceBus.PipelineComponent.CreatePipeline<NServiceBus.Pipeline.IOutgoingReplyContext>`
- `NServiceBus.PipelineComponent.CreatePipeline<NServiceBus.Pipeline.IOutgoingPublishContext>`
