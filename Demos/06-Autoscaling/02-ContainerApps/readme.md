# Scaling an Container hosted Azure Function using KEDA

- Execute `create-container-app.azcli` to run the demo.

Log Query:

```
ContainerAppConsoleLogs_CL | where ContainerAppName_s == 'queuereader' and Log_s contains 'Message ID' | project Time=TimeGenerated, AppName=ContainerAppName_s, Revision=RevisionName_s, Container=ContainerName_s, Message=Log_s | take 5
```