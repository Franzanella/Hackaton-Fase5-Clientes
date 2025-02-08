# Hackaton-Grupo24-Cliente

Este repositório é dedicado a API de clientes. Neste foi utilizado o mysql como banco de dados

O deploy deste foi feito Utilizando aws Lambda - serveless
análise de código e cobertura de testes utilizando SonarCloud são realizados via Github Actions.



## Grupo 24 - Integrantes
💻 *<b>RM355456</b>*: Franciele de Jesus Zanella Ataulo </br>
💻 *<b>RM355476</b>*: Bruno Luis Begliomini Ataulo </br>
💻 *<b>RM355921</b>*: Cesar Pereira Moroni </br>


## Nome Discord:
Franciele RM 355456</br>
Bruno - RM355476</br>
Cesar P Moroni RM355921</br>

## Desenho da arquitetura
Quando disparamos a Github Action, é realizado o build da aplicação e deploy na LAMBDA .
Desenho com detalhes da infraestrutura do software



Para essa API, utilizamos .NET 8.0

## Testes

Utilizamos a ferramenta SonarCloud para análise de código e cobertura de testes. Para este microsserviço, atingimos acima de 80% de cobertura, conforme abaixo:

https://sonarcloud.io/summary/overall?id=Franzanella_Hackaton-Fase5-Clientes&branch=main

![image1](/assets/cobertTest.png)

![image2](/assets/cobertura.png)

![image3](/assets/cobertura2.png)

![image4](/assets/teste.png)

## BDD 
Utilizamos BDD para buscar um cliente: 

![image5](/assets/bdd.png)
