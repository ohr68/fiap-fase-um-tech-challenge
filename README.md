# FIAP.FaseUm.TechChallenge 

kubectl apply -f configmap.yaml
kubectl apply -f grafana-deployment.yaml
kubectl apply -f sqlserver-pvc.yaml
kubectl apply -f prometheus-pvc.yaml

kubectl apply -f deployment-api.yaml
kubectl apply -f service-api.yaml
kubectl apply -f deployment-worker.yaml
kubectl apply -f deployment-sql.yaml
kubectl apply -f service-sql.yaml
kubectl apply -f deployment-rabbitmq.yaml
kubectl apply -f service-rabbitmq.yaml
kubectl apply -f deployment-prometheus.yaml
kubectl apply -f service-prometheus.yaml
kubectl apply -f deployment-grafana.yaml
kubectl apply -f service-grafana.yaml

kubectl port-forward svc/api-fiap-service 8080:8080
kubectl port-forward svc/grafana-service 3000:3000
kubectl port-forward svc/rabbitmq-service 15672:15672