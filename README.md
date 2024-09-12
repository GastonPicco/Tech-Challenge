# Tech-Challenge
Tech Challenge JamcCity
Video sobre el comportamiento de la Aplicacion https://www.youtube.com/watch?v=VWsngwI82tM&ab_channel=SebastianPiccoDev

- Optimizacion.
1) Separación de responsabilidades: Implementé una arquitectura sencilla pero funcional tratando de respetar el principio de responsabilidad única en lo posible. Cada clase tiene un rol específico en el sistema, lo que mejora la mantenibilidad y la capacidad de prueba del código.

2) Uso de interfaces: Para evitar duplicación de código y facilitar la reutilización, implementé interfaces. Esto permitió estandarizar funciones, como la carga y el guardado de datos en archivos XML, haciendo que el código sea más flexible y adaptable a futuros cambios o expansiones.

3) Escalabilidad sin tocar código: Mi enfoque se centró en lograr una aplicación escalable sin la necesidad de modificar el código base. Aunque no seguí un patrón de diseño específico, creé una logica propia que me permitió agregar funcionalidades y elementos sin intervención directa en el código fuente.

4) Evitar singletones: Trate de evitar a toda costa el uso de singletones, y busque otros caminos para resolver los problemas, como asignar valores en prefabs. En 2 casos tuve que hacer singletone por errores que no pude solucionar

5) Actualización de la UI sin el uso de Update: Toda la lógica relacionada con la interfaz de usuario está libre de los métodos Update de Unity. En lugar de eso, la UI responde a eventos y botones, mejorando la eficiencia y evitando actualizaciones innecesarias en cada frame.

6) Uso eficiente de bucles for y foreach: Seleccioné for o foreach según la necesidad y contexto del código.

7) Optimización de Prefabs: Los prefabs se utilizaron de manera eficiente para estandarizar comportamientos y datos.