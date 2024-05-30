# Code Challenge Juntos Somos+
## --Eligibles Listing API--

Está é uma API construída usando ASP.NET seguindo os princípios da Clean Architecture. Esta API permite a listagem e filtragem de clientes a partir de dados fornecidos em formatos CSV e JSON.
- API Rest com swagger
, Swagger, Linq, Injeção de dependecia, conceitos de orientação a objeto, Docker,

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
  - Abstrações para lógica de negócios e infraestrutura

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
    "UrlDataCsv": "https://storage.googleapis.com/juntossomosmais-code-challenge/input-backend.csv",
    "UrlDataJson": "https://storage.googleapis.com/juntossomosmais-code-challenge/input-backend.json"
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
    git clone https://github.com/seu-usuario/CaseJuntosSomosMais.git
    cd eligibles-listing-api
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
    dotnet run --project Auth.Api
```

A API estará disponível em https://localhost:44389 por padrão.

### Endpoints Disponíveis
- POST /api/customers/eligibles: Classifica e lista clientes elegíveis.

  - Request Body:

  ```json
        {
      "PageNumber": 1,
      "PageSize": 10,
      "Users": [
        {
          "Region": "Nordeste",
          "Type": "laborious"
        }
      ]
    }
  ```
  - Response:

  ```json
  [
    {
      "Type": "laborious",
      "Gender": "F",
      "Name": {
        "Title": "ms",
        "First": "jerueza",
        "Last": "moura"
      },
      "Location": {
        "Region": "Nordeste",
        "Street": "416 rua três",
        "City": "magé",
        "State": "alagoas",
        "Postcode": 73804,
        "Coordinates": {
          "Latitude": "15.3004",
          "Longitude": "-82.4361"
        },
        "Timezone": {
          "Offset": "+4:00",
          "Description": "Abu Dhabi, Muscat, Baku, Tbilisi"
        }
      },
      "Nationality": "BR",
      "Email": "jerueza.moura@example.com",
      "Birthday": "1948-08-29T06:01:44Z",
      "Registered": "2013-11-28T12:04:46Z",
      "TelephoneNumbers": [
        "+555667280423"
      ],
      "MobileNumbers": [
        "+551840117309"
      ],
      "Picture": {
        "Large": "https://randomuser.me/api/portraits/women/14.jpg",
        "Medium": "https://randomuser.me/api/portraits/med/women/14.jpg",
        "Thumbnail": "https://randomuser.me/api/portraits/thumb/women/14.jpg"
      }
    }
  ]
  ```
### Executar Testes Unitários
Os testes unitários estão localizados no projeto Auth.UnitTests. Para executar os testes unitários, use o comando:

  ```bash
dotnet test Auth.UnitTests
  ```

Executar Testes de Integração
Os testes de integração estão localizados no projeto Auth.IntegrationTests. Para executar os testes de integração, use o comando:

  ```bash
dotnet test Auth.IntegrationTests
  ```
