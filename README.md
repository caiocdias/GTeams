# GTeams

Sistema de Gestão de Equipes, Metas e Colaboradores

---

## Descrição

O GTeams é uma solução completa para gestão de equipes e colaboradores, envolvendo tanto backend quanto frontend. Desenvolvido em .NET 8 com WPF, o sistema possibilita o acompanhamento e gerenciamento de times, metas mensais, colaboradores e medições de desempenho de forma centralizada e eficiente.

A solução possui uma interface moderna para o usuário, além de uma forte estrutura backend que garante o controle seguro de dados, permissões, registro de atividades, e integrações para documentação e versionamento.

---

## Tecnologias Utilizadas

- .NET 8
- WPF (Frontend)
- Entity Framework Core 8
- C# 12.0
- Banco de Dados relacional
- LaTeX (Documentação formal)

---

## Principais Funcionalidades

- Cadastro e autenticação de colaboradores
- Gerenciamento completo de equipes, definição e controle de líderes e membros
- Definição e acompanhamento de metas mensais por equipe
- Registro de observações e informações relevantes dos colaboradores
- Controle de intervalos de medição, datas personalizadas e geração automática de calendários úteis
- Gestão de matrículas e e-mails dos colaboradores
- Segurança aprimorada: controle de permissões e senhas com hash robusto (PBKDF2 SHA-512)
- Relacionamentos e regras de negócios implementadas com Entity Framework Fluent API
- Interface gráfica para facilitar operações e visualização dos dados

---

## Estrutura do Projeto

O projeto é composto por:

- Backend: Serviços, modelos de dados, regras de negócio e persistência de informações
- Frontend: Interface WPF para interação do usuário
---

## Banco de Dados

O gerenciamento do banco ocorre por meio de Entity Framework Core e migrations para versionamento e atualização do schema.

---

## Como Executar
