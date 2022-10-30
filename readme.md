# Desafio Técnico - Backend

Para rodar o backend basta selecionar a API (KanbanDemo.API) como projeto default e executá-lo.

No arquivo appsettings.json existem propriedades que devem ser preenchidas:
"Credentials" > "login" -> Com o login para geração do token. Ex: "letscode"
"Credentials" > "password" -> Com a senha para geração do token. Ex: "lets@123"

"Jwt" > "Key" -> Chave para geração do token.


# ENDPOINTS:

(POST)      http://0.0.0.0:5000/login/
Retorna um token JWT que deve ser enviado no header de todas as outras requisições. HEADER: Authorization Bearer <TOKEN_JWT> 

(GET)       http://0.0.0.0:5000/cards/
Retorna uma lista de cards com todos os dados.

(POST)      http://0.0.0.0:5000/cards/
Insere um card e retorna a lista de cards com todos os dados. 

(PUT)       http://0.0.0.0:5000/cards/{id}
Altera o card informado no id e retorna este card alterado.

(DELETE)    http://0.0.0.0:5000/cards/{id}
Remove o card informado no id e retorna uma lista de cards com todos os dados.
