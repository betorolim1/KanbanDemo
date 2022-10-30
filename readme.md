# Desafio Técnico - Backend

Para rodar o backend basta selecionar a API (KanbanDemo.API) como projeto default e executá-lo.

No arquivo appsettings.json existem propriedades que devem ser preenchidas:
"Credentials" > "login" -> Com o login para geração do token. Ex: "letscode"
"Credentials" > "password" -> Com a senha para geração do token. Ex: "lets@123"

"Jwt" > "Key" -> Chave para geração do token.


# ENDPOINTS:

(POST)      http:/5000/login/
Retorna um token JWT que deve ser enviado no header de todas as outras requisições. HEADER: Authorization Bearer <TOKEN_JWT> 

(GET)       http:/5000/cards/
Retorna uma lista de cards com todos os dados.

(POST)      http:/5000/cards/
Insere um card e retorna a lista de cards com todos os dados. 

(PUT)       http:/5000/cards/{id}
Altera o card informado no id e retorna este card alterado.

(DELETE)    http:/5000/cards/{id}
Remove o card informado no id e retorna uma lista de cards com todos os dados.


# Obs:
O LogFilter sempre loga o card mesmo este tendo sido deletado anteriormente, foi uma forma que encontrei de deixar o recurdo de deleção logando o titulo.

Os métodos assíncronos do repositório poderiam retornar um IQuerable para mais filtros poderem ser incluidos futuramente, caso necessário. Mas por uma 
dificuldade em "Mockar" métodos como ToListAsync() e FirstOrDefaultAsync(), optei por deixá-los retornando uma lista.