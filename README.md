# RazorLibrary
API para cadastro, edi��o, remo��o e consulta de livros. 
Aplica��o feita com base no desafio da [https://devchallenge-v2.vercel.app/details/e3604e84-d7d5-4543-8a8e-ce4f5c2187d6](DevChallenge)

# Arquitetura

Foi adotado o uso da arquitetura hexagonal.

## Camadas

### Core

Cont�m as aplica��es core do neg�cio:

- RazorLibrary.Application -> cont�m as implementa��es de regras de neg�cio, como: servi�os, valida��es, etc.
- RazorLibrary.Domain -> cont�m as interfaces e abstra��es da aplica��o. N�o utiliza nenhum
pacote externo.

### Adapters

#### Driven (Adaptador dirigido)

Camada que invoca servi�os externos da solu��o, como conex�o com banco de dados.

- RazorLibrary.Infra -> cont�m as implementa��es e conex�o com servi�os externos, como:
	- Conex�o com o SQL Server;
	- Implementa��o dos Repositories (intera��o com o banco de dados);
	- Migrations;
	- Configura��o do DbContext (Entity Framework).

#### Driving (adaptador condutor)

Porta de entrada do hexagono. Camada que recebe o "input" dos usu�rios e efetua o processamento,
invocando as regras de neg�cio definidas na aplica��o.

- RazorLibrary.API -> porta de entrada da aplica��o. Recebe os dados do usu�rio e encaminha para o Core por 
meio das portas (interfaces).

# Refer�ncias

- https://devchallenge-v2.vercel.app/details/e3604e84-d7d5-4543-8a8e-ce4f5c2187d6;
- https://www.udemy.com/course/introducao-a-arquitetura-hexagonal/;
- https://alexalvess.medium.com/organizando-seu-projeto-net-com-arquitetura-hexagonal-parte-02-fe9a8ed6ab02