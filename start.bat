docker build -t aspcore-service-a -f src\OpenTelemetryDemo\OpenTelemetryDemo.ServiceA\Dockerfile .
docker build -t aspcore-service-b -f src\OpenTelemetryDemo\OpenTelemetryDemo.ServiceB\Dockerfile .
docker-compose up