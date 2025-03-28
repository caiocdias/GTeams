# GTeams

Sistema de Gestão de Equipes, Metas e Colaboradores

---

## Descrição

O GTeams é uma aplicação backend desenvolvida em .NET 8, voltada para o gerenciamento de equipes, colaboradores, metas mensais e medições de performance. O sistema oferece controle completo sobre dados de colaboradores, vínculos com equipes, metas atribuídas, observações e gestão de intervalos de medição.

Além do backend, o projeto contém documentação formal em LaTeX, consolidando as decisões arquiteturais e detalhes do sistema.

---

## Tecnologias Utilizadas

- .NET 8
- Entity Framework Core 8
- C#
- LaTeX (Documentação)
- Banco de Dados relacional

---

## Funcionalidades Principais

- Cadastro e autenticação de colaboradores
- Gerenciamento de equipes e membros (incluindo líder de equipe)
- Registro e controle de observações sobre colaboradores
- Definição de metas mensais por equipe
- Controle de intervalos de medição e datas personalizadas
- Gestão de matrículas e e-mails dos colaboradores
- Controle de permissões e senhas com hash (PBKDF2 com SHA-512)
- Relacionamentos e restrições modelados via Fluent API do EF Core

---

## Estrutura de Dados

O sistema é composto por:

- Colaboradores: Registro completo dos colaboradores incluindo dados pessoais, senhas e observações.
- Equipes: Cadastro e gerenciamento de equipes, com definição de lideranças.
- Metas: Controle de metas mensais de cada equipe vinculadas aos intervalos de medição.
- Intervalos de Medição: Definição de períodos e geração automática de dias úteis e finais de semana.
- Observações: Registro de eventos ou apontamentos importantes associados a colaboradores.
- Documentação LaTeX: Contém a modelagem completa do banco de dados, casos de uso e fluxos da aplicação.

---

## Estrutura do Projeto

```
/Models
    - Colaborador.cs
    - Equipe.cs
    - IntervaloMedicao.cs
    - Observacao.cs
    - Matricula.cs
    - Email.cs
    - EquipeColaborador.cs
    - EquipeMetaMensal.cs
    - DataPersonalizadaColaborador.cs
    - DataPersonalizadaMedicao.cs

/Data
    - AppDbContext.cs

/Documentacao
    - RequisitosFuncionais.tex
```

---

## Banco de Dados

O banco de dados é gerenciado via Entity Framework Core, utilizando migrations para versionamento do schema.

---

## Como Executar

```bash
# Restore das dependências
dotnet restore

# Build do projeto
dotnet build

# Executar aplicação
dotnet run
```

---

## Documentação

O projeto conta com documentação em LaTeX, disponível no arquivo main.tex. Recomendado compilar com pdflatex ou outra ferramenta compatível.
