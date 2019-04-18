# trace-crash

This console application demonstrates a `StackOverflowException` when using a custom `HttpClientHandler` while tracing with `dd-trace-dotnet`.

## Usage

Clone the respository and execute the following commands:

```
docker build -t trace-crash .
docker run -it --env DD_TRACE_ENABLED=false trace-crash
```

The output should show 2 successful calls to `https://www.google.com/`. The first using a basic `HttpClient` and the second using an `HttpClient` with a custom handler.

Now to see the crash execute: 

```
docker run -it --env DD_TRACE_ENABLED=true trace-crash
```

The first call is again successful but the second call gets stuck in a recursive loop ultimately resulting in a `StackOverflowException`