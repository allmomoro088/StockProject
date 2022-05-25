# StockProject
## Descrição
Projeto para estudar uso da API do IEXCloud para obtenção de dados de ações através de input do usuário. Também atualizar os valores em "real-time".

# Setup

## Pré requisitos
1. **Conta do IEXCloud**: Obtenha o secret token no site da [IEXCloud](https://iexcloud.io/).
2. **Visual Studio**: Community ou superior (Windows).
3. **MySQL**: Banco de dados MySQL, instalado e configurado.

## Rodando localmente
Clone o faça download do projeto. É recomendado o uso de [GitBash](https://git-scm.com/downloads) ou [GitHub desktop](https://desktop.github.com/):

    git clone https://github.com/allmomoro088/StockProject.git

**Visual Studio**:

Abra o projeto no Visual Studio, vá no arquivo **appsettings.json** e ajuste as informações de acordo com o ambiente, definindo os seguintes valores:

**ConnectionStrings**:
- stocksdb: `YOUR_SQL_SERVER_AND_INSTANCE` para o local do servidor MySQL e a respectiva instância
- stocksdb: `YOUR_SQL_USER` para o nome de usuário a ser utilizado para autênticação
- stocksdb:`YOUR_SQL_PASSWORD` para a senha do respectivo usuário

**API**:
- SecretToken: `YOUR_SECRET` para o secret token obtido no site da [IEXCloud](https://iexcloud.io/)

**DBContext Migration**
Abra o Package Manager Console e execute os seguintes comandos:

    Add-Migration "First Migration"

    Update-Database

Inicie o aplicativo no Visual Studio e abra `http://localhost:5000`. Escreva algum símbolo no campo de texto e clique em **Search**. A aplicação trará em outra página o resultado da busca. Os valores `Latest Price` e a porcentagem de variação do ativo será atualizado via JavaScript a cada 5 segundo, fazendo requisições para o endpoint `http://localhost:5000/api/live/{{symbol}}`
