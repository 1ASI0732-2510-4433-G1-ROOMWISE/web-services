Feature: EP01 Información del Producto

  Scenario: HU01 Información clara del producto
    Given el visitante se encuentra en la página de inicio
    When observa los detalles sobre el producto ofrecido
    Then obtiene una visión más clara de lo que la empresa tiene para ofrecer
      And decide registrarse o considerar la opción de registrarse como usuario

  Scenario: HU02 Conocer más acerca de la empresa
    Given el visitante navega en la sección Nosotros
    When revisa la información relevante sobre la compañía
    Then se genera un mayor interés por el producto
      And opta por registrarse como usuario

  Scenario: HU03 Conocer las ventajas del producto
    Given el visitante está en la sección de beneficios
    When revisa las ventajas asociadas al uso de la solución
    Then aumenta su interés por el producto
      And decide registrarse como usuario

  Scenario: HU04 Consultar opciones de precios
    Given el visitante se encuentra en la sección de planes de precios
    When examina las diferentes alternativas ofrecidas
    Then puede comprender mejor las opciones disponibles
      And tomar una decisión informada
      And optar por registrarse como usuario

  Scenario: HU05 Establecer contacto fácilmente
    Given el visitante se encuentra en la página principal
    When busca una opción de contacto accesible
    Then puede establecer comunicación de forma rápida y sencilla con la empresa