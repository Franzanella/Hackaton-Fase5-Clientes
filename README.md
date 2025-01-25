# Hackaton FIAP - G24 FASE 5

API desenvolvida para autenticar usuarios no sistema de processamento de imagens



- Vídeo: 


## Grupo 24 - Integrantes
💻 *<b>RM355456</b>*: Franciele de Jesus Zanella Ataulo </br>
💻 *<b>RM355476</b>*: Bruno Luis Begliomini Ataulo </br>
💻 *<b>RM355921</b>*: Cesar Pereira Moroni </br>


## Nome Discord:
Franciele RM 355456</br>
Bruno - RM355476</br>
Cesar P Moroni RM355921</br>

## Desenho da arquitetura

Desenho com detalhes da infraestrutura do software





Execute o Docker Engine.

Abra um terminal e execute o comando iniciar o minikube:


```
minikube start

```


Também no terminal, acesse a pasta kubernetes no arquivo cmd_init.txt dentro do projeto e execute os comandos:

```
kubectl apply -f database-configMap.yaml
kubectl apply -f database-pv.yaml
kubectl apply -f database-pvc.yaml
kubectl apply -f database-secrets.yaml
kubectl apply -f database-service.yaml
kubectl apply -f database-deployment.yaml
kubectl apply -f app-configmap.yaml
kubectl apply -f app-deployment.yaml
kubectl apply -f app-hpa.yaml
kubectl apply -f app-secrets.yaml
kubectl apply -f app-service.yaml
kubectl apply -f adminer-deployment.yaml
kubectl apply -f adminer-service.yaml

```



Execute o comando a seguir para visualizar os recursos criados no ambiente Kubernetes:


```
minikube dashboard

```


![kubernetes](assets/imagem15.png)


Abra o terminal na pasta kubernetes e execute o comando abaixo para expor o Adminer na porta 8090:


```

kubectl port-forward svc/adminer 8090:8080

```

Acesse o Adminer no browser: http://localhost:8090/

![sql](assets/imagem14.png)


```
Servidor: sqlserver
Usuário: SA
Senha: YourStrong!Passw0rd
Base de Dados: HackatonBD

```
Importe o arquivo scriptInserts.sql que esta na pasta API e clique em executar.


 
</br>
<b>Como acessar a API</b>:
</br>



Abra outro terminal e execute o comando

```
kubectl port-forward svc/app 8080:80

```
 
<b>API</b>: http://localhost:8080/swagger/index.html
</br>


</br>
</br>










