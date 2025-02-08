Feature: Buscar Cliente por CPF

  Para verificar a funcionalidade de busca de clientes
  Como um usuário
  Eu quero buscar um cliente pelo seu CPF
  Para garantir que o sistema retorne as informações corretas ou um status apropriado

  @tag1
  Scenario: Buscar cliente existente pelo CPF
    Given que existe um cliente com CPF 41512369020
    When eu faço uma busca pelo CPF 41512369020
  
  