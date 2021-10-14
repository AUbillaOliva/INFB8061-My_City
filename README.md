# INFB8060-My_City

![Imagen del actual desarrollo](https://i.imgur.com/EhlACtC.jpg)
_Imagen del actual progreso de desarrollo de la aplicación_

Este proyecto consiste en una actividad para la asignatura de Grafos y lenguajes formales, diseñado por un grupo de estudiantes de la carrera de Ingeniería civil en computación de la Universidad Tecnológica Metropolitana de Santiago de Chile (UTEM).

El propopsito de este proyecto consiste en la creacion de una ciudad generada aleatoriamente, con el proposito de cumplir con los objetivos propuestos.

---

## Objetivos

Se require una aplicación que permita simular el funcionamiento de una ciudad. Para ello se solicita que el programa haga lo siguiente:

- Debe aleatoriamente escoger el número de familias que viven en la ciudad. Mínimo 5 familias.
- Debe aleatoriamente escoger los integrantes de cada familia del punto anterior. Existirán familias con:
  - 1 Persona.
  - 2 Personas.
  - 2 Personas y 1 Niño.
  - 2 Personas y 2 Niños.
  - 2 Personas y 3 Niños.
- Cada familia tendrá una casa colocada aleatoriamente en la ciudad.
- La ciudad estará compuesta por:
  - Las casas de las familias.
  - 1 Colegio.
  - 2 Compañias: 1 de luz y 1 de agua. Cada miembro adulto de la familia trabajará en una compañía diferente.
  - Servicios de emergencia: Bomberos, Policía, Hospital.
  - 1 Supermercado.
- Optativamente, podrán agregar otros tipos de servicios como cines, iglesias, estadios, plazas, aeropuertos, etc.
- Las calles que unen la ciudad también deberán ser generadas automáticamente por la aplicación.

Una vez que se inicia la aplicación y se genera el mapa de la ciudad, donde ya están establecidos los lugares de cada uno de los ítems mencionados anteriormente, el usuario será capaz de:

- Ver el mapa de la ciudad y mostrar los ítems anteriores.
- Elegir una familia.
  - Que recorra automáticamente la ruta diaria de las mañanas, es decir: salir de casa, dejar los niños en el colegio, dejar a uno de los padres en una de las compañias y luego la segunda compañia donde trabaja el otro padre/madre.
  - Que recorra automáticamente la ruta diaria de las tardes, similar a la anterior, pero pasando por un supermercado antes de llegar a casa y posterior de pasar por el colegio.
  - Mover hacia el lugar que se desee.
- Siempre los movimientos deben ser óptimos. Para ello se deberá aleatoriamente ponderar la ciudad por cantidad de kms y por tiempo recorrido.
- Al escoger mover una familia a algún lugar, se podrá optar por hacer un camino óptimo por kms o por tiempo de recorrido.
- Se podrá escoger un incendio, robo o emergencia mpedica de una de las casas. En este caso, se deberán movilizar los respectivos servicios en el menor tiempo posible.
- La aplicación deberá alimentar de luz y agua desde las compañias, a cada una de las casas y servicios de emergencia de forma óptima.
- Al elegir la opción supermercado, la aplicación será capaz de entregar la ruta que seguirá un camión de comida y que repartirá a todas las casas, pasando tan solo una vez por cada una de ellas y luego volviendo al inicio.

- Se debe entregar un informe según lo estipulado en archivo de presentación del curso al iniciar la asignatura.
- El documento será entregado en formato PDF y debe ser dejado en carpeta que el profesor dispondrpa en SharePoint para cada grupo, previo a la fecha establecida, junto a un paquete comprimido con todos los códigos fuentes necesarios para levantar el aplicativo en cualquier computador.
- Si es una aplicación web, además, en el documento se debe especificar (manual de uso) la dirección web para ingresar.
- Si el aplicativo requiere plataformas o aplicaciónes extras, debe venir incluido en el manual (descarga e instalación). Por ejemplo. si el código es R, que requiero para correr ese código fuente.

Para evaluar los trabajos, habrá que poner atención a los siguientes puntos:

- Interfaz. Facilidad de uso, claridad, intuición, creatividad.
- Escalabilida, performance, rapidez.
- Entrega de información al usuario del tipo sistémico, no relacionado a grafos. (Log de ejecución).
- Control de flujo, caídas, errores.

**Fecha de entrega: 26/11/2021 9:00 hrs.**

## Desarrollo

La aplicación será construida en base al motor de juego multiplataforma [Unity](https://unity.com/), escrito con multiples lenguajes, entre los cuales destaca el lenguaje [C#](<https://en.wikipedia.org/wiki/C_Sharp_(programming_language)>)

El actual proyecto se está desarrollando bajo la metodología Kanban, puede usted ver el tablero en [Trello](https://trello.com/b/x8xMowpX/my-city)

## Integrantes

- Maximiliano Olave
- Fabian Pozzo
- Cristóbal Ramírez
- Álvaro Ubilla

## Licencia

Este proyecto esta licenciado bajo la licencia [GPL-3.0](https://github.com/AUbillaOliva/INFB8061-My_City/blob/master/LICENSE)
