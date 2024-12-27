# RazorLibrary
API para cadastro, edição, remoção e consulta de livros. 
Aplicação feita com base no desafio da [https://devchallenge-v2.vercel.app/details/e3604e84-d7d5-4543-8a8e-ce4f5c2187d6](DevChallenge)

# Arquitetura

Foi adotado o uso da arquitetura hexagonal.

## Camadas

### Core

Contém as aplicações core do negócio:

- RazorLibrary.Application -> contém as implementações de regras de negócio, como: serviços, validações, etc.
- RazorLibrary.Domain -> contém as interfaces e abstrações da aplicação. Não utiliza nenhum
pacote externo.

### Adapters

#### Driven (Adaptador dirigido)

Camada que invoca serviços externos da solução, como conexão com banco de dados.

- RazorLibrary.Infra -> contém as implementações e conexão com serviços externos, como:
	- Conexão com o SQL Server;
	- Implementação dos Repositories (interação com o banco de dados);
	- Migrations;
	- Configuração do DbContext (Entity Framework).

#### Driving (adaptador condutor)

Porta de entrada do hexagono. Camada que recebe o "input" dos usuários e efetua o processamento,
invocando as regras de negócio definidas na aplicação.

- RazorLibrary.API -> porta de entrada da aplicação. Recebe os dados do usuário e encaminha para o Core por 
meio das portas (interfaces).

# Referências

- https://devchallenge-v2.vercel.app/details/e3604e84-d7d5-4543-8a8e-ce4f5c2187d6;
- https://www.udemy.com/course/introducao-a-arquitetura-hexagonal/;
- https://alexalvess.medium.com/organizando-seu-projeto-net-com-arquitetura-hexagonal-parte-02-fe9a8ed6ab02