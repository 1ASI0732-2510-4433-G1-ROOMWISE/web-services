Feature: EP02 Crear cuenta
  Como gerente de un hotel o hostal
  Deseo crear una cuenta
  Para comenzar a utilizar la aplicación Sweet Manager

  Scenario: US05 Activación de cuenta de empleado
    Given el administrador ha registrado al trabajador en la plataforma
    When el empleado realiza la verificación de su cuenta
    Then su cuenta se activa correctamente
    And obtiene acceso al sistema

  Scenario: US06 Registro de cuenta para gerente
    Given ya se ha realizado el pago del plan
    When el usuario completa el formulario con los datos del hotel y su información como gerente
    Then se muestra un mensaje de bienvenida
    And se accede al panel principal del gerente

  Scenario: US11 Registro de cuenta para administrador
    Given el administrador ha registrado al trabajador en la plataforma
    When el empleado realiza la verificación de su cuenta
    Then su cuenta se activa correctamente
    And obtiene acceso al sistema