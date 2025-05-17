Feature: EP05 Administrar las tareas asignadas a los empleados
  Como gerente
  Deseo gestionar las tareas asignadas a los empleados
  Para asegurarme de que todo esté en orden

  Scenario: US13 Crear una nueva tarea para asignar a un empleado
    Given el usuario se encuentra en la sección de tareas
    When presiona el botón para agregar una nueva tarea
    And define los detalles de la tarea (descripción, empleado asignado, fecha límite, etc.)
    Then la tarea se añade a la lista de tareas
    And se muestra en la sección correspondiente

  Scenario: US14 Eliminar una tarea no necesaria
    Given el usuario se encuentra en la sección de tareas
    When selecciona la tarea que desea eliminar
    And presiona el botón para eliminarla
    Then la tarea se elimina correctamente de la lista de tareas

  Scenario: US15 Asignar o editar tareas
    Given el usuario tiene acceso para asignar o editar tareas en la sección de tareas
    When selecciona una tarea existente o crea una nueva tarea
    And ingresa o modifica los detalles (empleado asignado, descripción, fecha límite, etc.)
    Then la tarea se actualiza o se asigna correctamente al empleado correspondiente

  Scenario: US16 Asignar una tarea a un empleado específico
    Given el usuario tiene acceso para asignar tareas
    When selecciona una tarea
    And especifica el empleado asignado en el diálogo correspondiente
    Then la tarea se asigna al empleado seleccionado
    And los detalles se actualizan correctamente