## --Eligibles Listing API--

Está é uma API construída usando ASP.NET seguindo os princípios da Clean Architecture. Esta API permite a listagem, filtragem e paginação de clientes a partir de dados fornecidos em formatos CSV e JSON.

## Visão Geral

A API fornece endpoints para classificar e listar clientes elegíveis. Os dados são carregados de URLs configuráveis no `appsettings.json`.

## Estrutura do Projeto

A solução é organizada em quatro camadas principais seguindo a Clean Architecture:

- **EligiblesListingAPI.Api:** Ponto de entrada da aplicação.
  - Endpoints 
  - Configuração da API

- **EligiblesListingAPI.Infrastructure:** Comunicação com recursos externos, no nosso caso CSV e Json. 
  - Serviços Específicos de Infraestrutura

- **EligiblesListingAPI.Core:** Lógica de negócios da aplicação.  
  - Abstrações para lógica de negócios

- **EligiblesListingAPI.Domain:** Contém tudo o que deve ser compartilhado entre os projetos.
  - DTOs
  - Enums

## Arquitetura da API:
   - API em C# ASP.NET 8
   - API Rest com swagger
   - Linq
   - Injeção de dependêcia
   - Conceitos de Orientação a Objeto
   - Docker

### Pré-requisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Configuração do `appsettings.json`

No arquivo `appsettings.json`, configure as URLs para os dados CSV e JSON:

```json
{
  "SettingsCustomized": {
    "UrlDataCsv": "<SEU_LINK.csv>",
    "UrlDataJson": "<SEU_LINK.json>"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
```
### Instalação

1. Clone o repositório:
```bash
    git clone https://github.com/jhonatahaisten/EligiblesListing-API.git
    cd EligiblesListingAPI
```

2. Restaure as dependências:
```bash
    dotnet restore
```

3. Compile a solução:
```bash
    dotnet build
```

## Uso
### Executar a API

Para executar a API, use o comando:
```bash
    dotnet run --project EligiblesListingAPI
```

A API estará disponível em https://localhost:44389 por padrão.

### Endpoints Disponíveis
- POST /api/customers/eligibles: Classifica e lista clientes elegíveis.

  - Request Body:

  ```json
       {
        "pageNumber": 8,
        "pageSize": 1,
        "totalCount": 10,
        "users": [
      		{
         "Region": "sul",
      		"Type": "laborious"
		      }
          ]  
        }
  ```
  - Response:

  ```json
      [
    	{
    		"type": "laborious",
    		"gender": "f",
    		"name": {
    			"title": "mrs",
    			"first": "melina",
    			"last": "souza"
    		},
    		"location": {
    			"region": "sul",
    			"street": "1856 rua um",
    			"city": "garanhuns",
    			"state": "santa catarina",
    			"postcode": 51640,
    			"coordinates": {
    				"latitude": "-16.4160",
    				"longitude": "-93.7689"
    			},
    			"timezone": {
    				"offset": "+2:00",
    				"description": "Kaliningrad, South Africa"
    			}
    		},
    		"nationality": "BR",
    		"email": "melina.souza@example.com",
    		"birthday": "1963-11-03T13:17:36Z",
    		"registered": "2015-07-08T17:33:35Z",
    		"telephoneNumbers": [
    			"+552894017853"
    		],
    		"mobileNumbers": [
    			"+556111414458"
    		],
    		"picture": {
    			"large": "https://randomuser.me/api/portraits/women/46.jpg",
    			"medium": "https://randomuser.me/api/portraits/med/women/46.jpg",
    			"thumbnail": "https://randomuser.me/api/portraits/thumb/women/46.jpg"
    		}
    	}
      ]
  ```
### Executar via Docker

Construir a imagem Docker

  ```bash
docker build -f ./EligiblesListingAPI/Dockerfile -t case/eligibles-listing-api:latest .
  ```

Executar o Container Docker

  ```bash
docker run -it case/eligibles-listing-api:latest
  ```

### Executar Testes de Integração
Para executar os testes de integração, use o comando:

  ```bash
dotnet test
  ```
