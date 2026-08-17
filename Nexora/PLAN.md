

# Nexora — PLAN.md

## 1. Objetivo do projeto

O Nexora é um MVP de SaaS para gestão operacional de pequenos estabelecimentos, inicialmente com foco em lanchonetes.

O projeto possui dois objetivos:

1. Construir uma solução funcional para as principais dores operacionais do estabelecimento.
2. Desenvolver conhecimentos de engenharia de software, especialmente modelagem de domínio, regras de negócio, arquitetura em camadas, persistência e construção de APIs REST.

A prioridade é manter o MVP simples, evitando complexidade que possa ser adiada sem prejudicar a evolução do sistema.

---

# 2. Problema central

O sistema deve reduzir problemas relacionados a:

- pouca rastreabilidade dos pedidos;
- dificuldade para localizar vendas antigas;
- fechamento de caixa manual;
- dificuldade de acompanhar pagamentos;
- ausência de informações operacionais;
- pouca informação sobre clientes.

O foco inicial é criar uma base de dados operacional confiável sobre produtos, clientes, usuários, pedidos e pagamentos.

---

# 3. Escopo do MVP

## Incluído no MVP

- Usuários do estabelecimento;
- Clientes;
- Produtos;
- Pedidos;
- Itens de pedido;
- Origem do pedido;
- Controle do ciclo de vida do pedido;
- Controle de autorização de operações administrativas;
- Registro de quem criou um pedido no balcão;
- Histórico dos dados relevantes do produto dentro do pedido;
- Pagamento associado ao pedido;
- Consulta de pedidos;
- Listagem de pedidos;
- Persistência em banco relacional;
- API REST organizada em camadas/pastas.

## Fora do MVP

Os seguintes assuntos foram deliberadamente adiados:

- IA;
- automações;
- integração com WhatsApp;
- campanhas de marketing;
- PIX e integrações de pagamento externas;
- cupons;
- microserviços;
- Kubernetes;
- alta escalabilidade;
- integrações com adquirentes;
- funcionalidades avançadas de BI/analytics;
- carrinho de compras.

Esses itens poderão ser analisados futuramente quando o núcleo operacional estiver consolidado.

---

# 4. Arquitetura

O projeto será mantido inicialmente em uma única Solution/Project, utilizando separação por pastas e responsabilidades.

Estrutura conceitual:

```text
Nexora
├── Controllers
├── Services
├── Interfaces
├── Repositories
├── Models / Entities
├── Dtos
├── Enums
├── Data
├── Mappings
└── ...
```

A arquitetura busca separar:

- Controller: HTTP e entrada/saída da API;
- Service: orquestração dos casos de uso e regras de aplicação;
- Domínio/Entidades: estado, comportamento e invariantes próprias das entidades;
- Repository: persistência e acesso ao banco;
- DTOs: contrato da API;
- DbContext: infraestrutura de persistência;
- AutoMapper: mapeamento entre modelos quando fizer sentido.

Não será utilizado um `BaseEntity`/GUID neste momento.

---

# 5. Entidades do MVP

## Usuario

Representa usuários que operam o sistema do estabelecimento.

Responsabilidades principais:

- representar identidade operacional;
- possuir perfil/permissão;
- permitir identificar quem executou operações administrativas e operacionais.

Perfis definidos no domínio:

- Administrador;
- Operador.

Regra importante:

> Somente usuários com perfil de Administrador podem executar operações administrativas definidas para o MVP, como cadastro, alteração e desativação de produtos.

O perfil Operador é utilizado para ações operacionais, como transições de estado do pedido, conforme as permissões estabelecidas.

---

## Cliente

Representa o consumidor do estabelecimento.

Responsabilidades principais:

- armazenar informações do cliente;
- permitir associação de pedidos ao cliente;
- preservar histórico de relacionamento com os pedidos.

No momento, `ClienteId` no Pedido permanece opcional.

Isso permite que o MVP suporte pedidos sem cliente identificado, caso o fluxo operacional do estabelecimento exija.

---

## Produto

Representa um produto comercializado pelo estabelecimento.

Atributos definidos:

- Id;
- Nome;
- Descricao;
- Preco;
- Disponivel;
- DataCriacao;
- DataAtualizacao;
- ItensPedido.

### Regras de negócio

1. O nome do produto é obrigatório.
2. O nome possui limite máximo de 150 caracteres.
3. A descrição possui limite máximo de 500 caracteres.
4. O preço deve ser maior que zero.
5. Um produto pode estar disponível ou indisponível.
6. Somente Administrador pode cadastrar produto.
7. Somente Administrador pode alterar produto.
8. Somente Administrador pode desativar produto.
9. Um produto já desativado não precisa ser desativado novamente.
10. A desativação não remove o produto do banco.
11. Ao desativar um produto, `Disponivel` passa a `false`.
12. A data de atualização deve ser atualizada quando houver alteração relevante.

### Estratégia de histórico

Produtos não devem ser fisicamente removidos apenas para impedir sua utilização futura.

A desativação preserva o registro e seu histórico.

---

## Pedido

Representa uma venda/pedido realizado pelo estabelecimento.

Atributos definidos:

- Id;
- Numero;
- Status;
- Origem;
- ClienteId;
- Cliente;
- CriadoPorUsuarioId;
- CriadoPorUsuario;
- DataCriacao;
- DataAtualizacao;
- Itens;
- Pagamento.

### Regras estruturais

1. Cada pedido possui um número operacional.
2. O número do pedido deve ser gerado automaticamente pelo sistema.
3. O número deve seguir uma sequência incremental.
4. A estratégia definitiva para geração segura do número será definida durante a implementação, considerando concorrência.
5. O status pertence ao próprio Pedido.
6. O cliente pode ser nulo neste momento.
7. `CriadoPorUsuarioId` é opcional.
8. Pedidos online podem não possuir `CriadoPorUsuarioId`.
9. Pedidos registrados no balcão podem possuir `CriadoPorUsuarioId`.
10. O Pedido deve manter seus próprios itens.
11. O Pedido deve controlar suas próprias transições de estado.
12. O pagamento será associado ao Pedido.

---

# 6. Origem do pedido

O Pedido possui uma origem:

- Online;
- Balcão.

### Online

Representa um pedido realizado diretamente pelo cliente.

Exemplo:

```text
Cliente
   ↓
Pedido
```

Nesse cenário, `CriadoPorUsuarioId` pode ser nulo.

### Balcão

Representa um pedido registrado por um usuário/operador.

Exemplo:

```text
Usuario/Operador
   ↓
Cliente
   ↓
Pedido
```

Nesse cenário, `CriadoPorUsuarioId` deve permitir rastrear o usuário responsável pela criação.

---

# 7. Status do Pedido

O fluxo estabelecido para o MVP é:

```text
Criado
   ↓
EmPreparacao
   ↓
Pronto
   ↓
Entregue
```

Também existe o fluxo:

```text
Criado
   ↓
Cancelado
```

## Regras de edição

Um pedido pode ser editado somente quando:

```text
Status == Criado
```

Depois que entrar em:

```text
EmPreparacao
```

não poderá mais ser editado.

## Regras de cancelamento

Um pedido pode ser cancelado somente quando:

```text
Status == Criado
```

Depois de entrar em `EmPreparacao`, não poderá ser cancelado pelo fluxo normal definido no MVP.

## Transições

As transições de estado são executadas por usuários definidos pelo estabelecimento como operadores.

O Service deve verificar autorização.

A entidade Pedido deve verificar se a transição de estado é válida.

Assim:

```text
Service
→ "Este usuário pode executar a operação?"

Pedido
→ "Esta transição é válida?"
```

---

# 8. ItemPedido

Representa um produto dentro de um pedido.

Estrutura definida:

- Id;
- PedidoId;
- Pedido;
- ProdutoId;
- Produto;
- NomeProduto;
- Quantidade;
- PrecoUnitario;
- Observacao.

## Regras

1. Um ItemPedido pertence a um Pedido.
2. Um ItemPedido referencia um Produto.
3. `Quantidade` deve ser maior que zero.
4. `PrecoUnitario` deve ser maior que zero.
5. `NomeProduto` deve ser preservado no momento da venda.
6. `PrecoUnitario` deve ser preservado no momento da venda.
7. O ItemPedido funciona como um snapshot dos dados relevantes do produto no momento da venda.
8. Alterações posteriores no Produto não devem modificar o histórico do ItemPedido.
9. `Subtotal` não será persistido inicialmente.
10. `Subtotal` será calculado como:

```text
Quantidade × PrecoUnitario
```

11. A entidade não deve acessar Repository ou DbContext diretamente.

## Exemplo

Se:

```text
Produto = X-Burger
Preço atual = R$ 25,00
Quantidade = 2
```

o ItemPedido registra:

```text
NomeProduto = X-Burger
PrecoUnitario = 25,00
Quantidade = 2
```

Mesmo que posteriormente o produto passe a custar R$ 30,00, o pedido continuará representando a venda original por R$ 25,00.

---

# 9. Total do Pedido

Inicialmente, o Total do Pedido será derivado dos itens.

```text
Total =
Σ (Quantidade × PrecoUnitario)
```

Não será persistido inicialmente.

Isso evita manter simultaneamente:

```text
Quantidade
PrecoUnitario
Subtotal
Total
```

quando alguns desses valores podem ser derivados.

A estratégia poderá ser revista futuramente caso requisitos de desempenho, auditoria ou relatórios justifiquem a persistência de valores derivados.

---

# 10. Pagamento

O Pedido possui uma relação opcional com Pagamento:

```text
Pedido 1 ─── 0..1 Pagamento
```

O pagamento será desenvolvido após o fluxo básico de Pedido.

## Regra já estabelecida

O pagamento é obrigatório antes da finalização do pedido.

A transição exata na qual o pagamento passa a ser obrigatório será definida durante a implementação do fluxo de pagamento.

No MVP não serão implementadas:

- integração PIX;
- adquirentes;
- gateways externos;
- conciliação automática.

---

# 11. Regras de criação do Pedido

Na criação, o consumidor da API deverá fornecer somente informações que realmente pertencem ao comando de criação.

Conceitualmente:

```text
PedidoCreate
├── ClienteId
├── Origem
└── Itens
      ├── ProdutoId
      ├── Quantidade
      └── Observacao
```

Não devem ser recebidos diretamente do cliente:

- Numero;
- Status;
- Total;
- Subtotal;
- PrecoUnitario;
- DataCriacao;
- DataAtualizacao;
- CriadoPorUsuarioId.

Esses valores são determinados pelo sistema/contexto da operação.

---

# 12. Responsabilidades por camada

## Controller

Responsável por:

- receber requisições HTTP;
- validar estrutura básica do request;
- chamar o Service;
- retornar códigos HTTP;
- retornar DTOs.

Não deve conter regras de negócio complexas.

---

## Service

Responsável por:

- orquestrar casos de uso;
- buscar entidades necessárias;
- validar regras de aplicação;
- verificar autorização;
- coordenar entidades;
- chamar Repository;
- realizar mapeamentos;
- decidir o fluxo da operação.

Exemplo:

```text
PedidoService
├── buscar Cliente
├── buscar Produtos
├── validar disponibilidade
├── criar Pedido
├── adicionar Itens
└── persistir
```

---

## Domínio/Entidades

Responsável por:

- manter estado válido;
- proteger invariantes;
- executar comportamentos próprios;
- controlar transições do Pedido;
- controlar seus próprios itens.

Exemplo:

```text
Pedido
├── AdicionarItem()
├── RemoverItem()
├── AlterarQuantidadeItem()
├── Cancelar()
├── IniciarPreparacao()
├── MarcarComoPronto()
└── MarcarComoEntregue()
```

---

## Repository

Responsável por:

- consultar banco;
- persistir entidades;
- executar operações de acesso a dados;
- trabalhar com DbContext.

Não deve decidir regras de negócio.

---

# 13. Estratégia de implementação

O desenvolvimento será incremental.

A regra do projeto é:

> Não implementar a camada seguinte antes de fechar conceitualmente a anterior.

Fluxo:

```text
Modelagem
   ↓
Domínio
   ↓
DTO
   ↓
Repository
   ↓
Service
   ↓
Controller
   ↓
Testes
```

---

# 14. Etapas concluídas

## Etapa 1 — Definição do problema e fluxo operacional

**Concluída.**

Foram definidos:

- problema central;
- usuário principal;
- fluxo operacional;
- necessidade de rastreabilidade;
- criação e acompanhamento de pedidos;
- regras iniciais de edição;
- regras de cancelamento;
- necessidade de operadores;
- origem online/balcão.

---

## Etapa 2 — Modelagem inicial do domínio

**Concluída.**

Foram definidas as entidades principais:

- Usuario;
- Cliente;
- Produto;
- Pedido;
- ItemPedido;
- Pagamento.

Também foram definidos os principais enums e relacionamentos.

---

## Etapa 3 — Produto

**Concluída.**

Fluxos implementados:

### 3.1 — Cadastro de Produto

Concluído.

Regra:

> Somente Administrador pode cadastrar Produto.

### 3.2 — Alteração de Produto

Concluído.

Regra:

> Somente Administrador pode alterar Produto.

### 3.3 — Desativação de Produto

Concluído.

Fluxo implementado com:

- validação do usuário;
- autorização de Administrador;
- busca do Produto;
- método de domínio `Desativar()`;
- persistência via Repository;
- retorno DTO.

### 3.4 — Fluxo adicional de Produto

Concluído conforme evolução definida no projeto.

---

## Persistência inicial

**Concluída.**

Foram realizados:

- configuração do `AppDbContext`;
- configuração dos relacionamentos;
- parametrização das entidades;
- criação das migrations;
- persistência inicial no banco.

---

# 15. Etapas restantes

# Etapa 4 — Pedido

## 4.1 — Finalizar modelagem do ItemPedido

- revisar atributos;
- revisar relacionamentos;
- definir construtor;
- definir invariantes;
- definir snapshot histórico;
- definir Subtotal calculado.

**Status: em andamento.**

## 4.2 — Métodos de domínio do ItemPedido

- criar ItemPedido;
- alterar quantidade;
- alterar observação;
- proteger invariantes.

**Status: pendente.**

## 4.3 — Métodos de domínio do Pedido

- criação;
- adicionar item;
- remover item;
- alterar quantidade;
- calcular total;
- cancelar;
- iniciar preparação;
- marcar pronto;
- marcar entregue.

**Status: pendente.**

## 4.4 — Revisar relacionamentos EF

Validar:

```text
Cliente 1:N Pedido
Usuario 1:N Pedido
Pedido 1:N ItemPedido
Produto 1:N ItemPedido
Pedido 1:0..1 Pagamento
```

**Status: pendente.**

## 4.5 — DTOs de Pedido

Criar:

- PedidoCreate;
- ItemPedidoCreate;
- PedidoResponse;
- ItemPedidoResponse.

**Status: pendente.**

## 4.6 — Repository

Implementar consultas e persistência necessárias.

**Status: pendente.**

## 4.7 — Service de criação

Implementar o caso de uso de criação de Pedido.

**Status: pendente.**

## 4.8 — Controller de criação

Criar endpoint de criação.

**Status: pendente.**

## 4.9 — Consulta de Pedido

Implementar:

```http
GET /pedido/{id}
```

**Status: pendente.**

## 4.10 — Alteração dos itens

Implementar:

- adicionar item;
- alterar quantidade;
- remover item.

Somente em `Criado`.

**Status: pendente.**

## 4.11 — Cancelamento

Implementar cancelamento somente enquanto o pedido estiver em `Criado`.

**Status: pendente.**

## 4.12 — Transições de estado

Implementar:

```text
Criado
→ EmPreparacao
→ Pronto
→ Entregue
```

e:

```text
Criado
→ Cancelado
```

Com validação de autorização e validade da transição.

**Status: pendente.**

## 4.13 — Pagamento

Implementar entidade e fluxo básico.

**Status: pendente.**

## 4.14 — Listagem de Pedidos

Implementar:

```http
GET /pedido
```

**Status: pendente.**

## 4.15 — Teste do fluxo completo

Validar cenários:

- criação;
- alteração;
- cancelamento;
- preparação;
- conclusão;
- entrega;
- pagamento;
- produto inexistente;
- produto indisponível;
- quantidade inválida;
- usuário sem permissão;
- transição inválida;
- pedido inexistente.

**Status: pendente.**

---

# 16. Fluxo final esperado do MVP

O fluxo operacional principal deverá ser:

```text
                 ┌──────────────┐
                 │    Cliente   │
                 └──────┬───────┘
                        │
                        ▼
                 Criar Pedido
                        │
                        ▼
                     Criado
                        │
             ┌──────────┴──────────┐
             │                     │
        Editar itens           Cancelar
             │                     │
             └──────────┬──────────┘
                        │
                        ▼
                   Pagamento
                        │
                        ▼
                 Em Preparacao
                        │
                        ▼
                     Pronto
                        │
                        ▼
                    Entregue
```

Para pedidos de balcão:

```text
Usuario/Operador
       │
       ▼
Registrar Pedido
       │
       ▼
     Pedido
       │
       └── CriadoPorUsuarioId
```

---

# 17. Princípios que devem ser mantidos

Durante o desenvolvimento, seguir estas regras:

1. Não criar abstrações sem necessidade.
2. Não antecipar funcionalidades futuras.
3. Não colocar regra de negócio no Controller.
4. Não colocar regra de negócio no Repository.
5. Não fazer entidades acessarem banco diretamente.
6. Preferir comportamento de domínio quando uma entidade possuir uma regra própria.
7. Manter DTOs separados das entidades.
8. Não confiar em valores calculados enviados pelo cliente.
9. Preservar dados históricos relevantes das vendas.
10. Preferir operações explícitas e compreensíveis.
11. Revisar modelagem antes de implementar código.
12. Questionar complexidade desnecessária.
13. Adicionar infraestrutura somente quando existir uma necessidade real.
14. Manter o MVP pequeno e evolutivo.

---

# 18. Próximo passo

O próximo passo oficial é:

**Etapa 4.1 — Finalizar a modelagem do `ItemPedido`.**

Antes de implementar Repository, Service ou Controller, devemos fechar:

```text
ItemPedido
├── atributos
├── relacionamentos
├── construtor
├── invariantes
├── métodos
└── propriedade Subtotal
```

Depois disso, avançaremos sequencialmente pelo restante da Etapa 4.
