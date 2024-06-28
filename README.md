**API de faturamento de clientes**
---------------------
Tecnologias Utilizadas
---------------------
* .NET 8.0
* MongoDB
* xUnit em testes unitários
* Postman para consulta de api externa
---------------------
Chamadas de Serviços
---------------------
Billing
------------
* Inserir billings no banco de dados
* URL: /api/Billing
* Método: POST
* Descrição: Faz uma requisição na api externa de billings e insere o registro de billing e billingLines caso o cliente e o produto existam no banco de dados.
---------------------
Customer
------------
* Buscar Cliente por Id
* URL: /api/Customer/{id}
* Método: GET
* Descrição: Retorna um Cliente especifico.
------------
* Deletar Cliente por Id
* URL: /api/Customer/{id}
* Método: DELETE
* Descrição: Deleta um Cliente especifico lógicamente, ou seja, não removendo o registro do banco mas sim atualizando uma flag isDeleted.
------------
* Inserir Cliente
* URL: /api/Customer/{id}
* Método: POST
* Descrição: Insere um Cliente especifico baseado nos campos do Body da requisição.
------------
* Atualizar Cliente por Id
* URL: /api/Customer/{id}
* Método: PUT
* Descrição: Atualiza um Cliente especifico baseado nos campos do Body da requisição.
---------------------
Product
------------
* Buscar Produto por Id
* URL: /api/Product/{id}
* Método: GET
* Descrição: Retorna um Produto especifico.
------------
* Deletar Produto por Id
* URL: /api/Product/{id}
* Método: DELETE
* Descrição: Deleta um Produto especifico lógicamente, ou seja, não removendo o registro do banco mas sim atualizando uma flag isDeleted.
------------
* Inserir Produto
* URL: /api/Product/{id}
* Método: POST
* Descrição: Insere um Produto especifico baseado nos campos do Body da requisição.
------------
* Atualizar Produto por Id
* URL: /api/Product/{id}
* Método: PUT
* Descrição: Atualiza um Produto especifico baseado nos campos do Body da requisição.
---------------------
Configurações para Rodar Projeto
---------------------
* Alterar connection string do mongodb dentro da classe appsettings.json
* Inserir connection string da api externa de billings dentro da classe BillingController
* ![image](https://github.com/moiseshhabitzreuter/ca-backend-test/assets/139796338/8770bf41-0594-4277-89bd-60d629c57f4d)

* Alterar de https para http na opção de rodar o projeto
* ![image](https://github.com/moiseshhabitzreuter/ca-backend-test/assets/139796338/a382b2c3-027a-4663-bf97-005bb8ad8224)
* Rodar o projeto(F5) e utilizar a janela swagger para fazer requisições
* Ao utilizar requisições POST e PUT, alterar flag isDeleted para false

---------------------
Melhorias planejadas
---------------------
* Separação de responsabilidades para classes de serviço e repositórios
* Melhorar cobertura de código
* Remover campo id do body das requisições POST e PUT
* Alterar valor default da flag isDeleted de true para false no body das requisições POST e PUT
* Adicionar validações de email
