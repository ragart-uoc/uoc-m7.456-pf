# Logos

![Captura de pantalla de la página de título de Logos](https://folio-uploads-pro.s3.eu-west-1.amazonaws.com/wp-content/uploads/sites/8519/2023/01/29202129/pf-screenshot-1024x575.jpg)

"Logos" es el nombre de mi prototipo para la Práctica Final de la asignatura Programación de Videojuegos 2D del Máster Universitario en Diseño y Programación de Videojuegos de la UOC.

El objetivo de la práctica era desarrollar una experiencia completa en 2D de temática libre, utilizando los conocimientos adquiridos durante toda asignatura y realizando investigación por cuenta propia.

"Logos" sirve como demostración conceptual de "Idios", una propuesta de diseño que se llevó a cabo para la asignatura "Game Design" del Máster Universitario en Diseño y Programación de Videojuegos de la UOC y cuyo GDD puede encontrarse [aquí](https://gitlab.com/ragart-uoc/b2.499/uoc-b2.499-pec3/-/raw/master/sbanderas_idios_gdd.pdf?inline=false). No obstante, "Logos" es un juego autoconclusivo que permite disfrutar de una experiencia completa de principio a fin y que puede considerarse como una obra enmarcada dentro del mismo universo ludoficcional que "Idios". Dentro de ese microcosmos narrativo, el concepto de "Logos" hace referencia al término del griego antiguo λóγος, que significa “pensamiento”, y que aquí se utiliza como representación de la mente humana y su capacidad para, ante todo, proteger al organismo, elevándolo en cierta manera hasta el estatus de deidad, aunque ajena al concepto tradicional de dios.

## Vídeo explicativo

<span style="color: red;">[ EN PREPARACIÓN ]</span>

## Versión jugable

[Logos by Ragart on itch.io](https://ragart.itch.io/logos) <span style="color: red;">[ EN PREPARACIÓN ]</span>

## Repositorio en GitLab

[UOC - M7.456 - PEC Final en GitLab](https://gitlab.com/ragart-uoc/m7.456/uoc-m7.456-pec-final)

## Sinopsis

La muerte es inevitable. O quizás no.

Después de enterarse del suicidio de su mejor amiga, el Personaje, atormentado por no haber hecho nada para evitarlo, despierta en un mundo extraño en el que puede evocar situaciones vividas entre los dos y alterarlas para intentar cambiar los acontecimientos.

Sumergirse en los recuerdos le hará darse cuenta de que la realidad es más compleja de lo que aparenta. ¿Podrá anteponerse al Destino y evitar el funesto desenlace?

## Resumen argumental

*[Contiene detalles importantes de la trama del juego]*

Después de recibir la noticia del suicidio de su mejor amiga, el Personaje se pregunta si podría haber hecho algo para evitarlo e intenta recordar si hubo algún momento en el que ella hubiera dado señales de posibles problemas emocionales que la empujaran a quitarse la vida.

Al hacerlo, se ve transportado a un extraño mundo donde es capaz de revivir algunas situaciones clave vividas entre ambos y sobre las que puede influir para intentar cambiarlas, dando pie a otros recuerdos e incluso a nuevos desenlaces.

No obstante, todas las alternativas que el Personaje desbloquea acaban siempre en la muerte de su amiga, aunque por diferentes motivos.

Después de ver todas las alternativas posibles, un nuevo camino se abre y el Personaje encuentra con una representación más vívida de su amiga. Al interactuar con ella, la figura toma la forma del Personaje y le indica que su amiga no existe y que quien realmente está muriendo es él. La figura se identifica como Logos, la parte de su cerebro que intenta protegerlo de la realidad.

Cuando el Personaje le pregunta cuál es la causa real de su muerte, Logos le indica que eso ya no importa y que la única verdad de la que debe ser consciente es la de la inevitabilidad de su destino.

## Cómo jugar

El objetivo del juego es encontrar todas las alternativas argumentales al desenlace por defecto mediante la manipulación de los recuerdos. Para ello, el jugador puede aprender palabras de los diálogos y usarlas en otros para intentar provocar un cambio que evite el desenlace inicial o que permita acceder a otros recuerdos.

El juego se desarrolla en un bucle. Una vez visualizado uno de los finales, el jugador vuelve a la escena inicial y puede probar diferentes combinaciones.

El jugador puede llevar un máximo de cuatro palabras a la vez y, si durante una iteración del bucle quiere aprender alguna palabra adicional, debe descartar una de las que lleve en ese momento. No obstante, las palabras que se conserven al final de la iteración se aprenderán de manera permanente y podrán ser escogidas para equiparse desde el principio de la iteración siguiente.

El control se lleva a cabo mediante teclado y ratón:

- Las teclas WASD y las fechas direccionales mueven al personaje por el escenario.
- El ratón permite aprender y usar las palabras dentro del diálogo.

## Desarrollo

A efectos de cumplir lo solicitado en las instrucciones, el prototipo incluye las siguientes escenas:

- Créditos iniciales con la autoría del prototipo
- Introducción argumental que pone en contexto el motivo del duelo.
- Pantalla de título y menú principal con opciones para continuar la partida, iniciar una nueva y salir del juego, así como con los créditos. También incluye opciones de configuración:
  - Subir/bajar el volumen maestro
  - Cambiar la velocidad del texto en pantalla
  - Cambiar la velocidad del movimiento del personaje
- Escena de selección de palabras
- Escena de juego
- Escena final que se reproduce después de cada iteración del bucle y que varía en función del final obtenido

Además, para poder llevar a cabo la propuesta, se han utilizado varios de los componentes y técnicas utilizados en prototipos anteriores, pero otros se utilizan por primera vez. A continuación se destacan los más relevantes:

- Se han utilizado `tilemaps` isométricos para desplegar la escena de juego. A efectos de simular la superposición entre elementos, se han usado varios `tilemaps Z as Y` en diferentes `sorting layers` y con niveles diferentes para el valor Z. Además, debido a la complejidad de la escena de juego, se han elaborado `tiles` con formas similares a los bordes de los `sprites` utilizados para definir las colisiones. Tal y como puede observarse en la imagen siguiente, se han añadido varios tipos de `tiles` para colisiones:
  - Los `tiles` rosas son de carácter estáticos y se usan para definir los bordes exteriores de la escena.
  - Los `tiles` azules son de carácter temporal y se usan para definir las barreras entre las escenas, que desaparecer cuando se cumplen ciertas condiciones.
  - Los `tiles` verdes no son realmente `tiles`, sino objetos que disparan uno o varios eventos cuando el jugador entra en contacto con ellos.

![Captura de pantalla de la vista de Grid de Unity durante el desarrollo de Logos](https://folio-uploads-pro.s3.eu-west-1.amazonaws.com/wp-content/uploads/sites/8519/2023/01/29203515/imagen_2023-01-29_213514095.png)

- Los `tiles` de las diferentes capas se han añadido mediante el uso de `palettes`.

![Captura de pantalla del componente tilePalette de Unity](https://folio-uploads-pro.s3.eu-west-1.amazonaws.com/wp-content/uploads/sites/8519/2023/01/29210744/pf-screenshot-palettes.jpg)

- Para optimizar la creación de las animaciones de los personajes, se han utilizado `sprite libraries` y `blend trees`.

![Captura de pantalla de los elementos blendTree y spriteLibrary de Unity](https://folio-uploads-pro.s3.eu-west-1.amazonaws.com/wp-content/uploads/sites/8519/2023/01/29210624/pf-screenshot-spritelibraries-1024x575.png)

- Para poder proporcionar una experiencia completa con el teclado, se ha utilizado el componente Navigation y se han configurado de manera programática las diferentes lógicas que rigen el flujo de desplazamiento del usuario por los menús.

![Captura de pantalla del componente navigation de Unity](https://folio-uploads-pro.s3.eu-west-1.amazonaws.com/wp-content/uploads/sites/8519/2023/01/29211231/imagen_2023-01-29_221230497.png)

- Para poder trabajar con palabras específicas dentro de los textos del juego, se han utilizado las etiquetas de texto enriquecido del componente TextMeshPro. En particular, la etiqueta `<link>` permite tanto la caracterización de palabras dentro del texto como la interactividad por parte del jugador.

![Captura de pantalla de una versión preliminar de Logos en la que se muestra el uso del componente TextMeshPro](https://folio-uploads-pro.s3.eu-west-1.amazonaws.com/wp-content/uploads/sites/8519/2023/01/29211844/pf-screenshot-tmpro.jpg)

Además, para poder desplegar el complejo árbol de conversaciones que subyace a esta propuesta, se ha optado por crear varias entidades a nivel de código que reflejan las diferentes unidades que pueden extraerse del texto (segmentos, palabras, etc.) y que facilitan tanto la importación de texto desde un formato de intercambio como el desarrollo del flujo conversacional.

![Captura de pantalla del fragmento de código de Logos que define un segmento textual](https://folio-uploads-pro.s3.eu-west-1.amazonaws.com/wp-content/uploads/sites/8519/2023/01/29213359/pf-screenshot-entites_code.png)

Finalmente, también se ha incorporado un sistema de guardado y cargado que permite retomar el juego por donde se dejó. Para ello, se ha utilizado el almacén de `PlayerPrefs`, ya que el uso de almacenes de datos basados en ficheros dificulta la publicación en WebGL y se ha preferido ir por lo seguro en base al tiempo disponible.

![Captura de pantalla del fragmento de código de Logos que permite cargar una partida](https://folio-uploads-pro.s3.eu-west-1.amazonaws.com/wp-content/uploads/sites/8519/2023/01/29212830/imagen_2023-01-29_222829010.png)

## Problemas conocidos

En el momento de la publicación de la propuesta, se tiene conocimiento de los problemas siguientes:

- Los `tiles` de los puentes y las escenas se superponen en algunas ocasiones, haciendo que el personaje "salte" cuando pasa entre ellos.

## Por hacer

En el momento de la publicación de la propuesta, no fue posible llevar a cabo los siguientes conceptos, incluidos dentro del diseño original:

- Permitir que la navegación en los diálogos y la selección/uso de palabras se haga mediante el teclado para prescindir del uso del ratón.
- Extraer todas las cadenas de texto a un fichero de localización. Actualmente, las cadenas relativas a la UI están forzadas en el código y las relativas al contenido argumental están en un JSON con información adicional.
- Añadir unas sombras NPC que persigan al personaje por los puentes entre las diferentes escenas. Si el personaje es atrapado por ellas, aparece el mensaje "Memento Mori" y debe reiniciar el bucle desde la escena inicial.
- Hacer que el texto se escriba progresivamente en los cuadros de diálogo en vez de todo a la vez, implementando la configuración relativa a la velocidad de los textos en pantalla.

## Créditos

### Fuentes

- "Christopher Hand" - El Stinger - https://www.dafont.com/christopherhand.font
- "Broken 15" - Misprinted Type - https://www.dafont.com/broken15.font
- "Doctor Glitch" - Woodcutter - https://www.dafont.com/doctor-glitch.font

### Imágenes y animaciones

- "city game *tiles*et" - Withering Systems - https://withering-systems.itch.io/city-game-*tiles*et

### Sonidos

### Música

- "Dark Fallout" - remaxim - https://opengameart.org/content/dark-fallout
- "Dark Blue" - OveMelaa - https://opengameart.org/content/ove-melaa-dark-blue-orchestral-tune
- "Mist Forest" - Janne Hanhisuanto [Radakan] - https://opengameart.org/content/radakan-mist-forest

## Referencias

### Animaciones

- "Using Unity's New 2D Animation Package for Traditional Sprite Animation" - Kyle Kukshtel [GameDeveloper] - https://www.gamedeveloper.com/programming/using-unity-s-new-2d-animation-package-for-traditional-sprite-animation
- "How To *Actually* Use a Unity Sprite Resolver" - Harvtronix - https://harvtronix.com/blog/unity-how-to-use-a-sprite-resolver
- "2D Animation Swapping Sprites in Unity" - Binaya Poudel [Yarsa Labs] - https://blog.yarsalabs.com/2d-animation-swapping-sprites-in-unity/
- "How to Setup 2D Character Animations with Blend Trees in Unity" - Chris' Tutorials [YouTube] - https://www.youtube.com/watch?v=d_gSegD2FXo
- "How to Transition to Walk Animation for 2D Unity Characters (Part 2)" - Chris' Tutorials [YouTube] - https://www.youtube.com/watch?v=NgTf4av7vmE

### Tilemaps isométricos

- "Isometric Game: 3 Ways to Do It - 2D, 3D | Unity Tutorial" - Tamara Makes Games [YouTube] - https://www.youtube.com/watch?v=XeqKQBIa43g
- "Making an Isometric Tilemap in 2022" - Lawless Games [YouTube] - https://www.youtube.com/watch?v=ci1ba7jVLFw
- "Creating a Tile Palette for an Isometric Tilemap" - Unity Documentation - https://docs.unity3d.com/Manual/Tilemap-Isometric-Palette.html
- "Making an Isometric Tilemap with Elevations and Colliders in UNITY" - samyam [YouTube] - https://www.youtube.com/watch?v=_TY0F7Zm6Lc

### TextMeshPro

- "Rich Text" - Unity Manual - https://docs.unity3d.com/Packages/com.unity.textmeshpro@4.0/manual/RichText.html

### Guardado, cargado, PlayerPrefs y serialización

- "How to make a Save & Load System in Unity | 2022" - Trever Mock [YouTube] - https://www.youtube.com/watch?v=aUi9aijvpgs
- "How to SAVE/LOAD Your Game in Unity with PlayerPrefs" - Muddy Wolf [YouTube] - https://www.youtube.com/watch?v=xqZ0I2jtpZo
- "PlayerPrefs not working on WebGL build?" - CrazYunderscore [Unity Forums] - https://forum.unity.com/threads/playerprefs-not-working-on-webgl-build.666022/
- "Using JsonUtility in Unity to Save and Load Game Data" - Dan Cox [Digital Ephemera] - https://videlais.com/2021/02/25/using-jsonutility-in-unity-to-save-and-load-game-data/
- "JsonUtility.FromJson" - Unity Documentation - https://docs.unity3d.com/ScriptReference/JsonUtility.FromJson.html
- "Serialize and Deserialize Json and Json Array in Unity" - Snak [StackOverflow] - https://stackoverflow.com/questions/36239705/serialize-and-deserialize-json-and-json-array-in-unity

### Volumen y sliders

- "Audio/Volume Slider in Unity Done RIGHT | Unity Tutorial" - LlamAcademy [YouTube] - https://www.youtube.com/watch?v=V_Bf__ynKLE
- "How To Make A Volume Slider In 4 Minutes - Easy Unity Tutorial" - Hooson [YouTube] - https://www.youtube.com/watch?v=yWCHaTwVblk

### Localización (l10n) / internacionalización (i18n)

- "Localizing Unity Games Step by Step" - Mohammad Ashour [Phrase] - https://phrase.com/blog/posts/localizing-unity-games-official-localization-package/