# Eligibles Listing API

Esta é a Eligibles Listing API, uma API construída usando ASP.NET Core seguindo os princípios da Clean Architecture. Esta API permite a listagem e filtragem de clientes a partir de dados fornecidos em formatos CSV e JSON.

## Visão Geral

A API fornece endpoints para classificar e listar clientes elegíveis. Os dados são carregados de URLs configuráveis no `appsettings.json`.

## Estrutura do Projeto

A solução é organizada em quatro camadas principais seguindo a Clean Architecture:

- **Auth.Api:** Ponto de entrada da aplicação.
  - Endpoints
  - Middlewares
  - Configuração da API

- **Auth.Infrastructure:** Comunicação com recursos externos como banco de dados, cache, serviços web, etc.
  - Implementação de Repositórios
  - Proxies de Serviços Externos
  - Serviços Específicos de Infraestrutura

- **Auth.Core:** Lógica de negócios da aplicação.
  - Manipuladores de Requisição / Gerentes
  - Abstrações para lógica de negócios e infraestrutura

- **Auth.Domain:** Contém tudo o que deve ser compartilhado entre os projetos.
  - DTOs
  - Extensões Gerais

## Configuração

### Pré-requisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Configuração do `appsettings.json`

No arquivo `appsettings.json`, configure as URLs para os dados CSV e JSON:

```json
{
  "MySettings": {
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
