Feature: EP03 Gestión de habitaciones
  Como gerente o trabajador
  Deseo consultar y modificar el estado de las habitaciones del hotel
  Para mantener un control adecuado

  Scenario: US07 Modificar el estado de una habitación
    Given el usuario accede a la sección de Habitaciones
    When hace clic en el botón de edición
    And selecciona un nuevo estado desde la ventana emergente (popup)
    Then el estado de la habitación se actualiza correctamente

  Scenario: US08 Actualizar el estado de la habitación (Empleado)
    Given el usuario se encuentra en la sección de Habitaciones
    And el usuario finaliza su tarea de limpieza o mantenimiento
    When hace clic en el botón para actualizar el estado de la habitación
    Then el estado de la habitación se actualiza correctamente

  Scenario: US09 Agregar nuevas habitaciones al sistema
    Given el usuario tiene permisos para agregar habitaciones en la sección de Habitaciones
    When ingresa los detalles de la habitación (tipo, número, etc.)
    Then la nueva habitación se registra en el sistema
    And aparece en la lista de habitaciones