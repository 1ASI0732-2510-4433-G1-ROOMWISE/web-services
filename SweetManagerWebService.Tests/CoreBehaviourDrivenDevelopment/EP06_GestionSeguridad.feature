Feature: EP06 Gestión de Seguridad de Usuarios
  Como administrador o desarrollador de Sweet Manager
  Deseo garantizar que los usuarios puedan acceder al sistema de manera segura
  Para proteger la información del hotel

  Scenario: US17 Seguridad de Usuario
    Given el usuario se encuentra en la pantalla de inicio de sesión (Sign In)
    When introduce sus credenciales correctamente
    And se valida su token JWT
    Then el usuario puede iniciar sesión de manera exitosa
    And será dirigido a su dashboard