Feature: EP04 Administrar el inventario del hotel
  Como gerente
  Deseo gestionar el inventario del hotel
  Para asegurarme de que los suministros estén siempre disponibles

  Scenario: US10 Agregar un nuevo ítem al inventario
    Given el usuario tiene acceso para añadir ítems en la sección de Inventario
    When ingresa los detalles del nuevo ítem (nombre, cantidad, etc.)
    Then el ítem se añade al inventario correctamente
    And se muestra en la sección de Inventario

  Scenario: US12 Actualizar la información de un ítem en el inventario
    Given el usuario tiene acceso para modificar los detalles de un ítem
    When selecciona el ítem que desea actualizar
    And modifica sus detalles (cantidad, descripción, etc.)
    Then los cambios se reflejan correctamente en el inventario