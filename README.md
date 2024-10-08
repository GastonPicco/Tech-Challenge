# Tech-Challenge
Tech Challenge JamcCity
Video sobre el comportamiento de la Aplicacion https://www.youtube.com/watch?v=VWsngwI82tM&ab_channel=SebastianPiccoDev
La versión mini, por más de que sea una versión programada de 0, debería tener un comportamiento similar a la versión publicada en YouTube. Si no me equiboco, la diferencia principal es que al editar un empleado, no permite posiciones o senioridades que no existan. Hay que generarlas o crearlas antes de poder agregar empleados.

Es posible que el lunes 16 haga un push con un proyecto alternativo, ya que no quedé satisfecho con la estructura de mis datos. Aunque funciona bien, me di cuenta casi al final del Challenge de que no es lo suficientemente óptima. Me hubiera gustado implementar objetos más eficientes, anidando correctamente Positions > Seniorities > Employees. Una estructura como esta habría facilitado mucho el manejo de los datos de una forma más sencilla y efectiva. (CUMPLIDO) El archivo |mini Tech Challenge| es una solución alternativa desde 0 que me satisface más en estructura, este proyecto es mucho más fácil de navegar y entender, se reduce el número de scripts y su complejidad, no utiliza singletone y reutiliza lógicas de forma más eficiente, me hubiera gustado tener mas tiempo para profundizar en mas tests. A lo mejor hay algun commit mas.

En caso de no haber podido terminarlo a tiempo, el push se hará igualmente pero fuera de fecha.

- Optimizacion.
1) Separación de responsabilidades: Implementé una arquitectura sencilla pero funcional tratando de respetar el principio de responsabilidad única en lo posible. Cada clase tiene un rol específico en el sistema, lo que mejora la mantenibilidad y la capacidad de prueba del código.

2) Uso de interfaces: Para evitar duplicación de código y facilitar la reutilización, implementé interfaces. Esto permitió estandarizar funciones, como la carga y el guardado de datos en archivos XML, haciendo que el código sea más flexible y adaptable a futuros cambios o expansiones.

3) Escalabilidad sin tocar código: Mi enfoque se centró en lograr una aplicación escalable sin la necesidad de modificar el código base. Aunque no seguí un patrón de diseño específico, creé una logica propia que me permitió agregar funcionalidades y elementos sin intervención directa en el código fuente.

4) Evitar singletones: Trate de evitar a toda costa el uso de singletones, y busque otros caminos para resolver los problemas, como asignar valores en prefabs. En 2 casos tuve que hacer singletone por errores que no pude solucionar

5) Actualización de la UI sin el uso de Update: Toda la lógica relacionada con la interfaz de usuario está libre de los métodos Update de Unity. En lugar de eso, la UI responde a eventos y botones, mejorando la eficiencia y evitando actualizaciones innecesarias en cada frame.

6) Uso eficiente de bucles for y foreach: Seleccioné for o foreach según la necesidad y contexto del código.

7) Optimización de Prefabs: Los prefabs se utilizaron de manera eficiente para estandarizar comportamientos y datos.
