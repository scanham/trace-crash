FROM microsoft/dotnet:2.2-sdk as app

WORKDIR /src
COPY . .
RUN dotnet --info && \
    dotnet restore

RUN dotnet publish . -o /app --configuration Release -f netcoreapp2.2   

FROM microsoft/dotnet:2.2-runtime

ADD https://github.com/DataDog/dd-trace-dotnet/releases/download/v1.1.0/datadog-dotnet-apm_1.1.0_amd64.deb .
RUN dpkg -i ./datadog-dotnet-apm_1.1.0_amd64.deb

ENV CORECLR_ENABLE_PROFILING=1
ENV CORECLR_PROFILER={846F5F1C-F9AE-4B07-969E-05C26BC060D8}
ENV CORECLR_PROFILER_PATH=/opt/datadog/Datadog.Trace.ClrProfiler.Native.so
ENV DD_INTEGRATIONS=/opt/datadog/integrations.json

COPY --from=app /app /app
WORKDIR /app
ENTRYPOINT dotnet trace-crash.dll